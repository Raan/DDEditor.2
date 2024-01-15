using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivEditor.Controls
{
    internal class ObjectsDescriptions
    {
        //   A1|--------------------------|A2
        //     |                          |
        //     |                          |
        //     |                          |
        //     |                          |
        //     |                          |
        //     |                          |
        //     |                          |
        //     |              SP1 /-------|SP2
        //     |                /       / |
        //     |              /       /   |
        //     |            /       /     |
        //     |          /  TP   /       |
        //     |        /   .   /         |
        //     |      /       /           |
        //     |    /       /             |
        //     |  /       /               |
        //     |/SP0    /SP3              |
        //   A0|--------------------------|A3
        private string name;        // Имя объекта
        private int height;         // Высота спрайта
        private int width;          // Ширина спрайта
        private int var_0;
        private int var_1;
        private int var_2;
        private Point TP;           // Точка касания
        private Point SP0;
        private Point SP1;
        private Point SP2;
        private Point SP3;
        private int SXmin;
        private int SXmax;
        private int SYmin;
        private int SYmax;
        public string Name
        {
            get
            {
                return name;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
        }
        public int Width
        {
            get
            {
                return width;
            }
        }
        public Point TouchPoint
        {
            get
            {
                return TP;
            }
        }
        public int SXMin
        {
            get
            {
                return SXmin;
            }
        }
        public int SXMax
        {
            get
            {
                return SXmax;
            }
        }
        public int SYMin
        {
            get
            {
                return SYmin;
            }
        }
        public int SYMax
        {
            get
            {
                return SYmax;
            }
        }
        public Point Sp1
        {
            get
            {
                return SP1;
            }
        }
        public Point Sp3
        {
            get
            {
                return SP3;
            }
        }
        public  ObjectsDescriptions(string name, int hght, int wdth, Point touchPoint, int V0, int V1, int V2) 
        { 
            this.name = name;
            this.height = hght;
            this.width = wdth;
            this.var_0 = V0;
            this.var_1 = V1;
            this.var_2 = V2;
            this.TP = touchPoint;
            this.SP0.X = 0;
            this.SP0.Y = hght;
            this.SP2.Y = hght - (hght - touchPoint.Y) * 2;
            this.SP2.X = touchPoint.X * 2;
            this.SP3.Y = hght;
            this.SP3.X = touchPoint.X * 2 - (this.SP0.Y - this.SP2.Y);
            this.SP1.Y = this.SP2.Y;
            this.SP1.X = touchPoint.X * 2 - this.SP3.X;
            this.SXmin = SP1.X + SP1.Y;
            this.SYmin = SP1.Y;
            this.SXmax = SP3.X + SP3.Y;
            this.SYmax = SP3.Y;
        }
    }
}
