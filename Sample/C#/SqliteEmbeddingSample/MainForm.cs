using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace SqliteEmbeddingSample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonCreateNewDatabase_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dbPath = saveFileDialog.FileName;

                if (File.Exists(dbPath))
                {
                    MessageBox.Show(string.Format("The file {0} already exists", dbPath));
                    return;
                }

                SQLiteConnection.CreateFile(dbPath);

                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;New=False;Compress=True;", true))
                { 
                    connection.Open();

                    using (SQLiteCommand sqlitecommand = new SQLiteCommand("CREATE TABLE IF NOT EXISTS SomeTable (rowid INTEGER PRIMARY KEY AUTOINCREMENT)", connection))
                    {
                        sqlitecommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(string.Format("The database file {0} successully created", dbPath));
            }
        }
    }
}
