using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LudumDare.Utils;
using LudumDare.Units;
using LudumDare.Units.Navigation;
using LudumDare.WorldGraph;


namespace LudumDare.Delivery {

    public class DeliveryResolver: MonoBehaviour {


        [SerializeField]
        private DeliveryUnitStorageSocket unitStorage;

        [SerializeField]
        private UnitNavigator unitNavigator;

        private List<DeliveryCommand> _commands = new();

        private SpatialAStar<Node<NodeData>, NavUser> _map;


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

        private IReadOnlyList<PathNode> BuildPath(List<DeliveryCommand> commands) {
            
        }

        private void DispatchUnit(List<DeliveryCommand> commands, IUnitInstance unit) {
            unit.Occupied = true;
            var path = BuildPath(commands);
            StartCoroutine(DispatchUnitInternal(path, unit));
        }

        private IEnumerator DispatchUnitInternal(IReadOnlyList<PathNode> path, IUnitInstance unit) {
            var renderInstance = Instantiate(unitNavigator);
            yield return renderInstance.StartTraversal(path);
            unit.Occupied = false;
        }    

        public void AssignGraph(Graph<NodeData> graph) {
            _map = graph.ToAstar();
        }


    }

}