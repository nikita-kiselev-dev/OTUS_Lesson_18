using UnityEngine;

public class PlayerControllerPhysics : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private SpriteRenderer _shadowSpriteRenderer;

    [SerializeField]
    private Animator _animator;


    #region Physics

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Collider2D _collider2D;

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
        // Walking/Running
        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0f && !_feetCollider2D.IsTouchingLayers(_groundLayer))
        {
            if (!_isJumping)
            {
                _animator.SetBool(IsRunning, true);
            }

            _spriteRenderer.flipX = false;

            _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
        }
        else if (horizontal < 0f)
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
        var vertical = Input.GetAxis("Vertical");
        if (vertical > 0f && _feetCollider2D.IsTouchingLayers(_groundLayer))
        {
            _animator.SetBool(IsJumping, true);

            _shadowSpriteRenderer.enabled = false;

            _isJumping = true;

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _verticalSpeed);
        }
        else if (_feetCollider2D.IsTouchingLayers(_groundLayer))
        {
            _animator.SetBool(IsJumping, false);

            _isJumping = false;

            _shadowSpriteRenderer.enabled = true;
        }
    }
}