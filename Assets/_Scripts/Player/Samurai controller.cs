using UnityEngine;
using UnityEngine.InputSystem;

public class Samuraicontroller : MonoBehaviour
{
    InputAction Attack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Attack = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {


        if (Attack.triggered)
        {
            Debug.Log("A was pressed");
        }
    }
}
