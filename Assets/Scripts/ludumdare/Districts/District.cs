using UnityEngine;

namespace LudumDare.Districts
{
    public class District
    {
        public District(string id, Vector2Int lowerBound, Vector2Int upperBound, int price)
        {
            ID = id;
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Price = price;
        }


        public string ID { get; }
        public Vector2Int LowerBound { get; }
        public Vector2Int UpperBound { get; }
        public int Price { get; }
        
        public bool Contains(Vector2Int node)
        {
            return node.x >= LowerBound.x && node.x <= UpperBound.x &&
                   node.y >= LowerBound.y && node.y <= UpperBound.y;
        }
    }
}