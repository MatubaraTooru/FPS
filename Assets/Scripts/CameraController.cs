using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Vector2 _currentMousePosition;

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = 
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        _currentMousePosition = context.ReadValue<Vector2>();
    }
}
