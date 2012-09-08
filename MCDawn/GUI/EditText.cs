using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCDawn.Gui
{
    public partial class EditText : Form
    {
        public EditText()
        {
            InitializeComponent();
            string[] files = Directory.GetFiles("text");
            cmbTextFiles.Items.Clear();
            foreach (string s in files) 
            {
                string temp = s.Replace(".txt", ""); temp = temp.Replace("text\\", "");
                cmbTextFiles.Items.Add(temp);
            }
        }

        string file;
        string text;
        public void LoadText()
        {
            if (txtText.Text == null) { txtText.Text = "Select a text file please."; return; }
            file = cmbTextFiles.Text.ToLower();
            string path = "text/" + file + ".txt";
            if (File.Exists(path)) { text = File.ReadAllText(path); }
            else { if (file != "") File.Create(path); text = File.ReadAllText(path); }
            txtText.Text = text;
        }

        public void Save(string filename)
        {
            if (filename == "") return;
            filename = cmbTextFiles.Text.ToLower();
            if (!File.Exists("text/" + filename + ".txt")) { File.Create("text/" + filename + ".txt"); }
            File.WriteAllText("text/" + filename + ".txt", txtText.Text);
            text = File.ReadAllText("text/" + filename + ".txt");
            LoadText();
        }

        private void cmbTextFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("text");
            cmbTextFiles.Items.Clear();
            foreach (string s in files)
            {
                string temp = s.Replace(".txt", ""); temp = temp.Replace("text\\", "");
                cmbTextFiles.Items.Add(temp);
            }
            LoadText();
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            file = cmbTextFiles.SelectedIndex.ToString().ToLower();
            File.WriteAllText("text/" + file + ".txt", text);
            text = File.ReadAllText("text/" + file + ".txt");
            LoadText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            file = cmbTextFiles.SelectedIndex.ToString().ToLower();
            Save(file);
            Hide();
            Dispose();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            file = cmbTextFiles.SelectedIndex.ToString().ToLower();
            Save(file);
        }
    }
}
