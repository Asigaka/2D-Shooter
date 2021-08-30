using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Передвижение")]
    [SerializeField] private float speed = 4;

    [Header("Прыжки")]
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Компоненты")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;

    private bool _buttonMovementUI = true;
    private bool _isFacingRight = true;
    private bool _isGrounded;
    private float _localSpeed;
    private int _localJumpCount;

    private float _horizontal;
    private float _vertical;

    private void Start()
    {
        _localSpeed = 0;
        _localJumpCount = jumpCount;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_buttonMovementUI)
        {
            rb.velocity = new Vector2(_localSpeed, rb.velocity.y);
        }
    }

    public void OnLeftMove() => ButtonMove(false);

    public void OnRightMove() => ButtonMove(true);

    public void OnPointerUp() => _localSpeed = 0;

    public void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (_isGrounded)
            _localJumpCount = jumpCount;

        if ( _localJumpCount > 0)
        {
            rb.velocity = Vector2.up * jumpForce;

            _localJumpCount--;
        }
        else if (_isGrounded && _localJumpCount == 0)
            rb.velocity = Vector2.up * jumpForce;
    }

    private void ButtonMove(bool right)
    {
        if (right)
            _localSpeed = speed;
        else
            _localSpeed = -speed;

        if (_localSpeed > 0 && !_isFacingRight)
            Flip();
        else if (_localSpeed < 0 && _isFacingRight)
            Flip();
    }

    private void JoystickMoveValues()
    {
        _horizontal = joystick.Horizontal * speed;
    }

    private void JoystickMove()
    {

    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
