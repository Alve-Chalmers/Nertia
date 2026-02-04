using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction btuAction;

    void Start()
    {
        btuAction = InputSystem.actions.FindAction("BTU");
    }

    // Update is called once per frame
    void Update()
    {
        if (btuAction.WasPerformedThisFrame())
        {

        }
    }
}
