namespace DirectUISample
{
    partial class FmDUIControlDemo
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudRotate = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudSkewX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSkewY = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudScaleX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudScaleY = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudBorder = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkewX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkewY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorder)).BeginInit();
            this.SuspendLayout();
            // 
            // duiNativeControl1
            // 
            this.duiNativeControl1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.duiNativeControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.duiNativeControl1.Location = new System.Drawing.Point(0, 0);
            this.duiNativeControl1.Name = "duiNativeControl1";
            this.duiNativeControl1.Size = new System.Drawing.Size(573, 450);
            this.duiNativeControl1.TabIndex = 0;
            this.duiNativeControl1.Text = "duiNativeControl1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(599, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "X:";
            // 
            // nudX
            // 
            this.nudX.Location = new System.Drawing.Point(644, 29);
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(120, 20);
            this.nudX.TabIndex = 2;
            this.nudX.ValueChanged += new System.EventHandler(this.nudX_ValueChanged);
            // 
            // nudY
            // 
            this.nudY.Location = new System.Drawing.Point(644, 55);
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(120, 20);
            this.nudY.TabIndex = 4;
            this.nudY.ValueChanged += new System.EventHandler(this.nudY_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(599, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y:";
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(644, 81);
            this.nudWidth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(120, 20);
            this.nudWidth.TabIndex = 6;
            this.nudWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(599, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Width:";
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(644, 107);
            this.nudHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(120, 20);
            this.nudHeight.TabIndex = 8;
            this.nudHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(599, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Height:";
            // 
            // nudRotate
            // 
            this.nudRotate.Location = new System.Drawing.Point(644, 133);
            this.nudRotate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRotate.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudRotate.Name = "nudRotate";
            this.nudRotate.Size = new System.Drawing.Size(120, 20);
            this.nudRotate.TabIndex = 10;
            this.nudRotate.ValueChanged += new System.EventHandler(this.nudRotate_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(599, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Rotate:";
            // 
            // nudSkewX
            // 
            this.nudSkewX.DecimalPlaces = 2;
            this.nudSkewX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudSkewX.Location = new System.Drawing.Point(644, 159);
            this.nudSkewX.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSkewX.Name = "nudSkewX";
            this.nudSkewX.Size = new System.Drawing.Size(120, 20);
            this.nudSkewX.TabIndex = 12;
            this.nudSkewX.ValueChanged += new System.EventHandler(this.nudSkewX_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(599, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "SkewX:";
            // 
            // nudSkewY
            // 
            this.nudSkewY.DecimalPlaces = 2;
            this.nudSkewY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudSkewY.Location = new System.Drawing.Point(644, 185);
            this.nudSkewY.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSkewY.Name = "nudSkewY";
            this.nudSkewY.Size = new System.Drawing.Size(120, 20);
            this.nudSkewY.TabIndex = 14;
            this.nudSkewY.ValueChanged += new System.EventHandler(this.nudSkewY_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(599, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "SkewY:";
            // 
            // nudScaleX
            // 
            this.nudScaleX.DecimalPlaces = 1;
            this.nudScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleX.Location = new System.Drawing.Point(644, 211);
            this.nudScaleX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudScaleX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleX.Name = "nudScaleX";
            this.nudScaleX.Size = new System.Drawing.Size(120, 20);
            this.nudScaleX.TabIndex = 16;
            this.nudScaleX.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleX.ValueChanged += new System.EventHandler(this.nudScaleX_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(599, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "ScaleX:";
            // 
            // nudScaleY
            // 
            this.nudScaleY.DecimalPlaces = 1;
            this.nudScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleY.Location = new System.Drawing.Point(644, 237);
            this.nudScaleY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudScaleY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleY.Name = "nudScaleY";
            this.nudScaleY.Size = new System.Drawing.Size(120, 20);
            this.nudScaleY.TabIndex = 18;
            this.nudScaleY.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleY.ValueChanged += new System.EventHandler(this.nudScaleY_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(599, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "ScaleY:";
            // 
            // nudBorder
            // 
            this.nudBorder.Location = new System.Drawing.Point(644, 263);
            this.nudBorder.Name = "nudBorder";
            this.nudBorder.Size = new System.Drawing.Size(120, 20);
            this.nudBorder.TabIndex = 20;
            this.nudBorder.ValueChanged += new System.EventHandler(this.nudBorder_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(599, 267);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Border:";
            // 
            // FmDUIControlDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nudBorder);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nudScaleY);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nudScaleX);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudSkewY);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudSkewX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudRotate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.duiNativeControl1);
            this.Name = "FmDUIControlDemo";
            this.Text = "FmDUIControlDemo";
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkewX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkewY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DirectUI.Core.DUINativeControl duiNativeControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudRotate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudSkewX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudSkewY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudScaleX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudScaleY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudBorder;
        private System.Windows.Forms.Label label10;
    }
}