using System;
using Game;
using UnityEngine;

namespace Player
{
    public class VisibleController : MonoBehaviour
    {
        [SerializeField] private GameOverController m_GameOverController;
        
        private void OnBecameInvisible()
        {
            m_GameOverController.GameOver();
        }
    }
}