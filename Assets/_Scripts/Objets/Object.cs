using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;


public class Object : MonoBehaviour
{
    //Graphic References
    public SpriteRenderer r_spriteRenderer;
    public SpriteRenderer l_spriteRenderer;
    public Sprite r_sprite;
    public Sprite l_sprite;
    private GameObject r_object;
    private GameObject l_object;
    public string objectName;

    //Spline Values
    public AnimationCurve animationCurve;
    public float speed;
    public SplineContainer splinePath;
    private Spline spline;

    //Spline Parameters
    public GameObject moveObject;
    public bool loop;
    private float currentTime;


    //Set Cuttable Time
    [Range(0f, 1f)] public float Cut;
    public float reactionTime;
    bool cutCalled;

    //CutContols
    InputAction cut;

    //Set cuttable object
    bool canCut;
    bool isCut;
    bool canInputCut = true;

   


    private void Awake()
    {
<<<<<<< Updated upstream
        r_spriteRenderer.sprite = r_sprite;
        l_spriteRenderer.sprite = l_sprite; 

        r_object = r_spriteRenderer.gameObject;
        l_object = l_spriteRenderer.gameObject;

        spline = splinePath.Spline;
        cut = InputSystem.actions.FindAction("Attack");
=======
        
       
>>>>>>> Stashed changes
        

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelData.instance.state = LevelData.levelStates.ONGOING;
        r_spriteRenderer.sprite = r_sprite;
        l_spriteRenderer.sprite = l_sprite;

        r_object = r_spriteRenderer.gameObject;
        l_object = l_spriteRenderer.gameObject;

        spline = splinePath.Spline;
        cut = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCut)
        {
            TravelSpline();
        }

        SlashObject();
        

    }

    public void TravelSpline()
    {
       
        if (loop)
        {
            currentTime += Time.deltaTime * speed;
            //Loop the animation
            if (currentTime > 1)
            {
                currentTime = 0;
            }
        }
        else if (currentTime < 1)
        {
            currentTime += Time.deltaTime * speed;
        }
        Vector3 currentDistance = spline.EvaluatePosition(animationCurve.Evaluate(currentTime));
        moveObject.transform.position = currentDistance + splinePath.gameObject.transform.position;

        
    }

    public virtual void SlashObject()
    {
        if (currentTime >= Cut && cutCalled == false)
        {
            StartCoroutine(CuttingTime());
            cutCalled = true;


         
        }

        if (cut.triggered && canInputCut)
        {
            if (canCut == true)
            {
                Debug.Log(" SUCCESS !");
                isCut = true;
                canCut = false;
                LaunchObjet();
            }
            else
            {
                Debug.Log(" FAILURE !");
            }
<<<<<<< Updated upstream
           
=======
            StartCoroutine(handleEnd(State));

            canInputCut = false;

>>>>>>> Stashed changes
        }
      
    }

    private void LaunchObjet()
    {
       Rigidbody2D r_rb = l_object.GetComponent<Rigidbody2D>();
       Rigidbody2D l_rb = r_object.GetComponent<Rigidbody2D>();


        r_rb.simulated = true;
        l_rb.simulated = true;

        
        r_rb.AddForce(r_object.transform.right * 10, ForceMode2D.Impulse);
        l_rb.AddForce(-l_object.transform.right * 10, ForceMode2D.Impulse);
    }
    

    IEnumerator CuttingTime()
    {
        Debug.Log("CUT!");
        canCut = true;
        yield return new WaitForSeconds(reactionTime);
        canCut = false;
        
    }

<<<<<<< Updated upstream
=======
    IEnumerator handleEnd(LevelData.levelStates state)
    {
        yield return new WaitForSeconds(1.5f);
        yield return LevelData.instance.state = state;
        LevelData.instance.HandleEnd();

        
    }

>>>>>>> Stashed changes
    
}
