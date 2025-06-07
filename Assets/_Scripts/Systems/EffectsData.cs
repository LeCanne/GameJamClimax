using TMPro;
using UnityEngine;

public class EffectsData : MonoBehaviour
{
    public Animator animatorTransition;
    public Animator bannerRound;
    public TMP_Text boundText;
    public GameObject endScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
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

        if (LevelData.instance.beforeProcess)
        {
            bannerRound.Play("ShowRound");
            ShowRound();
        }

        if (LevelData.instance.gameEnded)
        {
            endScreen.SetActive(true);
            winScreen.SetActive(true);
        }

        if (LevelData.instance.hasLost)
        {
            endScreen.SetActive(true);
            loseScreen.SetActive(true);
        }

        LevelData.instance.CheckStates();
    }

    void HandleStartEffects()
    {
        animatorTransition.Play("TransitionStart");
    }

    void HandleEndEffects()
    {
        animatorTransition.Play("TransitionEnd");
    }

    void ShowRound()
    {
       
        boundText.text = "ROUND " + LevelSystem.instance.currentLevelID.ToString();
       
    }

   
}
