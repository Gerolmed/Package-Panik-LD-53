using System.Collections.Generic;
using UnityEngine;

namespace LudumDare.WorldGraph
{
    public class Node<T>
    {
        public Vector2Int Pos { get; }
        public object Data { get; }
        public DirectionMask Directions { get; }

        public List<Node<T>> Links { get; } = new();

        public int Id { get; }


        public Node(Vector2Int pos, object data, DirectionMask directions, int id)
        {
            Pos = pos;
            Data = data;
            Directions = directions;
            Id = id;
        }


        public bool HasLink(Node<T> other)
        {
            return Links.Contains(other);
        }


        public void AddLink(Node<T> other)
        {
            Links.Add(other);
        }
    }
}