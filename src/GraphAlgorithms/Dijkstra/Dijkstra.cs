using System;
using System.Collections.Generic;

namespace GraphAlgorithms.Dijkstra
{
    /// <summary>
    /// 节点
    /// </summary>
    public class Node
    {
        private string _name = "";
        public string name
        {
            get
            {
                return _name;
            }
        }

        public Node(string nodeName)
        {
            this._name = nodeName;
        }
    }

    /// <summary>
    /// 图
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// 所有节点
        /// </summary>
        private HashSet<Node> _nodes = new HashSet<Node>();
        public List<Node> nodes
        {
            get
            {
                List<Node> ns = new List<Node>();
                foreach (Node node in _nodes)
                {
                    ns.Add(node);
                }
                return ns;
            }
        }

        /// <summary>
        /// 节点之间的路径长度
        /// 如: A-B 10 表示节点A到B之间的路径长度为10
        /// </summary>
        private Dictionary<string, int> edges = new Dictionary<string, int>();


        public void AddNode(Node node)
        {
            _nodes.Add(node);
            this.AddEdge(node, node, 0);
        }

        public bool ContainsNode(Node node)
        {
            return _nodes.Contains(node);
        }

        public Node GetNode(string nodeName)
        {
            foreach (Node node in _nodes)
            {
                if (node.name == nodeName)
                {
                    return node;
                }
            }

            return null;
        }

        public void AddEdge(Node n1, Node n2, int value)
        {
            if (!ContainsNode(n1))
            {
                AddNode(n1);
            }

            if (!ContainsNode(n2))
            {
                AddNode(n2);
            }

            string edge = GetEdgeName(n1, n2);
            edges[edge] = value;
        }

        public int GetEdgeValue(Node n1, Node n2)
        {
            string edgeName = GetEdgeName(n1, n2);
            if (edges.ContainsKey(edgeName))
            {
                return edges[edgeName];
            }
            else
            {
                return int.MaxValue;
            }
        }

        public string GetEdgeName(Node n1, Node n2)
        {
            return string.Format("{0}-{1}", n1.name, n2.name);
        }
    }

    /// <summary>
    /// 节点之间的路径
    /// </summary>
    public class NodePath
    {
        public Node src = null;
        public Node dst = null;
        public List<Node> path = new List<Node>();
        public int value = 0;

        public NodePath(Node src, Node dst)
        {
            this.src = src;
            this.dst = dst;
        }

        public override string ToString()
        {
            string strPath = "";
            strPath += src.name;
            foreach (Node nd in path)
            {
                strPath += "-" + nd.name;
            }
            strPath += "-" + dst.name;

            return string.Format("{0}-{1} {2} : {3}",
                src.name, dst.name, value, strPath);
        }
    }

    /// <summary>
    /// Dijkstra最短路径算法
    /// </summary>
    public partial class Dijkstra
    {
        /// <summary>
        /// 执行算法
        /// 在图graph中,查询从src节点到其它所有节点的最短路径
        /// </summary>
        /// <param name="graph">图</param>
        /// <param name="src">源节点</param>
        /// <returns>源节点到其它节点的最短路径</returns>
        public static List<NodePath> Do(Graph graph, Node src)
        {
            if (!graph.ContainsNode(src))
            {
                return null;
            }

            List<NodePath> sList = new List<NodePath>();
            List<NodePath> uList = new List<NodePath>();

            //
            List<Node> nodes = graph.nodes;
            int nodeCnt = nodes.Count;
            foreach (Node item in nodes)
            {
                NodePath nodePath = new NodePath(src, item);
                nodePath.value = graph.GetEdgeValue(src, item);

                if (item == src)
                {
                    sList.Add(nodePath);
                }
                else
                {
                    uList.Add(nodePath);
                }
            }

            //
            for (int i = 1; i < nodeCnt; ++i)
            {
                // 排序uList,将最小值取出到sList中
                uList.Sort(
                    delegate(NodePath a, NodePath b)
                    {
                        return a.value.CompareTo(b.value);
                    }
                );

                NodePath min = uList[0];
                uList.RemoveAt(0);
                sList.Add(min);

                // 更新uList中的路径值
                for (int k = 0; k < uList.Count; ++k)
                {
                    if (min.value == int.MaxValue)
                    {
                        continue;
                    }

                    int value = graph.GetEdgeValue(min.dst, uList[k].dst);
                    if (value == int.MaxValue)
                    {
                        continue;
                    }

                    if (value + min.value < uList[k].value)
                    {
                        uList[k].value = value + min.value;
                        uList[k].path.Clear();
                        uList[k].path.AddRange(min.path);
                        uList[k].path.Add(min.dst);
                    }
                }
            }

            return sList;
        }
    }
}
