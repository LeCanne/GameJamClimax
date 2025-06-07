using UnityEngine;

public class EffectsData : MonoBehaviour
{
    public Animator animatorTransition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelData.instance.levelFinished)
        {
            HandleStartEffects();
            
        }

        if (LevelData.instance.levelStarted)
        {
            HandleEndEffects();
        }
    }

    void HandleStartEffects()
    {
        animatorTransition.Play("TransitionStart");
    }

    void HandleEndEffects()
    {
        animatorTransition.Play("TransitionEnd");
    }
}
