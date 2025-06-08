using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Samuraicontroller : MonoBehaviour
{
    InputAction Attack;
    public Animator animator;
    public AudioSource cutSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Attack = InputSystem.actions.FindAction("Attack");
    }

    private void OnEnable()
    {
        Object.OnCut += OnWin;
        Object.OnFail += OnFailed;
        LevelData.startLevel += OnStart;
    }

    private void OnDisable()
    {
        Object.OnCut -= OnWin;
        Object.OnFail -= OnFailed;
        LevelData.startLevel -= OnStart;
    }

    // Update is called once per frame
    void Update()
    {


        if (Attack.triggered)
        {
            Debug.Log("A was pressed");
        }

        
    }

    private void OnFailed()
    {
        animator.Play("Failure");
    }

    private void OnWin()
    {
        animator.Play("Attack");
        cutSound.Play();
    }

    private void OnStart()
    {
        animator.Play("Breathing");
    }
}
