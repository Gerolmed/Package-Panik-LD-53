using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace LudumDare.WorldGraph.TilemapGraph
{
    public class TilemapReader: MonoBehaviour
    {
        [SerializeField]
        private Tilemap tilemap;

        [SerializeField]
        private GraphEvent onGenerate;

        private void Start()
        {
            var graph = new Graph<NodeData>();
            var bounds = tilemap.cellBounds;

            for (var x = bounds.x; x < bounds.xMax; x++)
            {
                for (var y = bounds.y; y < bounds.yMax; y++)
                {
                    var tileBase = tilemap.GetTile(new Vector3Int(x, y, 0));
                    if(tileBase is not NodeTile nodeTile || nodeTile.DirectionMask  == DirectionMask.None) continue;
                    graph.AddNodeAt(new Vector2Int(x, y), new NodeData(), nodeTile.Walkable,
                        nodeTile.DirectionMask);
                }
            }
            
            onGenerate.Invoke(graph);
        }
    }
    
    [Serializable]
    public class GraphEvent: UnityEvent<Graph<NodeData>> {}
}