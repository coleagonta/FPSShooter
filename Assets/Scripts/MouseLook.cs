using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 0.5f; 
    private Vector2 _turn;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseInput();
        UpdateRotation();
    }

    void HandleMouseInput()
    {
        _turn.x += Input.GetAxis("Mouse X") * sensitivity;
        _turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        _turn.y = Mathf.Clamp(_turn.y, -20, 20);
    }

    void UpdateRotation()
    {
        transform.localRotation = Quaternion.Euler(-_turn.y, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, _turn.x, 0);
    }
}