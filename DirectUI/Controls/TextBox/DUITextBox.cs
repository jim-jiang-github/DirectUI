using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Core;
using DirectUI.Win32.Struct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Controls
{
    public class DUITextBox : DUIControl
    {
        #region 变量
        private SizeF expandSize = SizeF.Empty;
        private bool autoExpandSize = false;
        private bool wordWrap = true;
        private string watermark = string.Empty;
        internal Document1 words = null;
        private IntPtr hImmc;
        private bool multiline = false;
        private HorizontalAlignment textAlign = HorizontalAlignment.Left;
        #endregion
        #region 属性
        /// <summary> 根据文字的输入自动扩展尺寸
        /// </summary>
        public bool AutoExpandSize
        {
            get { return autoExpandSize; }
            set
            {
                if (autoExpandSize != value)
                {
                    autoExpandSize = value;
                    if (value)
                    {
                        float width = 0;
                        float height = 0;
                        if (this.Text == "")
                        {
                            width = this.TextSize(" ").Width + this.Border.BorderWidth * 2 + 6;
                            height = this.Font.GetHeight() + this.Border.BorderWidth * 2 + 6;
                        }
                        else
                        {
                            width = this.TextSize(this.Text).Width + this.Border.BorderWidth * 2 + 6;
                            height = this.words.LineWords.Count * this.Font.GetHeight() + this.Border.BorderWidth * 2 + 6;
                        }
                        this.UpdateBounds(this.X, this.Y, width, height, this.CenterX, this.CenterY, this.Rotate, this.SkewX, this.SkewY, this.ScaleX, this.ScaleY);
                    }
                    this.Invalidate();
                }
            }
        }
        /// <summary> 如果多行文本框控件可换行，则为 true；如果当用户键入的内容超过了控件的右边缘时，文本框控件自动水平滚动，则为 false。默认值为 true。
        /// </summary>
        public bool WordWrap
        {
            get { return wordWrap; }
            set
            {
                if (wordWrap != value)
                {
                    wordWrap = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 水印
        /// </summary>
        public string Watermark
        {
            get { return watermark; }
            set
            {
                if (watermark != value)
                {
                    watermark = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 获取或设置一个值，该值指示此控件是否为多行控件
        /// </summary>
        public bool Multiline
        {
            get { return multiline; }
            set
            {
                if (multiline != value)
                {
                    multiline = value;
                    this.Invalidate();
                }
            }
        }
        public HorizontalAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                if (textAlign != value)
                {
                    textAlign = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 获取或设置字符，该字符用于屏蔽单行 DUITextBox 控件中的密码字符
        /// </summary>
        public char PasswordChar { get; set; }
        #endregion
        public DUITextBox()
        {
            this.BorderWidth = 1;
            this.BackColor = Color.White;
            this.words = new Document1(this);
            //this.words.OffsetY = -100;
            //this.Text = "";
            //this.Text = "asdasd";
            //this.Watermark = "asdasdasd";
            this.Text = "as阿什顿\r\n驱蚊器玩儿玩儿儿童电饭锅\r\n奥迪女骄傲和";
            Document document = this.Text;
            //this.Text = "爱上大告诉很多个";
        }
        public override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            hImmc = Win32.NativeMethods.ImmGetContext(this.Handle);
            //var asd = this.TextSize(this.Text);
        }
        #region 重写
        public override string Text
        {
            get
            {
                if (base.Text == "") { return ""; }
                if (this.PasswordChar == '\0')
                {
                    return base.Text;
                }
                else
                {
                    return string.Join("\r\n", base.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None)
                        .Select(w => w == "" ? w : string.Concat(w.Select(a => this.PasswordChar.ToString()))));
                }
            }
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    this.words.LoadDocument(value);
                }
            }
        }
        public override void OnTextChanged(EventArgs e)
        {
            if (AutoExpandSize)
            {
                float width = 0;
                float height = 0;
                if (this.Text == "")
                {
                    width = this.TextSize(" ").Width + this.Border.BorderWidth * 2 + 6;
                    height = this.Font.GetHeight() + this.Border.BorderWidth * 2 + 6;
                }
                else
                {
                    width = this.TextSize(this.Text).Width + this.Border.BorderWidth * 2 + 6;
                    height = this.words.LineWords.Count * this.Font.GetHeight() + this.Border.BorderWidth * 2 + 6;
                }
                this.UpdateBounds(this.X, this.Y, width, height, this.CenterX, this.CenterY, this.Rotate, this.SkewX, this.SkewY, this.ScaleX, this.ScaleY);
            }
            base.OnTextChanged(e);
        }
        public void SetText(string text)
        {
            base.Text = text;
        }
        //public override int Width
        //{
        //    get
        //    {
        //        if (AutoExpandSize)
        //        {
        //            if (this.Text == "")
        //            {
        //                return (int)this.TextSize(" ").Width + this.Border.BorderWidth * 2 + 6;
        //            }
        //            return (int)this.TextSize(this.Text).Width + this.Border.BorderWidth * 2 + 6;
        //        }
        //        return base.Width;
        //    }
        //    set
        //    {
        //        base.Width = value;
        //    }
        //}
        public override float Height
        {
            get
            {
                if (AutoExpandSize)
                {
                    return base.Height;
                    //if (this.Text == "")
                    //{
                    //    return (int)this.Font.GetHeight() + this.Border.BorderWidth * 2 + 6;
                    //}
                    //return (int)(this.words.LineWords.Count * this.Font.GetHeight()) + this.Border.BorderWidth * 2 + 6;
                    ////return (int)this.TextSize(this.Text).Height;
                }
                if (this.Multiline)
                {
                    return base.Height;
                }
                else
                {
                    return 21;
                    //return 321;
                }
            }
            set
            {
                base.Height = value;
            }
        }
        public override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.words.ShowPos();
            this.Invalidate();
            f = true;
        }
        private bool f = false;
        private bool isMouseDown = false;
        //public virtual Rectangle BottomMoveBounds
        //{
        //    get { return new Rectangle(this.ClientRectangle.Location, new Size(VertexSize.Width, this.Height)); }
        //}
        public override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.words.HidePos();
            this.Invalidate();
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (f && this.words.SeletPos.StartPos != null && this.words.SeletPos.EndPos != null)
                {
                    f = false;
                    base.OnMouseDown(e);
                    return;
                }
                isMouseDown = true;
                this.words.SeletPos.StartPos = null;
                this.words.SeletPos.EndPos = null;
                PosPosition posPosition = this.PointToTextPosition(e.Location);
                this.words.SetPos(posPosition);
                this.words.SeletPos.StartPos = posPosition;
            }
            base.OnMouseDown(e);
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && isMouseDown)
            {
                PosPosition posPosition = this.PointToTextPosition(e.Location);
                this.words.SetPos(posPosition);
                if (this.words.SeletPos.StartPos == posPosition)
                {

                }
                this.words.SeletPos.EndPos = posPosition;
            }
            base.OnMouseMove(e);
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isMouseDown = false;
            }
            base.OnMouseUp(e);
        }
        public override void OnMouseDoubleClick(DUIMouseEventArgs e)
        {
            this.PerformSelectAll();
            base.OnMouseDoubleClick(e);
        }
        public override void OnKeyDown(KeyEventArgs e)
        {
            //Keys key = e.KeyData & Keys.KeyCode;
            Keys modifier = e.KeyData & Keys.Modifiers;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    this.PerformLeft();
                    break;
                case Keys.Right:
                    this.PerformRight();
                    break;
                case Keys.Up:
                    this.PerformUp();
                    break;
                case Keys.Down:
                    this.PerformDown();
                    break;
                case Keys.Space:
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    PerformEnter();
                    break;
                case Keys.ControlKey:
                    break;
                // Make sure these are not used to scroll the document around
                case Keys.Home | Keys.Shift:
                case Keys.Home:
                case Keys.End:
                case Keys.End | Keys.Shift:
                case Keys.Next | Keys.Shift:
                case Keys.Next:
                case Keys.Prior | Keys.Shift:
                case Keys.Prior:
                    //OnKeyPress(e.KeyCode);
                    e.Handled = true;
                    break;
                case Keys.Tab:
                    if ((e.Modifiers & Keys.Control) == 0)
                    {
                        //OnKeyPress(e.KeyCode);
                        e.Handled = true;
                    }
                    break;
                case Keys.Back:
                    if (modifier == Keys.Control)
                    {
                        PerformControlBack();
                    }
                    else
                    {
                        PerformBack();
                    }
                    e.Handled = true;
                    break;
                case Keys.Delete:
                    if (modifier == Keys.Control)
                    {
                        PerformControlDelete();
                    }
                    else
                    {
                        PerformDelete();
                    }
                    e.Handled = true;
                    break;
                case Keys.A:
                    if (modifier == Keys.Control)
                    {
                        PerformSelectAll();
                    }
                    break;
            }

            //// Ensure text is on screen when they are typing
            //if (this.mode != EditingMode.NotEditing)
            //{
            //    Point p = Point.Truncate(TextPositionToPoint(new Position(this.words.PosRow, this.words.PosColumn)));
            //    Rectangle bounds = Utility.RoundRectangle(DocumentWorkspace.VisibleDocumentRectangleF);
            //    bounds.Inflate(-(int)font.Height, -(int)font.Height);

            //    if (!bounds.Contains(p))
            //    {
            //        PointF newCenterPt = Utility.GetRectangleCenter((RectangleF)bounds);

            //        // horizontally off
            //        if (p.X > bounds.Right || p.Y < bounds.Left)
            //        {
            //            newCenterPt.X = p.X;
            //        }

            //        // vertically off
            //        if (p.Y > bounds.Bottom || p.Y < bounds.Top)
            //        {
            //            newCenterPt.Y = p.Y;
            //        }

            //        DocumentWorkspace.DocumentCenterPointF = newCenterPt;
            //    }
            //}
            //base.OnKeyDown(e);
        }
        public override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
        public override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13: // Enter
                    if (!this.Multiline)
                    {
                        e.Handled = true;
                    }
                    break;
                case (char)1: //Crtl+A
                case (char)127: //Crtl+Back
                case (char)8:  // Back
                    e.Handled = true;
                    break;
            }
            if (!e.Handled)
            {
                e.Handled = true;
                char a = e.KeyChar; //英文 
                this.words.AppendWord(a);
                //this.Text += a.ToString();
                //SetPosLocation(this.TextSize(this.Text).Width);
            }
            base.OnKeyPress(e);
        }
        public override void OnLocationChanged(EventArgs e)
        {
            this.words.RefreshPos();
            base.OnLocationChanged(e);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32.Const.WM.WM_IME_SETCONTEXT && m.WParam.ToInt32() == 1)
            {
                Win32.NativeMethods.ImmAssociateContext(this.Handle, hImmc);
            }
            base.WndProc(ref m);
        }
        //protected override void DrawBounds(DUIPaintEventArgs e)
        //{
        //    RectangleF borderRect = new RectangleF(0, 0, this.Width - 1, this.Height - 1);
        //    using (DUIPen borderPen = new DUIPen(this.Focused ? Color.SteelBlue : this.Border.BorderColor, this.Border.BorderWidth))
        //    using (DUIPen backPen = new DUIPen(this.BackColor, 1))
        //    using (DUISolidBrush backBrush = new DUISolidBrush(this.BackColor))
        //    {
        //        e.Graphics.FillRoundedRectangle(backBrush, borderRect, this.Radius);
        //        e.Graphics.DrawRoundedRectangle(backPen, borderRect, this.Radius);
        //        if (this.Border.BorderWidth > 0)
        //        {
        //            if (this.Border.BorderWidth == 1)
        //            {
        //                e.Graphics.DrawRoundedRectangle(borderPen, new RectangleF(0, 0, this.Width - 1, this.Height - 1), this.Radius);
        //            }
        //            else
        //            {
        //                e.Graphics.DrawRoundedRectangle(borderPen, new RectangleF(this.Border.BorderWidth / 2, this.Border.BorderWidth / 2, this.Width - this.Border.BorderWidth, this.Height - this.Border.BorderWidth), this.Radius);
        //            }
        //        }
        //    }
        //}
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            RectangleF borderRect = new RectangleF(0, 0, this.Width, this.Height);
            using (DUIPen borderPen = new DUIPen(this.Focused ? Color.Yellow : this.Border.BorderColor, this.Border.BorderWidth))
            using (DUIPen backPen = new DUIPen(this.BackColor, 1))
            using (DUISolidBrush backBrush = new DUISolidBrush(this.BackColor))
            {
                e.Graphics.FillRoundedRectangle(backBrush, borderRect, this.Radius);
                e.Graphics.DrawRoundedRectangle(backPen, borderRect, this.Radius);
                if (this.Border.BorderWidth > 0)
                {
                    if (this.Border.BorderWidth == 1)
                    {
                        e.Graphics.DrawRoundedRectangle(borderPen, new RectangleF(0, 0, this.Width, this.Height), this.Radius);
                    }
                    else
                    {
                        e.Graphics.DrawRoundedRectangle(borderPen, new RectangleF(this.Border.BorderWidth / 2, this.Border.BorderWidth / 2, this.Width - this.Border.BorderWidth, this.Height - this.Border.BorderWidth), this.Radius);
                    }
                }
            }
            if (this.BackgroundImage != null)
            {
                e.Graphics.DrawImage(this.BackgroundImage, new RectangleF(this.Border.BorderWidth, this.Border.BorderWidth, this.ClientSize.Width, this.ClientSize.Height));
            }
        }
        public override void OnPaint(DUIPaintEventArgs e)
        {
            base.OnPaint(e);
            StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
            format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            if (this.Text == string.Empty)
            {
                if (this.Watermark != string.Empty && !this.Focused)
                {
                    using (DUISolidBrush brush = new DUISolidBrush(Color.Gray))
                    {
                        e.Graphics.DrawString(this.Watermark, this.Font, brush, new PointF(this.words.OffsetX, this.words.OffsetY));
                    }
                }
            }
            else
            {
                e.Graphics.DrawString(this.Text, this.Font, DUIBrushes.Black, new PointF(this.words.OffsetX, this.words.OffsetY));
                if (this.words.SeletPos.StartPos != null && this.words.SeletPos.EndPos != null && this.words.SeletPos.StartPos != this.words.SeletPos.EndPos)
                {
                    int startRow = this.words.SeletPos.StartRow;
                    int endRow = this.words.SeletPos.EndRow;
                    int startColumn = this.words.SeletPos.StartColumn;
                    int endColumn = this.words.SeletPos.EndColumn;
                    PointF pStart = this.TextPositionToPoint(new PosPosition(startRow, startColumn));
                    PointF pEnd = this.TextPositionToPoint(new PosPosition(endRow, endColumn));
                    List<RectangleF> rects = new List<RectangleF>();
                    for (int i = startRow; i <= endRow; i++)
                    {
                        if (i == startRow)
                        {
                            if (endRow > startRow)
                            {
                                rects.Add(new RectangleF(new PointF(pStart.X, pStart.Y), new SizeF(this.TextSize(this.words.LineWords[startRow]).Width - pStart.X + this.words.OffsetX, this.Font.GetHeight())));
                            }
                            else
                            {
                                rects.Add(new RectangleF(new PointF(pStart.X, pStart.Y), new SizeF(pEnd.X - pStart.X, this.Font.GetHeight())));
                            }
                        }
                        else if (i == endRow && startRow != endRow)
                        {
                            rects.Add(new RectangleF(new PointF(this.words.OffsetX, pEnd.Y), new SizeF(pEnd.X - this.words.OffsetX, this.Font.GetHeight())));
                        }
                        else
                        {
                            rects.Add(new RectangleF(new PointF(this.words.OffsetX, i * this.Font.GetHeight() + this.words.OffsetY), new SizeF(this.TextSize(this.words.LineWords[i]).Width, this.Font.GetHeight())));
                        }
                    }
                    using (DUISolidBrush brush = new DUISolidBrush(this.Focused ? Color.FromArgb(51, 153, 255) : Color.FromArgb(134, 134, 134)))
                    {
                        e.Graphics.FillRectangles(brush, rects.ToArray());
                    }
                    if (startColumn == 0)
                    {
                        e.Graphics.DrawString(this.words.SelectText, this.Font, DUIBrushes.White, new PointF(this.words.OffsetX + this.TextSize(this.words.LineWords[startRow].Substring(0, startColumn)).Width, pStart.Y));
                    }
                    else
                    {
                        e.Graphics.DrawString(this.words.LineWords[startRow].Substring(0, startColumn) + this.words.SelectText, this.Font, DUIBrushes.White, new PointF(this.words.OffsetX, pStart.Y));
                        e.Graphics.DrawString(this.words.LineWords[startRow].Substring(0, startColumn), this.Font, DUIBrushes.Black, new PointF(this.words.OffsetX, pStart.Y));
                    }
                }
            }
        }
        #endregion
        #region 函数
        private void PerformSelectAll()
        {
            this.words.SeletPos.StartPos = new PosPosition(0, 0);
            this.words.SeletPos.EndPos = new PosPosition(this.words.LineWords.Count - 1, this.words.LineWords[this.words.LineWords.Count - 1].Length);
            this.words.SetPos(new PosPosition(this.words.LineWords.Count - 1, this.words.LineWords[this.words.LineWords.Count - 1].Length));
        }
        private void PerformBack()
        {
            if (this.words.SeletPos.StartPos != null && this.words.SeletPos.EndPos != null && this.words.SeletPos.StartPos != this.words.SeletPos.EndPos)
            {
                if (this.words.SeletPos.StartRow == this.words.SeletPos.EndRow)
                {
                    string currentLine = this.words.LineWords[this.words.SeletPos.StartRow];
                    this.words.LineWords[this.words.SeletPos.StartRow] = currentLine.Substring(0, this.words.SeletPos.StartColumn) + currentLine.Substring(this.words.SeletPos.EndColumn);
                    this.words.SetPos(new PosPosition(this.words.SeletPos.StartRow, this.words.SeletPos.StartColumn));
                }
                else
                {
                    string line1 = this.words.LineWords[this.words.SeletPos.StartRow];
                    string line2 = this.words.LineWords[this.words.SeletPos.EndRow];
                    this.words.LineWords[this.words.SeletPos.StartRow] = line1.Substring(0, this.words.SeletPos.StartColumn) + line2.Substring(this.words.SeletPos.EndColumn);
                    if (this.words.SeletPos.EndRow - this.words.SeletPos.StartRow >= 1)
                    {
                        for (int i = this.words.SeletPos.EndRow; i >= this.words.SeletPos.StartRow + 1; i--)
                        {
                            this.words.LineWords.RemoveAt(i);
                        }
                    }
                    this.words.SetPos(new PosPosition(this.words.SeletPos.StartRow, this.words.SeletPos.StartColumn));
                }
                this.words.SeletPos.StartPos = null;
                this.words.SeletPos.EndPos = null;
            }
            else
            {
                if (this.words.PosColumn == 0 && this.words.PosRow > 0)
                {
                    int ntp = this.words.LineWords[this.words.PosRow - 1].Length;
                    this.words.LineWords[this.words.PosRow - 1] = this.words.LineWords.ClearText(this.words.PosRow - 1) + this.words.LineWords.ClearText(this.words.PosRow);
                    this.words.LineWords.RemoveAt(this.words.PosRow);
                    this.words.SetPos(new PosPosition(this.words.PosRow - 1, ntp));
                }
                else if (this.words.PosColumn > 0)
                {
                    string ln = this.words.LineWords[this.words.PosRow];
                    if (this.words.PosColumn == ln.Length)
                    {
                        this.words.LineWords[this.words.PosRow] = ln.Substring(0, ln.Length - 1);
                    }
                    else
                    {
                        this.words.LineWords[this.words.PosRow] = ln.Substring(0, this.words.PosColumn - 1) + ln.Substring(this.words.PosColumn);
                    }
                    this.words.SetPos(new PosPosition(this.words.PosRow, this.words.PosColumn - 1));
                }
            }
            this.SetText(this.words.ToString());
        }
        private void PerformControlBack()
        {
            if (this.words.PosColumn == 0 && this.words.PosRow > 0)
            {
                PerformBack();
            }
            else if (this.words.PosColumn > 0)
            {
                string currentLine = this.words.LineWords[this.words.PosRow];
                int ntp = this.words.PosColumn;

                if (Char.IsLetterOrDigit(currentLine[ntp - 1]))
                {
                    while (ntp > 0 && (Char.IsLetterOrDigit(currentLine[ntp - 1])))
                    {
                        ntp--;
                    }
                }
                else if (Char.IsWhiteSpace(currentLine[ntp - 1]))
                {
                    while (ntp > 0 && (Char.IsWhiteSpace(currentLine[ntp - 1])))
                    {
                        ntp--;
                    }
                }
                else if (Char.IsPunctuation(currentLine[ntp - 1]))
                {
                    while (ntp > 0 && (Char.IsPunctuation(currentLine[ntp - 1])))
                    {
                        ntp--;
                    }
                }
                else
                {
                    ntp--;
                }
                this.words.LineWords[this.words.PosRow] = currentLine.Substring(0, ntp) + currentLine.Substring(this.words.PosColumn);
                this.words.SetPos(new PosPosition(this.words.PosRow, ntp));
            }
            this.SetText(this.words.ToString());
        }
        private void PerformDelete()
        {
            if (this.words.SeletPos.StartPos != null && this.words.SeletPos.EndPos != null && this.words.SeletPos.StartPos != this.words.SeletPos.EndPos)
            {
                if (this.words.SeletPos.StartRow == this.words.SeletPos.EndRow)
                {
                    string currentLine = this.words.LineWords[this.words.SeletPos.StartRow];
                    this.words.LineWords[this.words.SeletPos.StartRow] = currentLine.Substring(0, this.words.SeletPos.StartColumn) + currentLine.Substring(this.words.SeletPos.EndColumn);
                    this.words.SetPos(new PosPosition(this.words.SeletPos.StartRow, this.words.SeletPos.StartColumn));
                }
                else
                {
                    string line1 = this.words.LineWords[this.words.SeletPos.StartRow];
                    string line2 = this.words.LineWords[this.words.SeletPos.EndRow];
                    this.words.LineWords[this.words.SeletPos.StartRow] = line1.Substring(0, this.words.SeletPos.StartColumn) + line2.Substring(this.words.SeletPos.EndColumn);
                    if (this.words.SeletPos.EndRow - this.words.SeletPos.StartRow >= 1)
                    {
                        for (int i = this.words.SeletPos.EndRow; i >= this.words.SeletPos.StartRow + 1; i--)
                        {
                            this.words.LineWords.RemoveAt(i);
                        }
                    }
                    this.words.SetPos(new PosPosition(this.words.SeletPos.StartRow, this.words.SeletPos.StartColumn));
                }
                this.words.SeletPos.StartPos = null;
                this.words.SeletPos.EndPos = null;
            }
            else
            {
                if (this.words.PosRow == this.words.LineWords.Count - 1 && this.words.PosColumn == this.words.LineWords[this.words.LineWords.Count - 1].Length)
                {
                    return;
                }
                else if (this.words.PosColumn == this.words.LineWords[this.words.PosRow].Length)
                {
                    this.words.LineWords[this.words.PosRow] = this.words.LineWords[this.words.PosRow] + this.words.LineWords[this.words.PosRow + 1];
                    this.words.LineWords.RemoveAt(this.words.PosRow + 1);
                }
                else
                {
                    this.words.LineWords[this.words.PosRow] = this.words.LineWords.ClearText(this.words.PosRow).Substring(0, this.words.PosColumn) + this.words.LineWords.ClearText(this.words.PosRow).Substring(this.words.PosColumn + 1);
                }
            }
            this.SetText(this.words.ToString());
        }
        private void PerformControlDelete()
        {
            if (this.words.PosRow == this.words.LineWords.Count - 1 && this.words.PosColumn == this.words.LineWords[this.words.LineWords.Count - 1].Length)
            {
                return;
            }
            else if (this.words.PosColumn == this.words.LineWords[this.words.PosRow].Length)
            {
                this.words.LineWords[this.words.PosRow] = this.words.LineWords[this.words.PosRow] + this.words.LineWords[this.words.PosRow + 1];
                this.words.LineWords.RemoveAt(this.words.PosRow + 1);
            }
            else
            {
                int ntp = this.words.PosColumn;
                string currentLine = this.words.LineWords[this.words.PosRow];

                if (Char.IsLetterOrDigit(currentLine[ntp]))
                {
                    while (ntp < currentLine.Length && (Char.IsLetterOrDigit(currentLine[ntp])))
                    {
                        currentLine = currentLine.Remove(ntp, 1);
                    }
                }
                else if (Char.IsWhiteSpace(currentLine[ntp]))
                {
                    while (ntp < currentLine.Length && (Char.IsWhiteSpace(currentLine[ntp])))
                    {
                        currentLine = currentLine.Remove(ntp, 1);
                    }
                }
                else if (Char.IsPunctuation(currentLine[ntp]))
                {
                    while (ntp < currentLine.Length && (Char.IsPunctuation(currentLine[ntp])))
                    {
                        currentLine = currentLine.Remove(ntp, 1);
                    }
                }
                else
                {
                    ntp--;
                }
                this.words.LineWords[this.words.PosRow] = currentLine;
            }
            this.SetText(this.words.ToString());
        }
        private void PerformEnter()
        {
            string currentLine = this.words.LineWords[this.words.PosRow];

            if (this.words.PosColumn == currentLine.Length)
            {
                // If we are at the end of a line, insert an empty line at the next line
                this.words.LineWords.Insert(this.words.PosRow + 1, string.Empty);
            }
            else
            {
                this.words.LineWords.Insert(this.words.PosRow + 1, currentLine.Substring(this.words.PosColumn, currentLine.Length - this.words.PosColumn));
                this.words.LineWords[this.words.PosRow] = this.words.LineWords.ClearText(this.words.PosRow).Substring(0, this.words.PosColumn);
            }
            this.words.SetPos(new PosPosition(this.words.PosRow + 1, 0));
            this.SetText(this.words.ToString());
        }

        private void PerformLeft()
        {
            int column = this.words.PosColumn;
            int row = this.words.PosRow;
            if (column > 0)
            {
                column--;
            }
            else if (row > 0)
            {
                row--;
                column = this.words.LineWords[row].Length;
            }
            this.words.SetPos(new PosPosition(row, column));
        }
        private void PerformControlLeft()
        {
            //if (this.words.PosColumn > 0)
            //{
            //    int ntp = this.words.PosColumn;
            //    string currentLine = (string)lines[this.words.PosRow];

            //    if (Char.IsLetterOrDigit(currentLine[ntp - 1]))
            //    {
            //        while (ntp > 0 && (Char.IsLetterOrDigit(currentLine[ntp - 1])))
            //        {
            //            ntp--;
            //        }
            //    }
            //    else if (Char.IsWhiteSpace(currentLine[ntp - 1]))
            //    {
            //        while (ntp > 0 && (Char.IsWhiteSpace(currentLine[ntp - 1])))
            //        {
            //            ntp--;
            //        }
            //    }
            //    else if (ntp > 0 && Char.IsPunctuation(currentLine[ntp - 1]))
            //    {
            //        while (ntp > 0 && Char.IsPunctuation(currentLine[ntp - 1]))
            //        {
            //            ntp--;
            //        }
            //    }
            //    else
            //    {
            //        ntp--;
            //    }

            //    this.words.PosColumn = ntp;
            //}
            //else if (this.words.PosColumn == 0 && this.words.PosRow > 0)
            //{
            //    this.words.PosRow--;
            //    this.words.PosColumn = ((string)lines[this.words.PosRow]).Length;
            //}
        }
        private void PerformRight()
        {
            int column = this.words.PosColumn;
            int row = this.words.PosRow;
            if (column < this.words.LineWords[row].Length)
            {
                column++;
            }
            else if (column == this.words.LineWords[row].Length && row < this.words.LineWords.Count - 1)
            {
                row++;
                column = 0;
            }
            this.words.SetPos(new PosPosition(row, column));
        }
        private void PerformControlRight()
        {
            //if (this.words.PosColumn < ((string)lines[this.words.PosRow]).Length)
            //{
            //    int ntp = this.words.PosColumn;
            //    string currentLine = (string)lines[this.words.PosRow];

            //    if (Char.IsLetterOrDigit(currentLine[ntp]))
            //    {
            //        while (ntp < currentLine.Length && (Char.IsLetterOrDigit(currentLine[ntp])))
            //        {
            //            ntp++;
            //        }
            //    }
            //    else if (Char.IsWhiteSpace(currentLine[ntp]))
            //    {
            //        while (ntp < currentLine.Length && (Char.IsWhiteSpace(currentLine[ntp])))
            //        {
            //            ntp++;
            //        }
            //    }
            //    else if (ntp > 0 && Char.IsPunctuation(currentLine[ntp]))
            //    {
            //        while (ntp < currentLine.Length && Char.IsPunctuation(currentLine[ntp]))
            //        {
            //            ntp++;
            //        }
            //    }
            //    else
            //    {
            //        ntp++;
            //    }

            //    this.words.PosColumn = ntp;
            //}
            //else if (this.words.PosColumn == ((string)lines[this.words.PosRow]).Length && this.words.PosRow < lines.Count - 1)
            //{
            //    this.words.PosRow++;
            //    this.words.PosColumn = 0;
            //}
        }
        private void PerformUp()
        {
            PointF p = TextPositionToPoint(new PosPosition(this.words.PosRow, this.words.PosColumn));
            p.Y -= this.Font.GetHeight();
            this.words.SetPos(PointToTextPosition(Point.Ceiling(p)));
        }
        private void PerformDown()
        {
            PointF p = TextPositionToPoint(new PosPosition(this.words.PosRow, this.words.PosColumn));
            p.Y += this.Font.GetHeight();
            this.words.SetPos(PointToTextPosition(Point.Ceiling(p)));
        }
        internal SizeF TextSize(string text)
        {
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
                format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                SizeF sf = g.MeasureString(text, this.Font, new PointF(0, 0), format);
                //sf.Height = this.Font.GetHeight();
                return sf;
                //return Size.Ceiling(sf);
            }
        }
        private void InsertCharIntoString(char c)
        {
            //lines[this.words.PosRow] = ((string)lines[this.words.PosRow]).Insert(this.words.PosColumn, c.ToString());
            //this.sizes = null;
        }
        /// <summary> 通过控件的坐标来查找光标位置
        /// </summary>
        /// <param name="p">控件的坐标</param>
        /// <returns>光标位置</returns>
        private PosPosition PointToTextPosition(PointF p)
        {
            float lineHeight = this.Font.GetHeight();
            float x = p.X - this.words.OffsetX;
            float y = p.Y - this.words.OffsetY;
            PosPosition posPosition = new PosPosition();
            int row = (int)(y / lineHeight);
            if (row < 0)
            {
                row = 0;
            }
            if (row > this.words.LineWords.Count - 1)
            {
                row = this.words.LineWords.Count - 1;
            }
            posPosition.Row = row;
            string line = this.words.LineWords[posPosition.Row];
            for (int i = 1; i <= line.Length; i++)
            {
                string linePart = line.Substring(0, i);
                float end = this.TextSize(linePart).Width;
                string word = line.Substring(i - 1, 1);
                float start = end - this.TextSize(word).Width;
                float mid = (start + end) / 2;
                if (end > x)
                {
                    if (x < mid)
                    {
                        posPosition.Column = i - 1;
                    }
                    else
                    {
                        posPosition.Column = i;
                    }
                    break;
                }
                if (i == line.Length)
                {
                    posPosition.Column = line.Length;
                }
            }
            return posPosition;
        }
        private PointF TextPositionToPoint(PosPosition p)
        {
            PointF pf = new PointF(this.words.OffsetX, this.words.OffsetY);
            SizeF sz = TextSize(this.words.LineWords[p.Row].Substring(0, p.Column));
            SizeF fullSz = TextSize(this.words.LineWords[p.Row]);
            pf.X = sz.Width + this.words.OffsetX;
            pf.Y = p.Row * this.Font.GetHeight() + this.words.OffsetY;
            return pf;
            //pf = new PointF(clickPoint.X + sz.Width, clickPoint.Y + (sz.Height * p.Line));

            switch (this.textAlign)
            {
                case HorizontalAlignment.Left:
                    //pf = new PointF(clickPoint.X + sz.Width, clickPoint.Y + (sz.Height * p.Line));
                    break;

                case HorizontalAlignment.Center:
                    //pf = new PointF(clickPoint.X + (sz.Width - (fullSz.Width / 2)), clickPoint.Y + (sz.Height * p.Line));
                    break;

                case HorizontalAlignment.Right:
                    //pf = new PointF(clickPoint.X + (sz.Width - fullSz.Width), clickPoint.Y + (sz.Height * p.Line));
                    break;

                default:
                    break;
                    //throw new InvalidEnumArgumentException("Invalid Alignment");
            }

            return pf;
        }
        #endregion
    }
    /// <summary> 文档对象
    /// </summary>
    public class Document1
    {
        #region 只读
        private readonly string separator = "\r\n"; //分隔符
        #endregion
        #region 变量
        private DUITextBox owner = null;
        private Pos pos = null;
        private int offsetX = 3;
        private int offsetY = 3;
        private LineWordCollection lineWords = null;
        private SeletPos seletPos = null;
        #endregion
        #region 属性
        public string SelectText
        {
            get
            {
                int startRow = 0;
                int endRow = 0;
                int startColumn = 0;
                int endColumn = 0;
                if (this.SeletPos.StartPos.Row * 10 + this.SeletPos.StartPos.Column <= this.SeletPos.EndPos.Row * 10 + this.SeletPos.EndPos.Column)
                {
                    startRow = this.SeletPos.StartPos.Row;
                    endRow = this.SeletPos.EndPos.Row;
                    startColumn = this.SeletPos.StartPos.Column;
                    endColumn = this.SeletPos.EndPos.Column;
                }
                else
                {
                    endRow = this.SeletPos.StartPos.Row;
                    startRow = this.SeletPos.EndPos.Row;
                    endColumn = this.SeletPos.StartPos.Column;
                    startColumn = this.SeletPos.EndPos.Column;
                }
                string text = "";
                for (int i = startRow; i <= endRow; i++)
                {
                    if (i == startRow)
                    {
                        if (endRow > startRow)
                        {
                            text += this.LineWords[i].Substring(startColumn, this.LineWords[i].Length - startColumn) + this.separator;
                        }
                        else
                        {
                            text += this.LineWords[i].Substring(startColumn, endColumn - startColumn) + this.separator;
                        }
                    }
                    else if (i == endRow && startRow != endRow)
                    {
                        text += this.LineWords[i].Substring(0, endColumn);
                    }
                    else
                    {
                        text += this.LineWords[i] + this.separator;
                    }
                }
                return text;
            }
        }
        public SeletPos SeletPos
        {
            get { return seletPos; }
        }
        public LineWordCollection LineWords
        {
            get { return lineWords; }
        }
        public int PosRow { get { return pos.Row; } }
        public int PosColumn { get { return pos.Column; } }
        public int OffsetX
        {
            get { return offsetX; }
            set { offsetX = value; }
        }
        public int OffsetY
        {
            get { return offsetY; }
            set { offsetY = value; }
        }
        #endregion
        public Document1(DUITextBox owner)
        {
            this.owner = owner;
            this.pos = new Pos(owner);
            this.seletPos = new SeletPos(owner);
            this.lineWords = new LineWordCollection(this);
        }
        #region 函数
        /// <summary> 加载文档
        /// </summary>
        /// <param name="document">字符串</param>
        public void LoadDocument(string document)
        {
            string[] lines = document.Split(new string[] { this.separator }, StringSplitOptions.None);
            this.lineWords.Clear();
            foreach (string line in lines)
            {
                this.lineWords.Add(line);
            }
            this.pos.PosPosition = new PosPosition(this.lineWords.Count - 1, this.lineWords[this.lineWords.Count - 1].Length);
            //this.pos.Column = 0;
            //this.pos.Row = this.lineWords.Count - 1;
            //this.pos.Column = this.lineWords[this.lineWords.Count - 1].Length;
        }
        public void ShowPos()
        {
            this.pos.ShowPos();
        }
        public void HidePos()
        {
            this.pos.HidePos();
        }
        public void SetPos(PosPosition posPosition)
        {
            this.pos.PosPosition = posPosition;
            this.owner.Invalidate();
        }
        public void RefreshPos()
        {
            this.pos.RefreshPos();
        }
        public void BackWord(Pos pos)
        {
            //if (posSelect != null)
            //{

            //}
        }
        public void DelWord(Pos pos)
        {

        }
        /// <summary> 追加单个字母
        /// </summary>
        /// <param name="singleWord"></param>
        public void AppendWord(char word)
        {
            this.lineWords[pos.Row] = this.lineWords[pos.Row].Insert(pos.Column, word.ToString());
            this.owner.SetText(this.ToString());
            pos.Column++;
        }
        /// <summary> 追加单个字母
        /// </summary>
        /// <param name="singleWord"></param>
        public void AppendString(string str)
        {
            this.lineWords[pos.Row] = this.lineWords[pos.Row].Insert(pos.Column, str);
            this.owner.SetText(this.ToString());
            pos.Column += str.Length;
        }
        #endregion
        public class LineWordCollection : DUIItemCollection<Document1, string>
        {
            public LineWordCollection(Document1 owner)
                : base(owner)
            {
                this.Add("");
            }

            public override string this[int index]
            {
                get
                {
                    if (this.owner.owner.PasswordChar == '\0')
                    {
                        return base[index];
                    }
                    else
                    {
                        if (base[index] == "") { return ""; }
                        return string.Join("\r\n", base[index].Split(new string[] { "\r\n" }, StringSplitOptions.None)
                            .Select(w => string.Concat(w.Select(a => this.owner.owner.PasswordChar.ToString()))));
                    }
                }
                set
                {
                    base[index] = value;
                }
            }

            public string ClearText(int index)
            {
                return base[index];
            }
        }
        public override string ToString()
        {
            return string.Join(separator, this.lineWords.OfType<string>());
        }
    }
    public class Document : DUIItemCollection<Line>
    {
        #region 变量
        #endregion 
        #region 属性
        #endregion 
        #region 构造
        public Document()
        {
        }
        public Document(string document)
        {
            this.LoadDocument(document);
        }
        #endregion 
        #region 函数
        /// <summary> 加载文档
        /// </summary>
        /// <param name="document">字符串</param>
        private void LoadDocument(string document)
        {
            string[] lines = document.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            this.Clear();
            foreach (string line in lines)
            {
                this.Add(line);
            }
        }
        #endregion 
        #region 重写
        #endregion 
        #region operator
        public static implicit operator Document(string document)
        {
            return new Document(document);
        }
        #endregion 
    }
    /// <summary> 行对象
    /// </summary>
    public class Line : DUIItemCollection<Word>
    {
        #region 变量
        public string text;
        #endregion 
        #region 属性
        /// <summary> 行文本
        /// </summary>
        public string Text
        {
            get => text;
            private set
            {
                text = value;
            }
        }
        #endregion 
        #region 构造
        public Line()
        {
        }
        public Line(string line)
        {
            this.LoadLine(line);
        }
        #endregion 
        #region 函数
        /// <summary> 加载文档
        /// </summary>
        /// <param name="document">字符串</param>
        private void LoadLine(string line)
        {
            this.Text = line;
            char[] words = line.ToArray();
            this.Clear();
            foreach (char word in words)
            {
                this.Add(word);
            }
        }
        #endregion 
        #region 重写
        public override string ToString()
        {
            return this.Text;
        }
        #endregion 
        #region operator
        public static implicit operator Line(string line)
        {
            return new Line(line);
        }
        #endregion 
    }
    /// <summary> 一个字符对象
    /// </summary>
    public class Word
    {
        #region 变量
        public char @char;
        #endregion 
        #region 属性
        /// <summary> 字符
        /// </summary>
        public char Char
        {
            get => @char;
            private set
            {
                @char = value;
                this.OccupiedByte = System.Text.Encoding.Default.GetBytes(new char[] { this.Char }).Length;
            }
        }
        /// <summary> 占用字符
        /// </summary>
        public int OccupiedByte { get; private set; } = 0;
        #endregion
        #region 构造
        public Word()
        {
        }
        public Word(char @char)
        {
            this.Char = @char;
        }
        #endregion 
        #region 函数
        #endregion 
        #region 重写
        public override string ToString()
        {
            return this.Char.ToString();
        }
        #endregion 
        #region operator
        public static implicit operator Word(char @char)
        {
            return new Word(@char);
        }
        #endregion 
    }
    /// <summary> 光标位置对象
    /// </summary>
    public class PosPosition
    {
        #region 变量
        private int row = 0;
        private int column = 0;
        #endregion
        #region 属性
        /// <summary> 所在的行
        /// </summary>
        public int Row
        {
            get { return row; }
            set
            {
                if (row != value)
                {
                    row = value;
                }
            }
        }
        /// <summary> 所在的列
        /// </summary>
        public int Column
        {
            get { return column; }
            set
            {
                if (column != value)
                {
                    column = value;
                }
            }
        }
        #endregion
        /// <summary> 光标位置对象
        /// </summary>
        /// <param name="row">所在的行</param>
        /// <param name="column">所在的列</param>
        public PosPosition(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
        public PosPosition()
        {
        }

        public static bool operator ==(PosPosition p1, PosPosition p2)
        {
            return Nullable.Equals(p1, p2);
        }
        public static bool operator !=(PosPosition p1, PosPosition p2)
        {
            return !Nullable.Equals(p1, p2);
        }
        public override bool Equals(object obj)
        {
            if (obj is PosPosition p)
            {
                if (p == null)
                {
                    return false;
                }
                if (p.Column == this.Column && p.Row == this.Row)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class SeletPos
    {
        public int StartRow
        {
            get
            {
                if (this.StartPos.Row * 10 + this.StartPos.Column <= this.EndPos.Row * 10 + this.EndPos.Column)
                {
                    return this.StartPos.Row;
                }
                else
                {
                    return this.EndPos.Row;
                }
            }
        }
        public int EndRow
        {
            get
            {
                if (this.StartPos.Row * 10 + this.StartPos.Column <= this.EndPos.Row * 10 + this.EndPos.Column)
                {
                    return this.EndPos.Row;
                }
                else
                {
                    return this.StartPos.Row;
                }
            }
        }
        public int StartColumn
        {
            get
            {
                if (this.StartPos.Row * 10 + this.StartPos.Column <= this.EndPos.Row * 10 + this.EndPos.Column)
                {
                    return this.StartPos.Column;
                }
                else
                {
                    return this.EndPos.Column;
                }
            }
        }
        public int EndColumn
        {
            get
            {
                if (this.StartPos.Row * 10 + this.StartPos.Column <= this.EndPos.Row * 10 + this.EndPos.Column)
                {
                    return this.EndPos.Column;
                }
                else
                {
                    return this.StartPos.Column;
                }
            }
        }
        private PosPosition startPos;
        private PosPosition endPos;
        private DUITextBox owner = null;
        public PosPosition StartPos
        {
            get { return startPos; }
            set
            {
                if (startPos != value)
                {
                    startPos = value;
                    this.owner.Invalidate();
                }
            }
        }
        public PosPosition EndPos
        {
            get { return endPos; }
            set
            {
                if (endPos != value)
                {
                    endPos = value;
                    this.owner.Invalidate();
                }
            }
        }
        public SeletPos(DUITextBox owner)
        {
            this.owner = owner;
        }
    }
    /// <summary> 光标对象
    /// </summary>
    public class Pos
    {
        #region 变量
        private PosPosition posPosition = new PosPosition(0, 0);
        private DUITextBox owner = null;
        #endregion
        #region 属性
        /// <summary> 光标所在的行
        /// </summary>
        public int Row
        {
            get { return posPosition.Row; }
            set
            {
                if (posPosition.Row != value)
                {
                    posPosition.Row = value;
                    SetPosLocation();
                }
            }
        }
        /// <summary> 光标所在的列
        /// </summary>
        public int Column
        {
            get { return posPosition.Column; }
            set
            {
                if (posPosition.Column != value)
                {
                    posPosition.Column = value;
                    SetPosLocation();
                }
            }
        }
        /// <summary> 光标所在的列
        /// </summary>
        public PosPosition PosPosition
        {
            get { return posPosition; }
            set
            {
                if (posPosition.Row != value.Row || posPosition.Column != value.Column)
                {
                    posPosition = value;
                    SetPosLocation();
                }
            }
        }
        public void RefreshPos()
        {
            SetPosLocation();
        }
        #endregion
        public Pos(DUITextBox owner)
        {
            this.owner = owner;
        }
        public void ShowPos()
        {
            Win32.NativeMethods.CreateCaret(this.owner.Handle, (IntPtr)null, 1, (int)Math.Ceiling(this.owner.Font.GetHeight()));
            SetPosLocation();
            Win32.NativeMethods.ShowCaret(this.owner.Handle);
        }
        public void HidePos()
        {
            Win32.NativeMethods.HideCaret(this.owner.Handle);
        }
        private void SetPosLocation()
        {
            Win32.NativeMethods.SetCaretPos((int)(this.owner.PointToBaseParent(new PointF(this.owner.Border.BorderWidth + 2, 0)).X + (float)Math.Ceiling(this.owner.TextSize(this.owner.words.LineWords[Row].Substring(0, Column)).Width)),
                (int)(this.owner.PointToBaseParent(new PointF(this.owner.Border.BorderWidth + 2, 2)).Y + this.owner.Border.BorderWidth + (float)Math.Ceiling(Row * this.owner.Font.GetHeight())));
        }
    }
}
