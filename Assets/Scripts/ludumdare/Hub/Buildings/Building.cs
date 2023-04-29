using UnityEngine;
using UnityEngine.Events;
using LudumDare.MoneySystem;

namespace LudumDare.Hub.Buildings
{
    public class Building: MonoBehaviour
    {
        private int _level = 0;
        [SerializeField] private BuildingLevel[] levels;
        [SerializeField] private BuildingUpgradeEvent upgradeEvent;

        [SerializeField] private MoneyManagerSocket moneyManagerSocket;

        private void Start() 
        {
            ExecuteUpgrade();
        }

        public void Upgrade()
        {
            if (_level == levels.Length - 1) return;
            
            if (moneyManagerSocket.Instance.TryDeduct(levels[_level + 1].Price, TransactionType.Upgrades))
            {
                _level += 1;
                ExecuteUpgrade();
            } 
        }

        private void ExecuteUpgrade()
        {
            upgradeEvent.Invoke((_level, levels[_level], _level + 1 == levels.Length ? null : levels[_level + 1]));
        }
    }

#nullable enable
    [System.Serializable]
    public class BuildingUpgradeEvent: UnityEvent<(int, BuildingLevel, BuildingLevel?)> {}
}