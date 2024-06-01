using DivEditor.Controls;
using Editor.Controls;
using DivEditor;
using Lzo64;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Timer = System.Windows.Forms.Timer;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
//using Color = Microsoft.Xna.Framework.Color;

namespace Editor
{
    public partial class EditForm : Form
    {
        public static bool ShowSort = false;
        public static bool BigfillingTex = false;
        public static bool RandfillingTex = false;
        public static bool objectStepOneCell = false;
        public static bool formIsActive = true;
        public static int selectTextures = -1;
        public static int selectTollBarPage = 0;
        public static int effects = 0;
        public static Timer? timer;
        private int treeViewCounter = 0;
        private int objectsTreeViewNodePrevious = 0;
        XmlDocument xmlDoc;
        public static readonly LZOCompressor compressor = new LZOCompressor();
        //------------------------------------------------------------------------------------------------------------------------
        public EditForm()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(Timer_Tick!);
            timer.Interval = 100;
            timer.Start();
            eggsPicturePNG.Dock = DockStyle.Fill;
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        //------------------------------------------------------------------------------------------------------------------------
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowSort = !ShowSort;
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
                informationField.Text = "Loading textures";
            }
            else
            {
                informationField.Text = "config file not load";
            }

            for (int i = 0; i < GameData.terrain.Count; i++) // Заполняем список текстур
            {
                texturesTreeView.Nodes.Add(new TreeNode(GameData.terrain[i].TerrName));
            }
            // Заполняем список объектов
            xmlDoc = new XmlDocument();
            xmlDoc.Load(Vars.xmlMetObjHead);
            LoadTreeFromXmlDocument(xmlDoc);

            // Заполняем список болванчиков
            for (int i = 0; i < GameData.AC.Count; i++)
            {
                EggsListBox.Items.Add(i + " " + GameData.AC[i].name.PadRight(25, ' ') + " " + GameData.AC[i].attitude);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) // Отключаем реакцию на клавиатуру в листбоксах
        {
            if (msg.HWnd == EggsListBox.Handle || msg.HWnd == objectsBox.Handle)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void texturesTreeView1_AfterSelect(object sender, TreeViewEventArgs e) //Выбор текстуры из списка
        {
            String selectedTextures = texturesTreeView.SelectedNode.ToString();
            System.Diagnostics.Debug.WriteLine(selectedTextures);
            for (int i = 0; i < GameData.terrain.Count; i++)
            {
                if (selectedTextures == "TreeNode: " + GameData.terrain[i].TerrName)
                {
                    texturesBox1.ImageLocation = Vars.dirTexturesPreview + i.ToString().PadLeft(2, '0') + ".png";
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
                    Editor.Controls.MGGraphicalOutput.UpdateFullTileTexture();
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
            informationField.Text = "Large texture mapping mode";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void toolStripButton3_Click(object sender, EventArgs e) // Заполение текстурой 1х1
        {
            BigfillingTex = false;
            informationField.Text = "Small texture mapping mode";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void toolStripButton4_Click(object sender, EventArgs e) // Заполение случайной текстурой
        {
            RandfillingTex = !RandfillingTex;
            informationField.Text = "Random texture mapping mode";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void saveWorldToolStripMenuItem_Click(object sender, EventArgs e) // Сохраняем мир
        {
            FileManager.SaveWorldAndObjects(GameData.metaTileArray, GameData.objects, GameData.pathToEditWorldFolder, GameData.worldMapNumber, Objects.getObjectsCount());
            DataFile.WriteEggs(GameData.Eggs, Vars.dirDataFile + "\\05_Eggs.000");
            DataFile.WriteAgentClasses(GameData.AC, Vars.dirDataFile + "\\03_AgentClasses.000");
            DataFile.Assembly(GameData.pathToEditWorldFolder + "\\data.000");
            informationField.Text = "Save completed";
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void mainTollbar_Click(object sender, EventArgs e) // Выбор вкладки (текстурирование, объекты и т.д.)
        {
            selectTollBarPage = mainTollbar.SelectedIndex;
            SearchObjectButton.Visible = false;
            StepObjectButton.Visible = false;

            if (selectTollBarPage == 0) // Если выбрано текстурирование
            {

            }
            if (selectTollBarPage == 1) // Если выбрана работа с объектами
            {
                StepObjectButton.Visible = true;
                SearchObjectButton.Visible = true;
            }
            ObjectsTreeView.Size = new System.Drawing.Size(splitObjectsContainer.Panel1.Width / 2, splitObjectsContainer.Panel1.Height);
            objectsBox.Size = new System.Drawing.Size(splitObjectsContainer.Panel1.Width / 2, splitObjectsContainer.Panel1.Height);
        }
        //------------------------------------------------------------------------------------------------------------------------
        public int getEffects()
        {
            effects = 0;
            if (checkBoxWater.Checked)
            {
                effects |= Vars.tileEffect_Water;
            }
            if (checkBoxIndoors.Checked)
            {
                effects |= Vars.tileEffect_Indoors;
            }
            if (checkBoxFog.Checked)
            {
                effects |= Vars.tileEffect_Fog;
            }
            if (checkBoxObject.Checked)
            {
                effects |= Vars.tileEffect_Object;
            }
            return effects;
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void checkBoxWater_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void checkBoxIndoors_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void checkBoxFog_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void checkBoxObject_CheckedChanged(object sender, EventArgs e)
        {
            getEffects();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(CursorXCor.Text, out int x) && Int32.TryParse(CursorYCor.Text, out int y))
            {
                Editor.Controls.MGGraphicalOutput.SetCursor(x, y);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void Timer_Tick(object sender, EventArgs e)
        {
            var cur = Editor.Controls.MGGraphicalOutput.GetCursor();
            CursorXCor.Text = cur.X.ToString();
            CursorYCor.Text = cur.Y.ToString();
            timer.Stop();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i]!;
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = (inXmlNode.OuterXml).Trim();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void LoadTreeFromXmlDocument(XmlDocument dom)
        {
            try
            {
                ObjectsTreeView.Nodes.Clear();
                foreach (XmlNode node in dom.DocumentElement!.ChildNodes)
                {
                    if (node.ChildNodes.Count == 0 && string.IsNullOrEmpty(GetAttributeText(node, "name")))
                        continue;
                    AddNode(ObjectsTreeView.Nodes, node);
                }
                ObjectsTreeView.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        static string GetAttributeText(XmlNode inXmlNode, string name)
        {
            XmlAttribute attr = (inXmlNode.Attributes?[name])!;
            return attr?.Value!;
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void AddNode(TreeNodeCollection nodes, XmlNode inXmlNode)
        {
            if (inXmlNode.HasChildNodes)
            {
                string text = GetAttributeText(inXmlNode, "name");
                if (string.IsNullOrEmpty(text))
                    text = inXmlNode.Name;
                TreeNode newNode = nodes.Add(text);
                newNode.Tag = treeViewCounter;
                treeViewCounter++;
                XmlNodeList nodeList = inXmlNode.ChildNodes;
                for (int i = 0; i <= nodeList.Count - 1; i++)
                {
                    XmlNode xNode = inXmlNode.ChildNodes[i]!;
                    AddNode(newNode.Nodes, xNode);
                }
            }
            else
            {
                string text = GetAttributeText(inXmlNode, "name");
                if (string.IsNullOrEmpty(text))
                    text = (inXmlNode.OuterXml).Trim();
                TreeNode newNode = nodes.Add(text);
                newNode.Tag = treeViewCounter;
                treeViewCounter++;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void MainTollbar_SizeChanged(object sender, EventArgs e)
        {
            objectsBox.Size = new System.Drawing.Size(splitObjectsContainer.Panel1.Width / 2, splitObjectsContainer.Panel1.Height);
            ObjectsTreeView.Size = new System.Drawing.Size(splitObjectsContainer.Panel1.Width / 2, objectsBox.Height);
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void ObjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int selectedObjectNode = (int)ObjectsTreeView.SelectedNode.Tag;
            //System.Drawing.Font regular = new System.Drawing.Font(ObjectsTreeView.Font, System.Drawing.FontStyle.Regular);
            //System.Drawing.Font bold = new System.Drawing.Font(ObjectsTreeView.Font, System.Drawing.FontStyle.Bold);
            //ObjectsTreeView.Nodes[objectsTreeViewNodePrevious].NodeFont = regular;
            //objectsTreeViewNodePrevious = selectedObjectNode;
            //System.Diagnostics.Debug.WriteLine(ObjectsTreeView.SelectedNode.Index);
            //ObjectsTreeView.SelectedNode.NodeFont = bold;
            int firstObject;
            int lastObject;
            if (selectedObjectNode == 0)
            {
                firstObject = 0;
                lastObject = int.Parse(GameData.metaObjHead[selectedObjectNode][0]);
            }
            else
            {
                firstObject = int.Parse(GameData.metaObjHead[selectedObjectNode - 1][0]);
                lastObject = int.Parse(GameData.metaObjHead[selectedObjectNode][0]);
            }
            objectsBox.Items.Clear();
            for (int i = firstObject; i < lastObject; i++)
            {
                objectsBox.Items.Add("Object " + i);
            }
        }

        private void ObjectsBox_SelectedIndexChanged(object sender, EventArgs e) // Выводим выбранный метаобъект в окно просмота
        {
            string[] words = objectsBox.Text.Split(new char[] { ' ' });
            if (words.Length > 1 && int.TryParse(words[1], out int objectSelect))
            {
                int objCount = GameData.MObjects[objectSelect].objects.Count;
                Bitmap bitmap;
                if (objCount == 1)
                {
                    int obj = GameData.MObjects[objectSelect].objects[0].Id;
                    System.Diagnostics.Debug.WriteLine(obj);
                    Microsoft.Xna.Framework.Color[,] color = GetColorArray(obj);
                    bitmap = new(color.GetLength(0), color.Length / color.GetLength(0));
                    for (int i = 0; i < color.GetLength(0); i++)
                    {
                        for (int j = 0; j < color.Length / color.GetLength(0); j++)
                        {
                            Color c = Color.FromArgb(color[i, j].A, color[i, j].R, color[i, j].G, color[i, j].B);
                            bitmap.SetPixel(i, j, c);
                        }
                    }
                    objectsPictureBox.Image = bitmap;
                }
                else
                {
                    if (System.IO.File.Exists(@"Content\MetaObjects\" + objectSelect.ToString().PadLeft(6, '0') + ".png"))
                    {
                        objectsPictureBox.Image = System.Drawing.Image.FromFile(@"Content\MetaObjects\" + objectSelect.ToString().PadLeft(6, '0') + ".png");
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private Microsoft.Xna.Framework.Color[,] GetColorArray(int obj)
        {
            Texture2D SelectObj;
            SelectObj = MGGraphicalOutput.GetObjectTexture(obj);
            Microsoft.Xna.Framework.Color[] ColorArray = new Microsoft.Xna.Framework.Color[SelectObj.Height * SelectObj.Width];
            SelectObj.GetData(ColorArray);
            Microsoft.Xna.Framework.Color[,] colors2D = new Microsoft.Xna.Framework.Color[SelectObj.Width, SelectObj.Height];
            for (int r = 0; r < SelectObj.Width; r++)
            {
                for (int t = 0; t < SelectObj.Height; t++)
                {
                    colors2D[r, t] = ColorArray[r + t * SelectObj.Width];
                }
            }
            return colors2D;
        }

        private void StepObjectButton_Click(object sender, EventArgs e)
        {
            objectStepOneCell = !objectStepOneCell;
            StepObjectButton.Checked = objectStepOneCell;
        }

        private void SaveWorldImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("The procedure will take a few minutes, are you sure?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SaveWorldMapImage();
            }
        }
        private void SaveWorldMapImage()
        {
            int scale = Vars.saveWorldImageScale;
            int width = Vars.maxHorizontalTails;
            int height = Vars.maxVerticalTails;

            Bitmap bitmap = new(width * Vars.tileSize / scale, height * Vars.tileSize / scale);
            for (int y = 0; y < height; y++)
            {
                if (y % 10 == 0)
                {
                    informationField.Text = "Save tile image... " + y * 100 / height + " %";
                    this.Refresh();
                }
                for (int x = 0; x < width; x++)
                {
                    // Добавляем нижнюю текстуру
                    if (GameData.metaTileArray[y, x].DownTileTexture < 65535 && GameData.metaTileArray[y, x].DownTileTexture != 1996)
                    {
                        Texture2D tile = MGGraphicalOutput.GetTileTexture(GameData.metaTileArray[y, x].DownTileTexture);
                        Microsoft.Xna.Framework.Color[] ColorArray = new Microsoft.Xna.Framework.Color[Vars.tileSize * Vars.tileSize];
                        tile.GetData(ColorArray);
                        Microsoft.Xna.Framework.Color[,] colors2D = new Microsoft.Xna.Framework.Color[Vars.tileSize / scale, Vars.tileSize / scale];
                        for (int r = 0; r < Vars.tileSize / scale; r++)
                        {
                            for (int t = 0; t < Vars.tileSize / scale; t++)
                            {
                                colors2D[r, t] = ColorArray[r * scale + t * scale * Vars.tileSize];
                            }
                        }
                        for (int i = 0; i < colors2D.GetLength(0); i++)
                        {
                            for (int j = 0; j < colors2D.Length / colors2D.GetLength(0); j++)
                            {
                                Color c = Color.FromArgb(colors2D[i, j].A, colors2D[i, j].R, colors2D[i, j].G, colors2D[i, j].B);
                                bitmap.SetPixel(i + x * Vars.tileSize / scale, j + y * Vars.tileSize / scale, c);
                            }
                        }
                    }
                    // Добавляем верхнюю текстуру
                    if (GameData.metaTileArray[y, x].UpTileTexture < 65535 && GameData.metaTileArray[y, x].UpTileTexture != 1996)
                    {
                        Texture2D tile = MGGraphicalOutput.GetTileTexture(GameData.metaTileArray[y, x].UpTileTexture);
                        Microsoft.Xna.Framework.Color[] ColorArray = new Microsoft.Xna.Framework.Color[Vars.tileSize * Vars.tileSize];
                        tile.GetData(ColorArray);
                        Microsoft.Xna.Framework.Color[,] colors2D = new Microsoft.Xna.Framework.Color[Vars.tileSize / scale, Vars.tileSize / scale];
                        for (int r = 0; r < Vars.tileSize / scale; r++)
                        {
                            for (int t = 0; t < Vars.tileSize / scale; t++)
                            {
                                colors2D[r, t] = ColorArray[r * scale + t * scale * Vars.tileSize];
                            }
                        }
                        for (int i = 0; i < colors2D.GetLength(0); i++)
                        {
                            for (int j = 0; j < colors2D.Length / colors2D.GetLength(0); j++)
                            {
                                if (colors2D[i, j].A > 0)
                                {
                                    Color c = Color.FromArgb(colors2D[i, j].A, colors2D[i, j].R, colors2D[i, j].G, colors2D[i, j].B);
                                    bitmap.SetPixel(i + x * Vars.tileSize / scale, j + y * Vars.tileSize / scale, c);
                                }
                            }
                        }
                    }
                }
            }
            // Добавляем объекты
            List<long[]> objects = new();
            for (int k = 0; k < GameData.objects.Count; k++)
            {
                if (GameData.objects[k].SpriteID < 65535)
                {
                    objects.Add(new long[] { GameData.objects[k].SpriteID,
                        GameData.objects[k].AbsolutePixelPosition.X,
                        GameData.objects[k].AbsolutePixelPosition.Y,
                        (GameData.objects[k].AbsolutePixelPosition.Y + GameData.objectDesc[GameData.objects[k].SpriteID].TouchPoint.Y) * Vars.maxHorizontalTails * Vars.tileSize + GameData.objects[k].AbsolutePixelPosition.X + GameData.objectDesc[GameData.objects[k].SpriteID].TouchPoint.X,
                        GameData.objects[k].Height});
                }
            }
            objects.Sort((x, y) => x[3].CompareTo(y[3]));
            for (int k = 0; k < objects.Count; k++)
            {
                if (k % 700 == 0)
                {
                    informationField.Text = "Save object image... " + k * 100 / objects.Count + " %";
                    this.Refresh();
                }
                int objPosX = (int)objects[k][1];
                int objPosY = (int)objects[k][2];
                if ((objPosX < width * Vars.tileSize) && (objPosY < height * Vars.tileSize))
                {
                    Texture2D obj = MGGraphicalOutput.GetObjectTexture((int)objects[k][0]);
                    Microsoft.Xna.Framework.Color[] ColorArray = new Microsoft.Xna.Framework.Color[obj.Width * obj.Height];
                    obj.GetData(ColorArray);
                    Microsoft.Xna.Framework.Color[,] colors2D = new Microsoft.Xna.Framework.Color[obj.Width / scale, obj.Height / scale];
                    for (int r = 0; r < obj.Width / scale; r++)
                    {
                        for (int t = 0; t < obj.Height / scale; t++)
                        {
                            colors2D[r, t] = ColorArray[r * scale + t * scale * obj.Width];
                        }
                    }
                    for (int i = 0; i < colors2D.GetLength(0); i++)
                    {
                        for (int j = 0; j < colors2D.Length / colors2D.GetLength(0); j++)
                        {
                            if (colors2D[i, j].A > 0 &&
                                objPosX + obj.Width < width * Vars.tileSize &&
                                objPosY + obj.Height - (int)objects[k][4] < height * Vars.tileSize)
                            {
                                Color c = Color.FromArgb(colors2D[i, j].A, colors2D[i, j].R, colors2D[i, j].G, colors2D[i, j].B);
                                bitmap.SetPixel(i + objPosX / scale, j + (objPosY - (int)objects[k][4]) / scale, c);
                            }
                        }
                    }
                }
            }
            Bitmap b2 = new Bitmap(bitmap, new Size(bitmap.Width / 2, bitmap.Height / 2));
            b2.Save("Content\\world images\\world_" + GameData.worldMapNumber + ".jpg", ImageFormat.Jpeg);
            informationField.Text = " Done";
        }

        private void TESTBUTTON_Click(object sender, EventArgs e) // ===============================================
        {

        }

        private void texturesTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void ObjectsTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void saveMetaObjectsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int o = 0; o < GameData.MObjects.Count; o++)
            {
                if (GameData.MObjects[o].objects.Count > 1)
                {
                    int objN = o;
                    int objCount = GameData.MObjects[objN].objects.Count;
                    Microsoft.Xna.Framework.Color[,] color = new Microsoft.Xna.Framework.Color[9000, 9000];
                    for (int k = 0; k < objCount; k++)
                    {
                        int biasX = GameData.objectDesc[GameData.MObjects[objN].objects[0].Id].TouchPoint.X + GameData.MObjects[objN].objects[k].posX - GameData.objectDesc[GameData.MObjects[objN].objects[k].Id].TouchPoint.X;
                        int biasY = GameData.objectDesc[GameData.MObjects[objN].objects[0].Id].TouchPoint.Y - GameData.MObjects[objN].objects[k].posY - GameData.objectDesc[GameData.MObjects[objN].objects[k].Id].TouchPoint.Y - GameData.MObjects[objN].objects[k].posZ;
                        int obj = GameData.MObjects[objN].objects[k].Id;

                        Microsoft.Xna.Framework.Color[,] objColor = GetColorArray(obj);
                        for (int i = 0; i < objColor.GetLength(0); i++)
                        {
                            for (int j = 0; j < objColor.Length / objColor.GetLength(0); j++)
                            {
                                if (objColor[i, j].A != 0)
                                {
                                    color[i + 2500 + biasX, j + 2500 + biasY] = objColor[i, j];
                                }
                            }
                        }
                    }
                    Point start = new Point(-1, -1);
                    Point end = new Point(-1, -1);
                    for (int i = 0; i < color.GetLength(0); i++)
                    {
                        int sum = 0;
                        for (int j = 0; j < color.Length / color.GetLength(0); j++)
                        {
                            if (color[i, j].A != 0) sum++;
                        }
                        if (start.X < 0 && sum > 0) start.X = i;
                        if (end.X < 0 && start.X >= 0 && sum == 0) end.X = i;
                    }
                    for (int j = 0; j < color.Length / color.GetLength(0); j++)
                    {
                        int sum = 0;
                        for (int i = 0; i < color.GetLength(0); i++)
                        {
                            if (color[i, j].A != 0) sum++;
                        }
                        if (start.Y < 0 && sum > 0) start.Y = j;
                        if (end.Y < 0 && start.Y >= 0 && sum == 0) end.Y = j;
                    }
                    Bitmap bitmap = new(end.X - start.X, end.Y - start.Y);
                    for (int i = start.X; i < end.X; i++)
                    {
                        for (int j = start.Y; j < end.Y; j++)
                        {
                            Color c = Color.FromArgb(color[i, j].A, color[i, j].R, color[i, j].G, color[i, j].B);
                            bitmap.SetPixel(i - start.X, j - start.Y, c);
                        }
                    }
                    bitmap.Save("Content\\MetaObjects\\" + o.ToString().PadLeft(6, '0') + ".png", ImageFormat.Png);
                    informationField.Text = "Content\\MetaObjects\\" + o.ToString().PadLeft(6, '0') + ".png";
                    this.Refresh();
                }
            }
            informationField.Text = " Done";
        }
        private void objectsBox_SizeChanged(object sender, EventArgs e)
        {
            ObjectsTreeView.Size = new System.Drawing.Size(splitObjectsContainer.Panel1.Width / 2, objectsBox.Height);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "help.txt");
        }

        private void SearchObjectButton_Click_1(object sender, EventArgs e)
        {
            string result = Microsoft.VisualBasic.Interaction.InputBox("Enter object ID ( " + (GameData.objects.Count - 1) + " max)");
            int ID = 0;

            if (Int32.TryParse(result, out ID) && GameData.objects[ID].SpriteID != 0xffff && ID < GameData.objects.Count)
            {
                System.Diagnostics.Debug.WriteLine(ID);
                MGGraphicalOutput.tileBiasX = GameData.objects[ID].TilePosition.X;
                MGGraphicalOutput.tileBiasY = GameData.objects[ID].TilePosition.Y;
                MGGraphicalOutput.selectedObjID = ID;
            }
        }

        private void EditForm_Activated(object sender, EventArgs e)
        {
            formIsActive = true;
        }

        private void EditForm_Deactivate(object sender, EventArgs e)
        {
            formIsActive = false;
        }

        private void MonoWindow_Click(object sender, EventArgs e)
        {

        }

        private void EggsListBox_MouseDoubleClick(object sender, MouseEventArgs e) // Добавляем болванчика
        {
            if (EggsListBox.SelectedIndex > 0)
            {
                WriteLine("Add new " + GameData.AC[EggsListBox.SelectedIndex].name + " egg");
                MonoWindow.Focus();
                GameData.Eggs.Add(new Eggs(new int[]{(MGGraphicalOutput.tileBiasX) * Vars.tileSize,
                (MGGraphicalOutput.tileBiasY) * Vars.tileSize,
                EggsListBox.SelectedIndex,1,-1,-1,-1,-1,GameData.Eggs.Count,GameData.worldMapNumber}));
                MGGraphicalOutput.procMovingNewEgg = true;
                Cursor.Hide();
            }
        }
        private static void WriteLine(String line)
        {
            System.Diagnostics.Debug.WriteLine(line);
        }

        private void objectsBox_MouseDoubleClick(object sender, MouseEventArgs e) // Добавляем объект
        {
            //// 0 - мировой номер, 1 - SpriteID, 2 - поведение, 3 - координата Х, 4 - координата Y, 5 - сортировка
            string[] words = objectsBox.Text.Split(new char[] { ' ' });
            if (words.Length > 1 && int.TryParse(words[1], out int objectSelect))
            {
                MGGraphicalOutput.procMovingNewObject = true;
                int objCount = GameData.MObjects[objectSelect].objects.Count;
                for (int i = 0; i < objCount; i++)
                {
                    int Id = GameData.MObjects[objectSelect].objects[i].Id;
                    int Xcor = 0;
                    int Ycor = 0;
                    int heigth = GameData.MObjects[objectSelect].objects[i].posZ;
                    int biasX = GameData.objectDesc[GameData.MObjects[objectSelect].objects[0].Id].TouchPoint.X + GameData.MObjects[objectSelect].objects[i].posX - GameData.objectDesc[GameData.MObjects[objectSelect].objects[i].Id].TouchPoint.X;
                    int biasY = GameData.objectDesc[GameData.MObjects[objectSelect].objects[0].Id].TouchPoint.Y - GameData.MObjects[objectSelect].objects[i].posY - GameData.objectDesc[GameData.MObjects[objectSelect].objects[i].Id].TouchPoint.Y;
                    int sort = (Ycor + GameData.objectDesc[Id].TouchPoint.Y) * Vars.maxHorizontalTails * Vars.tileSize + Xcor;
                    int worldID = Objects.getObjectsCount();
                    MGGraphicalOutput.newObject.Add(new int[8] { worldID, Id, 1, Xcor, Ycor, sort, biasX, biasY });
                    Objects objNew = new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new Point(Xcor, Ycor), heigth, Id)
                    {
                        Height = heigth
                    };
                    GameData.objects.Add(objNew);
                }
                Cursor.Hide();
            }
        }

        private void EggsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EggsListBox.SelectedIndex > 0)
            {
                eggsPicturePNG.Visible = true;
                eggsPicturePNG.ImageLocation = Vars.dirAgentClassesImgPNG + EggsListBox.SelectedIndex.ToString() + ".png";
                ACPropertyBox_0.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.LVL_0].ToString();
                ACPropertyBox_1.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.attack].ToString();
                ACPropertyBox_2.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.protection].ToString();
                ACPropertyBox_3.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.armor].ToString();
                ACPropertyBox_4.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.view_radius].ToString();
                ACPropertyBox_5.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.hearing_radius].ToString();
                ACPropertyBox_6.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.lightning_resistance].ToString();
                ACPropertyBox_7.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.poison_resistance].ToString();
                ACPropertyBox_8.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.fire_resistance].ToString();
                ACPropertyBox_9.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.spirit_resistance].ToString();
            }
            else
            {
                eggsPicturePNG.Visible = false;
                ACPropertyBox_0.Text = "";
                ACPropertyBox_1.Text = "";
                ACPropertyBox_2.Text = "";
                ACPropertyBox_3.Text = "";
                ACPropertyBox_4.Text = "";
                ACPropertyBox_5.Text = "";
                ACPropertyBox_6.Text = "";
                ACPropertyBox_7.Text = "";
                ACPropertyBox_8.Text = "";
                ACPropertyBox_9.Text = "";
            }


        }

        private void EggsListBox_Leave(object sender, EventArgs e)
        {
            //eggsPicturePNG.Visible = false;
        }

        private void EggsListBox_Enter(object sender, EventArgs e)
        {
            //eggsPicturePNG.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EggsListBox.SelectedIndex > 0)
            {
                int x = 0;
                if (Int32.TryParse(ACPropertyBox_0.Text, out x)) // Save LVL
                {
                    if (x > 0 && x <= 100)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.LVL_0] = x;
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.LVL_1] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_1.Text, out x)) // Save attack
                {
                    if (x >= -255 && x <= 255)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.attack] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_2.Text, out x)) // Save protection
                {
                    if (x >= -255 && x <= 255)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.protection] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_3.Text, out x)) // Save armor
                {
                    if (x >= -255 && x <= 255)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.armor] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_4.Text, out x)) // Save view_radius
                {
                    if (x >= 0 && x <= 100)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.view_radius] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_5.Text, out x)) // Save hearing_radius
                {
                    if (x >= 0 && x <= 100)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.hearing_radius] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_6.Text, out x)) // Save lightning_resistance
                {
                    if (x >= -100 && x <= 100)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.lightning_resistance] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_7.Text, out x)) // Save poison_resistance
                {
                    if (x >= -100 && x <= 100)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.poison_resistance] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_8.Text, out x)) // Save fire_resistance
                {
                    if (x >= -100 && x <= 100)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.fire_resistance] = x;
                    }
                }
                if (Int32.TryParse(ACPropertyBox_9.Text, out x)) // Save spirit_resistance
                {
                    if (x >= -100 && x <= 100)
                    {
                        GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.spirit_resistance] = x;
                    }
                }
                ACPropertyBox_0.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.LVL_0].ToString();
                ACPropertyBox_1.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.attack].ToString();
                ACPropertyBox_2.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.protection].ToString();
                ACPropertyBox_3.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.armor].ToString();
                ACPropertyBox_4.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.view_radius].ToString();
                ACPropertyBox_5.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.hearing_radius].ToString();
                ACPropertyBox_6.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.lightning_resistance].ToString();
                ACPropertyBox_7.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.poison_resistance].ToString();
                ACPropertyBox_8.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.fire_resistance].ToString();
                ACPropertyBox_9.Text = GameData.AC[EggsListBox.SelectedIndex].var_1[AgentClasses.spirit_resistance].ToString();
            }
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------







    public static class Keyboard
    {
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
        private static KeyStates GetKeyState(System.Windows.Forms.Keys key)
        {
            KeyStates state = KeyStates.None;
            short retVal = GetKeyState((int)key);
            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;
            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;
            return state;
        }
        public static bool IsKeyDown(System.Windows.Forms.Keys key)
        {
            if (key == System.Windows.Forms.Keys.Shift) return Control.ModifierKeys == System.Windows.Forms.Keys.Shift;
            else if (key == System.Windows.Forms.Keys.Control) return Control.ModifierKeys == System.Windows.Forms.Keys.Control;
            else if (key == System.Windows.Forms.Keys.Alt) return Control.ModifierKeys == System.Windows.Forms.Keys.Alt;
            else return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }
        public static bool IsKeyToggled(System.Windows.Forms.Keys key)
        {
            return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
        }


    }
}
