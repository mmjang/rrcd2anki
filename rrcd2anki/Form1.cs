using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;

namespace rrcd2anki
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
        public Form1()
        {
            InitializeComponent();
            //////////////////////
            string line;
            var file = new StreamReader("forms-EN.txt");
            char[] ch = new char[2];
            ch[0] = ':';
            ch[1] = ',';
            while ((line = file.ReadLine()) != null)
            {
                string[] arr = line.Split(ch);
                string key = arr[0];
                var value = new List<string>();
                for (int i = 1; i < arr.Length; i++)
                {
                    value.Add(arr[i].Trim());
                }
                dict[key.Trim()] = value;
            }
            file.Close();
            ///////////////////////
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            var source = new AutoCompleteStringCollection();
            source.AddRange(dict.Keys.ToArray());
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            /////////////////////
            //string settings = "settings.json";
            //if (!File.Exists(settings))
            //{
            //    File.WriteAllText("settings.json", "{\"output_dir\":\"C:\\\"}");
            //}

            //string userPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            //if (Environment.OSVersion.Version.Major >= 6)
            //{
            //    userPath = Directory.GetParent(userPath).ToString();
            //}

            //if (!Directory.Exists(userPath + "\\Documents\\Anki"))
            //{
            //    return;
            //}

            //string[] subDirs = Directory.GetDirectories(userPath + "\\Documents\\Anki");
            //if (subDirs.Length != 2)
            //{
            //    return;
            //}

            //string mediaDir;
            //if (subDirs[0].Substring(subDirs[0].Length - 6) == "addons")
            //{
            //    mediaDir = subDirs[1];
            //}
            //else
            //{
            //    mediaDir = subDirs[0];
            //}

            //if (Directory.Exists(mediaDir + "\\collection.media"))
            //{
            //    button5.Text = mediaDir + "\\collection.media";
            //    folderBrowserDialog1.SelectedPath = mediaDir + "\\collection.media";
            //}

            //  if(subDirs[0] == "addons")
        }

        private void WordInput_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();
            if (!dict.ContainsKey(text))
            {
                comboBox1.Items.Clear();
                comboBox1.Enabled = false;
                return;
            }
            dynamic value = dict[text];
            comboBox1.Enabled = true;
            comboBox1.Items.Clear();
            foreach (string i in value)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////////////////////////////
            string text = textBox1.Text;
            Search(text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SubShow.CurrentSub > 0)
            {
                SubShow.CurrentSub = SubShow.CurrentSub - 1;
                SubShow.Show(pictureBox1, label2, label3, label4, label5);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int len = SubShow.CurrentWord.MySub.Count;
            if (SubShow.CurrentSub < len - 1)
            {
                SubShow.CurrentSub = SubShow.CurrentSub + 1;
                SubShow.Show(pictureBox1, label2, label3, label4, label5);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var sub = SubShow.CurrentWord.MySub[SubShow.CurrentSub];
            var mp3url = sub.SubAudio;
            var submp3 = new SubMp3(mp3url);
            submp3.Play();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var sub = SubShow.CurrentWord.MySub[SubShow.CurrentSub];
                string tempPath = Path.GetTempPath();
                string url = sub.SubAudio;
                Uri uri = new Uri(url);
                var mp3Base = uri.Segments[uri.Segments.Length - 1];
                var mp3InTemp = Path.Combine(tempPath, mp3Base);
                if (!File.Exists(mp3InTemp))
                {
                    var webClient = new WebClient();
                    webClient.DownloadFile(url, mp3InTemp);
                }

                var sc = new System.Collections.Specialized.StringCollection();
                sc.Add(mp3InTemp);
                Clipboard.SetFileDropList(sc);
                System.Media.SystemSounds.Exclamation.Play();
            }
            catch (Exception)
            {
                MessageBox.Show("你疯了吧");
            }
            //if (folderBrowserDialog1.SelectedPath == "")
            //{
            //    MessageBox.Show("还未选择媒体文件夹");
            //}
            //else
            //{
            //    var sub = SubShow.CurrentWord.MySub[SubShow.CurrentSub];
            //    string mediaPath = folderBrowserDialog1.SelectedPath;
            //    pictureBox1.Image.Save(Path.Combine(mediaPath, SubShow.GetMp3BaseName() + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            //    string url = sub.SubAudio;
            //    string tempPath = Path.GetTempPath();
            //    Uri uri = new Uri(url);
            //    var mp3Base = uri.Segments[uri.Segments.Length - 1];
            //    var mp3InTemp = Path.Combine(tempPath, mp3Base);
            //    if (!File.Exists(mp3InTemp))
            //    {
            //        var webClient = new WebClient();
            //        webClient.DownloadFile(url, mp3InTemp);
            //    }

            //    File.Copy(mp3InTemp, Path.Combine(mediaPath, SubShow.GetMp3BaseName()), true);

            //    //Clipboard
            //    string forClipBoard = "";
            //    forClipBoard += "![](" + SubShow.GetMp3BaseName() + ".jpg" + ")";
            //    forClipBoard += "\n\n";
            //    forClipBoard += "[sound:" + mp3Base + "]";
            //    forClipBoard += "";
            //    forClipBoard += "\n\n";
            //    forClipBoard += "" + sub.SubEN + "";
            //    forClipBoard += "" + sub.SubCN + "";
            //    Clipboard.SetText(forClipBoard);
            //    MessageBox.Show("保存成功, 且以下内容已复制到剪切板\n" + forClipBoard);

            //}
        }

        async Task delay(int n)
        {
            await Task.Delay(n);
        }

        private async void label2_DoubleClick(object sender, EventArgs e)
        {
            Label la = (Label)sender;
            Clipboard.SetText(la.Text);
            string ori = la.Text;
            la.Text = "复制成功！";
            await delay(300);
            la.Text = ori;

        }

        private async void label3_DoubleClick(object sender, EventArgs e)
        {
            Label la = (Label)sender;
            Clipboard.SetText(la.Text);
            string ori = la.Text;
            la.Text = "复制成功！";
            await delay(300);
            la.Text = ori;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("双击复制文字", (Label)sender);
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("双击复制文字", (Label)sender);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private async void label5_DoubleClick(object sender, EventArgs e)
        {
            Label la = (Label)sender;
            Clipboard.SetText(la.Text);
            string ori = la.Text;
            la.Text = "复制成功！";
            await delay(300);
            la.Text = ori;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //string text = comboBox1.Text.Trim();
            //dynamic value = dict[text];
            //if (value != null)
            //{
            //    comboBox1.Items.Clear();
            //    foreach (string i in value)
            //    {
            //        comboBox1.Items.Add(i);
            //    }
            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Search(comboBox1.SelectedItem.ToString());
        }

        private void Search(string text)
        {
            if (text == "")
            {
                MessageBox.Show("关键词为空");
            }
            else
            {
                SubSearch subsearch = new SubSearch(text);
                //test
                try
                {
                    Word word = subsearch.getWord();
                    SubShow.CurrentWord = word;
                    SubShow.Show(pictureBox1, label2, label3, label4, label5);
                }
                catch (Exception except)
                {
                    MessageBox.Show(except.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var sub = SubShow.CurrentWord.MySub[SubShow.CurrentSub];
                string tempPath = Path.GetTempPath();
                string url = sub.SubAudio;
                Uri uri = new Uri(url);
                var mp3Base = uri.Segments[uri.Segments.Length - 1];
                var mp3InTemp = Path.Combine(tempPath, mp3Base);
                string picPath = Path.Combine(tempPath, SubShow.GetMp3BaseName() + ".jpg");
                pictureBox1.Image.Save(picPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                var sc = new System.Collections.Specialized.StringCollection();
                sc.Add(picPath);
                Clipboard.SetFileDropList(sc);
                System.Media.SystemSounds.Exclamation.Play();
            }
            catch (Exception)
            {
                MessageBox.Show("你疯了吧");
            }
        }
    }
}
