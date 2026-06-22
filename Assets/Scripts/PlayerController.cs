using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float tempSpeed = 5.0f;
    [SerializeField] private float tempJumpSpeed = 5.0f;
    
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private bool _isGrounded;

    private void Awake()
    {
        if (!TryGetComponent<Rigidbody2D>(out _rb))
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded || Input.GetKey(KeyCode.Space) &&  _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, tempJumpSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _spriteRenderer.flipX = false;
            transform.Translate(Vector3.right * tempSpeed * Time.deltaTime  );
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _spriteRenderer.flipX = true;
            transform.Translate(Vector3.left * tempSpeed * Time.deltaTime  );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        { 
            _isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
