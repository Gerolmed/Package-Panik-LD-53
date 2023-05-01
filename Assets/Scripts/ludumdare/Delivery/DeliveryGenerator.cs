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
                
                var targets = DistributeOnto(possibleTargets, 5);
                resolver.ExecuteDelivery(targets.Select(target => new DeliveryCommand(target, DeliveryType.Mail)));

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