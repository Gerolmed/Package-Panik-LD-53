using UnityEngine;

namespace LudumDare.Sounds
{
    public class MusicKeepAlive: MonoBehaviour
    {
        private static MusicKeepAlive _instance;


        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
}