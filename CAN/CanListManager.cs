using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TTOperator.Util.Log;


namespace CAN
{

    public class CanListManager
    {

        /// <summary>
        /// 队列
        /// </summary>
        private ConcurrentQueue<FrameInfo> _list;
        /// <summary>
        /// 出队的第一个数据
        /// </summary>
        private FrameInfo _lastList;
        /// <summary>
        /// 设置队列数据量上限
        /// </summary>
        private int _fullLimit;
        /// <summary>
        /// 队列数据量上限
        /// </summary>
        /// 
        /// <summary>
        /// 获取队列
        /// </summary>
        private ConcurrentQueue<FrameInfo> ListBuffer
        {
            get { return _list; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// 
        public CanListManager()
        {
            _list = new ConcurrentQueue<FrameInfo>();
            _lastList = new FrameInfo("1", "1", "1", "1", "1");
        }

        /// <summary>
        /// 增加数据到管理队列
        /// </summary>
        /// <param name="data">数据串</param>
        /// <returns></returns>
        public void Add(FrameInfo data)
        {
            try
            {
                _list.Enqueue(data);
                OnCanReceviedEvent(new CanFrameArgs(data));
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }

        }

        /// <summary>
        /// 去除队列最开始的数据
        /// </summary>
        /// <returns>是否成功去除</returns>
        public FrameInfo Remove()
        {
            if (_list.IsEmpty)
            {
                return null;
            }
            else
            {
                _list.TryDequeue(out _lastList);
                return _lastList;
            }
        }

        /// <summary>
        /// 获取队列中数据数量
        /// </summary>
        /// <returns></returns>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// 队列是否可用
        /// </summary>
        public bool IsAvailable
        {
            get { return !_list.IsEmpty; }
        }

        /// <summary>
        /// 队列满事件
        /// </summary>
        public event EventHandler<CanFrameArgs> CanReceviedEvent;
        /// <summary>
        /// 引发队列满事件
        /// </summary>
        /// <param name="e"></param>
        private void OnCanReceviedEvent(CanFrameArgs e)
        {
            EventHandler<CanFrameArgs> temp = CanReceviedEvent;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        public void Clear()
        {
            _list = new ConcurrentQueue<FrameInfo>();
        }
    }
}
