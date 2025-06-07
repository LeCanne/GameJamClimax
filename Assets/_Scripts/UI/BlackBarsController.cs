using System.Collections;
using UnityEngine;

public class BlackBarsController : MonoBehaviour
{
    [Space]

    [SerializeField, Range(0f,1f)] private float _progress = 0f;

    [Space]

    [SerializeField] private float _topBarStartPosition;
    [SerializeField] private float _topBarEndPosition;

    [Space]

    [SerializeField] private float _bottomBarStartPosition;
    [SerializeField] private float _bottomBarEndPosition;

    [Space]

    [SerializeField] private float _topBarApexPosition;
    [SerializeField] private float _bottomBarApexPosition;

    [Header("Transitions")]

    [SerializeField] private float _apexTransitionDuration = 0.1f;
    [SerializeField] private AnimationCurve _apexTransitionCurve;

    [Space]

    [SerializeField] private float _resetTransitionDuration = 0.25f;
    [SerializeField] private AnimationCurve _resetTransitionCurve;

    [Header("References")]

    [SerializeField] private RectTransform _topBar;
    [SerializeField] private RectTransform _bottomBar;

    private Coroutine _triggerApexCoroutine;
    private Coroutine _resetBarsCoroutine;

    private bool _enableBarUpdate = true;

    private void Update()
    {
        if (_enableBarUpdate)
        {
            UpdateBars();
        }
    }

    public void SetProgress(float newProgress)
    {
        _progress = newProgress;
    }

    public void UpdateBars()
    {
        float topBarY = Mathf.Lerp(_topBarStartPosition, _topBarEndPosition, _progress);
        float bottomBarY = Mathf.Lerp(_bottomBarStartPosition, _bottomBarEndPosition, _progress);

        _topBar.anchoredPosition = new Vector2(_topBar.anchoredPosition.x, topBarY);
        _bottomBar.anchoredPosition = new Vector2(_bottomBar.anchoredPosition.x, bottomBarY);
    }

    public void TriggerApex()
    {
        if (_triggerApexCoroutine != null)
        {
            StopCoroutine(_triggerApexCoroutine);
        }

        StartCoroutine(TriggerApexCoroutine());

        IEnumerator TriggerApexCoroutine()
        {
            _enableBarUpdate = false;
            float timeElapsed = 0f;

            float topBarInitialY = _topBar.anchoredPosition.y;
            float bottomBarInitialY = _bottomBar.anchoredPosition.y;

            while (timeElapsed < _apexTransitionDuration)
            {
                float lerp = _apexTransitionCurve.Evaluate(timeElapsed / _apexTransitionDuration);
                float topBarNewY = Mathf.Lerp(topBarInitialY, _topBarApexPosition, lerp);
                float bottomBarNewY = Mathf.Lerp(bottomBarInitialY, _bottomBarApexPosition, lerp);

                _topBar.anchoredPosition = new Vector2(_topBar.anchoredPosition.x, topBarNewY);
                _bottomBar.anchoredPosition = new Vector2(_bottomBar.anchoredPosition.x, bottomBarNewY);

                yield return null;
                timeElapsed += Time.deltaTime;
            }

            _topBar.anchoredPosition = new Vector2(_topBar.anchoredPosition.x, _topBarApexPosition);
            _bottomBar.anchoredPosition = new Vector2(_bottomBar.anchoredPosition.x, _bottomBarApexPosition);
        }
    }

    public void ResetBars()
    {
        if (_resetBarsCoroutine != null)
        {
            StopCoroutine(_resetBarsCoroutine);
        }

        StartCoroutine(ResetBarsCoroutine());

        IEnumerator ResetBarsCoroutine()
        {
            _enableBarUpdate = false;
            _progress = 0f;
            float timeElapsed = 0f;

            float topBarInitialY = _topBar.anchoredPosition.y;
            float bottomBarInitialY = _bottomBar.anchoredPosition.y;

            while (timeElapsed < _resetTransitionDuration)
            {
                float lerp = _resetTransitionCurve.Evaluate(timeElapsed / _resetTransitionDuration);
                float topBarNewY = Mathf.Lerp(topBarInitialY, _topBarStartPosition, lerp);
                float bottomBarNewY = Mathf.Lerp(bottomBarInitialY, _bottomBarStartPosition, lerp);

                _topBar.anchoredPosition = new Vector2(_topBar.anchoredPosition.x, topBarNewY);
                _bottomBar.anchoredPosition = new Vector2(_bottomBar.anchoredPosition.x, bottomBarNewY);

                yield return null;
                timeElapsed += Time.deltaTime;
            }

            _topBar.anchoredPosition = new Vector2(_topBar.anchoredPosition.x, _topBarStartPosition);
            _bottomBar.anchoredPosition = new Vector2(_bottomBar.anchoredPosition.x, _topBarStartPosition);

            _enableBarUpdate = true;
        }
    }

    #region debug

    [ContextMenu("Trigger Apex")]
    private void TriggerApexTest()
    {
        Invoke("TriggerApex", 1f);
    }

    [ContextMenu("Reset Bars")]
    private void ResetBarsTest()
    {
        Invoke("ResetBars", 1f);
    }

    #endregion
}
