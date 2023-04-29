using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building: MonoBehaviour
{
    private int _level = 0;
    
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _portability;
    [SerializeField] private BuildingLevel[] _levels;

    public void Upgrade()
    {
        _level += 1;
        _levels[_level].Upgrade(this);
    }

    public void SetSprite(Sprite newSprite)
    {
        _sprite = newSprite;
    }

    public void SetPortability(int newPortability)
    {
        _portability = newPortability;
    }

    public abstract void LaunchDelivery();
}
