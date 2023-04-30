using UnityEngine;

namespace LudumDare.Satisfaction
{
    [CreateAssetMenu(menuName = "LD/Satisfaction Manager Socket", order = 0)]
    public class SatisfactionManagerSocket: ScriptableObject
    {
        
        public ISatisfactionManager Instance { get; set; }


        private void Awake()
        {
            Instance = null;
        }


        private void OnDestroy()
        {
            Instance = null;
        }
    }
}