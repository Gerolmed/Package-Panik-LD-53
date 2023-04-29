using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare.Hub.Buildings
{
    [CreateAssetMenu(menuName = "BuildingLevel")]
    public class BuildingLevel : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string description;
        [SerializeField] private int price;
        public Sprite Sprite => sprite;
        public string Description => description;
        public int Price => price;
    }
}