using UnityEngine;

namespace LudumDare.Stats
{
    public class StatTrackManager: MonoBehaviour, IStatTrackManager
    {
        [SerializeField]
        private StatTrackManagerManagerSocket socket;


        private void Awake()
        {
            if (socket.Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            socket.Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        public int MoneyReceived { get; set; }
        public int MailDelivered { get; set; }
        public int PackagesDelivered { get; set; }
        public int DronesDelivered { get; set; }
    }
}