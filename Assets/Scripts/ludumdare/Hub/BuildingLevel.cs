using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "BuildingLevel")]
public class BuildingLevel : ScriptableObject
{
    [SerializeField] private int _portability;
    [SerializeField] private Sprite _sprite;

    public void Upgrade(Building target)
    {
        target.SetPortability(_portability);
        target.SetSprite(_sprite);
    }
}
