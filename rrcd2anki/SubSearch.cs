using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace rrcd2anki
{
    class SubSearch
    {
        private string word { get; set; }

        public SubSearch(string word)
        {
            this.word = word;
        }

        public XmlDocument getXMLRespond()
        {
            string url = "http://www.91dict.com/rr/seek_word.php?keyword=" + this.word + "&Submit=seek";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            return xmlDoc;
        }

        public Word getWord()
        {
            Word word = new Word();
            XmlDocument xmlDoc = this.getXMLRespond();
            XmlNodeList subxml = xmlDoc.GetElementsByTagName("sub");
            List<Sub> subList = new List<Sub>();
            if (subxml.Count == 0)
            {
                throw new Exception("没有找到字幕");
            }
            else
            {
                word.Name = xmlDoc.GetElementsByTagName("name")[0].InnerText;
                foreach (XmlNode node in subxml)
                {
                    string en = node["suben"].InnerText;
                    string cn = node["subcn"].InnerText;
                    string img = node["subimg"].InnerText;
                    string mp3 = node["subaudio"].InnerText;
                    string filmname = node["filmname"].InnerText;
                    Sub sub = new Sub();
                    sub.SubEN = en;
                    sub.SubCN = cn;
                    sub.SubImg = img;
                    sub.SubAudio = mp3;
                    sub.FilmName = filmname; 
                    subList.Add(sub);
                }
                word.MySub = subList;
                return word;

            }
        }
    }
}