using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private GameObject m_GameOverPopup;
        [SerializeField] private Button m_RestartButton;
        
        public void GameOver()
        {
            m_GameOverPopup.SetActive(true);
            Time.timeScale = 0.0f;
        }
        
        private void Awake()
        {
            m_RestartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1.0f;
        }
    }
}