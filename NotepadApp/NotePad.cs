using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace NotepadApp
{
    public partial class NotePad : Form
    {
        public NotePad()
        {
            InitializeComponent();

            //Create font combo box
            Graphics gr = rtbText.CreateGraphics();
            foreach(FontFamily fontFamily in FontFamily.GetFamilies(gr))
            {
                cmbFont.Items.Add(fontFamily.Name);
            }

            //Create font size combo box
            for(int fontSize = 8; fontSize < 12; fontSize++) //8, 9, 10, 11
            {
                cmbFontSize.Items.Add(fontSize);
            }
            for (int fontSize = 12; fontSize < 49; fontSize += 2) //increments of 2 from 12 onwards
            {
                cmbFontSize.Items.Add(fontSize);
            }


            //Create list of colours
            var colourList = new List<System.Drawing.Color>();
            foreach (var prop in typeof(System.Drawing.Color).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (prop.PropertyType == typeof(System.Drawing.Color))
                {
                    colourList.Add((System.Drawing.Color)prop.GetValue(null));
                }
            }

            foreach (Color colour in colourList)
            {
                cmbFontColour.Items.Add(colour);
            }
        }

        private void NotePad_Load(object sender, EventArgs e)
        {

            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {           
            //Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            // Initialize the SaveFileDialog to specify the RTF extension for the file.
            saveFileDialog1.Filter = "Rich Text File(*.rtf)| *.rtf";

            //// Determine if the user selected a file name from the saveFileDialog.
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFileDialog1.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                rtbText.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                MessageBox.Show("saved successfully", "Address File : " + saveFileDialog1.FileName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            //display NewFile form - asks user if they want to open a new notepad file
            NewFile nf = new NewFile();
            nf.Show();
            this.Hide();

            
        }

        private void cboFont_SelectionChangeComitted(object sender, EventArgs e)
        {
            rtbText.SelectionFont = new Font(cmbFont.Text, rtbText.SelectionFont.Size, rtbText.SelectionFont.Style);
        }

        private void cboTextSize_SelectionChangeComitted(object sender, EventArgs e)
        {
            rtbText.SelectionFont = new Font(rtbText.SelectionFont.FontFamily, int.Parse(cmbFontSize.Text), rtbText.SelectionFont.Style);
        }

        private void cboTextColour_SelectionChangeComitted(object sender, EventArgs e)
        {
            Color selectedColour = Color.FromName(cmbFontColour.Text);

            rtbText.SelectionColor = selectedColour;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Font SelectedText_Font = rtbText.SelectionFont; //Takes Font object of the selected text 
            if (SelectedText_Font != null) 
            {
                rtbText.SelectionFont = new Font(SelectedText_Font, 
                SelectedText_Font.Style ^ FontStyle.Bold); //Constructs new font using the existing font
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Font SelectedText_Font = rtbText.SelectionFont;
            if (SelectedText_Font != null)
            {
                rtbText.SelectionFont = new Font(SelectedText_Font,
                SelectedText_Font.Style ^ FontStyle.Italic);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Font SelectedText_Font = rtbText.SelectionFont;
            if (SelectedText_Font != null)
            {
                rtbText.SelectionFont = new Font(SelectedText_Font,
                  SelectedText_Font.Style ^ FontStyle.Underline);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Font SelectedText_Font = rtbText.SelectionFont;
            if (SelectedText_Font != null)
            {
                rtbText.SelectionFont = new Font(SelectedText_Font,
                  SelectedText_Font.Style ^ FontStyle.Strikeout);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (rtbText.SelectionBackColor == Color.Yellow)
            {
                rtbText.SelectionBackColor = System.Drawing.Color.White;
            }
            else
            {
                rtbText.SelectionBackColor = System.Drawing.Color.Yellow;
            }
        }
    }
}
