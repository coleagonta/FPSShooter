using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    
    private Animator _playerAnimator;
    private Rigidbody _rb;
    
    private Vector2 _turn;
    private Vector3 _deltaMove;
    private float _horizontalMove;
    private float _verticalMove;
    private bool _jump;
    private bool _isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerAnimator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateMovement();
        UpdateAnimator();
    }
    
    

    void UpdateMovement()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");
        _jump = Input.GetKeyDown(KeyCode.Space);
        
        
        
        if (_jump && _isGrounded)
        {
            _rb.AddForce(new Vector3(0, 5, 0), ForceMode.VelocityChange);
            _isGrounded = false;
        }
        
        _deltaMove = new Vector3(_horizontalMove, 0, _verticalMove) * (speed * Time.deltaTime);
        gameObject.transform.Translate(_deltaMove);
    }

    private void OnCollisionEnter(Collision ground)
    {
        {
            _isGrounded = true;
        }
    }

    
    void UpdateAnimator()
    {
        if (_verticalMove > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _playerAnimator.SetInteger("Move", 2);
                speed = 4;
            }
            else
            {
                _playerAnimator.SetInteger("Move", 1);
                speed = 2;
            }
        }
        else if (_verticalMove < 0)
        {
            _playerAnimator.SetInteger("Move", -1);
            speed = 2;
        }
        else if (_horizontalMove != 0)
        {
            _playerAnimator.SetInteger("Move", 3);
            speed = 2;
        }
        else
        {
            _playerAnimator.SetInteger("Move", 0);
            speed = 0;
        }
    }
}