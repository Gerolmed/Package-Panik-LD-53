using UnityEngine;

namespace LudumDare.Satisfaction
{
    public class SatisfactionManager: MonoBehaviour, ISatisfactionManager
    {
        [SerializeField]
        private SatisfactionManagerSocket socketRef;

        [SerializeField]
        private int current = 90;


        private void Awake()
        {
            socketRef.Instance = this;
        }


        public int Current => current;

        public int Total { get; } = 100;
        public float Percentage => Current / (float)Total;
    }
}