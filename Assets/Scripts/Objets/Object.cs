using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Object : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite objectSpriteLeft;
    public Sprite objectSpriteRight;
    public string objectString;
    public AnimationCurve animationCurve;
    public float speed;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SlashObject()
    {

    }
}
