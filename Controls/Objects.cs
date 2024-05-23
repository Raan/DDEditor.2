using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivEditor.Controls
{
    internal class Objects
    {
        // File objects.x
        // var_0   var_1   var_2   var_3   var_4   var_5   var_6   var_7   var_8   var_9   X cor   Y cor   var_10  spriteID
        // 81 30 | 07 05 | 00 11 | 18 BD | 00 01 | 06 00 | 28 00 | 00 00 | 00 00 | 00 00 | D4 65 | 71 15 | 3E 00 | EB 0B
        private bool objectExists = true;           // Флаг существования объекта, если false то объект удален
        private int spriteID;                       // Имя
        private int height;                         // Высота объекта
        private int effect;                         // Эффект на плитке
        private Point absolutPpixelPosition;        // Позиция объекта в абсолютных координатах
        private Point tilePosition;                 // Позиция плитки, на которой находится объект
        private int var_0;
        private int var_1;
        private int var_2;
        private int var_3;
        private int var_4;
        private int var_5;
        private int var_6;
        private int var_7;
        private int var_8;
        private int var_9;
        private int var_10;                         // Высота объекта
        private static int count = 0;
        public bool Exists
        {
            get
            {
                return objectExists;
            }
        }
        public int SpriteID
        {
            get 
            { 
                return spriteID; 
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value >= 0 && value < Vars.maxObjectHeight)
                {
                    height = value;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(value + " вне допустимого диапазона");
                    height = 0;
                }
            }
        }
        public int Effect
        {
            get
            {
                return effect;
            }
            set
            {
                effect = value;
            }
        }
        public int Var_0
        {
            get { return var_0; }
            set { var_0 = value; }
        }
        public int Var_1
        {
            get { return var_1; }
            set { var_1 = value; }
        }
        public int Var_2
        {
            get { return var_2; }
            set { var_2 = value; }
        }
        public int Var_3
        {
            get { return var_3; }
            set { var_3 = value; }
        }
        public int Var_4
        {
            get { return var_4; }
            set { var_4 = value; }
        }
        public int Var_5
        {
            get { return var_5; }
            set { var_5 = value; }
        }
        public int Var_6
        {
            get { return var_6; }
            set { var_6 = value; }
        }
        public int Var_7
        {
            get { return var_7; }
            set { var_7 = value; }
        }
        public int Var_8
        {
            get { return var_8; }
            set { var_8 = value; }
        }
        public int Var_9
        {
            get { return var_9; }
            set { var_9 = value; }
        }
        public int Var_10
        {
            get { return var_10; }
            set { var_10 = value; }
        }
        public Point TilePosition
        {
            get
            {
                return tilePosition;
            }
            set
            {
                if (value.X >= 0 && value.X < Vars.maxHorizontalTails && value.Y >= 0 && value.Y < Vars.maxVerticalTails)
                {
                    tilePosition = value;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(value + " вне допустимого диапазона");
                }
            }
        }
        public Point AbsolutePixelPosition
        {
            get
            {
                return absolutPpixelPosition;
            }
            set
            {
                if (value.X >= 0 && value.X < Vars.tileSize * Vars.maxHorizontalTails && value.Y >= 0 && value.Y < Vars.tileSize * Vars.maxVerticalTails)
                {
                    absolutPpixelPosition = value;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(value + " вне допустимого диапазона");
                }
            }
        }
        public Point PixelPositionInTile
        {
            get
            {
                // Добавляем Vars.tileSize и единицу для того, чтобы избежать деления на ноль в крайних координатах
                return new Point((this.absolutPpixelPosition.X + Vars.tileSize) % ((this.tilePosition.X + 1) * Vars.tileSize),
                                 (this.absolutPpixelPosition.Y + Vars.tileSize) % ((this.tilePosition.Y + 1) * Vars.tileSize));
            }
        }
        public Objects(int v0, int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, Point PixelPos, int v10, int name)
        {
            this.spriteID = name;
            this.absolutPpixelPosition = PixelPos;
            this.var_0 = v0;
            this.var_1 = v1;
            this.var_2 = v2;
            this.var_3 = v3;
            this.var_4 = v4;
            this.var_5 = v5;
            this.var_6 = v6;
            this.var_7 = v7;
            this.var_8 = v8;
            this.var_9 = v9;
            this.var_10 = v10;
            if (name == 65535) this.objectExists = false;
            count++;
        }

        public void setTilePosition(int y, int x)
        {
            this.tilePosition.X = x;
            this.tilePosition.Y = y;
        }
        public void delObject()
        {
            this.objectExists = false;
        }
        public bool objectSate()
        {
            return this.objectExists;
        }
        public static int getObjectsCount()
        {
            return count;
        }
        public static void clearObjectsCount()
        {
            count = 0;
        }
    }
}
