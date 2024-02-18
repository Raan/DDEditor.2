using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivEditor.Controls
{
    public class Sprite
    {
        public int Heigth;
        public int Wigth;
        public int[]? Data;
        public Microsoft.Xna.Framework.Color[]? Color;

        //public Sprite(int H, int W, int[] D) 
        //{ 
        //    Heigth = H;
        //    Wigth = W;
        //    Data = D;
        //}
        public Sprite(int H, int W, Microsoft.Xna.Framework.Color[] C)
        {
            Heigth = H;
            Wigth = W;
            Color = C;
        }
    }
}
