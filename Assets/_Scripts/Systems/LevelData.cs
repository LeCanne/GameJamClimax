using System.Collections;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData instance;
    public float currentProgress;
    public float cutTime;
    public bool winLevel;
    public bool levelStarted;
    public bool levelFinished;

    public float loadingTime;

    public enum levelStates
    {
        ONGOING,
        WON,
        LOST,
    }

    public levelStates state;
    
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     

    }

    public void HandleEnd()
    {

        switch (state)
        {
            case levelStates.ONGOING:
                break;
            case levelStates.WON:
                levelFinished = true;
                break;
            case levelStates.LOST:
                break;
        }
       
        StartCoroutine(LoadingTime());

    }

    IEnumerator LoadingTime()
    {
        yield return new WaitForSeconds(0.1f);
        CheckStates();
        yield return new WaitForSeconds(loadingTime);
        LevelSystem.instance.NextLevel();
        yield return levelStarted = true;
        yield return new WaitForSeconds(0.1f);
        CheckStates();

    }

    public void CheckStates()
    {
        if (levelFinished == true)
        {

            levelFinished = false;
        }

        if (levelStarted == true)
        {
            levelStarted = false;
        }
    }


    
}
