using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombPlane
{
    internal class Board
    {
        public int PlaneNum = 0;
        public static int[,] board = new int[11, 11];//生成一个10*10的二维数组，下表从1开始
        private HashSet<Point> points = new HashSet<Point>();
        public Board() 
        { 
            for(int i=0;i<11;i++)
            {
                for(int j=0;j<11;j++)
                {
                    board[i, j] = 0;
                }
            }//初始化board数组
        }
        public void AddAPlane(Plane plane)//添加一架飞机
        {
            try
            {
                foreach (System.Drawing.Point p in plane.PointSet)
                {
                    if (p.X <= 0 || p.X > 10 || p.Y <= 0 || p.Y > 10)
                    {
                        
                        throw new PlaneLocationIllegal("飞机超出边界")
                        {
                            Target = p
                        };
                    }//判断飞机是否超出边界
                    else if (board[p.X, p.Y] != 0 )
                    {
                        throw new PlaneLocationIllegal("飞机重叠")
                        {
                            Target = p
                        };
                    }//判断飞机是否重叠
                }
                foreach (System.Drawing.Point p in plane.PointSet)
                {
                    board[p.X, p.Y] = 1;
                    points.Add(p);
                }//如果飞机位置合法，将飞机坐标加入board数组
                PlaneNum++;//飞机数量加一
                board[plane.Plane_head.X, plane.Plane_head.Y] = 2;//将飞机头坐标加入board数组,2代表机头
            }
            catch(PlaneLocationIllegal e)
            {
                //后续加入错误处理，重新摆放该飞机,可能需要把异常处理提到更外层去处理
                Console.WriteLine(e.Message);
                Console.WriteLine("错误坐标：" + e.Target);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }   
        }
        public void RemoveAPlane(Plane plane)//移除一架飞机
        {
            foreach (System.Drawing.Point p in plane.PointSet)
            {
                board[p.X, p.Y] = 0;
                points.Remove(p);
            }//将飞机坐标从board数组中移除
            PlaneNum--;//飞机数量减一
        }
    }

    public class PlaneLocationIllegal : Exception
    {
        public PlaneLocationIllegal() { }

        public PlaneLocationIllegal(string message) : base(message) { }

        public PlaneLocationIllegal(string message, Exception innerException) : base(message, innerException) { }

        public Point Target { get; set; } // 自定义属性，用于存储错误位置
    }
}
