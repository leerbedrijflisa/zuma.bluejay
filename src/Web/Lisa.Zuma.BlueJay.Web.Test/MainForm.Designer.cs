namespace Lisa.Zuma.BlueJay.Web.Test
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
            this.btnSubmitNote = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lbMedia = new System.Windows.Forms.ListBox();
            this.lblMedia = new System.Windows.Forms.Label();
            this.btnAddMedia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSubmitNote
            // 
            this.btnSubmitNote.Location = new System.Drawing.Point(337, 283);
            this.btnSubmitNote.Name = "btnSubmitNote";
            this.btnSubmitNote.Size = new System.Drawing.Size(75, 23);
            this.btnSubmitNote.TabIndex = 0;
            this.btnSubmitNote.Text = "Submit note";
            this.btnSubmitNote.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(53, 13);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message:";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(15, 25);
            this.txtMessage.MaxLength = 500;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(397, 89);
            this.txtMessage.TabIndex = 2;
            // 
            // lbMedia
            // 
            this.lbMedia.FormattingEnabled = true;
            this.lbMedia.Location = new System.Drawing.Point(15, 143);
            this.lbMedia.Name = "lbMedia";
            this.lbMedia.Size = new System.Drawing.Size(397, 134);
            this.lbMedia.TabIndex = 3;
            // 
            // lblMedia
            // 
            this.lblMedia.AutoSize = true;
            this.lblMedia.Location = new System.Drawing.Point(12, 125);
            this.lblMedia.Name = "lblMedia";
            this.lblMedia.Size = new System.Drawing.Size(39, 13);
            this.lblMedia.TabIndex = 4;
            this.lblMedia.Text = "Media:";
            // 
            // btnAddMedia
            // 
            this.btnAddMedia.Location = new System.Drawing.Point(15, 283);
            this.btnAddMedia.Name = "btnAddMedia";
            this.btnAddMedia.Size = new System.Drawing.Size(87, 23);
            this.btnAddMedia.TabIndex = 5;
            this.btnAddMedia.Text = "Add media";
            this.btnAddMedia.UseVisualStyleBackColor = false;
            this.btnAddMedia.Click += new System.EventHandler(this.btnAddMedia_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 318);
            this.Controls.Add(this.btnAddMedia);
            this.Controls.Add(this.lblMedia);
            this.Controls.Add(this.lbMedia);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSubmitNote);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmitNote;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ListBox lbMedia;
        private System.Windows.Forms.Label lblMedia;
        private System.Windows.Forms.Button btnAddMedia;
    }
}

