using UnityEngine;

namespace LudumDare.Satisfaction
{
    public class SatisfactionManager: MonoBehaviour, ISatisfactionManager
    {
        [SerializeField]
        private SatisfactionManagerSocket socketRef;


        private void Awake()
        {
            socketRef.Instance = this;
        }
    }
}