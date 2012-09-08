namespace MCDawn.Gui
{
    partial class EditText
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
            this.lblEditTextFiles = new System.Windows.Forms.Label();
            this.cmbTextFiles = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEditTextFiles
            // 
            this.lblEditTextFiles.AutoSize = true;
            this.lblEditTextFiles.Location = new System.Drawing.Point(12, 9);
            this.lblEditTextFiles.Name = "lblEditTextFiles";
            this.lblEditTextFiles.Size = new System.Drawing.Size(76, 13);
            this.lblEditTextFiles.TabIndex = 0;
            this.lblEditTextFiles.Text = "Edit Text Files:";
            // 
            // cmbTextFiles
            // 
            this.cmbTextFiles.FormattingEnabled = true;
            this.cmbTextFiles.Location = new System.Drawing.Point(94, 6);
            this.cmbTextFiles.Name = "cmbTextFiles";
            this.cmbTextFiles.Size = new System.Drawing.Size(192, 21);
            this.cmbTextFiles.TabIndex = 1;
            this.cmbTextFiles.SelectedIndexChanged += new System.EventHandler(this.cmbTextFiles_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(232, 321);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(54, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(13, 321);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(61, 23);
            this.btnDiscard.TabIndex = 4;
            this.btnDiscard.Text = "Discard";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(12, 33);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtText.Size = new System.Drawing.Size(274, 282);
            this.txtText.TabIndex = 5;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(171, 321);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(55, 23);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // EditText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 356);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbTextFiles);
            this.Controls.Add(this.lblEditTextFiles);
            this.Name = "EditText";
            this.Text = "Edit Text Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEditTextFiles;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDiscard;
        private System.Windows.Forms.TextBox txtText;
        public System.Windows.Forms.ComboBox cmbTextFiles;
        private System.Windows.Forms.Button btnApply;
    }
}