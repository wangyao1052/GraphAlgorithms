using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms.PackageWrap_JarvisMarch
{
    public partial class PackageWrap
    {
        public static void Test()
        {
            List<LitMath.Vector2> pnts = new List<LitMath.Vector2>();
            pnts.Add(new LitMath.Vector2(0, 0));
            pnts.Add(new LitMath.Vector2(10, 0));
            pnts.Add(new LitMath.Vector2(10, 10));
            pnts.Add(new LitMath.Vector2(0, 10));
            pnts.Add(new LitMath.Vector2(2, 2));
            pnts.Add(new LitMath.Vector2(5, 3));

            List<LitMath.Vector2> polygon = Do(pnts);

            foreach (LitMath.Vector2 pnt in polygon)
            {
                Console.WriteLine(pnt.ToString());
            }
        }
    }
}
