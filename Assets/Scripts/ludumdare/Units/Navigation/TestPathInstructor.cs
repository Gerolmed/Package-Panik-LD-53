using System;
using UnityEngine;

namespace LudumDare.Units.Navigation
{
    public class TestPathInstructor: MonoBehaviour
    {
        private void Start()
        {
            var navigator = GetComponent<UnitNavigator>();
            navigator.StartTraversal(new []
            {
                new PathNode() {Pos = new Vector2Int(2, -1)},
                new PathNode() {Pos = new Vector2Int(2, 0)},
                new PathNode() {Pos = new Vector2Int(2, 1)},
                new PathNode() {Pos = new Vector2Int(2, 3)},
                new PathNode() {Pos = new Vector2Int(2, 4)},
                new PathNode() {Pos = new Vector2Int(3, 4)},
                new PathNode() {Pos = new Vector2Int(4, 4)},
                new PathNode() {Pos = new Vector2Int(5, 4)},
                new PathNode() {Pos = new Vector2Int(5, 3)},
                new PathNode() {Pos = new Vector2Int(5, 2)},
                new PathNode() {Pos = new Vector2Int(4, 2)},
            });
        }
    }
}