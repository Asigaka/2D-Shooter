using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Передвижение")]
    [SerializeField] private float speed = 4;
    [SerializeField] private bool joystickMove = true;

    [Header("Прыжки")]
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Компоненты")]
    [SerializeField] private GameObject joystickMovementUI;
    [SerializeField] private GameObject buttonsMovementUI;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;

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

    private void FixedUpdate()
    {
        if (joystickMove)
            JoystickMove();

        rb.velocity = new Vector2(_localSpeed, rb.velocity.y);
    }

    public void SwitchMovement()
    {
        joystickMove = !joystickMove;
        buttonsMovementUI.SetActive(!joystickMove);
        joystickMovementUI.SetActive(joystickMove);
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
        if (!joystickMove)
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
    }

    private void JoystickMove()
    {
        if (joystickMove)
        {
            if (joystick.Horizontal >= 0.2f)
                _localSpeed = speed;
            else if (joystick.Horizontal <= -0.2f)
                _localSpeed = -speed;
            else
                _localSpeed = 0;

            if (_localSpeed > 0 && !_isFacingRight)
                Flip();
            else if (_localSpeed < 0 && _isFacingRight)
                Flip();

            /*float vertical = joystick.Vertical;

            if (vertical >= 0.5f)
                Jump();
            */
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
