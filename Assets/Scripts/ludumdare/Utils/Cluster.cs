using System.Collections.Generic;

namespace LudumDare.Utils
{

    public static class CluserUtil
    {


        public delegate float Distance<T>(T item1, T item2);

        public delegate float ClusterFitness<T>(ICollection<T> cluster);

        public static List<T> FindFirstCluster<T>(
            IReadOnlyList<T> items,
            Distance<T> distance,
            ClusterFitness<T> fitness,
            float fitnessTreshhold)
        {

            BTreeNode<T> connections =  null; // new LinkedList<(T, T, float)>();
            for (int from = 0; from < items.Count; ++from)
            {
                for (int to = from + 1; to < items.Count; ++to)
                {
                    var dist = distance(items[from], items[to]);

                    if (connections == null) {
                        connections = new BTreeNode<T>((items[from], items[to], dist));
                    } else {
                        connections.Insert((items[from], items[to], dist));
                    }
                }
            }

            var clusterMapping = new Dictionary<T, List<T>>();
            foreach (var item in items)
            {
                var cluster = new List<T>() { item };
                if (fitness(cluster) > fitnessTreshhold) return cluster;

                clusterMapping[item] = cluster;
            }

            if(connections == null) return null;
            var (currentRoot, smallest) = connections.GetAndRemoveSmallest();
            while (smallest != null)
            {
                var (from, to, dist) = smallest.Value;
                
                var fromCluster = clusterMapping[from];
                fromCluster.AddRange(clusterMapping[to]);
                clusterMapping[to] = fromCluster;
                if (fitness(fromCluster) > fitnessTreshhold) return fromCluster;

                if (currentRoot != null)
                    (currentRoot, smallest) = currentRoot.GetAndRemoveSmallest();
                else
                    smallest = null;
            }

            return null;
        }

    }

}