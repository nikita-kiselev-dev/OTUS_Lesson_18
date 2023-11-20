using Player;
using UnityEngine;

public class PlayerControllerPhysics : PlayerControllerBase
{
    #region Physics

    private const float _jumpVerticalForceFull = 1500.0f;
    private const float _jumpVerticalForceSide = 1100.0f;

    private const float _jumpHorizontalForceUp = 0.0f;
    private const float _jumpHorizontalForceLeft = -1000.0f;
    private const float _jumpHorizontalForceRight = 1000.0f;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private Collider2D m_LeftSideCollider2D;
    
    [SerializeField]
    private Collider2D m_RightSideCollider2D;

    [SerializeField]
    private Collider2D _feetCollider2D;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _verticalSpeed;

    [SerializeField]
    private LayerMask _groundLayer;

    private bool _isJumping;

    #endregion


    private void Update()
    {
        var isTouchingGround = _feetCollider2D.IsTouchingLayers(_groundLayer);
        var isTouchingWallsLeft = m_LeftSideCollider2D.IsTouchingLayers(_groundLayer);
        var isTouchingWallsRight = m_RightSideCollider2D.IsTouchingLayers(_groundLayer);
        var isTouchingWalls = isTouchingWallsLeft || isTouchingWallsRight;

        Debug.Log($"ground: {isTouchingGround}");
        Debug.Log($"left: {isTouchingWallsLeft}");
        Debug.Log($"right: {isTouchingWallsRight}");
        
        // Walking/Running
        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0f && isTouchingGround)
        {
            if (!_isJumping)
            {
                _animator.SetBool(IsRunning, true);
            }

            _spriteRenderer.flipX = false;
            
            _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
        }
        else if (horizontal < 0f && isTouchingGround)
        {
            if (!_isJumping)
            {
                _animator.SetBool(IsRunning, true);
            }

            _spriteRenderer.flipX = true;

            _rigidbody2D.velocity = new Vector2(-_speed, _rigidbody2D.velocity.y);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }

        // Jumping
        var jumpButtonPressed = Input.GetKeyDown(KeyCode.Space);
        
        if (jumpButtonPressed && isTouchingGround)
        {
            Jump(_jumpHorizontalForceUp, _jumpVerticalForceFull);
        }
        else if (jumpButtonPressed && isTouchingWallsLeft)
        {
            Jump(_jumpHorizontalForceRight, _jumpVerticalForceFull);
        }
        else if (jumpButtonPressed && isTouchingWallsRight)
        {
            Jump(_jumpHorizontalForceLeft, _jumpVerticalForceFull);
        }
        else if (isTouchingGround)
        {
            _animator.SetBool(IsJumping, false);

            _isJumping = false;

            _shadowSpriteRenderer.enabled = true;
        }
    }

    private void Jump(float sideForce, float upForce)
    {
        _animator.SetBool(IsJumping, true);

        _shadowSpriteRenderer.enabled = false;

        _isJumping = true;
        
        _rigidbody2D.AddForce(new Vector2(sideForce, upForce));
    }
}