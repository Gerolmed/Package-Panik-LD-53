using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace LudumDare.Districts
{
    public interface IDistrictManager
    {
        IEnumerable<District> GetAll();
        IEnumerable<District> GetUnlocked();
        IEnumerable<District> GetLocked();


        void Unlock(District district);
        bool IsLocked(District district);
        
        [CanBeNull]
        District GetAt(Vector2Int node);
    }
}