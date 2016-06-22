using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rrcd2anki
{
    class SubShow
    {
        public static int CurrentSub { get; set; }
        public static int TotalSub { get; set; }
        public static Word currentWord;
        public static Word CurrentWord
        {
            get
            {
                return currentWord;
            }
            set
            {
                currentWord = value;
                //init current sub to 0
                CurrentSub = 0;
                //set total sub
                TotalSub = currentWord.MySub.Count;
            }
        }

        public static void Show(PictureBox pictureBox1, Label label2, Label label3, Label numberLabel,Label label5)
        {
            if(CurrentWord == null)
            {
                throw new Exception("未设置CurrentWord");
            }
            else
            {
                pictureBox1.Load(CurrentWord.MySub[CurrentSub].SubImg);
                label2.Text = CurrentWord.MySub[CurrentSub].SubEN;
                label3.Text = CurrentWord.MySub[CurrentSub].SubCN;
                label5.Text = "——" + currentWord.MySub[CurrentSub].FilmName;
                numberLabel.Text = (CurrentSub + 1) + "/" + TotalSub;
            }
        }

        public static string GetImageBaseName()
        {
            var uri = new Uri(CurrentWord.MySub[CurrentSub].SubImg);
            return uri.Segments.Last();
        }
        public static string GetMp3BaseName()
        {
            var uri = new Uri(CurrentWord.MySub[CurrentSub].SubAudio);
            return uri.Segments.Last();
        }
    }
}
