﻿
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare.WorldGraph.TilemapGraph
{
    public class NodeTile: Tile
    {
        [SerializeField]
        private DirectionMask directionMask;

        [SerializeField]
        private bool walkable = false;

        public DirectionMask DirectionMask => directionMask;

        public bool Walkable => walkable;

#if UNITY_EDITOR
     
        // The following is a helper that adds a menu item to create a RoadTile Asset
        [MenuItem("Assets/Create/NodeTile")]
        public static void CreateRoadTile()
        {
            var path = EditorUtility.SaveFilePanelInProject("Save Node Tile", "New Node Tile", "asset", "Save Node Tile", "Assets");
            if (path == "")
                return;
            AssetDatabase.CreateAsset(CreateInstance<NodeTile>(), path);
        }
#endif
    }
}