using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphAlgorithms;
using GraphAlgorithms.Dijkstra;
using GraphAlgorithms.PackageWrap_JarvisMarch;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dijkstra
            GraphAlgorithms.Dijkstra.Dijkstra.Test();

            // PackageWrap_JarvisMarch
            GraphAlgorithms.PackageWrap_JarvisMarch.PackageWrap.Test();
        }
    }
}
