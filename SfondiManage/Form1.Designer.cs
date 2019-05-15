namespace SfondiManage
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cMenuPan = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.notProp = new System.Windows.Forms.NotifyIcon(this.components);
            this.cMen1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eSCIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.drpAttivo = new System.Windows.Forms.ComboBox();
            this.bw01 = new System.ComponentModel.BackgroundWorker();
            this.combob = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.checkIMG = new System.Windows.Forms.CheckBox();
            this.lTot = new System.Windows.Forms.Label();
            this.drpdGrandezza = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cMenuPan.SuspendLayout();
            this.cMen1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flowLayoutPanel1.BackgroundImage")));
            this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.ContextMenuStrip = this.cMenuPan;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 94);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(773, 442);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Click += new System.EventHandler(this.flowLayoutPanel1_Click);
            this.flowLayoutPanel1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_Controls);
            this.flowLayoutPanel1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_Controls);
            this.flowLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.flowLayoutPanel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // cMenuPan
            // 
            this.cMenuPan.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMenuPan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripMenuItem2});
            this.cMenuPan.Name = "cMenuPan";
            this.cMenuPan.ShowImageMargin = false;
            this.cMenuPan.Size = new System.Drawing.Size(137, 58);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 24);
            this.toolStripMenuItem1.Text = "Aggiungi";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(136, 24);
            this.toolStripMenuItem2.Text = "Elimina Tutti";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(12, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Aggiungi cambio sfondo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnaddRuleBG_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(0, 542);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(797, 60);
            this.button2.TabIndex = 2;
            this.button2.Text = "Imposta";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnsavesci_Click);
            // 
            // notProp
            // 
            this.notProp.ContextMenuStrip = this.cMen1;
            this.notProp.Icon = ((System.Drawing.Icon)(resources.GetObject("notProp.Icon")));
            this.notProp.Text = "Gestione Desktop";
            this.notProp.Visible = true;
            this.notProp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notProp_MouseDoubleClick);
            // 
            // cMen1
            // 
            this.cMen1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMen1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eSCIToolStripMenuItem});
            this.cMen1.Name = "cMen1";
            this.cMen1.Size = new System.Drawing.Size(157, 28);
            // 
            // eSCIToolStripMenuItem
            // 
            this.eSCIToolStripMenuItem.Name = "eSCIToolStripMenuItem";
            this.eSCIToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.eSCIToolStripMenuItem.Text = "Chiudi tutto";
            this.eSCIToolStripMenuItem.Click += new System.EventHandler(this.eSCIToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // drpAttivo
            // 
            this.drpAttivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drpAttivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpAttivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drpAttivo.FormattingEnabled = true;
            this.drpAttivo.Items.AddRange(new object[] {
            "Aperto all\'avvio",
            "Non aperto all\'avvio"});
            this.drpAttivo.Location = new System.Drawing.Point(587, 34);
            this.drpAttivo.Name = "drpAttivo";
            this.drpAttivo.Size = new System.Drawing.Size(198, 24);
            this.drpAttivo.TabIndex = 3;
            this.drpAttivo.SelectedIndexChanged += new System.EventHandler(this.drpAttivo_SelectedIndexChanged);
            // 
            // combob
            // 
            this.combob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.combob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combob.FormattingEnabled = true;
            this.combob.Location = new System.Drawing.Point(663, 4);
            this.combob.Name = "combob";
            this.combob.Size = new System.Drawing.Size(122, 24);
            this.combob.TabIndex = 4;
            this.combob.SelectedIndexChanged += new System.EventHandler(this.combob_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(584, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tema App";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 42);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(119, 17);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Reimposta Sfondi";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // checkIMG
            // 
            this.checkIMG.AutoSize = true;
            this.checkIMG.Checked = true;
            this.checkIMG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIMG.Location = new System.Drawing.Point(15, 67);
            this.checkIMG.Name = "checkIMG";
            this.checkIMG.Size = new System.Drawing.Size(132, 21);
            this.checkIMG.TabIndex = 7;
            this.checkIMG.Text = "Mostra Immagini";
            this.checkIMG.UseVisualStyleBackColor = true;
            this.checkIMG.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lTot
            // 
            this.lTot.AutoSize = true;
            this.lTot.Location = new System.Drawing.Point(223, 11);
            this.lTot.Name = "lTot";
            this.lTot.Size = new System.Drawing.Size(16, 17);
            this.lTot.TabIndex = 8;
            this.lTot.Text = "0";
            // 
            // drpdGrandezza
            // 
            this.drpdGrandezza.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drpdGrandezza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpdGrandezza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drpdGrandezza.FormattingEnabled = true;
            this.drpdGrandezza.Items.AddRange(new object[] {
            "Larghi",
            "Medi",
            "Piccoli"});
            this.drpdGrandezza.Location = new System.Drawing.Point(663, 64);
            this.drpdGrandezza.Name = "drpdGrandezza";
            this.drpdGrandezza.Size = new System.Drawing.Size(122, 24);
            this.drpdGrandezza.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(587, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Larghezza";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 602);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.drpdGrandezza);
            this.Controls.Add(this.lTot);
            this.Controls.Add(this.checkIMG);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combob);
            this.Controls.Add(this.drpAttivo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(4000, 1200);
            this.MinimumSize = new System.Drawing.Size(473, 320);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestione sfondi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.cMenuPan.ResumeLayout(false);
            this.cMen1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NotifyIcon notProp;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox drpAttivo;
        private System.Windows.Forms.ContextMenuStrip cMen1;
        private System.Windows.Forms.ToolStripMenuItem eSCIToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bw01;
        private System.Windows.Forms.ComboBox combob;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox checkIMG;
        private System.Windows.Forms.Label lTot;
        private System.Windows.Forms.ContextMenuStrip cMenuPan;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ComboBox drpdGrandezza;
        private System.Windows.Forms.Label label2;
    }
}

