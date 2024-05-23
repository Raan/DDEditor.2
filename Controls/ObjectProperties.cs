using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DivEditor.Controls
{
    public partial class ObjectProperties : Form
    {
        private int[] Prop;
        private int SID;
        public ObjectProperties(int[] prop, int SelObj)
        {
            InitializeComponent();
            Prop = prop;
            SID = GameData.objects[SelObj].SpriteID;
            textBox1.Text = "";
            //System.Diagnostics.Debug.WriteLine(Prop.Length);
            //for (int i = 0; i < Prop.Length; i++)
            //{
            //    var hexid = $"0x{Prop[i]:X8}";
            //    //textBox1.Text += hexid + "\r\n";
            //    //textBox1.Text += ToBinary(Prop[i]) + " ";
            //    textBox1.Text += Convert.ToString(Prop[i], 2).PadLeft(16, '0') + " ";

            //}
            //textBox1.Text += SID;
            //textBox1.Text += "\r\n";

            for (int i = 0; i < GameData.objects.Count; i++) 
            { 
                if (GameData.objects[i].SpriteID == SID) 
                {
                    textBox1.Text += Convert.ToString(GameData.objects[i].Var_0, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_0.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_1, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_1.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_2, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_2.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_3, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_3.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_4, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_4.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_5, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_5.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_6, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_6.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_7, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_7.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_8, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_8.ToString().PadLeft(6, '0') + ") " +
                            Convert.ToString(GameData.objects[i].Var_9, 2).PadLeft(16, '0') + " (" + GameData.objects[i].Var_9.ToString().PadLeft(6, '0') + ") " +
                            GameData.objects[i].Var_10.ToString().PadLeft(2, '0') + " " + i;
                    textBox1.Text += "\r\n";
                }
            }
        }

        private void ObjectProperties_Load(object sender, EventArgs e)
        {

        }
        public static string ToBinary(int myValue)
        {
            string binVal = Convert.ToString(myValue, 2);
            int bits = 0;
            int bitblock = 4;

            for (int i = 0; i < binVal.Length; i = i + bitblock)
            { bits += bitblock; }

            return binVal.PadLeft(bits, '0');
        }
    }
}
