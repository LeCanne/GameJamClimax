using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData instance;
    public float currentProgress;
    public float cutTime;
    public bool winLevel;

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

    
}
