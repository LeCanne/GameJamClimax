using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager Instance;

    private Volume _globalVolume;

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

        _globalVolume = GetComponent<Volume>();
    }

    public void SetNegative(bool newState)
    {
        _globalVolume.weight = (newState) ? 1.0f : 0.0f;
    }

    [ContextMenu("Activate")]
    private void Activate()
    {
        SetNegative(true);
    }

    [ContextMenu("Deactivate")]
    private void Deactivate()
    {
        SetNegative(false);
    }
}
