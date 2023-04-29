using LudumDare.TimeControl;
using UnityEngine;

namespace LudumDare.MoneySystem
{
    [CreateAssetMenu(menuName = "LD/Money Manager Socket", order = 0)]
    public class MoneyManagerSocket : ScriptableObject
    {
        public IMoneyManager Instance { get; set; }


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