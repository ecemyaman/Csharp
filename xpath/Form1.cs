using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.API.Search;

namespace XPATH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text);
            string html = getRequest(textBox1.Text);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            textBox2.Text = html;

            textBox3.Text += textBox1.Text;
            textBox3.Text += "-----------------------------------------" + System.Environment.NewLine;

            foreach (var inputnode in doc.DocumentNode.SelectNodes("//input"))
            {
                textBox3.Text += inputnode.XPath;
                textBox3.Text += System.Environment.NewLine;
            }

        }
        public static string getRequest(string url)
        {
            try
            {
                HttpWebRequest istek = (HttpWebRequest)WebRequest.Create(url);
                CookieContainer cookie = new CookieContainer();
                istek.Method = "GET";
                istek.CookieContainer = cookie;
                string cevap;
                using (Stream response = istek.GetResponse().GetResponseStream())
                using (StreamReader reader1 = new StreamReader(response))
                cevap = reader1.ReadToEnd();
                return cevap;

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }
    }
}
