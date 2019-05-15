using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SfondiManage
{
    public partial class Form1 : Form
    {
        List<DateTimePicker> lora;
        List<TextBox> lcose;
        List<ComboBox> lcombo;
        List<GroupBox> ITMES;
        List<CheckBox> lCh;

        private void Lbl_Click(object sender, EventArgs e)
        {
            var dad = (sender as Label).Parent.GetHashCode();
            lora.RemoveAll(c => c.Parent.GetHashCode() == dad);
            lcose.RemoveAll(c => c.Parent.GetHashCode() == dad);
            lcombo.RemoveAll(c => c.Parent.GetHashCode() == dad);

            (sender as Label).Parent.Parent.Dispose();
            //(sender as Label).Parent.Dispose();

            if (lcose.Count == 0)
                flowLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(new ComponentResourceManager(typeof(Form1)).GetObject("flowLayoutPanel1.BackgroundImage")));


        }

        private void btnsavesci_Click(object sender, EventArgs e)
        {
            var arrOra = lora.ToArray();
            var arrCose = lcose.ToArray();
            var arrDrop = lcombo.ToArray();
            bool errore = false;
            string szerrore = "";

            List<RuleBG> res = new List<RuleBG>();
            for (int i = 0; i < lcose.Count; i++)
            {
                if (!arrCose[i].Parent.IsDisposed)
                {
                    try
                    {
                        var s = new RuleBG(
                                    arrOra[i].Value.Hour,
                                    arrOra[i].Value.Minute,
                                    arrCose[i].Text,
                                    arrDrop[i].SelectedItem.ToString());

                        res.Add(s);
                    }
                    catch (Exception ee)
                    {
                        szerrore = ee.Message;
                        errore = true;
                    }
                }
            }
            if (!errore)
            {
                File.Delete(Out.FileRuleBG);
                if (res.Count > 0)
                {
                    File.WriteAllLines(Out.FileRuleBG, res.Select(r => r.ToString()));
                    Task.Run(() =>
                    {
                        RuleBG.Attuale(res).Set();
                    });
                }

                nasconditi(true);
            }
            else
                MessageBox.Show(szerrore, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnaddRuleBG_Click(object sender, EventArgs e)
        {
            addRuleBG(DateTime.Now.Hour, DateTime.Now.Minute);
        }

        private void addRuleBG(RuleBG svgl)
        {
            addRuleBG(svgl.Ora, svgl.Minuti, cosa: svgl.IMGPath, stile: svgl.FormatStile);
        }

        private void addRuleBG(int h, int m, string stile = null, string cosa = "")
        {

            var p = new FlowLayoutPanel();

            var grpb = new GroupBox();
            grpb.BackColor = ATTIVO.ITEM;
            ITMES.Add(grpb);
            grpb.Text = "Cambio sfondo";

            var tm = new DateTimePicker();
            tm.Format = DateTimePickerFormat.Custom;
            tm.CustomFormat = "HH:mm";
            tm.ShowUpDown = true;
            tm.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, h, m, 0);
            lora.Add(tm);

            var img = new PictureBox();
            img.SizeMode = PictureBoxSizeMode.Zoom;
            img.BorderStyle = BorderStyle.FixedSingle;

            var txtb = new TextBox();
            lcose.Add(txtb);
            txtb.Anchor = (AnchorStyles.Left | AnchorStyles.Right);

            var btnb = new Button();
            btnb.Text = "Sfoglia";
            btnb.Anchor = (AnchorStyles.Left | AnchorStyles.Right);

            var drpdL = new ComboBox();
            lcombo.Add(drpdL);
            drpdL.FlatStyle = FlatStyle.Flat;
            drpdL.DropDownStyle = ComboBoxStyle.DropDownList;
            drpdL.Items.AddRange(new string[] { "Adatta", "Centrato", "Ripetuto" });

            if (stile == null)
                drpdL.SelectedIndex = 0;
            else
                drpdL.SelectedItem = stile;
            drpdL.Anchor = (AnchorStyles.Left | AnchorStyles.Right);

            var drpdlink = new ComboBox();
            drpdlink.FlatStyle = FlatStyle.Flat;
            drpdlink.DropDownStyle = ComboBoxStyle.DropDownList;
            drpdlink.Items.AddRange(new string[] { "Immagine online", "Immagine su computer" });

            var lbl = new Label();
            lbl.Text = "X";
            lbl.ForeColor = Color.Red;

            var chImg = new CheckBox();
            chImg.Text = "Immagine";
            chImg.Anchor = (AnchorStyles.Left);
            chImg.Checked = true;
            lCh.Add(chImg);

            var btndup = new Button() { Text = "Duplica" };

            chImg.CheckedChanged += (sender, EventArgs) => { check_CheckedChanged(sender, EventArgs, img); };
            btnb.Click += (sender, EventArgs) => { Scegli_Click(sender, EventArgs, txtb); };
            img.DoubleClick += (sender, EventArgs) => { PictureBox_dClick(sender, EventArgs); };

            drpdlink.SelectedIndexChanged += (sender, EventArgs) => { Switch_Click(sender, EventArgs, txtb, btnb); };
            lbl.Click += Lbl_Click;
            txtb.TextChanged += (sender, EventArgs) => { txt_Click(sender, EventArgs, img, drpdlink); };
            txtb.DoubleClick += (sender, EventArgs) => { txt_SelectAll(sender, EventArgs); };
            txtb.Text = cosa;
            btndup.Click += (sender, EventArgs) => { DUplica(sender, EventArgs, txtb, tm, drpdL); };

            if (cosa == "" || (Out.IsLocalFile(txtb.Text)))
                drpdlink.SelectedIndex = 1;
            else
                drpdlink.SelectedIndex = 0;


            //--------------------------
            //p.Width = flowLayoutPanel1.Width - 50;
            p.Width = getLarghezza();
            grpb.Width = p.Width + 6;

            btndup.Height = 30;
            btndup.Width = p.Width - (btndup.Margin.Left * 2);

            img.Size = new Size(p.Width - 20, ((p.Width - 40) * Utility.RadioScreen.Y) / Utility.RadioScreen.X);
            var latPAD = (p.Width - img.Width) / 2;
            img.Margin = new Padding(img.Margin.Left + latPAD, img.Margin.Top, img.Margin.Right + latPAD, img.Margin.Bottom);

            p.Height = 80 + img.Height + btndup.Height;

            grpb.Height = p.Height + 20;

            p.Top = grpb.Height - p.Height;
            p.Left = (grpb.Width - p.Width) / 2;

            grpb.Margin = new Padding(grpb.Margin.Left + 1, grpb.Margin.Top + 5, grpb.Margin.Right + 1, grpb.Margin.Bottom + 5);

            btnb.Width = 90;
            btnb.Height = 30;
            chImg.Width = btnb.Width;

            lbl.Top = 70;
            lbl.Width = 20;

            txtb.Width = p.Width - lbl.Width - tm.Width;

            drpdL.Width = 80;
            drpdL.Margin = new Padding(lbl.Width + 10, drpdL.Margin.Top, drpdL.Margin.Right, drpdL.Margin.Bottom);

            drpdlink.Width = txtb.Width;

            tm.Width = drpdL.Width;

            p.BorderStyle = BorderStyle.Fixed3D;

            p.Controls.Add(lbl);
            p.Controls.Add(tm);
            p.WrapContents = true;
            p.Controls.Add(txtb);
            p.Controls.Add(btnb);
            p.Controls.Add(drpdL);
            p.Controls.Add(drpdlink);
            p.Controls.Add(chImg);
            p.Controls.Add(img);
            p.Controls.Add(btndup);

            grpb.Controls.Add(p);
            grpb.Anchor = (AnchorStyles.Left | AnchorStyles.Right);

            chImg.Checked = checkIMG.Checked;

            flowLayoutPanel1.BackgroundImage = null;
            flowLayoutPanel1.Controls.Add(grpb);
            flowLayoutPanel1.Focus();
            flowLayoutPanel1.ScrollControlIntoView(grpb);
        }

        void txt_SelectAll(object sender, EventArgs eventArgs)
        {
            if ((sender as TextBox).ReadOnly)
                return;
            (sender as TextBox).SelectAll();
            (sender as TextBox).Focus();

        }

        void txt_Click(object sender, EventArgs e, PictureBox img, ComboBox drpd)
        {
            //Task.Run(() =>
            //{
            try
            {
                img.ImageLocation = ((sender as TextBox).Text);
                img.Refresh();

                Image otima = null;
                if (Out.IsLocalFile(img.ImageLocation))
                    otima = Image.FromFile(img.ImageLocation);
                else
                    if (Out.URLExists(img.ImageLocation))
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        otima = Image.FromStream(new MemoryStream(wc.DownloadData(img.ImageLocation)));
                    }
                if (otima != null)
                    Task.Run(() =>
                    {
                        img.BackColor = PictureAnalysis.GetMostUsedColor(otima).MostUsedColor;
                    });
            }
            catch { }
            //});
        }

        void PictureBox_dClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start((sender as PictureBox).ImageLocation);
            }
            catch { }
        }

        void Scegli_Click(object sender, EventArgs e, TextBox index)
        {
            using (OpenFileDialog browser = new OpenFileDialog())
            {
                browser.Title = "Scegli sfondo";
                browser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                browser.Filter = "Immagini|*.jpg;*.jpeg;*.bmp;*.png;";

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    index.Text = browser.FileName;
                }

            }
            flowLayoutPanel1.Focus();

        }

        void Switch_Click(object sender, EventArgs e, TextBox index, Button sfoglia)
        {
            var isLINK = ((sender as ComboBox).SelectedIndex == 0);

            sfoglia.Enabled = !isLINK;
            index.ReadOnly = !isLINK;

            flowLayoutPanel1.Focus();

        }

        private void check_CheckedChanged(object sender, EventArgs e, PictureBox i)
        {
            var s = sender as CheckBox;
            i.Visible = s.Checked;
            if (s.Checked)
            {
                i.Parent.Height += i.Height;
                i.Parent.Parent.Height += i.Height;
            }
            else
            {
                i.Parent.Height -= i.Height;
                i.Parent.Parent.Height -= i.Height;
            }

            if (s.Checked != checkIMG.Checked)
                checkIMG.CheckState = CheckState.Indeterminate;

        }

        void DUplica(object sender, EventArgs e, TextBox index, DateTimePicker ora, ComboBox stile)
        {
            addRuleBG(ora.Value.Hour, ora.Value.Minute, stile.SelectedItem.ToString(), index.Text);
        }

    }
}
