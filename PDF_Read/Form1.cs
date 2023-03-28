using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PDF_Read
{
    public partial class PDF_Read : Form
    {
        public PDF_Read()
        {
            InitializeComponent();

            this.btnOpenFile.Click += new EventHandler(btnOpenFile_Click);
        }

        void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();

            if (fd.FileName != "")
            {
                string str = Ex(fd.FileName);

                if (!string.IsNullOrEmpty(str))
                {
                    //string[] arr = str.Split("Eone");
                    //string splstrF = arr[1].ToString().Substring(3);

                    this.txtRseult.Text = str.Replace("\n",Environment.NewLine);
                }
            }
        }

        private string Ex(string sPath)
        {
            StringBuilder result = new StringBuilder();
            //StringBuilder text = new StringBuilder();
            try
            {
                using (Stream newpdfStream = new FileStream(sPath, FileMode.Open, FileAccess.Read))
                {
                    PdfReader pdfReader = new PdfReader(newpdfStream);

                    for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                    {
                        result.Append(PdfTextExtractor.GetTextFromPage(pdfReader, i, new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy())).Append("\r\n\r\n\r\n");


                        //ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        //string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, i, strategy);

                        //currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                        //text.Append(currentText);
                    }
                    //pdfReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result.ToString();
        }
    }
}
