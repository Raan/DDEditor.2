using SharpDX.Direct2D1.Effects;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivEditor.Controls
{
    internal class Terrain // Палитра текстур
    {
        private String terrName = "";                       // Название текстуры 
        private String transition = "";                     // Смешиваемая текстура (если есть)
        private int system;                                 // Неизвестная переменная (0 или 1)
        private byte tileEffect;                            // Эффекты на плитке при наложении текстуры (туман, вода, помещение и т.д.) (если есть)
        private List<int[]> baseTile = new List<int[]>();   // Список полнотелых текстур
        private List<int[]>[] trns = new List<int[]>[16];   // Список переходных текстур (если есть)
        private static int count = 0;                       // Количествово текстур
        public Terrain()
        {
            count++;
            for (int i = 0; i < 16; i++) trns[i] = new List<int[]>();
        }
        public String TerrName
        {
            get
            {
                return terrName;
            }
            set
            {
                terrName = value;
            }
        }
        public byte TileEffect
        {
            get
            {
                return tileEffect;
            }
            set
            {
                tileEffect = value;
            }
        }
        public String Transition
        {
            get
            {
                return transition;
            }
            set
            {
                transition = value;
            }
        }
        public int System
        {
            get
            {
                return system;
            }
            set
            {
                system = value;
            }
        }
        public int BaseTileCount
        {
            get
            {
                return baseTile.Count;
            }
        }
        public void addTrns(int num, int trn, int cind)
        {
            trns[num].Add(new int[] {trn, cind});
        }
        public void addBaseTile(int bas, int cind)
        {
            baseTile.Add(new int[] { bas, cind });
        }
        public int getTrnsCount(int num)
        {
            if (num < trns.Length)
            {
                return trns[num].Count;
            }
            else return 0;
        }
        public int getTrnsUp(int num, int trn)
        {
            if (trns[num].Count > 0)
            {
                return trns[num][trn][1];
            }
            else return 0;
        }
        public int getTrnsDown(int num, int trn)
        {
            if (trns[num].Count > 0)
            {
                return trns[num][trn][0];
            }
            else return 0;
        }
        public int getBaseTileUp(int num)
        {
            if (baseTile[num].Length > 0)
            {
                return baseTile[num][1];
            }
            else return 0;
        }
        public int getBaseTileDown(int num)
        {
            if (baseTile[num].Length > 0)
            {
                return baseTile[num][0];
            }
            else return 0;
        }
        public static int TotalCount()
        {
            return count;
        }
    }
}
