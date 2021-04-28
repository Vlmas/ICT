using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
    class Utils
    {
        public static void Step(int x, int y, int w, int h, Bitmap b, Color oc, Color fc, Queue<Point> q)
        {
            if(x >= 0 && x < w && y >= 0 && y < h)
            {
                if(b.GetPixel(x, y) == oc)
                {
                    b.SetPixel(x, y, fc);
                    q.Enqueue(new Point(x, y));
                }
            }
        }

        public static Bitmap Fill(Bitmap bitmap, Point originPoint, Color originColor, Color fillColor)
        {
            Point curPoint;
            Queue<Point> queue = new Queue<Point>();
            bitmap.SetPixel(originPoint.X, originPoint.Y, fillColor);
            queue.Enqueue(originPoint);

            while(queue.Count > 0)
            {
                curPoint = queue.Dequeue();
                Step(curPoint.X + 1, curPoint.Y, bitmap.Width, bitmap.Height, bitmap, originColor, fillColor, queue);
                Step(curPoint.X - 1, curPoint.Y, bitmap.Width, bitmap.Height, bitmap, originColor, fillColor, queue);
                Step(curPoint.X, curPoint.Y + 1, bitmap.Width, bitmap.Height, bitmap, originColor, fillColor, queue);
                Step(curPoint.X, curPoint.Y - 1, bitmap.Width, bitmap.Height, bitmap, originColor, fillColor, queue);
            }

            return bitmap;
        }
    }
}
