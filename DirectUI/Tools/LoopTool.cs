using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectUI
{
    internal delegate void LoopTickHandler(object sender, int index);
    internal class LoopTool
    {
        public event LoopTickHandler LoopTick;
        protected virtual void OnLoopTick(int index)
        {
            if (LoopTick != null)
            {
                LoopTick.Invoke(this, index);
            }
        }
        private bool isLooping = false;
        private Stopwatch sw = new Stopwatch();
        public int CurrentIndex { get; set; }
        public int MinIndex { get; set; }
        public int MaxIndex { get; set; }
        public int Interval { get; set; }
        public bool IsLooping { get => isLooping; set => isLooping = value; }
        public LoopTool(int interval = 100, int minIndex = 0, int maxIndex = 0)
        {
            this.Interval = interval;
            this.MinIndex = minIndex;
            this.MaxIndex = maxIndex;
        }
        public void Start()
        {
            if (this.IsLooping) { return; }
            this.IsLooping = true;
            Task.Factory.StartNew(()=> 
            {
                int spendTime = this.Interval; //当前花费的时长，一帧时长等于(1000/FPS)现在默认为40ms
                while (this.IsLooping)
                {
                    sw.Reset();
                    sw.Start();
                    int indexIncrease = (spendTime - spendTime % this.Interval) / this.Interval; //通过花费时长计算出帧数的增长量
                    CurrentIndex = (indexIncrease + CurrentIndex) > MaxIndex - 1 ? MinIndex : indexIncrease + CurrentIndex;
                    OnLoopTick(CurrentIndex);
                    spendTime = spendTime % this.Interval; //计算出剩余的花费时间，用于累加到下次的绘图总
                    sw.Stop();
                    if ((int)sw.ElapsedMilliseconds < this.Interval) //如果绘图消耗的时间小于一个刷新周期
                    {
                        Thread.Sleep(this.Interval - (int)sw.ElapsedMilliseconds); //那么就补上一定的延迟凑满一个刷新周期
                        //System.Threading.Timer t = new System.Threading.Timer((a) =>
                        //{
                        //    are.Set();
                        //}, null, this.Interval - (int)sw.ElapsedMilliseconds, System.Threading.Timeout.Infinite);
                        //are.WaitOne();
                        spendTime += this.Interval; //花费时间累加上一个刷新周期
                    }
                    else
                    {
                        spendTime += (int)sw.ElapsedMilliseconds; //如果绘图消耗的时间大于预计的刷新周期，那么花费时长累加上绘图消耗的时间
                    }
                }
            });
        }
        public void Reset()
        {
            this.CurrentIndex = MinIndex;
        }
        public void Stop()
        {
            if (!this.IsLooping) { return; }
            this.IsLooping = false;
        }
    }
}
