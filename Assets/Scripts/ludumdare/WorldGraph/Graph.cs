﻿using System;
using System.Collections.Generic;
using UnityEngine;
using LudumDare.Utils;

namespace LudumDare.WorldGraph
{
    public class Graph<T>
    {

        private int _idCounter = 0;

        private int _leftBound = 0;
        private int _rightBound = 0;
        private int _topBound = 0;
        private int _botBound = 0;

        public Dictionary<Vector2Int, Node<T>> NodeGraph { get; } = new();


        public SpatialAStar<NavNode<T>, NavUser> ToAstar() {
            var fillerNode = new FillerNode<T>();
            var (gWidth, gHeight) = (_rightBound - _leftBound + 1, _topBound - _botBound + 1);
            var grid = new NavNode<T>[gWidth, gHeight];
            for (var x = 0; x < gWidth; ++x) {
                for (var y = 0; y < gHeight; ++y) {
                    grid[x, y] = fillerNode;
                }
            }

            foreach (var pos in NodeGraph.Keys) {
                // Debug.Log(pos.x + ", " + pos.y);
                grid[pos.x - _leftBound, pos.y - _botBound] = NodeGraph[pos];
            }

            var astar = new SpatialAStar<NavNode<T>, NavUser>(grid);
            return astar;
        }

        public Vector2Int GetRelativePos(Vector2Int pos) {
            return new Vector2Int(pos.x - _leftBound, pos.y - _botBound);
        }

        public Node<T> AddNodeAt(Vector2Int pos, T data,
            DirectionMask directions = DirectionMask.None)
        {
            if(pos.x < _leftBound) _leftBound = pos.x;
            if(pos.x > _rightBound) _rightBound = pos.x;
            if(pos.y < _botBound) _botBound = pos.y;
            if(pos.y > _topBound) _topBound = pos.y;   

            var node = new Node<T>(pos, data, directions, _idCounter++);

            NodeGraph.Add(pos, node);
            UpdateLinks(node);

            return node;
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
            return NodeGraph.TryGetValue(pos, out node);
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