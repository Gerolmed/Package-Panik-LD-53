using System.Collections.Generic;
using System.Linq;
using LudumDare.Districts;
using LudumDare.TimeControl;
using LudumDare.Units;
using LudumDare.Utils;
using LudumDare.WorldGraph;
using UnityEngine;

namespace LudumDare.Delivery
{
    
    public class DeliveryGenerator: MonoBehaviour
    {
        [SerializeField]
        private TimeControlManagerSocket controlManagerSocket;
        [SerializeField]
        private DistrictManagerSocket districtManagerSocket;

        [SerializeField]
        private DeliveryResolver resolver;

        private HashSet<Vector2Int> _targets = new();
        private Dictionary<string, List<Vector2Int>> _targetCache = new();

        private RandomMathFunction _packetDistributionFunction = new RandomMathFunction();

        public void OnGraph(Graph<NodeData> graph)
        {
            _targets = graph.NodeGraph
                .Where(keyValuePair => keyValuePair.Value.Data.IsTarget)
                .Select(pair => pair.Key)
                .ToHashSet();
        }
        
        public void DoGenerate(long cycle)
        {
            
            foreach (var district in districtManagerSocket.Instance.GetUnlocked())
            {
                var possibleTargets = _targetCache.GetOrCreate(district.ID,
                    () => CollectAllFor(district));

                var cyclesPerDay = controlManagerSocket.Instance.CyclesPerDay;
                var age = cycle - (int) (cyclesPerDay * district.UnlockedSince / 60 / 24);
 
                var targets0 = DistributeOnto(possibleTargets, (int) _packetDistributionFunction.GetRandomValue(age) / 2);
                var targets1 = DistributeOnto(possibleTargets, (int) _packetDistributionFunction.GetRandomValue(age) / 2);
                var droneTargets = DistributeOnto(possibleTargets, (int) (_packetDistributionFunction.GetRandomValue(age) / 10 - 4));
                
                var commands = new List<DeliveryCommand>();
                // commands.AddRange(targets0.Select(target => new DeliveryCommand(target, DeliveryType.Mail, cycle)));
                commands.AddRange(targets1.Select(target => new DeliveryCommand(target, DeliveryType.Package, cycle)));
                commands.AddRange(droneTargets.Select(target => new DeliveryCommand(target, DeliveryType.DronePackage, cycle)));

                resolver.ExecuteDelivery(commands);
            }
        }


        private List<Vector2Int> DistributeOnto(List<Vector2Int> targets, int amount)
        {
            if (amount <= 0 || targets.Count == 0) return new List<Vector2Int>();
            if (amount >= targets.Count)
            {
                var childResults = DistributeOnto(targets, amount - targets.Count);
                childResults.AddRange(targets);
                return childResults;
            }
            
            var results = new List<Vector2Int>();
            var possibleTargets = new List<Vector2Int>(targets);

            for (var i = 0; i < amount; i++)
            {
                var index = Random.Range(0, possibleTargets.Count);
                results.Add(possibleTargets[index]);
                possibleTargets.RemoveAt(index);
            }
            
            return results;
        }


        private List<Vector2Int> CollectAllFor(District district)
        {
            return _targets.Where(district.Contains).ToList();
        }
    }
}
