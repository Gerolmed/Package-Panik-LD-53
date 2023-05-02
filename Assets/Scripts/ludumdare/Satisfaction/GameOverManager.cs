using LudumDare.TimeControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare.Satisfaction
{
    public class GameOverManager: MonoBehaviour
    {
        [SerializeField]
        private SatisfactionManagerSocket satisfactionManagerSocket;
        [SerializeField]
        private TimeControlManagerSocket timeControlManager;


        private void Update()
        {
            if(timeControlManager.Instance.TimeMode == TimeMode.Paused) return;
            if(satisfactionManagerSocket.Instance.Current > 0) return;
            DoGameOver();
        }


        private void DoGameOver()
        {
            timeControlManager.Instance.TimeMode = TimeMode.Paused;
            SceneManager.LoadScene("Scenes/GameOver");
        }
    }
}