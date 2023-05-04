using System;
using Spire.Pdf;
using System.IO;
using System.Windows.Forms;
using Spire.Pdf.Texts;
using Spire.Pdf.Grid;

namespace PDF2CSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnConvert_Click(object sender, EventArgs e)
        {
            string inputFolder = txtInputFolder.Text;
            string outputFolder = txtOutputFolder.Text;
            
            if (string.IsNullOrEmpty(inputFolder) || string.IsNullOrEmpty(outputFolder))
            {
                lblStatus.Text = "Please provide input and output folders.";
                return;
            }

            if (!Directory.Exists(inputFolder))
            {
                lblStatus.Text = "Input folder does not exist.";
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                lblStatus.Text = "Output folder does not exist.";
                return;
            }

            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
            foreach (string pdfFile in pdfFiles)
            {
                try
                {
                    string csvFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(pdfFile) + ".xlsx");
                    //Create a PdfDocument instance
                    PdfDocument document = new PdfDocument();
                    //Load a PDF file
                    document.LoadFromFile(pdfFile);
                    //Save to Excel
                    document.SaveToFile(csvFile, FileFormat.XLSX);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error converting " + pdfFile + ": " + ex.Message;
                }
            }

            // Show a message box to indicate that the conversion is complete
            MessageBox.Show("Conversion complete.", "PDF to CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public FolderBrowserDialog fdialog = new FolderBrowserDialog();
        //Default Open Folder
        string myFolder = @"C:\PDF";
        private void btnOpenInputFolder_Click(object sender, EventArgs e)
        {
            fdialog.SelectedPath = myFolder;
            if (fdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                txtInputFolder.Text = fdialog.SelectedPath;
                txtOutputFolder.Text = fdialog.SelectedPath;
            }
            else
            {
                txtInputFolder.Text = "";
            }
        }
        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog fdialog = new FolderBrowserDialog();
            if (fdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtInputFolder.Text = fdialog.SelectedPath;
            }
            else
            {
                //If not selected default folder is Input Folder
                txtInputFolder.Text = txtInputFolder.Text;
            }
        }
    }
}
