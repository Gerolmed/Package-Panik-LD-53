using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare.WorldGraph
{
    public class Graph<T>
    {

        private int _idCounter = 0;

        private Dictionary<Vector2Int, Node<T>> _nodeGraph = new();

        private Dictionary<(int, int), (Node<T>, int)> _pathCache = new();


        private Node<T> AddNodeAt(Vector2Int pos, T data,
            DirectionMask directions = DirectionMask.None)
        {
            var node = new Node<T>(pos, data, directions, _idCounter++);

            _nodeGraph.Add(pos, node);
            UpdateLinks(node);

            return node;
        }


        public (Node<T>, int) GetNext(Node<T> from, Node<T> to) {
            var key = (from.Id, to.Id);
            if (_pathCache.ContainsKey(key))
                return _pathCache[key];

            if (from == to) return (null, 0);

            // avoid infinite loop
            _pathCache[key] = (null, int.MaxValue);

            int minDistance = 0;
            Node<T> next = null;
            foreach (var link in from.Links) {
                var result = GetNext(link, to);
                if (next == null || result.Item2 < minDistance) {
                    next = link;
                    minDistance = result.Item2;
                }
            }

            var res = (next, minDistance + 1);
            _pathCache[key] = res;
            return res;
        }


        private void UpdateLinks(Node<T> node)
        {

            var linkDirections = new[]
            {
                (new Vector2Int(0, 1), DirectionMask.Up),
                (new Vector2Int(1, 0), DirectionMask.Right),
                (new Vector2Int(0, -1), DirectionMask.Down),
                (new Vector2Int(-1, 0), DirectionMask.Left),
            };
            // If allows any direction
            foreach (var linkDirection in linkDirections)
            {
                UpdateDirection(node, linkDirection);
            }
        }


        private void UpdateDirection(Node<T> node, (Vector2Int, DirectionMask) linkDirection, bool updateNeighbour = true)
        {
            
            var directions = node.Directions;
            var pos = node.Pos;
            
            if (!TryGetNode(pos + linkDirection.Item1, out var other))
            {
                return;
            }
            
            if(updateNeighbour)
            {
                UpdateDirection(other, InvertLinkDir(linkDirection), false);
            }
            
            // Should this node connect in that direction
            if ((directions & linkDirection.Item2) != linkDirection.Item2 && !node.HasLink(other))
            {
                node.AddLink(other);
            }
        }


        private static (Vector2Int, DirectionMask) InvertLinkDir((Vector2Int, DirectionMask) linkDirection)
        {
            var newDir = linkDirection.Item2 switch
            {
                DirectionMask.None => DirectionMask.None,
                DirectionMask.Up => DirectionMask.Down,
                DirectionMask.Right => DirectionMask.Left,
                DirectionMask.Down => DirectionMask.Up,
                DirectionMask.Left => DirectionMask.Right,
                _ => throw new ArgumentOutOfRangeException()
            };

            return (linkDirection.Item1 * -1, newDir);
        }


        private bool TryGetNode(Vector2Int pos, out Node<T> node)
        {
            return _nodeGraph.TryGetValue(pos, out node);
        }
    }

    [Flags]
    public enum DirectionMask
    {
        None = 0b0000000,
        Up = 0b0000001,
        Right = 0b0000010,
        Down = 0b0000100,
        Left = 0b0001000,
    }
}