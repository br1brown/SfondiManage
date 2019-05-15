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

namespace SfondiManage
{
    public partial class Form1 : Form
    {
        public static TEMA ATTIVO;

        private static TEMA[] temi = {
            new TEMA("BLU")
            {
                MAIN = Color.LightBlue,
                BTN = Color.AliceBlue,
                PANEL = Color.CadetBlue,
                ITEM = Color.LightBlue
            },
            new TEMA("GIALLO")
            {
                MAIN = SystemColors.Info,
                BTN = Color.DarkKhaki,
                ITEM = Color.FromArgb(239, 234, 211),
                PANEL = Color.Khaki
            },
            new TEMA ("VERDE")
            {
                MAIN = Color.LightGreen,
                BTN = Color.LightSeaGreen,
                PANEL = Color.GreenYellow,
                ITEM = Color.LightGreen
            } };
        private void SetTema(string nome)
        {
            var t = temi.ToList().Where(f => f.NOME.Equals(nome.ToUpper())).FirstOrDefault();
            if (t == null)
                return;
            ATTIVO = t;

            BackColor = t.MAIN;

            button1.BackColor = t.BTN;
            button2.BackColor = t.BTN;
            drpAttivo.BackColor = t.BTN;
            drpdGrandezza.BackColor = t.BTN;
            combob.BackColor = t.BTN;

            flowLayoutPanel1.BackColor = t.PANEL;

            ITMES.ForEach(r =>
            {
                if (!r.IsDisposed)
                    r.BackColor = t.ITEM;
            });
            File.WriteAllText(Out.FileTEMA, nome.ToUpper());

        }

        public static bool sempreattivo = Utility.isAvviaSempre();
        private static bool chiudu = false;
        public Form1()
        {
            lora = new List<DateTimePicker>();
            lcose = new List<TextBox>();
            lcombo = new List<ComboBox>();
            ITMES = new List<GroupBox>();
            lCh = new List<CheckBox>();

            InitializeComponent();
            if (!Directory.Exists(Out.DirRuleBG))
                Directory.CreateDirectory(Out.DirRuleBG);
            if (!File.Exists(Out.FileTEMA))
                File.WriteAllText(Out.FileTEMA, "BLU");
            if (!File.Exists(Out.FileGrandezzaItem))
                File.WriteAllText(Out.FileGrandezzaItem, "Piccoli");

            drpdGrandezza.SelectedItem = Out.leggiTXT(Out.FileGrandezzaItem).First();
            combob.Items.AddRange(temi.ToList().Select(asdf => asdf.NOME).ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Utility.avviasempre(sempreattivo);

            var temaora = (Out.leggiTXT(Out.FileTEMA).First());

            combob.SelectedItem = temaora;

            if (sempreattivo)
                drpAttivo.SelectedIndex = 0;
            else
                drpAttivo.SelectedIndex = 1;

        }

        private void Ricarica()
        {
            flowLayoutPanel1.Controls.Clear();
            lora.Clear();
            lcose.Clear();
            lcombo.Clear();
            ITMES.Clear();

            if (File.Exists(Out.FileRuleBG))
            {
                Enabled = false;
                var Inorder = new List<RuleBG>();
                foreach (var sv in Out.leggiTXT(Out.FileRuleBG))
                {
                    try
                    {
                        Inorder.Add(new RuleBG(sv));
                    }
                    catch (Exception ef)
                    {
                        if (Out.IS_ON_NET)
                            MessageBox.Show(ef.Message, "Problema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            MessageBox.Show("Connsesisone internet assente! alcune immagini potrebbero andare perse\nIl programma verrà chiuso!", "Problema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            chiudu = true;
                            Application.Exit();
                        }

                    }
                }
                Inorder = Inorder.OrderBy(f => f.MinutiOra).ToList();
                if (Inorder.Count > 0)
                    Task.Run(() =>
                    {
                        RuleBG.Attuale(Inorder).Set();
                    });

                Inorder.ForEach(sv => { addRuleBG(sv); });

                Enabled = true;
                flowLayoutPanel1.Update();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!chiudu)
            {
                e.Cancel = true;
                nasconditi(true);
            }

        }

        private void notProp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            nasconditi(false);
        }

        public void nasconditi(bool sino)
        {
            notProp.Visible = sino;
            if (sino)
            {
                Hide();
            }
            else
            {
                //Ricarica();
                Show();
                TopMost = true;
                Select();
                TopMost = false;
                flowLayoutPanel1.Focus();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Second == 0)
            {
                var notInorder = new List<RuleBG>();
                foreach (var sv in Out.leggiTXT(Out.FileRuleBG))
                {
                    try
                    {
                        var kk = new RuleBG(sv);
                        if (DateTime.Now.Hour == kk.Ora && DateTime.Now.Minute == kk.Minuti && DateTime.Now.Second == 0)
                            kk.Set();
                    }
                    catch
                    {
                    }

                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            nasconditi(sempreattivo);
            //Refresh();
            drpdGrandezza.SelectedIndexChanged += drpdGrandezza_SelectedIndexChanged;
            Ricarica();
        }

        private void drpAttivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            sempreattivo = drpAttivo.SelectedIndex == 0;
            Task.Run(() =>
            {
                Utility.avviasempre(sempreattivo);
            });

        }

        private void eSCIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chiudu = true;
            Application.Exit();
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Focus();
        }

        private void combob_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTema(combob.SelectedItem.ToString());
            flowLayoutPanel1.Focus();
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                bool all = true;
                foreach (var ss in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (!Out.IMGAvalable(ss, true))
                        all = false;
                }
                if (all)
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            var d = DateTime.Now;
            foreach (var ss in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                addRuleBG(
                    h: d.Hour,
                    m: d.Minute,
                    cosa: ss);
                d = d.AddMinutes(10);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Ricarica();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                nasconditi(true);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            lCh.ForEach(ch =>
            {
                if (!ch.IsDisposed)
                {
                    if (checkIMG.CheckState != CheckState.Indeterminate)
                        ch.Checked = checkIMG.Checked;
                }
            });
            flowLayoutPanel1.Focus();
        }

        private void flowLayoutPanel1_Controls(object sender, ControlEventArgs e)
        {
            lTot.Text = flowLayoutPanel1.Controls.Count.ToString();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnaddRuleBG_Click(null, null);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ITMES.ForEach(r => { r.Dispose(); });
            flowLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(new ComponentResourceManager(typeof(Form1)).GetObject("flowLayoutPanel1.BackgroundImage")));
        }

        private int getLarghezza()
        {
            switch (drpdGrandezza.SelectedItem.ToString())
            {
                case "Larghi":
                    return 950;
                case "Piccoli":
                    return 350;
            }
            return 650;
        }

        private void drpdGrandezza_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ricarica();
            File.WriteAllText(Out.FileGrandezzaItem, drpdGrandezza.SelectedItem.ToString());

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
