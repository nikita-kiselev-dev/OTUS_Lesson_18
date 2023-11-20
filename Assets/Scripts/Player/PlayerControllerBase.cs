using UnityEngine;

namespace Player
{
    public abstract class PlayerControllerBase : MonoBehaviour
    {
        private protected static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private protected static readonly int IsJumping = Animator.StringToHash("IsJumping");

        [SerializeField]
        private protected SpriteRenderer _spriteRenderer;

        [SerializeField]
        private protected SpriteRenderer _shadowSpriteRenderer;

        [SerializeField]
        private protected Animator _animator;

        [SerializeField]
        private protected Transform m_PlayerPosition;
    
        public Transform PlayerPosition => m_PlayerPosition;
    }
}