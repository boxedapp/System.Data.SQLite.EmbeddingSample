namespace SqliteEmbeddingSample
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCreateNewDatabase = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // buttonCreateNewDatabase
            // 
            this.buttonCreateNewDatabase.Location = new System.Drawing.Point(39, 25);
            this.buttonCreateNewDatabase.Name = "buttonCreateNewDatabase";
            this.buttonCreateNewDatabase.Size = new System.Drawing.Size(226, 90);
            this.buttonCreateNewDatabase.TabIndex = 0;
            this.buttonCreateNewDatabase.Text = "Create new database";
            this.buttonCreateNewDatabase.UseVisualStyleBackColor = true;
            this.buttonCreateNewDatabase.Click += new System.EventHandler(this.buttonCreateNewDatabase_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 148);
            this.Controls.Add(this.buttonCreateNewDatabase);
            this.Name = "MainForm";
            this.Text = "Sqlite Embedding Sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateNewDatabase;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

