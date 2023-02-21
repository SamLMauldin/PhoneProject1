using UnityEngine;
using UnityEngine.InputSystem;
public class TouchManager : MonoBehaviour
{
    private Camera mainCamera;

    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private InputAction touchHoldAction;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchHoldAction = playerInput.actions["TouchHold"];
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
        touchHoldAction.performed += TouchHold;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
        touchHoldAction.performed -= TouchHold;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);
        DetectObject();
    }

    private void TouchHold(InputAction.CallbackContext context)
    {
        Debug.Log("Held");
    }
    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider != null)
            {
                Debug.Log("Hit " + hit.collider.tag);
            }
        }
    }

}
