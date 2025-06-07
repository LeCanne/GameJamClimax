using UnityEngine;

// Credits to @kurtdekker
[ExecuteInEditMode]
[RequireComponent (typeof(LineRenderer))]
public class ScalableCircle : MonoBehaviour
{
    [field : Space]

    [field : SerializeField] public float Radius { get; set; } = 1f;

    [field : SerializeField] public float Width { get; set; } = 0.25f;

    [field : SerializeField] public int Segments { get; set; } = 8;

    [Space]

    [SerializeField] private bool _alwaysUpdate = false;

    public LineRenderer _lineRenderer { get; private set; }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        UpdateRing();
    }

    private void Update()
    {
        if (_alwaysUpdate) UpdateRing();
    }

    [ContextMenu("Set Color Test")]
    public void SetColorTest()
    {
        SetColor(Color.red);
    }

    public void SetColor(Color color)
    {
        Gradient newGradient = _lineRenderer.colorGradient;

        GradientColorKey[] colorKeys = { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f)};
        GradientAlphaKey[] alphaKeys = { new GradientAlphaKey(color.a, 0f), new GradientAlphaKey(color.a, 1f) };

        newGradient.colorSpace = _lineRenderer.colorGradient.colorSpace;
        newGradient.SetKeys(colorKeys, alphaKeys);

        _lineRenderer.colorGradient = newGradient;
    }

    [ContextMenu("Update Ring")]
    public void UpdateRing()
    {
        _lineRenderer.widthMultiplier = Width;
        MakeRing(_lineRenderer, Radius, Segments);
    }

    // pass in your LineRenderer
    //	- make sure it has a valid material
    //	- control the width of it however you like
    //	- make sure it is set to loop
    private void MakeRing(LineRenderer lineRenderer, float radius, int segments)
    {
        Vector3[] points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            float angle = (i * Mathf.PI * 2) / segments;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            points[i] = new Vector3(x, y, 0);
        }

        lineRenderer.positionCount = segments;
        lineRenderer.SetPositions(points);
    }
}