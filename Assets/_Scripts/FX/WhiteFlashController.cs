using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFlashController : MonoBehaviour
{
    [Space]

    [SerializeField] private AnimationCurve _whiteScreenOpacityCurve;

    [Header("References")]

    [SerializeField] private RawImage _whiteImage;

    [Header("Debug")]

    [SerializeField] private float _testFlashDuration = 0.25f;

    private Coroutine _triggerflashCoroutine;

    private void Awake()
    {
        _whiteImage = GetComponentInChildren<RawImage>();
    }

    public void TriggerFlash(float flashDuration = 0.15f)
    {
        if (_triggerflashCoroutine != null)
        {
            StopCoroutine(_triggerflashCoroutine);
        }

        _triggerflashCoroutine = StartCoroutine(TriggerFlashCoroutine());

        IEnumerator TriggerFlashCoroutine()
        {
            float timeElapsed = 0f;

            while (timeElapsed < flashDuration)
            {
                Color newColor = _whiteImage.color;
                newColor.a = _whiteScreenOpacityCurve.Evaluate(timeElapsed / flashDuration);
                _whiteImage.color = newColor;
                yield return null;
                timeElapsed += Time.deltaTime;
            }

            Color endColor = _whiteImage.color;
            endColor.a = _whiteScreenOpacityCurve.Evaluate(timeElapsed / flashDuration);
            _whiteImage.color = endColor;
        }
    }

    [ContextMenu("Trigger Flash")]
    private void TriggerFlashTest()
    {
        Invoke("TriggerFlashTest2", 1f);
    }

    private void TriggerFlashTest2()
    {
        TriggerFlash(_testFlashDuration);
    }
}
