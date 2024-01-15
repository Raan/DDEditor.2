using DivEditor.Controls;
using Editor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Editor
{
    public partial class EditForm : Form
    {
        public static bool ShowSort = false;
        public static bool BigfillingTex = false;
        public static bool RandfillingTex = false;
        public static int selectTextures = -1;
        public static int selectTollBarPage = -1;
        public static int effects = 0;
        //------------------------------------------------------------------------------------------------------------------------
        public EditForm()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            InitializeComponent();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        //------------------------------------------------------------------------------------------------------------------------
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowSort = !ShowSort;
            toolStripLabel1.Text = "Objects sort: " + ShowSort.ToString();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void EditForm_Shown(object sender, EventArgs e) //Заполнение контейнеров после загрузки основной формы
        {
            if (FileManager.ReadConfig(Vars.pathConfigFile) && !GameData.READY)
            {
                GameData.Initialize();
                System.Diagnostics.Debug.WriteLine(GameData.pathToDivFolder);
                System.Diagnostics.Debug.WriteLine(GameData.pathToEditWorldFolder);
                System.Diagnostics.Debug.WriteLine(GameData.worldMapNumber);
                GameData.READY = true;
                informationField.Text = "config file load";
            }
            else
            {
                informationField.Text = "config file not load";
            }

            for (int i = 0; i < GameData.terrain.Count; i++) // Заполняем список текстур
            {
                texturesTreeView1.Nodes.Add(new TreeNode(GameData.terrain[i].TerrName));
            }

            toolStripLabel1.Text = "Objects sort: " + ShowSort.ToString();

            //List<string> objName = new();
            //for (int i = 0; i < GameData.metobj.Count; i++)
            //{
            //    bool presence = false;
            //    for (int j = 0; j < objName.Count; j++)
            //    {
            //        if (objName[j] == GameData.metobj[i].group)
            //        {
            //            presence = true;
            //        }
            //    }
            //    if (!presence)
            //    {
            //        objName.Add(GameData.metobj[i].group);
            //    }
            //}

        }
        //------------------------------------------------------------------------------------------------------------------------
        private void texturesTreeView1_AfterSelect(object sender, TreeViewEventArgs e) //Выбор текстуры из списка
        {
            String selectedTextures = texturesTreeView1.SelectedNode.ToString();
            System.Diagnostics.Debug.WriteLine(selectedTextures);
            for (int i = 0; i < GameData.terrain.Count; i++)
            {
                if (selectedTextures == "TreeNode: " + GameData.terrain[i].TerrName)
                {
                    texturesBox1.ImageLocation = @"Content\\textures\\" + i.ToString().PadLeft(2, '0') + ".png";
                    texturesBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    selectTextures = i;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void openWorldToolStripMenuItem_Click(object sender, EventArgs e) // Диалог открытия файла world
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "data",
                Filter = "world files (*.x)|*.x*",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog1.FileName;
                //F:\SteamLibrary\steamapps\common\divine_divinity\savegames\test_3\world.x0
                if (path.Remove(0, path.Length - 8) == "world.x0" ||
                    path.Remove(0, path.Length - 8) == "world.x1" ||
                    path.Remove(0, path.Length - 8) == "world.x2" ||
                    path.Remove(0, path.Length - 8) == "world.x3" ||
                    path.Remove(0, path.Length - 8) == "world.x4")
                {
                    informationField.Text = path + " open";
                    GameData.pathToEditWorldFolder = path.Remove(path.Length - 9, 9);
                    GameData.worldMapNumber = int.Parse(path.Remove(0, path.Length - 1));
                    GameData.Initialize();
                    FileManager.WriteConfig();
                }
                else informationField.Text = "Couldn't open world";
            }
            else informationField.Text = "Couldn't open world";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void gameFolderToolStripMenuItem_Click(object sender, EventArgs e) // Диалог открытия файла div.exe
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "data",
                Filter = "div.exe (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog1.FileName;
                //F:\SteamLibrary\steamapps\common\divine_divinity\div.exe
                if (path.Remove(0, path.Length - 7) == "div.exe")
                {
                    informationField.Text = path + " open";
                    GameData.pathToDivFolder = path.Remove(path.Length - 8, 8);
                }
                else informationField.Text = "Couldn't open div.exe";
            }
            else informationField.Text = "Couldn't open div.exe";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void toolStripButton2_Click(object sender, EventArgs e) // Заполение текстурой 3х3
        {
            BigfillingTex = true;
            toolStripButton3.BackColor = Color.WhiteSmoke;
            toolStripButton2.BackColor = Color.LightGray;
            informationField.Text = "Large texture mapping mode";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void toolStripButton3_Click(object sender, EventArgs e) // Заполение текстурой 1х1
        {
            BigfillingTex = false;
            toolStripButton2.BackColor = Color.WhiteSmoke;
            toolStripButton3.BackColor = Color.LightGray;
            informationField.Text = "Small texture mapping mode";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void toolStripButton4_Click(object sender, EventArgs e) // Заполение случайной текстурой
        {
            RandfillingTex = !RandfillingTex;
            if (!RandfillingTex) toolStripButton4.BackColor = Color.WhiteSmoke;
            else toolStripButton4.BackColor = Color.LightGray;
            informationField.Text = "Random texture mapping mode";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void saveWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.SaveWorldAndObjects(GameData.metaTileArray, GameData.objects, GameData.pathToEditWorldFolder, GameData.worldMapNumber, Objects.getObjectsCount());
            informationField.Text = "Save world and objects completed";
        }

        private void mainTollbar_Click(object sender, EventArgs e)
        {
            selectTollBarPage = mainTollbar.SelectedIndex;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public int getEffects()
        {
            effects = 0;
            if (checkBoxWater.Checked)
            {
                effects = effects | Vars.tileEffect_Water;
            }
            if (checkBoxIndoors.Checked)
            {
                effects = effects | Vars.tileEffect_Indoors;
            }
            if (checkBoxFog.Checked)
            {
                effects = effects | Vars.tileEffect_Fog;
            }
            if (checkBoxObject.Checked)
            {
                effects = effects | Vars.tileEffect_Object;
            }
            return effects;
        }

        private void checkBoxWater_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }

        private void checkBoxIndoors_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }

        private void checkBoxFog_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }

        private void checkBoxObject_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }
    }
}
