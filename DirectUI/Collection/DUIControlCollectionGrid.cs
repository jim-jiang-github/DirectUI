/******************************************************************
* 使本项目源码或本项目生成的DLL前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能，
* * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
* * 1、你可以在开发的软件产品中使用和修改本项目的源码和DLL，但是请保留所有相关的版权信息。
* * 2、不能将本项目源码与作者的其他项目整合作为一个单独的软件售卖给他人使用。
* * 3、不能传播本项目的源码和DLL，包括上传到网上、拷贝给他人等方式。
* * 4、以上协议暂时定制，由于还不完善，作者保留以后修改协议的权利。
* 
*         Copyright (C):       煎饼的归宿
*         CLR版本:             4.0.30319.42000
*         注册组织名:          Microsoft
*         命名空间名称:        DirectUI.Collection
*         文件名:              DUIControlCollectionGrid
*         当前系统时间:        2019/4/1 星期一 上午 10:13:20
*         当前登录用户名:      Administrator
*         创建年份:            2019
*         版权所有：           煎饼的归宿QQ：375324644
******************************************************************/
using DirectUI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Collection
{
    /// <summary> 网格分布控件集
    /// </summary>
    public abstract class DUIControlCollectionGrid : DUIControl.DUIControlCollection
    {
        private SizeF lastControlSize = SizeF.Empty;
        /// <summary> 每个子控件的间隔
        /// </summary>
        protected abstract float Space { get; }
        /// <summary> 横向个数
        /// </summary>
        protected abstract int HCount { get; }
        /// <summary> 纵向个数
        /// </summary>
        protected abstract int VCount { get; }
        /// <summary> 子控件尺寸
        /// </summary>
        private SizeF ControlSize => new SizeF(
            this.owner.ClientSize.Width <= 0 ? 0 : (this.owner.ClientSize.Width - this.Space * (this.HCount - 1)) / this.HCount,
            this.owner.ClientSize.Height <= 0 ? 0 : (this.owner.ClientSize.Height - this.Space * (this.VCount - 1)) / this.VCount);
        public override DUIControl this[int index]
        {
            get
            {
                if (lastControlSize != ControlSize)
                {
                    lastControlSize = ControlSize;
                    this.ResetLocation();
                }
                return base[index];
            }
            set => base[index] = value;
        }
        public override IEnumerator GetEnumerator()
        {
            if (lastControlSize != ControlSize)
            {
                lastControlSize = ControlSize;
                this.ResetLocation();
            }
            return base.GetEnumerator();
        }
        public DUIControlCollectionGrid(DUIControl owner) : base(owner)
        {
            this.owner.Resize += (s, e) => { this.ResetLocation(); };
        }
        #region 重写的方法用来执行ResetLocation
        public override void Add(DUIControl item)
        {
            base.Add(item);
            this.ResetLocation();
        }
        public override void AddRange(ICollection item)
        {
            base.AddRange(item);
            this.ResetLocation();
        }
        public override void Remove(DUIControl item)
        {
            base.Remove(item);
            this.ResetLocation();
        }
        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            this.ResetLocation();
        }
        public override void Move(int fromIndex, int toIndex)
        {
            base.Move(fromIndex, toIndex);
            this.ResetLocation();
        }
        public override void SetChildIndex(DUIControl child, int newIndex)
        {
            base.SetChildIndex(child, newIndex);
            this.ResetLocation();
        }
        #endregion
        public void ResetLocation()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Size = this.ControlSize;
                int x = i % this.HCount;
                int y = i / this.HCount;
                this[i].Location = new PointF(x * this[i].Size.Width + x * this.Space, y * this[i].Size.Height + y * this.Space);
            }
        }
    }
}
