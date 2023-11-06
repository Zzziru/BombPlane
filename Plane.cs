using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BombPlane
{
    internal class Plane
    {
        public Point Plane_head;
        public Point[] PointSet;
        public Direction dir;
        public enum Direction
        {
            Up=0, Down=1,Left=2,Right=3//飞机机头朝向
        }
        public Plane(Point head,Direction dir)
        {
            Plane_head = head;
            this.dir = dir;
            int head_x = head.X;
            int head_y = head.Y;
            switch (dir)
            {
                case Direction.Up:
                    PointSet = new Point[10] {
                                new Point(head_x, head_y),//机头
                                new Point(head_x, head_y + 1),
                                new Point(head_x - 1, head_y + 1),
                                new Point(head_x + 1, head_y + 1),
                                new Point(head_x - 2, head_y + 1),
                                new Point(head_x + 2, head_y + 1),//机翼
                                new Point(head_x, head_y + 2),
                                new Point(head_x, head_y + 3),//连接
                                new Point(head_x + 1, head_y + 3),
                                new Point(head_x - 1, head_y + 3),//机尾
                            };
                    break;
                case Direction.Down:
                    PointSet = new Point[10] {
                                new Point(head_x, head_y),//机头
                                new Point(head_x, head_y - 1),
                                new Point(head_x - 1, head_y - 1),
                                new Point(head_x + 1, head_y - 1),
                                new Point(head_x - 2, head_y - 1),
                                new Point(head_x + 2, head_y - 1),//机翼
                                new Point(head_x, head_y - 2),
                                new Point(head_x, head_y - 3),//连接
                                new Point(head_x + 1, head_y - 3),
                                new Point(head_x - 1, head_y - 3),//机尾      
                            };
                    break;
                case Direction.Left:
                    PointSet = new Point[10] {
                            new Point(head_x, head_y),
                            new Point(head_x + 1, head_y),
                            new Point(head_x + 1, head_y + 1),
                            new Point(head_x + 1, head_y - 1),
                            new Point(head_x + 1, head_y + 2),
                            new Point(head_x + 1, head_y - 2),
                            new Point(head_x + 2, head_y),
                            new Point(head_x + 3, head_y),
                            new Point(head_x + 3, head_y + 1),
                            new Point(head_x + 3, head_y - 1),
                            };
                    break;
                case Direction.Right:
                    PointSet = new Point[10] {
                            new Point(head_x, head_y),
                            new Point(head_x - 1, head_y),
                            new Point(head_x - 1, head_y + 1),
                            new Point(head_x - 1, head_y - 1),
                            new Point(head_x - 1, head_y + 2),
                            new Point(head_x - 1, head_y - 2),
                            new Point(head_x - 2, head_y),
                            new Point(head_x - 3, head_y),
                            new Point(head_x - 3, head_y + 1),
                            new Point(head_x - 3, head_y - 1),
                            };
                    break;
                default:
                    throw new Exception("Direction Unexpected");
            }
        }

    }
}
