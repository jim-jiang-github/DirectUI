namespace DirectUISample
{
    partial class FmDUIScaleableControlDemo
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
            this.duiNativeControl1 = new DirectUI.Core.DUINativeControl();
            this.SuspendLayout();
            // 
            // duiNativeControl1
            // 
            this.duiNativeControl1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.duiNativeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duiNativeControl1.Location = new System.Drawing.Point(0, 0);
            this.duiNativeControl1.Name = "duiNativeControl1";
            this.duiNativeControl1.Size = new System.Drawing.Size(800, 450);
            this.duiNativeControl1.TabIndex = 0;
            this.duiNativeControl1.Text = "duiNativeControl1";
            // 
            // FmDUIScaleableControlDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.duiNativeControl1);
            this.Name = "FmDUIScaleableControlDemo";
            this.Text = "FmDUIScaleableControlDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private DirectUI.Core.DUINativeControl duiNativeControl1;
    }
}