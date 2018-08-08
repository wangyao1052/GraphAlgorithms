using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LitMath;

namespace GraphAlgorithms.PackageWrap_JarvisMarch
{
    public partial class PackageWrap
    {
        public static List<Vector2> Do(List<Vector2> pnts)
        {
            int count = pnts.Count;
            List<Vector2> polygon = new List<Vector2>();
            List<int> indexsPolygon = new List<int>();

            // 找出最左下方的点
            Vector2 pntLeftDown = pnts[0];
            int index = 0;
            for (int i = 1; i < count; ++i)
            {
                if (pnts[i].x <= pntLeftDown.x
                    && pnts[i].y <= pntLeftDown.y)
                {
                    index = i;
                    pntLeftDown = pnts[i];
                }
            }
            indexsPolygon.Add(index);

            // 
            int indexPre = -1;
            Vector2 vBase = new Vector2();
            while (true)
            {
                vBase = indexPre == -1 ?
                    new Vector2(1, 0) : pnts[index] - pnts[indexPre];
                double angle = Math.PI * 3;
                double angleTemp = 0.0;
                int indexToFind = -1;
                for (int i = 0; i < count; ++i)
                {
                    if (i == index || i == indexPre)
                    {
                        continue;
                    }
                    
                    angleTemp = Vector2.SignedAngleInRadian(
                        vBase, pnts[i] - pnts[index]);
                    if (angleTemp < 0)
                    {
                        angleTemp += Math.PI * 2;
                    }
                    if (angleTemp < angle)
                    {
                        angle = angleTemp;
                        indexToFind = i;
                    }
                }

                if (indexToFind == indexsPolygon[0])
                {
                    break;
                }
                else
                {
                    indexPre = index;
                    index = indexToFind;
                    indexsPolygon.Add(index);
                }
            }

            //
            foreach (int i in indexsPolygon)
            {
                polygon.Add(pnts[i]);
            }

            return polygon;
        }
    }
}
