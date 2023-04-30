using System.Collections.Generic;
using UnityEngine;
using LudumDare.Utils;

namespace LudumDare.WorldGraph
{

    public interface NavNode<T>: IPathNode<NavUser> {
        
    }

    public class Node<T>: NavNode<T> {

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

        public bool IsWalkable(NavUser user) {
            return true;
        }

    }


    public class FillerNode<T>: NavNode<T> {

        public bool IsWalkable(NavUser user) {
            return false;
        }

    }

}