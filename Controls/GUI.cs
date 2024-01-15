using MonoGame.Forms.NET.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace DivEditor.Controls
{
    internal class GUI
    {
        private Texture2D? textures;
        int Xcor, Ycor, Heigth, Width;
        bool active = true;
        bool pressed = false;
        public GUI(Texture2D? text, int xcor, int ycor, int heigth, int width, bool act) 
        { 
            this.textures = text;
            this.Xcor = xcor;
            this.Ycor = ycor;
            this.Heigth = heigth;
            this.Width = width;
            this.active = act;
        }
        public bool Click(int xcor, int ycor)
        {
            if (active && !pressed)
            {
                if (xcor >= this.Xcor && (xcor <= this.Xcor + this.Width) && ycor >= this.Ycor && (ycor <= this.Ycor + this.Heigth))
                {
                    pressed = true;
                    return true;
                }
                else
                {
                    pressed = false;
                    return false;
                }
            }
            else
            {
                pressed = false;
                return false;
            }
        }
    }
}
