using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare.Districts
{
    public class DistrictManager: MonoBehaviour, IDistrictManager
    {
        [SerializeField]
        private DistrictManagerSocket districtManagerSocket;
        [SerializeField]
        private DistrictEvent onUnlock;

        private List<District> _districts = new();
        private readonly HashSet<string> _unlocked = new();

        private void Awake()
        {
            districtManagerSocket.Instance = this;
        }


        public IEnumerable<District> GetAll()
        {
            return _districts;
        }


        public IEnumerable<District> GetUnlocked()
        {
            return GetAll().Where(district => !IsLocked(district));
        }


        public IEnumerable<District> GetLocked()
        {
            return GetAll().Where(IsLocked);
        }


        public void Unlock(District district)
        {
            _unlocked.Add(district.ID);
            onUnlock.Invoke(district);
        }


        public bool IsLocked(District district)
        {
            return !_unlocked.Contains(district.ID);
        }


        public void Add(District district)
        {
            _districts.Add(district);
        }


        public District GetAt(Vector2Int node)
        {
            return _districts.FirstOrDefault(district => district.Contains(node));
        }
    }
    
    [Serializable]
    public class DistrictEvent: UnityEvent<District> {}
}