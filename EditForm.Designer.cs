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
            toolStripButton1 = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            CursorYCor = new ToolStripTextBox();
            CursorXCor = new ToolStripTextBox();
            setCursor = new ToolStripButton();
            DelObjectBut = new ToolStripButton();
            CopyObjButton = new ToolStripButton();
            FixObjectButton = new ToolStripButton();
            StepObjectButton = new ToolStripButton();
            TESTBUTTON = new ToolStripButton();
            AddNewObjectButton = new ToolStripButton();
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
            MonoWindow.Size = new Size(1116, 649);
            MonoWindow.TabIndex = 1;
            MonoWindow.Text = "MonoWindow";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(30, 30);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripLabel1, toolStripButton2, toolStripButton3, toolStripButton4, CursorYCor, CursorXCor, setCursor, DelObjectBut, CopyObjButton, FixObjectButton, StepObjectButton, TESTBUTTON, AddNewObjectButton, SearchObjectButton });
            toolStrip1.Location = new Point(0, 28);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1488, 37);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(34, 34);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(111, 34);
            toolStripLabel1.Text = "toolStripLabel1";
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = DivEditor.Properties.Resources.texBig;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(34, 34);
            toolStripButton2.Text = "toolStripButton2";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = DivEditor.Properties.Resources.texSmall;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(34, 34);
            toolStripButton3.Text = "toolStripButton3";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.BackColor = SystemColors.Control;
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = DivEditor.Properties.Resources.texRan;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(34, 34);
            toolStripButton4.Text = "toolStripButton4";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // CursorYCor
            // 
            CursorYCor.Alignment = ToolStripItemAlignment.Right;
            CursorYCor.Name = "CursorYCor";
            CursorYCor.Size = new Size(50, 37);
            // 
            // CursorXCor
            // 
            CursorXCor.Alignment = ToolStripItemAlignment.Right;
            CursorXCor.Name = "CursorXCor";
            CursorXCor.Size = new Size(50, 37);
            // 
            // setCursor
            // 
            setCursor.Alignment = ToolStripItemAlignment.Right;
            setCursor.BackColor = SystemColors.ControlLight;
            setCursor.DisplayStyle = ToolStripItemDisplayStyle.Text;
            setCursor.Image = (Image)resources.GetObject("setCursor.Image");
            setCursor.ImageTransparentColor = Color.Magenta;
            setCursor.Name = "setCursor";
            setCursor.Size = new Size(32, 34);
            setCursor.Text = "Go";
            setCursor.Click += ToolStripButton5_Click;
            // 
            // DelObjectBut
            // 
            DelObjectBut.DisplayStyle = ToolStripItemDisplayStyle.Text;
            DelObjectBut.Enabled = false;
            DelObjectBut.Image = (Image)resources.GetObject("DelObjectBut.Image");
            DelObjectBut.ImageTransparentColor = Color.Magenta;
            DelObjectBut.Name = "DelObjectBut";
            DelObjectBut.Size = new Size(36, 34);
            DelObjectBut.Text = "Del";
            DelObjectBut.Visible = false;
            // 
            // CopyObjButton
            // 
            CopyObjButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            CopyObjButton.Enabled = false;
            CopyObjButton.Image = (Image)resources.GetObject("CopyObjButton.Image");
            CopyObjButton.ImageTransparentColor = Color.Magenta;
            CopyObjButton.Name = "CopyObjButton";
            CopyObjButton.Size = new Size(47, 34);
            CopyObjButton.Text = "Copy";
            CopyObjButton.Visible = false;
            // 
            // FixObjectButton
            // 
            FixObjectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            FixObjectButton.Enabled = false;
            FixObjectButton.Image = (Image)resources.GetObject("FixObjectButton.Image");
            FixObjectButton.ImageTransparentColor = Color.Magenta;
            FixObjectButton.Name = "FixObjectButton";
            FixObjectButton.Size = new Size(31, 34);
            FixObjectButton.Text = "Fix";
            FixObjectButton.Visible = false;
            // 
            // StepObjectButton
            // 
            StepObjectButton.BackColor = SystemColors.Control;
            StepObjectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            StepObjectButton.Image = (Image)resources.GetObject("StepObjectButton.Image");
            StepObjectButton.ImageTransparentColor = Color.Magenta;
            StepObjectButton.Name = "StepObjectButton";
            StepObjectButton.Size = new Size(82, 34);
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
            TESTBUTTON.Size = new Size(101, 34);
            TESTBUTTON.Text = "TESTBUTTON";
            TESTBUTTON.Click += TESTBUTTON_Click;
            // 
            // AddNewObjectButton
            // 
            AddNewObjectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            AddNewObjectButton.Enabled = false;
            AddNewObjectButton.Image = (Image)resources.GetObject("AddNewObjectButton.Image");
            AddNewObjectButton.ImageTransparentColor = Color.Magenta;
            AddNewObjectButton.Name = "AddNewObjectButton";
            AddNewObjectButton.Size = new Size(123, 34);
            AddNewObjectButton.Text = "Add New Object";
            AddNewObjectButton.Visible = false;
            AddNewObjectButton.Click += AddNewObjectButton_Click;
            // 
            // SearchObjectButton
            // 
            SearchObjectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SearchObjectButton.Image = (Image)resources.GetObject("SearchObjectButton.Image");
            SearchObjectButton.ImageTransparentColor = Color.Magenta;
            SearchObjectButton.Name = "SearchObjectButton";
            SearchObjectButton.Size = new Size(103, 34);
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
            mainTollbar.Size = new Size(342, 651);
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
            texturesTabPage1.Size = new Size(334, 618);
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
            texturesSplitContainer2.Size = new Size(328, 612);
            texturesSplitContainer2.SplitterDistance = 419;
            texturesSplitContainer2.TabIndex = 0;
            // 
            // texturesTreeView
            // 
            texturesTreeView.Dock = DockStyle.Fill;
            texturesTreeView.Location = new Point(0, 0);
            texturesTreeView.Name = "texturesTreeView";
            texturesTreeView.Size = new Size(326, 417);
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
            texturesBox1.Size = new Size(326, 187);
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
            objectsTabPage2.Size = new Size(334, 618);
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
            splitObjectsContainer.Size = new Size(328, 612);
            splitObjectsContainer.SplitterDistance = 426;
            splitObjectsContainer.TabIndex = 0;
            // 
            // objectsBox
            // 
            objectsBox.Dock = DockStyle.Right;
            objectsBox.FormattingEnabled = true;
            objectsBox.ItemHeight = 20;
            objectsBox.Location = new Point(177, 0);
            objectsBox.Name = "objectsBox";
            objectsBox.Size = new Size(151, 426);
            objectsBox.TabIndex = 1;
            objectsBox.SelectedIndexChanged += ObjectsBox_SelectedIndexChanged;
            objectsBox.SizeChanged += objectsBox_SizeChanged;
            // 
            // ObjectsTreeView
            // 
            ObjectsTreeView.Location = new Point(0, 0);
            ObjectsTreeView.Name = "ObjectsTreeView";
            ObjectsTreeView.Size = new Size(171, 426);
            ObjectsTreeView.TabIndex = 0;
            ObjectsTreeView.AfterSelect += ObjectsTreeView_AfterSelect;
            ObjectsTreeView.KeyDown += ObjectsTreeView_KeyDown;
            // 
            // objectsPictureBox
            // 
            objectsPictureBox.Dock = DockStyle.Fill;
            objectsPictureBox.Location = new Point(0, 0);
            objectsPictureBox.Name = "objectsPictureBox";
            objectsPictureBox.Size = new Size(328, 182);
            objectsPictureBox.TabIndex = 0;
            objectsPictureBox.TabStop = false;
            // 
            // eggsTabPage3
            // 
            eggsTabPage3.BackColor = SystemColors.Control;
            eggsTabPage3.Location = new Point(4, 29);
            eggsTabPage3.Name = "eggsTabPage3";
            eggsTabPage3.Padding = new Padding(3);
            eggsTabPage3.Size = new Size(334, 618);
            eggsTabPage3.TabIndex = 2;
            eggsTabPage3.Text = "Eggs";
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
            effectsTabPage3.Size = new Size(334, 618);
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
            PanelMonoWindow.Size = new Size(1118, 651);
            PanelMonoWindow.TabIndex = 5;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { informationField });
            statusStrip1.Location = new Point(0, 725);
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
            mainSplitContainer1.Size = new Size(1476, 657);
            mainSplitContainer1.SplitterDistance = 348;
            mainSplitContainer1.TabIndex = 7;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1488, 751);
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
        private ToolStripButton toolStripButton1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripLabel toolStripLabel1;
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
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
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
        private ToolStripButton DelObjectBut;
        private ToolStripButton CopyObjButton;
        private ToolStripButton FixObjectButton;
        private ToolStripButton StepObjectButton;
        private ToolStripButton TESTBUTTON;
        private ToolStripMenuItem saveWorldImageToolStripMenuItem;
        private ToolStripMenuItem saveMetaObjectsImageToolStripMenuItem;
        private ToolStripButton AddNewObjectButton;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripButton SearchObjectButton;
    }
}