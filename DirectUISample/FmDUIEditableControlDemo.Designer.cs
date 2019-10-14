namespace DirectUISample
{
    partial class FmDUIEditableControlDemo
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
            this.btnAddImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // duiNativeControl1
            // 
            this.duiNativeControl1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.duiNativeControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.duiNativeControl1.Location = new System.Drawing.Point(0, 0);
            this.duiNativeControl1.Name = "duiNativeControl1";
            this.duiNativeControl1.Size = new System.Drawing.Size(664, 450);
            this.duiNativeControl1.TabIndex = 0;
            this.duiNativeControl1.Text = "duiNativeControl1";
            // 
            // btnAddImage
            // 
            this.btnAddImage.Location = new System.Drawing.Point(696, 12);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(75, 23);
            this.btnAddImage.TabIndex = 1;
            this.btnAddImage.Text = "新增图片";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // FmDUIEditableControlDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.duiNativeControl1);
            this.Name = "FmDUIEditableControlDemo";
            this.Text = "FmDUIEditableControlDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private DirectUI.Core.DUINativeControl duiNativeControl1;
        private System.Windows.Forms.Button btnAddImage;
    }
}