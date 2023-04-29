#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare.WorldGraph.Tilemap
{
    public class NodeTile: Tile
    {
        
#if UNITY_EDITOR
     
        // The following is a helper that adds a menu item to create a RoadTile Asset
        [MenuItem("Assets/Create/NodeTile")]
        public static void CreateRoadTile()
        {
            var path = EditorUtility.SaveFilePanelInProject("Save Node Tile", "New Node Tile", "asset", "Save Node Tile", "Assets");
            if (path == "")
                return;
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<NodeTile>(), path);
        }
#endif
    }
}