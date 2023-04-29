using System.Collections.Generic;
using UnityEngine;

namespace LudumDare.WorldGraph
{
    public class Node<T>
    {
        public Vector2Int Pos { get; }
        public object Data { get; }
        public DirectionMask Directions { get; }

        private List<Node<T>> _links = new();


        public Node(Vector2Int pos, object data, DirectionMask directions)
        {
            Pos = pos;
            Data = data;
            Directions = directions;
        }


        public bool HasLink(Node<T> other)
        {
            return _links.Contains(other);
        }


        public void AddLink(Node<T> other)
        {
            _links.Add(other);
        }
    }
}