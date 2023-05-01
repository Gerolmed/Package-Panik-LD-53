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
using LudumDare.Satisfaction;
using LudumDare.TimeControl;


namespace LudumDare.Delivery
{

    public class DeliveryResolver : MonoBehaviour
    {


        [SerializeField]
        private DeliveryUnitStorageSocket unitStorage;

        [SerializeField]
        private WarehouseManagerSocket warehouseManager;

        [SerializeField]
        private SatisfactionManagerSocket satisfaction;

        [SerializeField]
        private TimeControlManagerSocket timeCtl;

        [SerializeField]
        private UnitNavigator unitNavigator;

        private List<DeliveryCommand> _commands = new();

        private Graph<NodeData> _graph;
        private SpatialAStar<NavNode<NodeData>, NavUser> _map;


        public void ExecuteDelivery(IEnumerable<DeliveryCommand> batch)
        {
            _commands.AddRange(batch);

            if (_map == null)
                return;

            DispatchAllUnits();
        }


        public void DispatchAllUnits()
        {
            var storage = unitStorage.Instance;
            if (storage == null)
                return;

            /*
            var units =  storage.GetAvailableUnits();
            foreach (var unit in units) {
                if (unit.Occupied)
                    continue;

                var commands = _commands.FindAll(cmd => cmd.DeliveryType == unit.Type.DeliveryType).Take(100).ToList();
                var cluster = CluserUtil.FindFirstCluster<DeliveryCommand>(commands, Distance, (cluster) => cluster.Count, 2);
                if(cluster == null || cluster.Count == 0) continue;
                foreach (var cmd in cluster) {
                    _commands.RemoveAll((cmd) => cluster.Contains(cmd));
                }

                DispatchUnit(cluster.Select(cmd => cmd.Pos).ToList(), unit);
            }
            */

            var proceessedCommands = new List<DeliveryCommand>();

            var mailCommands = _commands.FindAll(cmd => cmd.DeliveryType == DeliveryType.Mail).Take(100).ToList();
            var mailUnits = storage.GetAvailableUnits().Where((unit) => (unit.Type.DeliveryType == DeliveryType.Mail && !unit.Occupied));
            Dictionary<DeliveryCommand, List<DeliveryCommand>> clusterStorage = null;
            BTreeNode<DeliveryCommand> connections = null;
            List<DeliveryCommand> cluster = null;
            foreach (var unit in mailUnits)
            {
                (cluster, clusterStorage, connections) = CluserUtil.FindFirstCluster<DeliveryCommand>(mailCommands, Distance, (cluster) => cluster.Count, 2, clusterStorage, connections);

                if(cluster == null || cluster.Count == 0) continue;
                DispatchUnit(cluster.Select(cmd => cmd.Pos).ToList(), unit);

                proceessedCommands.AddRange(cluster);
                cluster.Clear();

                if(connections == null) break;
            }

            var packageCommands = _commands.FindAll(cmd => cmd.DeliveryType == DeliveryType.Package).Take(100).ToList();
            var packageUnits = storage.GetAvailableUnits().Where((unit) => (unit.Type.DeliveryType == DeliveryType.Package && !unit.Occupied));
            (clusterStorage, connections, cluster) = (null, null, null);
            foreach (var unit in packageUnits)
            {
                (cluster, clusterStorage, connections) = CluserUtil.FindFirstCluster<DeliveryCommand>(mailCommands, Distance, (cluster) => cluster.Count, 2, clusterStorage, connections);

                if(cluster == null || cluster.Count == 0) continue;
                DispatchUnit(cluster.Select(cmd => cmd.Pos).ToList(), unit);

                proceessedCommands.AddRange(cluster);
                cluster.Clear();
                
                if(connections == null) break;
            }
            
            var droneCommands = _commands.FindAll(cmd => cmd.DeliveryType == DeliveryType.DronePackage).Take(100).ToList();
            var droneeUnits = storage.GetAvailableUnits().Where((unit) => (unit.Type.DeliveryType == DeliveryType.DronePackage && !unit.Occupied));
            (clusterStorage, connections, cluster) = (null, null, null);
            foreach (var unit in packageUnits)
            {
                (cluster, clusterStorage, connections) = CluserUtil.FindFirstCluster<DeliveryCommand>(mailCommands, Distance, (cluster) => cluster.Count, 2, clusterStorage, connections);

                if(cluster == null || cluster.Count == 0) continue;
                DispatchUnit(cluster.Select(cmd => cmd.Pos).ToList(), unit);

                proceessedCommands.AddRange(cluster);
                cluster.Clear();
                
                if(connections == null) break;
            }

            _commands.RemoveAll((cmd) => proceessedCommands.Contains(cmd));

            var cycle = timeCtl.Instance.Cycle;
            var expiredCommands = _commands.Where(cmd => (cycle - cmd.Cycle > timeCtl.Instance.CyclesPerDay)).ToList();
            foreach (var exCmd in expiredCommands) {
                satisfaction.Instance.Deduct(5);
                _commands.Remove(exCmd);
            }

            Debug.Log("Packets in queue: " + _commands.Count);
        }


        private float Distance(DeliveryCommand cmd1, DeliveryCommand cmd2)
        {
            /*
            // TODO: fix user
            var steps = _map.Search(_graph.GetRelativePos(cmd1.Pos), _graph.GetRelativePos(cmd2.Pos), new GroundUnitUser());
            if (steps == null) return float.MaxValue;

            return steps.Count;
            */
            return (cmd1.Pos.x - cmd2.Pos.x) * (cmd1.Pos.x - cmd2.Pos.x) + (cmd1.Pos.y - cmd2.Pos.y) * (cmd1.Pos.y - cmd2.Pos.y);
        }

        private IReadOnlyList<PathNode> BuildPath(List<Vector2Int> commands, IWarehouse warehouse, NavUser user)
        {
            var path = new List<PathNode>();
            path.Add(new PathNode() { Pos = warehouse.GetPosition() });

            foreach (var cmd in commands)
            {
                MoveToDestintion(cmd, path, user);
            }

            MoveToDestintion(warehouse.GetPosition(), path, user);
            return path;
        }

        private void MoveToDestintion(Vector2Int dest, List<PathNode> path, NavUser user)
        {
            var current = _graph.GetRelativePos(path.Last().Pos);
            var relDest = _graph.GetRelativePos(dest);
            var steps = _map.Search(current, relDest, user);
            if (steps == null) return;

            foreach (var node in steps)
            {
                if (node is Node<NodeData>)
                    path.Add(new PathNode() { Pos = (node as Node<NodeData>).Pos });
            }
        }

        private void DispatchUnit(List<Vector2Int> positions, IUnitInstance unit)
        {
            unit.Occupied = true;

            IWarehouse nearest = null;
            var distance = 0;
            foreach (var current in warehouseManager.Instance.GetAll()) {
                var currentDistance = 
                    (current.GetPosition().x - positions[0].x) * (current.GetPosition().x - positions[0].x) + 
                    (current.GetPosition().y - positions[0].y) + (current.GetPosition().y - positions[0].y);
                
                if (nearest == null || currentDistance < distance) {
                    nearest = current;
                    distance = currentDistance;
                }
            }

            var path = BuildPath(positions, nearest, unit.Type.NavUser);
            StartCoroutine(DispatchUnitInternal(path, unit));
        }

        private IEnumerator DispatchUnitInternal(IReadOnlyList<PathNode> path, IUnitInstance unit)
        {
            var renderInstance = Instantiate(unitNavigator);
            renderInstance.Unit = unit.Type;
            yield return renderInstance.StartTraversal(path);
            unit.Occupied = false;
            Destroy(renderInstance.gameObject);

            // DispatchAllUnits();
        }

        public void AssignGraph(Graph<NodeData> graph)
        {
            _map = graph.ToAstar();
            _graph = graph;
        }


    }

}