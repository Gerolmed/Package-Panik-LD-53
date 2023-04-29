using UnityEngine;
using UnityEngine.Events;

namespace LudumDare.Hub.Buildings
{
    public class Building: MonoBehaviour
    {
        private int _level = 0;
        [SerializeField] private BuildingLevel[] levels;
        [SerializeField] private BuildingUpgradeEvent upgradeEvent;

        private void Start() 
        {
            ExecuteUpgrade();
        }

        public void Upgrade()
        {
            if (_level == levels.Length - 1) return;

            _level += 1;
            ExecuteUpgrade();
        }

        private void ExecuteUpgrade()
        {
            upgradeEvent.Invoke((_level, levels[_level]));
        }
    }

    [System.Serializable]
    public class BuildingUpgradeEvent: UnityEvent<(int, BuildingLevel)> {}
}