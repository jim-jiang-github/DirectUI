using DirectUI.Common;
using DirectUI.Core;
/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2017/11/29 11:36:01
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUIRadioButton : DUICheckBox
    {
        #region 属性
        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
            set
            {
                if (base.Checked != value)
                {
                    if (!base.Checked)
                    {
                        base.Checked = value;
                        if (this.DUIParent != null)
                        {
                            this.DUIParent.DUIControls.OfType<DUIRadioButton>().Where(r => r != this).ToList().ForEach(r =>
                            {
                                r.SetChecked(false);
                            });
                        }
                    }
                }
            }
        }
        #endregion
        public DUIRadioButton()
        {
        }
        private void SetChecked(bool check)
        {
            base.Checked = check;
        }
        public override void OnPaint(DUIPaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            float radius = 6;
            using (DUIPen pen = new DUIPen(this.ForeColor, 2))
            {
                e.Graphics.DrawEllipse(pen, new RectangleF(3, 3, this.Height - 6, this.Height - 6));
            }
            if (this.Checked)
            {
                e.Graphics.FillEllipse(DUIBrushes.White, new RectangleF(this.Height / 2 - radius / 2, this.Height / 2 - radius / 2, radius, radius));
            }
            SizeF sizef = e.Graphics.MeasureString(this.Text, this.Font);
            using (DUISolidBrush brush = new DUISolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, brush, new PointF(this.Height, this.Height / 2 - sizef.Height / 2 - 2));
            }
            if (!this.Enabled)
            {
                using (DUISolidBrush sb = new DUISolidBrush(Color.FromArgb(150, Color.Gray)))
                {
                    e.Graphics.FillRectangle(sb, e.ClipRectangle);
                }
            }
        }
    }
}
