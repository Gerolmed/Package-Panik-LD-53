using System.Collections.Generic;

namespace LudumDare.Utils
{

    public static class CluserUtil
    {


        public delegate float Distance<T>(T item1, T item2);

        public delegate float ClusterFitness<T>(ICollection<T> cluster);

        public static ICollection<T> FindFirstCluster<T>(
            IReadOnlyList<T> items,
            Distance<T> distance,
            ClusterFitness<T> fitness,
            float fitnessTreshhold)
        {

            var connections = new LinkedList<(T, T, float)>();
            for (int from = 0; from < items.Count; ++from)
            {
                for (int to = from + 1; to < items.Count; ++to)
                {
                    var dist = distance(items[from], items[to]);

                    var current = connections.First;
                    if (current == null)
                    {
                        connections.AddFirst((items[from], items[to], dist));
                        continue;
                    }

                    while (current.Value.Item3 < dist && current.Next != null)
                    {
                        current = current.Next;
                    }

                    connections.AddBefore(current, new LinkedListNode<(T, T, float)>((items[from], items[to], dist)));
                }
            }

            var clusterMapping = new Dictionary<T, List<T>>();
            foreach (var item in items)
            {
                var cluster = new List<T>() { item };
                if (fitness(cluster) > fitnessTreshhold) return cluster;

                clusterMapping[item] = cluster;
            }

            var currentConn = connections.First;
            while (currentConn != null)
            {
                var (from, to, dist) = currentConn.Value;
                
                var fromCluster = clusterMapping[from];
                fromCluster.AddRange(clusterMapping[to]);
                if (fitness(fromCluster) > fitnessTreshhold) return fromCluster;

                currentConn = currentConn.Next;
            }

            return null;
        }

    }

}