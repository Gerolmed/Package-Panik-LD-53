using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using LudumDare.Utils;
using LudumDare.Units;
using LudumDare.Units.Navigation;
using LudumDare.WorldGraph;
using LudumDare.WorldGraph.Warehouses;


namespace LudumDare.Delivery {

    public class DeliveryResolver: MonoBehaviour {


        [SerializeField]
        private DeliveryUnitStorageSocket unitStorage;

        [SerializeField]
        private WarehouseManagerSocket warehouseManager;

        [SerializeField]
        private UnitNavigator unitNavigator;

        private List<DeliveryCommand> _commands = new();

        private Graph<NodeData> _graph;
        private SpatialAStar<NavNode<NodeData>, NavUser> _map;


        public void ExecuteDelivery(List<DeliveryCommand> batch) {
             _commands.AddRange(batch);

            if (_map == null)
                return;

            var storage = unitStorage.Instance;
            if(storage == null) 
                return;

            var units =  storage.GetAvailableUnits();
            foreach (var unit in units) {
                if (unit.Occupied)
                    continue;

                var commands = _commands.FindAll(cmd => cmd.DeliveryType == unit.Type.DeliveryType);
                var cluster = CluserUtil.FindFirstCluster<DeliveryCommand>(commands, Distance, (cluster) => cluster.Count, 2);
                if(cluster == null || cluster.Count == 0) continue;
                foreach (var cmd in cluster) {
                    _commands.RemoveAll((cmd) => cluster.Contains(cmd));
                }

                DispatchUnit(cluster.Select(cmd => cmd.Pos).ToList(), unit);
            }
        }

        private float Distance(DeliveryCommand cmd1, DeliveryCommand cmd2) {
            /*
            // TODO: fix user
            var steps = _map.Search(_graph.GetRelativePos(cmd1.Pos), _graph.GetRelativePos(cmd2.Pos), new GroundUnitUser());
            if (steps == null) return float.MaxValue;

            return steps.Count;
            */
            return (cmd1.Pos.x - cmd2.Pos.x) * (cmd1.Pos.x - cmd2.Pos.x) + (cmd1.Pos.y - cmd2.Pos.y) * (cmd1.Pos.y - cmd2.Pos.y);
        }

        private IReadOnlyList<PathNode> BuildPath(List<Vector2Int> commands, NavUser user) {
            var path = new List<PathNode>();
            var warehouse = warehouseManager.Instance.GetAll().First();
            path.Add(new PathNode() {Pos = warehouse.GetPosition()});

            foreach (var cmd in commands) {
                MoveToDestintion(cmd, path, user);
            }

            MoveToDestintion(warehouse.GetPosition(), path, user);
            return path;
        }

        private void MoveToDestintion(Vector2Int dest, List<PathNode> path, NavUser user) {
            var current = _graph.GetRelativePos(path.Last().Pos);
            var relDest = _graph.GetRelativePos(dest);
            var steps = _map.Search(current, relDest, user);
            if(steps == null) return;
            
            foreach (var node in steps) {
                if(node is Node<NodeData>)
                    path.Add(new PathNode() {Pos = (node as Node<NodeData>).Pos});
            }
        }

        private void DispatchUnit(List<Vector2Int> positions, IUnitInstance unit) {
            unit.Occupied = true;
            var path = BuildPath(positions, unit.Type.NavUser);
            StartCoroutine(DispatchUnitInternal(path, unit));
        }

        private IEnumerator DispatchUnitInternal(IReadOnlyList<PathNode> path, IUnitInstance unit) {
            var renderInstance = Instantiate(unitNavigator);
            yield return renderInstance.StartTraversal(path);
            unit.Occupied = false;
            Destroy(renderInstance.gameObject);
        }    

        public void AssignGraph(Graph<NodeData> graph) {
            _map = graph.ToAstar();
            _graph = graph;
        }


    }

}