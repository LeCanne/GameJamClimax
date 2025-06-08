using UnityEngine;

public class SlashFXController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Object.OnCut += OnCut;
    }

    private void OnDisable()
    {
        Object.OnCut -= OnCut;
    }

    private void OnCut()
    {
        TriggerFX();
    }

    [ContextMenu("Trigger")]
    public void TriggerFX()
    {
        _animator.SetTrigger("slash");
    }
}
