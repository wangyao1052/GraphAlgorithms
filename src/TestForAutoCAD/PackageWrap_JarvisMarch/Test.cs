using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace TestForAutoCAD.PackageWrap_JarvisMarch
{
    public class Test
    {
        [CommandMethod("GA_PackageWrap_JarvisMarch")]
        public static void Do()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // 范围矩形起点
            PromptPointResult ppr = doc.Editor.GetPoint("\n请指定范围矩形的起点: ");
            if (ppr.Status != PromptStatus.OK)
            {
                return;
            }
            Point3d boundRectPnt1 = ppr.Value;

            // 范围矩形终点
            ppr = doc.Editor.GetCorner("\n请指定范围矩形的终点: ", boundRectPnt1);
            if (ppr.Status != PromptStatus.OK)
            {
                return;
            }
            Point3d boundRectPnt2 = ppr.Value;

            // 生成随机点个数
            PromptIntegerOptions intOpts = new PromptIntegerOptions("\n请输入随机点个数(>=3): ");
            intOpts.AllowNegative = false;
            intOpts.LowerLimit = 3;
            PromptIntegerResult pir = doc.Editor.GetInteger(intOpts);
            if (pir.Status != PromptStatus.OK
                || pir.Value < 3)
            {
                return;
            }
            int pointCnt = pir.Value;

            // 产生随机点
            double minX = boundRectPnt1.X < boundRectPnt2.X ? boundRectPnt1.X : boundRectPnt2.X;
            double minY = boundRectPnt1.Y < boundRectPnt2.Y ? boundRectPnt1.Y : boundRectPnt2.Y;
            double maxX = boundRectPnt1.X > boundRectPnt2.X ? boundRectPnt1.X : boundRectPnt2.X;
            double maxY = boundRectPnt1.Y > boundRectPnt2.Y ? boundRectPnt1.Y : boundRectPnt2.Y;

            List<LitMath.Vector2> pnts = new List<LitMath.Vector2>();
            Random random = new Random();
            for (int i = 0; i < pointCnt; ++i)
            {
                double x = random.NextDouble() * (maxX - minX) + minX;
                double y = random.NextDouble() * (maxY - minY) + minY;
                pnts.Add(new LitMath.Vector2(x, y));
            }
            //pnts.Add(new LitMath.Vector2(100, 100));
            //pnts.Add(new LitMath.Vector2(200, 100));
            //pnts.Add(new LitMath.Vector2(50, 100));

            //pnts.Add(new LitMath.Vector2(100, 1000));
            //pnts.Add(new LitMath.Vector2(200, 1000));
            //pnts.Add(new LitMath.Vector2(50, 1000));
            
            // 在CAD中生成随机点
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTableRecord curSpace = tr.GetObject(
                    db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                foreach (LitMath.Vector2 pnt in pnts)
                {
                    DBPoint dbPnt = new DBPoint(new Point3d(pnt.x, pnt.y, 0));
                    curSpace.AppendEntity(dbPnt);
                    tr.AddNewlyCreatedDBObject(dbPnt, true);
                }

                tr.Commit();
            }

            // 调用算法算出最小外包多边形
            List<LitMath.Vector2> polygon = GraphAlgorithms.PackageWrap_JarvisMarch.PackageWrap.Do(pnts);

            // 在CAD中绘制出多边形
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTableRecord curSpace = tr.GetObject(
                    db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                Polyline pline = new Polyline();
                int index = 0;
                foreach (LitMath.Vector2 pnt in polygon)
                {
                    pline.AddVertexAt(index++, new Point2d(pnt.x, pnt.y), 0, 0, 0);
                }
                pline.Closed = true;

                curSpace.AppendEntity(pline);
                tr.AddNewlyCreatedDBObject(pline, true);
                tr.Commit();
            }
        }
    }
}
