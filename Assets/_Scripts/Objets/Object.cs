using System;
using System.Collections;
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

    bool lost;

    public static Action OnCut;
    public static Action OnFail;

    [Space]

    public ShockwaveFXController _shockwaveFX;

    private const float HITSTOP_DURATION = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelData.instance.hasEnded = false;
        LevelData.instance.state = LevelData.levelStates.ONGOING;
        LevelData.instance.cutTime = Cut;

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
        LevelData.instance.currentProgress = currentTime;

        if (LevelData.instance.processingLevel == true)
        {
            if (!isCut)
            {
                TravelSpline();
            }

            SlashObject();
        }
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
        if(currentTime >= 1)
        {
          StartCoroutine(handleEnd(LevelData.levelStates.LOST));
        }

        
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
                if (LevelSystem.instance.currentLevelID >= 3)
                {
                    SpawnShockwave();
                }

                DoHitStop();
            }
            else
            {
                Debug.Log(" FAILURE !");
                OnFail?.Invoke();
            }

            canInputCut = false;
            OnCut?.Invoke();
        }
      
    }

    private void DoHitStop()
    {
        StartCoroutine(HitStopCoroutine());

        IEnumerator HitStopCoroutine()
        {
            Time.timeScale = 0f;

            yield return new WaitForSecondsRealtime(1f);

            Time.timeScale = 1f;

            isCut = true;
            canCut = false;
            LaunchObjet();
            StartCoroutine(handleEnd(LevelData.levelStates.WON));
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
    
    private void SpawnShockwave()
    {
        Debug.Log("Shockwave");
        var shockwave = Instantiate(_shockwaveFX, moveObject.transform.position, Quaternion.identity);
        shockwave.TriggerFX(5f, 0.2f);
    }

    IEnumerator CuttingTime()
    {
        Debug.Log("CUT!");
        canCut = true;
        yield return new WaitForSeconds(reactionTime);
        canCut = false;
        
    }


    IEnumerator handleEnd(LevelData.levelStates state)
    {
        if (state == LevelData.levelStates.LOST)
        {
            OnFail?.Invoke();
        }

        LevelData.instance.currentProgress = 0f;
        LevelData.instance.hasEnded = true;

        yield return new WaitForSeconds(1.5f);
        yield return LevelData.instance.state = state;
        LevelData.instance.HandleEnd();

        
    }


    
}
