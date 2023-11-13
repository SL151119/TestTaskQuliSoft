using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Vehicle")]
    [SerializeField] private Transform vehicle;

    [Header("Canvas")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Buttons")]
    [SerializeField] private Button flipVehicleButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private bool _isGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        Time.timeScale = 1f;

        _isGameOver = false;

    }

    private void Start()
    {
        flipVehicleButton.onClick.AddListener(FlipVehicle);
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);

        _isGameOver = true;

        Time.timeScale = 0f;
    }

    private void FlipVehicle()
    {
        if (_isGameOver is false)
        {
            vehicle.transform.position += Vector3.up * 3f;
            vehicle.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    private void OnDestroy()
    {
        flipVehicleButton.onClick.RemoveListener(FlipVehicle);
        restartButton.onClick.RemoveListener(RestartGame);
        exitButton.onClick.RemoveListener(ExitGame);
    }
}
