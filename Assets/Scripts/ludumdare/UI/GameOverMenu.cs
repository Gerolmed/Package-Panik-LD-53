using System;
using LudumDare.Stats;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare.UI
{
    public class GameOverMenu: MonoBehaviour
    {
        [SerializeField]
        private StatTrackManagerManagerSocket statSocket;

        [SerializeField]
        private TMP_Text moneyText;
        [SerializeField]
        private TMP_Text mailText;
        [SerializeField]
        private TMP_Text packageText;
        [SerializeField]
        private TMP_Text droneText;
        
        private void Start()
        {
            moneyText.text = Formatted(statSocket.Instance.MoneyReceived);
            mailText.text = Formatted(statSocket.Instance.MailDelivered);
            packageText.text = Formatted(statSocket.Instance.PackagesDelivered);
            droneText.text = Formatted(statSocket.Instance.DronesDelivered);

        }
        
        private string Formatted(int balance)
        {
            return $"{balance:n0}";
        }


        public void ReturnToMainMenu()
        {
            statSocket.Instance.MoneyReceived = 0;
            statSocket.Instance.MailDelivered = 0;
            statSocket.Instance.PackagesDelivered = 0;
            statSocket.Instance.DronesDelivered = 0;
            SceneManager.LoadScene("Scenes/Main Menu");
        }
    }
}