using UnityEngine;

public class SlashFXController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    [ContextMenu("Trigger")]
    public void TriggerFX()
    {
        _animator.SetTrigger("slash");
    }
}
