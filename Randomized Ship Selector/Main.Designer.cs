namespace Randomized_Ship_Selector
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnRandom = new System.Windows.Forms.Button();
            this.pbOutput = new System.Windows.Forms.PictureBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.cb_nonPremium = new System.Windows.Forms.CheckBox();
            this.cb_Premium = new System.Windows.Forms.CheckBox();
            this.lbl_Tiers = new System.Windows.Forms.Label();
            this.lbl_Ships = new System.Windows.Forms.Label();
            this.cb_T1 = new System.Windows.Forms.CheckBox();
            this.cb_T2 = new System.Windows.Forms.CheckBox();
            this.cb_T3 = new System.Windows.Forms.CheckBox();
            this.cb_T4 = new System.Windows.Forms.CheckBox();
            this.cb_T5 = new System.Windows.Forms.CheckBox();
            this.cb_T6 = new System.Windows.Forms.CheckBox();
            this.cb_T7 = new System.Windows.Forms.CheckBox();
            this.cb_T8 = new System.Windows.Forms.CheckBox();
            this.cb_T9 = new System.Windows.Forms.CheckBox();
            this.cb_T10 = new System.Windows.Forms.CheckBox();
            this.cb_N_USN = new System.Windows.Forms.CheckBox();
            this.cb_N_IJN = new System.Windows.Forms.CheckBox();
            this.cb_N_VMF = new System.Windows.Forms.CheckBox();
            this.cb_N_FN = new System.Windows.Forms.CheckBox();
            this.cb_N_RM = new System.Windows.Forms.CheckBox();
            this.cb_N_KM = new System.Windows.Forms.CheckBox();
            this.cb_N_PA = new System.Windows.Forms.CheckBox();
            this.cb_N_RN = new System.Windows.Forms.CheckBox();
            this.cb_N_ORP = new System.Windows.Forms.CheckBox();
            this.cb_N_Commonwealth = new System.Windows.Forms.CheckBox();
            this.lbl_CountTxt = new System.Windows.Forms.Label();
            this.lbl_Count = new System.Windows.Forms.Label();
            this.lbl_Classes = new System.Windows.Forms.Label();
            this.cb_C_Destroyer = new System.Windows.Forms.CheckBox();
            this.cb_C_Cruiser = new System.Windows.Forms.CheckBox();
            this.cb_C_Battleship = new System.Windows.Forms.CheckBox();
            this.cb_C_Carrier = new System.Windows.Forms.CheckBox();
            this.btn_Credits = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(86, 314);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(170, 32);
            this.btnRandom.TabIndex = 0;
            this.btnRandom.Text = "Randomize";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // pbOutput
            // 
            this.pbOutput.Location = new System.Drawing.Point(203, 367);
            this.pbOutput.Name = "pbOutput";
            this.pbOutput.Size = new System.Drawing.Size(122, 22);
            this.pbOutput.TabIndex = 1;
            this.pbOutput.TabStop = false;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(7, 376);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(180, 13);
            this.lblOutput.TabIndex = 2;
            this.lblOutput.Text = "The randomfier has chosen this ship:";
            // 
            // cb_nonPremium
            // 
            this.cb_nonPremium.AutoSize = true;
            this.cb_nonPremium.Checked = true;
            this.cb_nonPremium.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_nonPremium.Location = new System.Drawing.Point(92, 17);
            this.cb_nonPremium.Name = "cb_nonPremium";
            this.cb_nonPremium.Size = new System.Drawing.Size(89, 17);
            this.cb_nonPremium.TabIndex = 3;
            this.cb_nonPremium.Text = "Non Premium";
            this.cb_nonPremium.UseVisualStyleBackColor = true;
            // 
            // cb_Premium
            // 
            this.cb_Premium.AutoSize = true;
            this.cb_Premium.Checked = true;
            this.cb_Premium.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Premium.Location = new System.Drawing.Point(190, 17);
            this.cb_Premium.Name = "cb_Premium";
            this.cb_Premium.Size = new System.Drawing.Size(66, 17);
            this.cb_Premium.TabIndex = 4;
            this.cb_Premium.Text = "Premium";
            this.cb_Premium.UseVisualStyleBackColor = true;
            // 
            // lbl_Tiers
            // 
            this.lbl_Tiers.AutoSize = true;
            this.lbl_Tiers.Location = new System.Drawing.Point(56, 40);
            this.lbl_Tiers.Name = "lbl_Tiers";
            this.lbl_Tiers.Size = new System.Drawing.Size(30, 13);
            this.lbl_Tiers.TabIndex = 5;
            this.lbl_Tiers.Text = "Tiers";
            // 
            // lbl_Ships
            // 
            this.lbl_Ships.AutoSize = true;
            this.lbl_Ships.Location = new System.Drawing.Point(118, 40);
            this.lbl_Ships.Name = "lbl_Ships";
            this.lbl_Ships.Size = new System.Drawing.Size(43, 13);
            this.lbl_Ships.TabIndex = 6;
            this.lbl_Ships.Text = "Nations";
            // 
            // cb_T1
            // 
            this.cb_T1.AutoSize = true;
            this.cb_T1.Checked = true;
            this.cb_T1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T1.Location = new System.Drawing.Point(54, 65);
            this.cb_T1.Name = "cb_T1";
            this.cb_T1.Size = new System.Drawing.Size(32, 17);
            this.cb_T1.TabIndex = 7;
            this.cb_T1.Text = "1";
            this.cb_T1.UseVisualStyleBackColor = true;
            // 
            // cb_T2
            // 
            this.cb_T2.AutoSize = true;
            this.cb_T2.Checked = true;
            this.cb_T2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T2.Location = new System.Drawing.Point(54, 88);
            this.cb_T2.Name = "cb_T2";
            this.cb_T2.Size = new System.Drawing.Size(32, 17);
            this.cb_T2.TabIndex = 8;
            this.cb_T2.Text = "2";
            this.cb_T2.UseVisualStyleBackColor = true;
            // 
            // cb_T3
            // 
            this.cb_T3.AutoSize = true;
            this.cb_T3.Checked = true;
            this.cb_T3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T3.Location = new System.Drawing.Point(54, 111);
            this.cb_T3.Name = "cb_T3";
            this.cb_T3.Size = new System.Drawing.Size(32, 17);
            this.cb_T3.TabIndex = 9;
            this.cb_T3.Text = "3";
            this.cb_T3.UseVisualStyleBackColor = true;
            // 
            // cb_T4
            // 
            this.cb_T4.AutoSize = true;
            this.cb_T4.Checked = true;
            this.cb_T4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T4.Location = new System.Drawing.Point(54, 134);
            this.cb_T4.Name = "cb_T4";
            this.cb_T4.Size = new System.Drawing.Size(32, 17);
            this.cb_T4.TabIndex = 10;
            this.cb_T4.Text = "4";
            this.cb_T4.UseVisualStyleBackColor = true;
            // 
            // cb_T5
            // 
            this.cb_T5.AutoSize = true;
            this.cb_T5.Checked = true;
            this.cb_T5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T5.Location = new System.Drawing.Point(54, 157);
            this.cb_T5.Name = "cb_T5";
            this.cb_T5.Size = new System.Drawing.Size(32, 17);
            this.cb_T5.TabIndex = 11;
            this.cb_T5.Text = "5";
            this.cb_T5.UseVisualStyleBackColor = true;
            // 
            // cb_T6
            // 
            this.cb_T6.AutoSize = true;
            this.cb_T6.Checked = true;
            this.cb_T6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T6.Location = new System.Drawing.Point(54, 180);
            this.cb_T6.Name = "cb_T6";
            this.cb_T6.Size = new System.Drawing.Size(32, 17);
            this.cb_T6.TabIndex = 12;
            this.cb_T6.Text = "6";
            this.cb_T6.UseVisualStyleBackColor = true;
            // 
            // cb_T7
            // 
            this.cb_T7.AutoSize = true;
            this.cb_T7.Checked = true;
            this.cb_T7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T7.Location = new System.Drawing.Point(54, 203);
            this.cb_T7.Name = "cb_T7";
            this.cb_T7.Size = new System.Drawing.Size(32, 17);
            this.cb_T7.TabIndex = 13;
            this.cb_T7.Text = "7";
            this.cb_T7.UseVisualStyleBackColor = true;
            // 
            // cb_T8
            // 
            this.cb_T8.AutoSize = true;
            this.cb_T8.Checked = true;
            this.cb_T8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T8.Location = new System.Drawing.Point(54, 226);
            this.cb_T8.Name = "cb_T8";
            this.cb_T8.Size = new System.Drawing.Size(32, 17);
            this.cb_T8.TabIndex = 14;
            this.cb_T8.Text = "8";
            this.cb_T8.UseVisualStyleBackColor = true;
            // 
            // cb_T9
            // 
            this.cb_T9.AutoSize = true;
            this.cb_T9.Checked = true;
            this.cb_T9.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T9.Location = new System.Drawing.Point(54, 249);
            this.cb_T9.Name = "cb_T9";
            this.cb_T9.Size = new System.Drawing.Size(32, 17);
            this.cb_T9.TabIndex = 15;
            this.cb_T9.Text = "9";
            this.cb_T9.UseVisualStyleBackColor = true;
            // 
            // cb_T10
            // 
            this.cb_T10.AutoSize = true;
            this.cb_T10.Checked = true;
            this.cb_T10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_T10.Location = new System.Drawing.Point(54, 272);
            this.cb_T10.Name = "cb_T10";
            this.cb_T10.Size = new System.Drawing.Size(38, 17);
            this.cb_T10.TabIndex = 16;
            this.cb_T10.Text = "10";
            this.cb_T10.UseVisualStyleBackColor = true;
            // 
            // cb_N_USN
            // 
            this.cb_N_USN.AutoSize = true;
            this.cb_N_USN.Checked = true;
            this.cb_N_USN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_USN.Location = new System.Drawing.Point(121, 65);
            this.cb_N_USN.Name = "cb_N_USN";
            this.cb_N_USN.Size = new System.Drawing.Size(96, 17);
            this.cb_N_USN.TabIndex = 17;
            this.cb_N_USN.Text = "USN (Amercia)";
            this.cb_N_USN.UseVisualStyleBackColor = true;
            // 
            // cb_N_IJN
            // 
            this.cb_N_IJN.AutoSize = true;
            this.cb_N_IJN.Checked = true;
            this.cb_N_IJN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_IJN.Location = new System.Drawing.Point(121, 89);
            this.cb_N_IJN.Name = "cb_N_IJN";
            this.cb_N_IJN.Size = new System.Drawing.Size(80, 17);
            this.cb_N_IJN.TabIndex = 18;
            this.cb_N_IJN.Text = "IJN (Japan)";
            this.cb_N_IJN.UseVisualStyleBackColor = true;
            // 
            // cb_N_VMF
            // 
            this.cb_N_VMF.AutoSize = true;
            this.cb_N_VMF.Checked = true;
            this.cb_N_VMF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_VMF.Location = new System.Drawing.Point(121, 113);
            this.cb_N_VMF.Name = "cb_N_VMF";
            this.cb_N_VMF.Size = new System.Drawing.Size(89, 17);
            this.cb_N_VMF.TabIndex = 19;
            this.cb_N_VMF.Text = "VMF (Russia)";
            this.cb_N_VMF.UseVisualStyleBackColor = true;
            // 
            // cb_N_FN
            // 
            this.cb_N_FN.AutoSize = true;
            this.cb_N_FN.Checked = true;
            this.cb_N_FN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_FN.Location = new System.Drawing.Point(121, 137);
            this.cb_N_FN.Name = "cb_N_FN";
            this.cb_N_FN.Size = new System.Drawing.Size(82, 17);
            this.cb_N_FN.TabIndex = 20;
            this.cb_N_FN.Text = "FN (French)";
            this.cb_N_FN.UseVisualStyleBackColor = true;
            // 
            // cb_N_RM
            // 
            this.cb_N_RM.AutoSize = true;
            this.cb_N_RM.Checked = true;
            this.cb_N_RM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_RM.Location = new System.Drawing.Point(121, 161);
            this.cb_N_RM.Name = "cb_N_RM";
            this.cb_N_RM.Size = new System.Drawing.Size(71, 17);
            this.cb_N_RM.TabIndex = 21;
            this.cb_N_RM.Text = "RM (Italy)";
            this.cb_N_RM.UseVisualStyleBackColor = true;
            // 
            // cb_N_KM
            // 
            this.cb_N_KM.AutoSize = true;
            this.cb_N_KM.Checked = true;
            this.cb_N_KM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_KM.Location = new System.Drawing.Point(121, 184);
            this.cb_N_KM.Name = "cb_N_KM";
            this.cb_N_KM.Size = new System.Drawing.Size(93, 17);
            this.cb_N_KM.TabIndex = 22;
            this.cb_N_KM.Text = "KM (Germany)";
            this.cb_N_KM.UseVisualStyleBackColor = true;
            // 
            // cb_N_PA
            // 
            this.cb_N_PA.AutoSize = true;
            this.cb_N_PA.Checked = true;
            this.cb_N_PA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_PA.Location = new System.Drawing.Point(121, 207);
            this.cb_N_PA.Name = "cb_N_PA";
            this.cb_N_PA.Size = new System.Drawing.Size(91, 17);
            this.cb_N_PA.TabIndex = 23;
            this.cb_N_PA.Text = "PA (Pan Asia)";
            this.cb_N_PA.UseVisualStyleBackColor = true;
            // 
            // cb_N_RN
            // 
            this.cb_N_RN.AutoSize = true;
            this.cb_N_RN.Checked = true;
            this.cb_N_RN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_RN.Location = new System.Drawing.Point(121, 231);
            this.cb_N_RN.Name = "cb_N_RN";
            this.cb_N_RN.Size = new System.Drawing.Size(90, 17);
            this.cb_N_RN.TabIndex = 24;
            this.cb_N_RN.Text = "RN (England)";
            this.cb_N_RN.UseVisualStyleBackColor = true;
            // 
            // cb_N_ORP
            // 
            this.cb_N_ORP.AutoSize = true;
            this.cb_N_ORP.Checked = true;
            this.cb_N_ORP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_ORP.Location = new System.Drawing.Point(121, 255);
            this.cb_N_ORP.Name = "cb_N_ORP";
            this.cb_N_ORP.Size = new System.Drawing.Size(91, 17);
            this.cb_N_ORP.TabIndex = 25;
            this.cb_N_ORP.Text = "ORP (Poland)";
            this.cb_N_ORP.UseVisualStyleBackColor = true;
            // 
            // cb_N_Commonwealth
            // 
            this.cb_N_Commonwealth.AutoSize = true;
            this.cb_N_Commonwealth.Checked = true;
            this.cb_N_Commonwealth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_N_Commonwealth.Location = new System.Drawing.Point(121, 279);
            this.cb_N_Commonwealth.Name = "cb_N_Commonwealth";
            this.cb_N_Commonwealth.Size = new System.Drawing.Size(98, 17);
            this.cb_N_Commonwealth.TabIndex = 26;
            this.cb_N_Commonwealth.Text = "Commonwealth";
            this.cb_N_Commonwealth.UseVisualStyleBackColor = true;
            // 
            // lbl_CountTxt
            // 
            this.lbl_CountTxt.AutoSize = true;
            this.lbl_CountTxt.Location = new System.Drawing.Point(7, 358);
            this.lbl_CountTxt.Name = "lbl_CountTxt";
            this.lbl_CountTxt.Size = new System.Drawing.Size(158, 13);
            this.lbl_CountTxt.TabIndex = 27;
            this.lbl_CountTxt.Text = "Amount of ships to choose from:";
            // 
            // lbl_Count
            // 
            this.lbl_Count.AutoSize = true;
            this.lbl_Count.Location = new System.Drawing.Point(171, 358);
            this.lbl_Count.Name = "lbl_Count";
            this.lbl_Count.Size = new System.Drawing.Size(13, 13);
            this.lbl_Count.TabIndex = 28;
            this.lbl_Count.Text = "0";
            // 
            // lbl_Classes
            // 
            this.lbl_Classes.AutoSize = true;
            this.lbl_Classes.Location = new System.Drawing.Point(257, 40);
            this.lbl_Classes.Name = "lbl_Classes";
            this.lbl_Classes.Size = new System.Drawing.Size(43, 13);
            this.lbl_Classes.TabIndex = 29;
            this.lbl_Classes.Text = "Classes";
            // 
            // cb_C_Destroyer
            // 
            this.cb_C_Destroyer.AutoSize = true;
            this.cb_C_Destroyer.Checked = true;
            this.cb_C_Destroyer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_C_Destroyer.Location = new System.Drawing.Point(241, 65);
            this.cb_C_Destroyer.Name = "cb_C_Destroyer";
            this.cb_C_Destroyer.Size = new System.Drawing.Size(71, 17);
            this.cb_C_Destroyer.TabIndex = 30;
            this.cb_C_Destroyer.Text = "Destroyer";
            this.cb_C_Destroyer.UseVisualStyleBackColor = true;
            // 
            // cb_C_Cruiser
            // 
            this.cb_C_Cruiser.AutoSize = true;
            this.cb_C_Cruiser.Checked = true;
            this.cb_C_Cruiser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_C_Cruiser.Location = new System.Drawing.Point(241, 88);
            this.cb_C_Cruiser.Name = "cb_C_Cruiser";
            this.cb_C_Cruiser.Size = new System.Drawing.Size(58, 17);
            this.cb_C_Cruiser.TabIndex = 31;
            this.cb_C_Cruiser.Text = "Cruiser";
            this.cb_C_Cruiser.UseVisualStyleBackColor = true;
            // 
            // cb_C_Battleship
            // 
            this.cb_C_Battleship.AutoSize = true;
            this.cb_C_Battleship.Checked = true;
            this.cb_C_Battleship.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_C_Battleship.Location = new System.Drawing.Point(241, 111);
            this.cb_C_Battleship.Name = "cb_C_Battleship";
            this.cb_C_Battleship.Size = new System.Drawing.Size(72, 17);
            this.cb_C_Battleship.TabIndex = 32;
            this.cb_C_Battleship.Text = "Battleship";
            this.cb_C_Battleship.UseVisualStyleBackColor = true;
            // 
            // cb_C_Carrier
            // 
            this.cb_C_Carrier.AutoSize = true;
            this.cb_C_Carrier.Checked = true;
            this.cb_C_Carrier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_C_Carrier.Location = new System.Drawing.Point(241, 134);
            this.cb_C_Carrier.Name = "cb_C_Carrier";
            this.cb_C_Carrier.Size = new System.Drawing.Size(56, 17);
            this.cb_C_Carrier.TabIndex = 33;
            this.cb_C_Carrier.Text = "Carrier";
            this.cb_C_Carrier.UseVisualStyleBackColor = true;
            // 
            // btn_Credits
            // 
            this.btn_Credits.Location = new System.Drawing.Point(12, 318);
            this.btn_Credits.Name = "btn_Credits";
            this.btn_Credits.Size = new System.Drawing.Size(50, 24);
            this.btn_Credits.TabIndex = 34;
            this.btn_Credits.Text = "Credits";
            this.btn_Credits.UseVisualStyleBackColor = true;
            this.btn_Credits.Click += new System.EventHandler(this.btn_Credits_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 419);
            this.Controls.Add(this.btn_Credits);
            this.Controls.Add(this.cb_C_Carrier);
            this.Controls.Add(this.cb_C_Battleship);
            this.Controls.Add(this.cb_C_Cruiser);
            this.Controls.Add(this.cb_C_Destroyer);
            this.Controls.Add(this.lbl_Classes);
            this.Controls.Add(this.lbl_Count);
            this.Controls.Add(this.lbl_CountTxt);
            this.Controls.Add(this.cb_N_Commonwealth);
            this.Controls.Add(this.cb_N_ORP);
            this.Controls.Add(this.cb_N_RN);
            this.Controls.Add(this.cb_N_PA);
            this.Controls.Add(this.cb_N_KM);
            this.Controls.Add(this.cb_N_RM);
            this.Controls.Add(this.cb_N_FN);
            this.Controls.Add(this.cb_N_VMF);
            this.Controls.Add(this.cb_N_IJN);
            this.Controls.Add(this.cb_N_USN);
            this.Controls.Add(this.cb_T10);
            this.Controls.Add(this.cb_T9);
            this.Controls.Add(this.cb_T8);
            this.Controls.Add(this.cb_T7);
            this.Controls.Add(this.cb_T6);
            this.Controls.Add(this.cb_T5);
            this.Controls.Add(this.cb_T4);
            this.Controls.Add(this.cb_T3);
            this.Controls.Add(this.cb_T2);
            this.Controls.Add(this.cb_T1);
            this.Controls.Add(this.lbl_Ships);
            this.Controls.Add(this.lbl_Tiers);
            this.Controls.Add(this.cb_Premium);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.cb_nonPremium);
            this.Controls.Add(this.pbOutput);
            this.Controls.Add(this.btnRandom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Random Ship Selector";
            ((System.ComponentModel.ISupportInitialize)(this.pbOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.PictureBox pbOutput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.CheckBox cb_nonPremium;
        private System.Windows.Forms.CheckBox cb_Premium;
        private System.Windows.Forms.Label lbl_Tiers;
        private System.Windows.Forms.Label lbl_Ships;
        private System.Windows.Forms.CheckBox cb_T1;
        private System.Windows.Forms.CheckBox cb_T2;
        private System.Windows.Forms.CheckBox cb_T3;
        private System.Windows.Forms.CheckBox cb_T4;
        private System.Windows.Forms.CheckBox cb_T5;
        private System.Windows.Forms.CheckBox cb_T6;
        private System.Windows.Forms.CheckBox cb_T7;
        private System.Windows.Forms.CheckBox cb_T8;
        private System.Windows.Forms.CheckBox cb_T9;
        private System.Windows.Forms.CheckBox cb_T10;
        private System.Windows.Forms.CheckBox cb_N_USN;
        private System.Windows.Forms.CheckBox cb_N_IJN;
        private System.Windows.Forms.CheckBox cb_N_VMF;
        private System.Windows.Forms.CheckBox cb_N_FN;
        private System.Windows.Forms.CheckBox cb_N_RM;
        private System.Windows.Forms.CheckBox cb_N_KM;
        private System.Windows.Forms.CheckBox cb_N_PA;
        private System.Windows.Forms.CheckBox cb_N_RN;
        private System.Windows.Forms.CheckBox cb_N_ORP;
        private System.Windows.Forms.CheckBox cb_N_Commonwealth;
        private System.Windows.Forms.Label lbl_CountTxt;
        private System.Windows.Forms.Label lbl_Count;
        private System.Windows.Forms.Label lbl_Classes;
        private System.Windows.Forms.CheckBox cb_C_Destroyer;
        private System.Windows.Forms.CheckBox cb_C_Cruiser;
        private System.Windows.Forms.CheckBox cb_C_Battleship;
        private System.Windows.Forms.CheckBox cb_C_Carrier;
        private System.Windows.Forms.Button btn_Credits;
    }
}

