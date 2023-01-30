namespace Function
{
    partial class frmProcess
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
            this.pn_process = new DevExpress.XtraWaitForm.ProgressPanel();
            this.SuspendLayout();
            // 
            // pn_process
            // 
            this.pn_process.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pn_process.Appearance.Options.UseBackColor = true;
            this.pn_process.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pn_process.AppearanceCaption.Options.UseFont = true;
            this.pn_process.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pn_process.AppearanceDescription.Options.UseFont = true;
            this.pn_process.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_process.Location = new System.Drawing.Point(0, 0);
            this.pn_process.Name = "pn_process";
            this.pn_process.Size = new System.Drawing.Size(240, 87);
            this.pn_process.TabIndex = 1;
            this.pn_process.Text = "progressPanel1";
            // 
            // frmProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 87);
            this.Controls.Add(this.pn_process);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmProcess";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel pn_process;

    }
}