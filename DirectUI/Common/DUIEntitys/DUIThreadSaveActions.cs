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
*         命名空间名称:        DirectUI.Common.DUIEntitys
*         文件名:              DUIThreadSaveActions
*         当前系统时间:        2018/9/6 星期四 上午 9:38:05
*         当前登录用户名:      Administrator
*         创建年份:            2018
*         版权所有：           煎饼的归宿QQ：375324644
******************************************************************/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DirectUI.Common.DUIEntitys
{
    /// <summary> 线程安全的Actions执行集合
    /// </summary>
    internal static class DUIThreadSaveActions
    {
        private static ActionCollection actions = null;
        private static ActionCollection Actions => actions ?? (actions = new ActionCollection());
        /// <summary> 执行检测间隔
        /// </summary>
        static DUIThreadSaveActions()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    Actions.Do();
                }
            })
            { IsBackground = true, Name = "DUIThreadSaveActions" }.Start();
        }
        /// <summary> 添加一个待执行的Action
        /// </summary>
        /// <param name="loadingAction">加载函数</param>
        /// <param name="callback">完成加载的回调函数</param>
        public static void Add(Action loadingAction, Action callback = null)
        {
            Actions.Add(new AsynLoadingAction(loadingAction, callback));
        }
        private class ActionCollection
        {
            private readonly object lockObj = new object();
            /// <summary> 追加的Action，不直接向doingActions因为线程不是安全的
            /// </summary>
            private List<AsynLoadingAction> appendActions = new List<AsynLoadingAction>();
            /// <summary> 执行的Action集合
            /// </summary>
            private List<AsynLoadingAction> doingActions = new List<AsynLoadingAction>();
            /// <summary> 添加一个待执行的Action
            /// </summary>
            /// <param name="item"></param>
            public void Add(AsynLoadingAction item)
            {
                lock (lockObj)
                {
                    if (appendActions.Exists(a => a.Equals(item)) || doingActions.Exists(a => a.Equals(item)))
                    {
                        return;
                    }
                    this.appendActions.Add(item);
                }
            }
            /// <summary> 执行Action
            /// </summary>
            internal void Do()
            {
                lock (lockObj)
                {
                    this.doingActions.AddRange(this.appendActions);
                    this.appendActions.Clear();
                }
                foreach (AsynLoadingAction a in this.doingActions)
                {
                    a.Invoke();
                }
                this.doingActions.Clear();
            }
        }
        #region AsynLoadingAction
        /// <summary> 异步加载动作对象
        /// </summary>
        private class AsynLoadingAction
        {
            private Action loadingAction = null;
            private Action callback = null;
            /// <summary> 构造函数
            /// </summary>
            /// <param name="loadingAction">加载函数</param>
            /// <param name="callback">完成加载的回调函数</param>
            public AsynLoadingAction(Action loadingAction, Action callback = null)
            {
                this.loadingAction = loadingAction;
                this.callback = callback;
            }
            /// <summary> 执行加载动作
            /// </summary>
            public void Invoke()
            {
                if (this.loadingAction != null)
                {
                    this.loadingAction.Invoke();
                }
                if (this.callback != null)
                {
                    this.callback.Invoke();
                }
            }
            /// <summary> 是否存在一样的动作
            /// </summary>
            /// <param name="asynLoadingAction"></param>
            /// <returns></returns>
            public bool Equals(AsynLoadingAction asynLoadingAction)
            {
                return this.loadingAction == asynLoadingAction.loadingAction && this.callback == asynLoadingAction.callback;
            }
        }
        #endregion
    }
}
