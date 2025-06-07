using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;
    public GameObject[] levels;
    public int currentLevelID;
    public GameObject currentLevelObject;
    public bool noMoreLevels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
    void Start()
    {
        currentLevelID = Mathf.Clamp(currentLevelID, 0, levels.Length - 1);
        currentLevelObject = levels[currentLevelID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    
    void LoadLevel(int level)
    {
        currentLevelObject.SetActive(false);
        currentLevelID = level;
        currentLevelID = Mathf.Clamp(currentLevelID, 0, levels.Length - 1);
        currentLevelObject = levels[currentLevelID];
    }

    public void CheckLevel() 
    {
        if (currentLevelID >= levels.Length - 1)
        {
            noMoreLevels = true;
        }
    }
    public void NextLevel()
    {
        CheckLevel();
        if(noMoreLevels == true)
        {
            return;
        }
        currentLevelObject.SetActive(false);
        currentLevelID += 1;
        currentLevelObject = levels[currentLevelID];
        currentLevelObject.SetActive(true);
        

        noMoreLevels = false;
       
    }

    

    
}
