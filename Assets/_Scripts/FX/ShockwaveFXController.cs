using System.Collections;
using UnityEngine;

public class ShockwaveFXController : MonoBehaviour
{
    [Space]

    [SerializeField] private AnimationCurve _circleWidthCurve;
    [SerializeField] private AnimationCurve _circleRadiusCurve;

    [Header("Debug")]

    [SerializeField] private bool _debug = false;
    [SerializeField] private float _testDuration = 0.1f;
    [SerializeField] private float _testRadius = 1f;

    private ScalableCircle _scalableCircle;
    private Coroutine _shockwaveCoroutine; 

    private void Awake()
    {
        _scalableCircle = GetComponentInChildren<ScalableCircle>();
    }

    private void Update()
    {
        if (!_debug) return;

        if ( Input.GetKeyDown(KeyCode.Space))
        {
            TriggerFX(_testRadius, _testDuration);
        }
    }

    public void TriggerFX(float shockwaveRadius = 1.5f, float shockwaveDuration = 0.1f)
    {
        if (_shockwaveCoroutine != null)
        {
            StopCoroutine(_shockwaveCoroutine);
        }

        _shockwaveCoroutine = StartCoroutine(ShockwaveCoroutine());

        IEnumerator ShockwaveCoroutine()
        {
            float timeElapsed = 0f;
            while (timeElapsed < shockwaveDuration)
            {
                _scalableCircle.Width = _circleWidthCurve.Evaluate(timeElapsed / shockwaveDuration);
                _scalableCircle.Radius = _circleRadiusCurve.Evaluate(timeElapsed / shockwaveDuration) * shockwaveRadius;
                yield return null;
                timeElapsed += Time.deltaTime;
            }

            _scalableCircle.Width = 0f;
            _scalableCircle.Radius = 0f;
            Destroy(gameObject);
        }
    }
}
