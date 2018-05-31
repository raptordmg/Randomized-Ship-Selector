namespace Randomized_Ship_Selector
{
    partial class Credits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credits));
            this.lbl_credits_1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.llbl_panzerschiffer_forum = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_credits_1
            // 
            this.lbl_credits_1.AutoSize = true;
            this.lbl_credits_1.Location = new System.Drawing.Point(12, 38);
            this.lbl_credits_1.Name = "lbl_credits_1";
            this.lbl_credits_1.Size = new System.Drawing.Size(97, 13);
            this.lbl_credits_1.TabIndex = 0;
            this.lbl_credits_1.Text = "Creator: D Inbound";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Countour icons courtesy of: Panzerschiffer";
            // 
            // llbl_panzerschiffer_forum
            // 
            this.llbl_panzerschiffer_forum.AutoSize = true;
            this.llbl_panzerschiffer_forum.Location = new System.Drawing.Point(12, 121);
            this.llbl_panzerschiffer_forum.Name = "llbl_panzerschiffer_forum";
            this.llbl_panzerschiffer_forum.Size = new System.Drawing.Size(159, 13);
            this.llbl_panzerschiffer_forum.TabIndex = 2;
            this.llbl_panzerschiffer_forum.TabStop = true;
            this.llbl_panzerschiffer_forum.Text = "Historical Ensigns Contour Icons";
            this.llbl_panzerschiffer_forum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl_panzerschiffer_forum_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Download from the World of Warships Forums:";
            // 
            // Credits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 158);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.llbl_panzerschiffer_forum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_credits_1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Credits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Credits";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_credits_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llbl_panzerschiffer_forum;
        private System.Windows.Forms.Label label2;
    }
}