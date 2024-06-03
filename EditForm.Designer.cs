namespace Editor
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param spriteID="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            MonoWindow = new Controls.MGGraphicalOutput();
            toolStrip1 = new ToolStrip();
            CursorYCor = new ToolStripTextBox();
            CursorXCor = new ToolStripTextBox();
            setCursor = new ToolStripButton();
            StepObjectButton = new ToolStripButton();
            TESTBUTTON = new ToolStripButton();
            SearchObjectButton = new ToolStripButton();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openWorldToolStripMenuItem = new ToolStripMenuItem();
            saveWorldToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            gameFolderToolStripMenuItem = new ToolStripMenuItem();
            saveWorldImageToolStripMenuItem = new ToolStripMenuItem();
            saveMetaObjectsImageToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            mainTollbar = new TabControl();
            texturesTabPage1 = new TabPage();
            texturesSplitContainer2 = new SplitContainer();
            texturesTreeView = new TreeView();
            texturesBox1 = new PictureBox();
            objectsTabPage2 = new TabPage();
            splitObjectsContainer = new SplitContainer();
            objectsBox = new ListBox();
            ObjectsTreeView = new TreeView();
            objectsPictureBox = new PictureBox();
            eggsTabPage3 = new TabPage();
            splitContainer1 = new SplitContainer();
            EggsListBox = new ListBox();
            button1 = new Button();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ACPropertyBox_9 = new TextBox();
            ACPropertyBox_8 = new TextBox();
            ACPropertyBox_7 = new TextBox();
            ACPropertyBox_6 = new TextBox();
            ACPropertyBox_5 = new TextBox();
            ACPropertyBox_4 = new TextBox();
            ACPropertyBox_3 = new TextBox();
            ACPropertyBox_2 = new TextBox();
            ACPropertyBox_1 = new TextBox();
            ACPropertyBox_0 = new TextBox();
            eggsPicturePNG = new PictureBox();
            effectsTabPage3 = new TabPage();
            checkBoxObject = new CheckBox();
            checkBoxFog = new CheckBox();
            checkBoxIndoors = new CheckBox();
            checkBoxWater = new CheckBox();
            PanelMonoWindow = new Panel();
            statusStrip1 = new StatusStrip();
            informationField = new ToolStripStatusLabel();
            mainSplitContainer1 = new SplitContainer();
            toolStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            mainTollbar.SuspendLayout();
            texturesTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)texturesSplitContainer2).BeginInit();
            texturesSplitContainer2.Panel1.SuspendLayout();
            texturesSplitContainer2.Panel2.SuspendLayout();
            texturesSplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)texturesBox1).BeginInit();
            objectsTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitObjectsContainer).BeginInit();
            splitObjectsContainer.Panel1.SuspendLayout();
            splitObjectsContainer.Panel2.SuspendLayout();
            splitObjectsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectsPictureBox).BeginInit();
            eggsTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eggsPicturePNG).BeginInit();
            effectsTabPage3.SuspendLayout();
            PanelMonoWindow.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer1).BeginInit();
            mainSplitContainer1.Panel1.SuspendLayout();
            mainSplitContainer1.Panel2.SuspendLayout();
            mainSplitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // MonoWindow
            // 
            MonoWindow.Dock = DockStyle.Fill;
            MonoWindow.Enabled = false;
            MonoWindow.ForeColor = SystemColors.ActiveBorder;
            MonoWindow.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef;
            MonoWindow.Location = new Point(0, 0);
            MonoWindow.MouseHoverUpdatesOnly = false;
            MonoWindow.Name = "MonoWindow";
            MonoWindow.Size = new Size(1128, 831);
            MonoWindow.TabIndex = 1;
            MonoWindow.Text = "MonoWindow";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(30, 30);
            toolStrip1.Items.AddRange(new ToolStripItem[] { CursorYCor, CursorXCor, setCursor, StepObjectButton, TESTBUTTON, SearchObjectButton });
            toolStrip1.Location = new Point(0, 28);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1488, 27);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // CursorYCor
            // 
            CursorYCor.Alignment = ToolStripItemAlignment.Right;
            CursorYCor.Name = "CursorYCor";
            CursorYCor.Size = new Size(50, 27);
            // 
            // CursorXCor
            // 
            CursorXCor.Alignment = ToolStripItemAlignment.Right;
            CursorXCor.Name = "CursorXCor";
            CursorXCor.Size = new Size(50, 27);
            // 
            // setCursor
            // 
            setCursor.Alignment = ToolStripItemAlignment.Right;
            setCursor.BackColor = SystemColors.ControlLight;
            setCursor.DisplayStyle = ToolStripItemDisplayStyle.Text;
            setCursor.Image = (Image)resources.GetObject("setCursor.Image");
            setCursor.ImageTransparentColor = Color.Magenta;
            setCursor.Name = "setCursor";
            setCursor.Size = new Size(32, 24);
            setCursor.Text = "Go";
            setCursor.Click += ToolStripButton5_Click;
            // 
            // StepObjectButton
            // 
            StepObjectButton.BackColor = SystemColors.Control;
            StepObjectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            StepObjectButton.Image = (Image)resources.GetObject("StepObjectButton.Image");
            StepObjectButton.ImageTransparentColor = Color.Magenta;
            StepObjectButton.Name = "StepObjectButton";
            StepObjectButton.Size = new Size(82, 24);
            StepObjectButton.Text = "Step 1 cell";
            StepObjectButton.Visible = false;
            StepObjectButton.Click += StepObjectButton_Click;
            // 
            // TESTBUTTON
            // 
            TESTBUTTON.Alignment = ToolStripItemAlignment.Right;
            TESTBUTTON.BackColor = SystemColors.Info;
            TESTBUTTON.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TESTBUTTON.Image = (Image)resources.GetObject("TESTBUTTON.Image");
            TESTBUTTON.ImageTransparentColor = Color.Magenta;
            TESTBUTTON.Name = "TESTBUTTON";
            TESTBUTTON.RightToLeft = RightToLeft.No;
            TESTBUTTON.Size = new Size(101, 24);
            TESTBUTTON.Text = "TESTBUTTON";
            TESTBUTTON.Click += TESTBUTTON_Click;
            // 
            // SearchObjectButton
            // 
            SearchObjectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SearchObjectButton.Image = (Image)resources.GetObject("SearchObjectButton.Image");
            SearchObjectButton.ImageTransparentColor = Color.Magenta;
            SearchObjectButton.Name = "SearchObjectButton";
            SearchObjectButton.Size = new Size(103, 24);
            SearchObjectButton.Text = "Search object";
            SearchObjectButton.Visible = false;
            SearchObjectButton.Click += SearchObjectButton_Click_1;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1488, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openWorldToolStripMenuItem, saveWorldToolStripMenuItem, settingsToolStripMenuItem, saveWorldImageToolStripMenuItem, saveMetaObjectsImageToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(44, 24);
            fileToolStripMenuItem.Text = "file";
            // 
            // openWorldToolStripMenuItem
            // 
            openWorldToolStripMenuItem.Name = "openWorldToolStripMenuItem";
            openWorldToolStripMenuItem.Size = new Size(257, 26);
            openWorldToolStripMenuItem.Text = "Open World";
            openWorldToolStripMenuItem.Click += openWorldToolStripMenuItem_Click;
            // 
            // saveWorldToolStripMenuItem
            // 
            saveWorldToolStripMenuItem.Name = "saveWorldToolStripMenuItem";
            saveWorldToolStripMenuItem.Size = new Size(257, 26);
            saveWorldToolStripMenuItem.Text = "Save World";
            saveWorldToolStripMenuItem.Click += saveWorldToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gameFolderToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(257, 26);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // gameFolderToolStripMenuItem
            // 
            gameFolderToolStripMenuItem.Name = "gameFolderToolStripMenuItem";
            gameFolderToolStripMenuItem.Size = new Size(175, 26);
            gameFolderToolStripMenuItem.Text = "Game folder";
            gameFolderToolStripMenuItem.Click += gameFolderToolStripMenuItem_Click;
            // 
            // saveWorldImageToolStripMenuItem
            // 
            saveWorldImageToolStripMenuItem.Name = "saveWorldImageToolStripMenuItem";
            saveWorldImageToolStripMenuItem.Size = new Size(257, 26);
            saveWorldImageToolStripMenuItem.Text = "Save World Image";
            saveWorldImageToolStripMenuItem.Click += SaveWorldImageToolStripMenuItem_Click;
            // 
            // saveMetaObjectsImageToolStripMenuItem
            // 
            saveMetaObjectsImageToolStripMenuItem.Name = "saveMetaObjectsImageToolStripMenuItem";
            saveMetaObjectsImageToolStripMenuItem.Size = new Size(257, 26);
            saveMetaObjectsImageToolStripMenuItem.Text = "Save MetaObjects Image";
            saveMetaObjectsImageToolStripMenuItem.Click += saveMetaObjectsImageToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(52, 24);
            helpToolStripMenuItem.Text = "help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // mainTollbar
            // 
            mainTollbar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTollbar.Controls.Add(texturesTabPage1);
            mainTollbar.Controls.Add(objectsTabPage2);
            mainTollbar.Controls.Add(eggsTabPage3);
            mainTollbar.Controls.Add(effectsTabPage3);
            mainTollbar.Location = new Point(3, 3);
            mainTollbar.Name = "mainTollbar";
            mainTollbar.SelectedIndex = 0;
            mainTollbar.Size = new Size(342, 833);
            mainTollbar.TabIndex = 4;
            mainTollbar.SizeChanged += MainTollbar_SizeChanged;
            mainTollbar.Click += mainTollbar_Click;
            // 
            // texturesTabPage1
            // 
            texturesTabPage1.BackColor = SystemColors.Control;
            texturesTabPage1.BackgroundImageLayout = ImageLayout.None;
            texturesTabPage1.Controls.Add(texturesSplitContainer2);
            texturesTabPage1.Location = new Point(4, 29);
            texturesTabPage1.Name = "texturesTabPage1";
            texturesTabPage1.Padding = new Padding(3);
            texturesTabPage1.Size = new Size(334, 800);
            texturesTabPage1.TabIndex = 0;
            texturesTabPage1.Text = " Textures";
            // 
            // texturesSplitContainer2
            // 
            texturesSplitContainer2.BorderStyle = BorderStyle.FixedSingle;
            texturesSplitContainer2.Dock = DockStyle.Fill;
            texturesSplitContainer2.Location = new Point(3, 3);
            texturesSplitContainer2.Name = "texturesSplitContainer2";
            texturesSplitContainer2.Orientation = Orientation.Horizontal;
            // 
            // texturesSplitContainer2.Panel1
            // 
            texturesSplitContainer2.Panel1.Controls.Add(texturesTreeView);
            // 
            // texturesSplitContainer2.Panel2
            // 
            texturesSplitContainer2.Panel2.Controls.Add(texturesBox1);
            texturesSplitContainer2.Size = new Size(328, 794);
            texturesSplitContainer2.SplitterDistance = 543;
            texturesSplitContainer2.TabIndex = 0;
            // 
            // texturesTreeView
            // 
            texturesTreeView.Dock = DockStyle.Fill;
            texturesTreeView.Location = new Point(0, 0);
            texturesTreeView.Name = "texturesTreeView";
            texturesTreeView.Size = new Size(326, 541);
            texturesTreeView.TabIndex = 0;
            texturesTreeView.TabStop = false;
            texturesTreeView.AfterSelect += texturesTreeView1_AfterSelect;
            texturesTreeView.KeyDown += texturesTreeView_KeyDown;
            // 
            // texturesBox1
            // 
            texturesBox1.Dock = DockStyle.Fill;
            texturesBox1.Location = new Point(0, 0);
            texturesBox1.Name = "texturesBox1";
            texturesBox1.Size = new Size(326, 245);
            texturesBox1.TabIndex = 0;
            texturesBox1.TabStop = false;
            // 
            // objectsTabPage2
            // 
            objectsTabPage2.BackColor = SystemColors.Control;
            objectsTabPage2.Controls.Add(splitObjectsContainer);
            objectsTabPage2.Location = new Point(4, 29);
            objectsTabPage2.Name = "objectsTabPage2";
            objectsTabPage2.Padding = new Padding(3);
            objectsTabPage2.Size = new Size(334, 800);
            objectsTabPage2.TabIndex = 1;
            objectsTabPage2.Text = "Objects";
            // 
            // splitObjectsContainer
            // 
            splitObjectsContainer.Dock = DockStyle.Fill;
            splitObjectsContainer.Location = new Point(3, 3);
            splitObjectsContainer.Name = "splitObjectsContainer";
            splitObjectsContainer.Orientation = Orientation.Horizontal;
            // 
            // splitObjectsContainer.Panel1
            // 
            splitObjectsContainer.Panel1.Controls.Add(objectsBox);
            splitObjectsContainer.Panel1.Controls.Add(ObjectsTreeView);
            // 
            // splitObjectsContainer.Panel2
            // 
            splitObjectsContainer.Panel2.Controls.Add(objectsPictureBox);
            splitObjectsContainer.Size = new Size(328, 794);
            splitObjectsContainer.SplitterDistance = 552;
            splitObjectsContainer.TabIndex = 0;
            // 
            // objectsBox
            // 
            objectsBox.Dock = DockStyle.Right;
            objectsBox.FormattingEnabled = true;
            objectsBox.ItemHeight = 20;
            objectsBox.Location = new Point(177, 0);
            objectsBox.Name = "objectsBox";
            objectsBox.Size = new Size(151, 552);
            objectsBox.TabIndex = 1;
            objectsBox.SelectedIndexChanged += ObjectsBox_SelectedIndexChanged;
            objectsBox.SizeChanged += objectsBox_SizeChanged;
            objectsBox.MouseDoubleClick += objectsBox_MouseDoubleClick;
            // 
            // ObjectsTreeView
            // 
            ObjectsTreeView.Location = new Point(0, 0);
            ObjectsTreeView.Name = "ObjectsTreeView";
            ObjectsTreeView.Size = new Size(171, 549);
            ObjectsTreeView.TabIndex = 0;
            ObjectsTreeView.AfterSelect += ObjectsTreeView_AfterSelect;
            ObjectsTreeView.KeyDown += ObjectsTreeView_KeyDown;
            // 
            // objectsPictureBox
            // 
            objectsPictureBox.Dock = DockStyle.Fill;
            objectsPictureBox.Location = new Point(0, 0);
            objectsPictureBox.Name = "objectsPictureBox";
            objectsPictureBox.Size = new Size(328, 238);
            objectsPictureBox.TabIndex = 0;
            objectsPictureBox.TabStop = false;
            // 
            // eggsTabPage3
            // 
            eggsTabPage3.BackColor = SystemColors.Control;
            eggsTabPage3.Controls.Add(splitContainer1);
            eggsTabPage3.Location = new Point(4, 29);
            eggsTabPage3.Name = "eggsTabPage3";
            eggsTabPage3.Padding = new Padding(3);
            eggsTabPage3.Size = new Size(334, 800);
            eggsTabPage3.TabIndex = 2;
            eggsTabPage3.Text = "Eggs";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(EggsListBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.AutoScroll = true;
            splitContainer1.Panel2.Controls.Add(button1);
            splitContainer1.Panel2.Controls.Add(label10);
            splitContainer1.Panel2.Controls.Add(label9);
            splitContainer1.Panel2.Controls.Add(label8);
            splitContainer1.Panel2.Controls.Add(label7);
            splitContainer1.Panel2.Controls.Add(label6);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_9);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_8);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_7);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_6);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_5);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_4);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_3);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_2);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_1);
            splitContainer1.Panel2.Controls.Add(ACPropertyBox_0);
            splitContainer1.Panel2.Controls.Add(eggsPicturePNG);
            splitContainer1.Size = new Size(328, 794);
            splitContainer1.SplitterDistance = 391;
            splitContainer1.TabIndex = 0;
            // 
            // EggsListBox
            // 
            EggsListBox.Dock = DockStyle.Fill;
            EggsListBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            EggsListBox.FormattingEnabled = true;
            EggsListBox.ItemHeight = 18;
            EggsListBox.Location = new Point(0, 0);
            EggsListBox.Name = "EggsListBox";
            EggsListBox.Size = new Size(328, 391);
            EggsListBox.TabIndex = 0;
            EggsListBox.SelectedIndexChanged += EggsListBox_SelectedIndexChanged;
            EggsListBox.Enter += EggsListBox_Enter;
            EggsListBox.Leave += EggsListBox_Leave;
            EggsListBox.MouseDoubleClick += EggsListBox_MouseDoubleClick;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(224, 343);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Location = new Point(141, 303);
            label10.Name = "label10";
            label10.RightToLeft = RightToLeft.No;
            label10.Size = new Size(113, 20);
            label10.TabIndex = 20;
            label10.Text = "Spirit resistance";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(150, 270);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.No;
            label9.Size = new Size(102, 20);
            label9.TabIndex = 19;
            label9.Text = "Fire resistance";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(133, 237);
            label8.Name = "label8";
            label8.RightToLeft = RightToLeft.No;
            label8.Size = new Size(121, 20);
            label8.TabIndex = 18;
            label8.Text = "Poison resistance";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(115, 204);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.No;
            label7.Size = new Size(140, 20);
            label7.TabIndex = 17;
            label7.Text = "Lightning resistance";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(150, 171);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.No;
            label6.Size = new Size(105, 20);
            label6.TabIndex = 16;
            label6.Text = "Healing radius";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(167, 138);
            label5.Name = "label5";
            label5.RightToLeft = RightToLeft.No;
            label5.Size = new Size(85, 20);
            label5.TabIndex = 15;
            label5.Text = "View radius";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(204, 105);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.No;
            label4.Size = new Size(51, 20);
            label4.TabIndex = 14;
            label4.Text = "Armor";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(177, 72);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.No;
            label3.Size = new Size(77, 20);
            label3.TabIndex = 13;
            label3.Text = "Protection";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(204, 39);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(51, 20);
            label2.TabIndex = 12;
            label2.Text = "Attack";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(224, 6);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(31, 20);
            label1.TabIndex = 11;
            label1.Text = "LVL";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ACPropertyBox_9
            // 
            ACPropertyBox_9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_9.Location = new Point(260, 300);
            ACPropertyBox_9.Name = "ACPropertyBox_9";
            ACPropertyBox_9.Size = new Size(65, 27);
            ACPropertyBox_9.TabIndex = 10;
            // 
            // ACPropertyBox_8
            // 
            ACPropertyBox_8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_8.Location = new Point(260, 267);
            ACPropertyBox_8.Name = "ACPropertyBox_8";
            ACPropertyBox_8.Size = new Size(65, 27);
            ACPropertyBox_8.TabIndex = 9;
            // 
            // ACPropertyBox_7
            // 
            ACPropertyBox_7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_7.Location = new Point(260, 234);
            ACPropertyBox_7.Name = "ACPropertyBox_7";
            ACPropertyBox_7.Size = new Size(65, 27);
            ACPropertyBox_7.TabIndex = 8;
            // 
            // ACPropertyBox_6
            // 
            ACPropertyBox_6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_6.Location = new Point(260, 201);
            ACPropertyBox_6.Name = "ACPropertyBox_6";
            ACPropertyBox_6.Size = new Size(65, 27);
            ACPropertyBox_6.TabIndex = 7;
            // 
            // ACPropertyBox_5
            // 
            ACPropertyBox_5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_5.Location = new Point(260, 168);
            ACPropertyBox_5.Name = "ACPropertyBox_5";
            ACPropertyBox_5.Size = new Size(65, 27);
            ACPropertyBox_5.TabIndex = 6;
            // 
            // ACPropertyBox_4
            // 
            ACPropertyBox_4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_4.Location = new Point(260, 135);
            ACPropertyBox_4.Name = "ACPropertyBox_4";
            ACPropertyBox_4.Size = new Size(65, 27);
            ACPropertyBox_4.TabIndex = 5;
            // 
            // ACPropertyBox_3
            // 
            ACPropertyBox_3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_3.Location = new Point(260, 102);
            ACPropertyBox_3.Name = "ACPropertyBox_3";
            ACPropertyBox_3.Size = new Size(65, 27);
            ACPropertyBox_3.TabIndex = 4;
            // 
            // ACPropertyBox_2
            // 
            ACPropertyBox_2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_2.Location = new Point(260, 69);
            ACPropertyBox_2.Name = "ACPropertyBox_2";
            ACPropertyBox_2.Size = new Size(65, 27);
            ACPropertyBox_2.TabIndex = 3;
            // 
            // ACPropertyBox_1
            // 
            ACPropertyBox_1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_1.Location = new Point(260, 36);
            ACPropertyBox_1.Name = "ACPropertyBox_1";
            ACPropertyBox_1.Size = new Size(65, 27);
            ACPropertyBox_1.TabIndex = 2;
            // 
            // ACPropertyBox_0
            // 
            ACPropertyBox_0.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ACPropertyBox_0.Location = new Point(260, 3);
            ACPropertyBox_0.Name = "ACPropertyBox_0";
            ACPropertyBox_0.Size = new Size(65, 27);
            ACPropertyBox_0.TabIndex = 1;
            // 
            // eggsPicturePNG
            // 
            eggsPicturePNG.Location = new Point(0, 0);
            eggsPicturePNG.Name = "eggsPicturePNG";
            eggsPicturePNG.Size = new Size(56, 55);
            eggsPicturePNG.TabIndex = 0;
            eggsPicturePNG.TabStop = false;
            // 
            // effectsTabPage3
            // 
            effectsTabPage3.Controls.Add(checkBoxObject);
            effectsTabPage3.Controls.Add(checkBoxFog);
            effectsTabPage3.Controls.Add(checkBoxIndoors);
            effectsTabPage3.Controls.Add(checkBoxWater);
            effectsTabPage3.Location = new Point(4, 29);
            effectsTabPage3.Name = "effectsTabPage3";
            effectsTabPage3.Padding = new Padding(3);
            effectsTabPage3.Size = new Size(334, 800);
            effectsTabPage3.TabIndex = 3;
            effectsTabPage3.Text = "Tile Effects";
            effectsTabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxObject
            // 
            checkBoxObject.AutoSize = true;
            checkBoxObject.Location = new Point(6, 96);
            checkBoxObject.Name = "checkBoxObject";
            checkBoxObject.Size = new Size(75, 24);
            checkBoxObject.TabIndex = 3;
            checkBoxObject.Text = "Object";
            checkBoxObject.UseVisualStyleBackColor = true;
            checkBoxObject.CheckedChanged += checkBoxObject_CheckedChanged;
            // 
            // checkBoxFog
            // 
            checkBoxFog.AutoSize = true;
            checkBoxFog.Location = new Point(6, 66);
            checkBoxFog.Name = "checkBoxFog";
            checkBoxFog.Size = new Size(56, 24);
            checkBoxFog.TabIndex = 2;
            checkBoxFog.Text = "Fog";
            checkBoxFog.UseVisualStyleBackColor = true;
            checkBoxFog.CheckedChanged += checkBoxFog_CheckedChanged;
            // 
            // checkBoxIndoors
            // 
            checkBoxIndoors.AutoSize = true;
            checkBoxIndoors.Location = new Point(6, 36);
            checkBoxIndoors.Name = "checkBoxIndoors";
            checkBoxIndoors.Size = new Size(81, 24);
            checkBoxIndoors.TabIndex = 1;
            checkBoxIndoors.Text = "Indoors";
            checkBoxIndoors.UseVisualStyleBackColor = true;
            checkBoxIndoors.CheckedChanged += checkBoxIndoors_CheckedChanged;
            // 
            // checkBoxWater
            // 
            checkBoxWater.AutoSize = true;
            checkBoxWater.Location = new Point(6, 6);
            checkBoxWater.Name = "checkBoxWater";
            checkBoxWater.Size = new Size(70, 24);
            checkBoxWater.TabIndex = 0;
            checkBoxWater.Text = "Water";
            checkBoxWater.UseVisualStyleBackColor = true;
            checkBoxWater.CheckedChanged += checkBoxWater_CheckedChanged;
            // 
            // PanelMonoWindow
            // 
            PanelMonoWindow.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PanelMonoWindow.BorderStyle = BorderStyle.FixedSingle;
            PanelMonoWindow.Controls.Add(MonoWindow);
            PanelMonoWindow.Location = new Point(3, 3);
            PanelMonoWindow.Name = "PanelMonoWindow";
            PanelMonoWindow.Size = new Size(1130, 833);
            PanelMonoWindow.TabIndex = 5;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { informationField });
            statusStrip1.Location = new Point(0, 907);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1488, 26);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // informationField
            // 
            informationField.Name = "informationField";
            informationField.Size = new Size(151, 20);
            informationField.Text = "toolStripStatusLabel1";
            // 
            // mainSplitContainer1
            // 
            mainSplitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainSplitContainer1.Location = new Point(0, 68);
            mainSplitContainer1.Name = "mainSplitContainer1";
            // 
            // mainSplitContainer1.Panel1
            // 
            mainSplitContainer1.Panel1.Controls.Add(mainTollbar);
            // 
            // mainSplitContainer1.Panel2
            // 
            mainSplitContainer1.Panel2.Controls.Add(PanelMonoWindow);
            mainSplitContainer1.Size = new Size(1488, 839);
            mainSplitContainer1.SplitterDistance = 348;
            mainSplitContainer1.TabIndex = 7;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1488, 933);
            Controls.Add(mainSplitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(2560, 1440);
            MinimumSize = new Size(800, 600);
            Name = "EditForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Divinity Editor";
            Activated += EditForm_Activated;
            Deactivate += EditForm_Deactivate;
            Load += Form2_Load;
            Shown += EditForm_Shown;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            mainTollbar.ResumeLayout(false);
            texturesTabPage1.ResumeLayout(false);
            texturesSplitContainer2.Panel1.ResumeLayout(false);
            texturesSplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)texturesSplitContainer2).EndInit();
            texturesSplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)texturesBox1).EndInit();
            objectsTabPage2.ResumeLayout(false);
            splitObjectsContainer.Panel1.ResumeLayout(false);
            splitObjectsContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitObjectsContainer).EndInit();
            splitObjectsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)objectsPictureBox).EndInit();
            eggsTabPage3.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)eggsPicturePNG).EndInit();
            effectsTabPage3.ResumeLayout(false);
            effectsTabPage3.PerformLayout();
            PanelMonoWindow.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            mainSplitContainer1.Panel1.ResumeLayout(false);
            mainSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer1).EndInit();
            mainSplitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controls.MGGraphicalOutput MonoWindow;
        private ToolStrip toolStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private TabControl mainTollbar;
        private TabPage texturesTabPage1;
        private TabPage objectsTabPage2;
        private Panel PanelMonoWindow;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem openWorldToolStripMenuItem;
        private ToolStripMenuItem saveWorldToolStripMenuItem;
        private ToolStripStatusLabel informationField;
        private TabPage eggsTabPage3;
        private SplitContainer mainSplitContainer1;
        private SplitContainer texturesSplitContainer2;
        private TreeView texturesTreeView;
        private SplitContainer splitObjectsContainer;
        private PictureBox texturesBox1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem gameFolderToolStripMenuItem;
        private TabPage effectsTabPage3;
        private CheckBox checkBoxIndoors;
        private CheckBox checkBoxWater;
        private CheckBox checkBoxFog;
        private CheckBox checkBoxObject;
        private ToolStripTextBox CursorXCor;
        private ToolStripTextBox CursorYCor;
        private ToolStripButton setCursor;
        private TreeView ObjectsTreeView;
        private ListBox objectsBox;
        private PictureBox objectsPictureBox;
        private ToolStripButton StepObjectButton;
        private ToolStripButton TESTBUTTON;
        private ToolStripMenuItem saveWorldImageToolStripMenuItem;
        private ToolStripMenuItem saveMetaObjectsImageToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripButton SearchObjectButton;
        private SplitContainer splitContainer1;
        private ListBox EggsListBox;
        private PictureBox eggsPicturePNG;
        private TextBox ACPropertyBox_5;
        private TextBox ACPropertyBox_4;
        private TextBox ACPropertyBox_3;
        private TextBox ACPropertyBox_2;
        private TextBox ACPropertyBox_1;
        private TextBox ACPropertyBox_0;
        private TextBox ACPropertyBox_9;
        private TextBox ACPropertyBox_8;
        private TextBox ACPropertyBox_7;
        private TextBox ACPropertyBox_6;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button button1;
    }
}