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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openWorldToolStripMenuItem = new ToolStripMenuItem();
            saveWorldToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            gameFolderToolStripMenuItem = new ToolStripMenuItem();
            mainTollbar = new TabControl();
            texturesTabPage1 = new TabPage();
            texturesSplitContainer2 = new SplitContainer();
            texturesTreeView1 = new TreeView();
            texturesBox1 = new PictureBox();
            objectsTabPage2 = new TabPage();
            splitContainer1 = new SplitContainer();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
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
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripLabel1, toolStripButton2, toolStripButton3, toolStripButton4 });
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
            toolStripButton4.BackColor = Color.WhiteSmoke;
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = DivEditor.Properties.Resources.texRan;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(34, 34);
            toolStripButton4.Text = "toolStripButton4";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1488, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openWorldToolStripMenuItem, saveWorldToolStripMenuItem, settingsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(44, 24);
            fileToolStripMenuItem.Text = "file";
            // 
            // openWorldToolStripMenuItem
            // 
            openWorldToolStripMenuItem.Name = "openWorldToolStripMenuItem";
            openWorldToolStripMenuItem.Size = new Size(172, 26);
            openWorldToolStripMenuItem.Text = "Open World";
            openWorldToolStripMenuItem.Click += openWorldToolStripMenuItem_Click;
            // 
            // saveWorldToolStripMenuItem
            // 
            saveWorldToolStripMenuItem.Name = "saveWorldToolStripMenuItem";
            saveWorldToolStripMenuItem.Size = new Size(172, 26);
            saveWorldToolStripMenuItem.Text = "Save World";
            saveWorldToolStripMenuItem.Click += saveWorldToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gameFolderToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(172, 26);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // gameFolderToolStripMenuItem
            // 
            gameFolderToolStripMenuItem.Name = "gameFolderToolStripMenuItem";
            gameFolderToolStripMenuItem.Size = new Size(175, 26);
            gameFolderToolStripMenuItem.Text = "Game folder";
            gameFolderToolStripMenuItem.Click += gameFolderToolStripMenuItem_Click;
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
            texturesSplitContainer2.Panel1.Controls.Add(texturesTreeView1);
            // 
            // texturesSplitContainer2.Panel2
            // 
            texturesSplitContainer2.Panel2.Controls.Add(texturesBox1);
            texturesSplitContainer2.Size = new Size(328, 612);
            texturesSplitContainer2.SplitterDistance = 419;
            texturesSplitContainer2.TabIndex = 0;
            // 
            // texturesTreeView1
            // 
            texturesTreeView1.Dock = DockStyle.Fill;
            texturesTreeView1.Location = new Point(0, 0);
            texturesTreeView1.Name = "texturesTreeView1";
            texturesTreeView1.Size = new Size(326, 417);
            texturesTreeView1.TabIndex = 0;
            texturesTreeView1.AfterSelect += texturesTreeView1_AfterSelect;
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
            objectsTabPage2.Controls.Add(splitContainer1);
            objectsTabPage2.Location = new Point(4, 29);
            objectsTabPage2.Name = "objectsTabPage2";
            objectsTabPage2.Padding = new Padding(3);
            objectsTabPage2.Size = new Size(334, 618);
            objectsTabPage2.TabIndex = 1;
            objectsTabPage2.Text = "Objects";
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
            splitContainer1.Panel1.Controls.Add(comboBox4);
            splitContainer1.Panel1.Controls.Add(comboBox3);
            splitContainer1.Panel1.Controls.Add(comboBox2);
            splitContainer1.Panel1.Controls.Add(comboBox1);
            splitContainer1.Size = new Size(328, 612);
            splitContainer1.SplitterDistance = 426;
            splitContainer1.TabIndex = 0;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(165, 37);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(160, 28);
            comboBox4.TabIndex = 3;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(3, 37);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(160, 28);
            comboBox3.TabIndex = 2;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(165, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(160, 28);
            comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(3, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(160, 28);
            comboBox1.TabIndex = 0;
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
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(2560, 1440);
            MinimumSize = new Size(800, 600);
            Name = "EditForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Divinity Editor";
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
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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
        private TreeView texturesTreeView1;
        private SplitContainer splitContainer1;
        private PictureBox texturesBox1;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
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
    }
}