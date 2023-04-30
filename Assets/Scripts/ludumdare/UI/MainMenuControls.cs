using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare.UI
{
    public class MainMenuControls: MonoBehaviour
    {
        public void EnterGame()
        {
            SceneManager.LoadScene("Scenes/MainWorld");
        }
    }
}