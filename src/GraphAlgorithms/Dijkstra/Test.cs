using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms.Dijkstra
{
    public partial class Dijkstra
    {
        public static void Test()
        {
            Graph graph = new Graph();

            Node nodeA = new Node("A");
            Node nodeB = new Node("B");
            Node nodeC = new Node("C");
            Node nodeD = new Node("D");
            Node nodeE = new Node("E");
            Node nodeF = new Node("F");
            Node nodeG = new Node("G");

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddNode(nodeD);
            graph.AddNode(nodeE);
            graph.AddNode(nodeF);
            graph.AddNode(nodeG);

            graph.AddEdge(nodeA, nodeB, 12);
            graph.AddEdge(nodeA, nodeF, 16);
            graph.AddEdge(nodeA, nodeG, 14);

            graph.AddEdge(nodeB, nodeA, 12);
            graph.AddEdge(nodeB, nodeC, 10);
            graph.AddEdge(nodeB, nodeF, 7);

            graph.AddEdge(nodeC, nodeB, 10);
            graph.AddEdge(nodeC, nodeD, 3);
            graph.AddEdge(nodeC, nodeE, 5);
            graph.AddEdge(nodeC, nodeF, 6);

            graph.AddEdge(nodeD, nodeC, 3);
            graph.AddEdge(nodeD, nodeE, 4);

            graph.AddEdge(nodeE, nodeC, 5);
            graph.AddEdge(nodeE, nodeD, 4);
            graph.AddEdge(nodeE, nodeF, 2);
            graph.AddEdge(nodeE, nodeG, 8);

            graph.AddEdge(nodeF, nodeA, 16);
            graph.AddEdge(nodeF, nodeB, 7);
            graph.AddEdge(nodeF, nodeC, 6);
            graph.AddEdge(nodeF, nodeE, 2);
            graph.AddEdge(nodeF, nodeG, 9);

            graph.AddEdge(nodeG, nodeA, 14);
            graph.AddEdge(nodeG, nodeE, 8);
            graph.AddEdge(nodeG, nodeF, 9);

            List<NodePath> nodePaths = Dijkstra.Do(graph, nodeD);
            if (nodePaths != null)
            {
                foreach (NodePath np in nodePaths)
                {
                    Console.WriteLine(np.ToString());
                }
            }
        }
    }
}
