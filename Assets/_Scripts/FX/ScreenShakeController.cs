using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    [Space]

    [SerializeField] private AnimationCurve _amplitudeDecreaseCurve;

    [Header("Debug")]

    [SerializeField] private float _testShakeAmplitude = 1.0f;
    [SerializeField] private float _testFadeDuration = 1.0f;

    private CinemachineBasicMultiChannelPerlin _noise;

    private Coroutine _amplitudeDecreaseCoroutine;

    private void Awake()
    {
        _noise = GetComponent<CinemachineBasicMultiChannelPerlin>();

        ScreenShake.controller = this;
    }

    public void Shake(float amplitude = 1f, float fadeDuration = 1f)
    {
        _noise.AmplitudeGain = amplitude;
        DecreaseAmplitude(fadeDuration);
    }

    private void DecreaseAmplitude(float fadeDuration)
    {
        if (_amplitudeDecreaseCoroutine != null)
        {
            StopCoroutine(_amplitudeDecreaseCoroutine);
        }

        _amplitudeDecreaseCoroutine = StartCoroutine(AmplitudeDecreaseCoroutine(fadeDuration));
    }

    private IEnumerator AmplitudeDecreaseCoroutine(float fadeDuration)
    {
        float timeElapsed = 0f;
        float initialAmplitude = _noise.AmplitudeGain;

        while (timeElapsed < fadeDuration)
        {
            float lerp = _amplitudeDecreaseCurve.Evaluate(timeElapsed / fadeDuration);
            float newNoise = Mathf.Lerp(initialAmplitude, 0f, lerp);
            _noise.AmplitudeGain = newNoise;
            yield return null;
            timeElapsed += Time.deltaTime;
        }

        _noise.AmplitudeGain = 0f;
    }

    [ContextMenu("Shake")]
    private void ShakeTest()
    {
        ScreenShake.Shake(_testShakeAmplitude, _testFadeDuration);
    }
}
