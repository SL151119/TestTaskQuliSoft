using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class WorldGenerator : MonoBehaviour
{
    [Header("Sprite Shape Controller")]
    [SerializeField] private SpriteShapeController spriteShapeController;

    [Header("World Settings")]
    [SerializeField, Range(3, 1000)] private int groundLength = 100;
    [SerializeField, Range(1f, 50f)] private float xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float curveSmoothness = 2f;
    [SerializeField] private float noiseStep = 0.5f;
    [SerializeField] private float bottom = 10f;

    private Vector3 _lastPos;

    private void OnValidate()
    {
        spriteShapeController.spline.Clear();

        for (int i = 0; i < groundLength; i++)
        {
            _lastPos = transform.position + 
                new Vector3(i * xMultiplier, 
                Mathf.PerlinNoise(0, i * noiseStep) * yMultiplier);

            spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i is not 0 && i != groundLength - 1)
            {
                spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteShapeController.spline.SetLeftTangent(i, Vector3.left * xMultiplier * curveSmoothness);
                spriteShapeController.spline.SetRightTangent(i, Vector3.right * xMultiplier * curveSmoothness);
            }
        }

        spriteShapeController.spline.InsertPointAt(groundLength, new Vector3(_lastPos.x, transform.position.y - bottom));
        spriteShapeController.spline.InsertPointAt(groundLength + 1, new Vector3(transform.position.x, transform.position.y - bottom));
    }
}
