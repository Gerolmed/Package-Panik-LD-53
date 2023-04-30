using UnityEngine;
using System;
using System.Collections;
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
                var command = _commands.Pop(command => command.DeliveryType == unit.Type.DeliveryType);
                if(command == null) continue;
                var commandList = new List<DeliveryCommand>();
                commandList.Add(command);
                DispatchUnit(new List<DeliveryCommand>(commandList), unit);
            }
        }

        private IReadOnlyList<PathNode> BuildPath(List<DeliveryCommand> commands, NavUser user) {
            var path = new List<PathNode>();
            var warehouse = warehouseManager.Instance.GetAll().GetEnumerator().Current;
            path.Add(new PathNode() {Pos = warehouse.GetPosition()});

            foreach (var cmd in commands) {
                MoveToDestintion(cmd.Pos, path, user);
            }

            MoveToDestintion(warehouse.GetPosition(), path, user);
            return path;
        }

        private void MoveToDestintion(Vector2Int dest, List<PathNode> path, NavUser user) {
            var current = _graph.GetRelativePos(path[-1].Pos);
            var relDest = _graph.GetRelativePos(dest);
            var steps = _map.Search(current, relDest, user);
            if(steps == null) return;
            
            foreach (var node in steps) {
                if(node is Node<NodeData>)
                    path.Add(new PathNode() {Pos = (node as Node<NodeData>).Pos});
            }
        }

        private void DispatchUnit(List<DeliveryCommand> commands, IUnitInstance unit) {
            unit.Occupied = true;
            var path = BuildPath(commands, unit.Type.NavUser);
            StartCoroutine(DispatchUnitInternal(path, unit));
        }

        private IEnumerator DispatchUnitInternal(IReadOnlyList<PathNode> path, IUnitInstance unit) {
            var renderInstance = Instantiate(unitNavigator);
            yield return renderInstance.StartTraversal(path);
            unit.Occupied = false;
        }    

        public void AssignGraph(Graph<NodeData> graph) {
            _map = graph.ToAstar();
            _graph = graph;
        }


    }

}