using UnityEngine;
using UnityEngine.UI;

public class TransitionCanvas : MonoBehaviour
{
    public Image transitionImage;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayTransition()
    {
        animator.Play("TransitionStart");
    }

    void ReverseTransition()
    {
        animator.Play("TransitionEnd");
    }
}
