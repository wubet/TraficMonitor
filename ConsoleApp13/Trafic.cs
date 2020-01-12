using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TraficMonitor
{
    public class Trafic : ITrafic
    {
        private int _count;
        private int _totalCount = 0;
        private List<Tuple<DateTime, int>> _listCount;

        public Trafic()
        {
            _listCount = new List<Tuple<DateTime, int>>();
            Start();
        }

        public void Hit()
        {
            _count++;
        }
        public int HitCount()
        {
            DateTime current = DateTime.Now;
            for(int i = _listCount.Count-1; i > 0; --i)
            {
                var hitCount = _listCount[i];
                if((current - hitCount.Item1).TotalSeconds <= 1)
                {
                    _totalCount += hitCount.Item2;
                }
                else
                {
                    break;
                }
            }

            return _totalCount;
        }

        private void Start()
        {
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(CountHandler);
            myTimer.Interval = 1000; // 1000 ms is one second
            myTimer.Start();

        }

        private void CountHandler(object source, ElapsedEventArgs e)
        {
            if(_count > 0)
            {
                var tuple = new Tuple<DateTime, int>(DateTime.Now, _count );
                _listCount.Add(tuple);
                _count = 0;
                _totalCount = 0;
            }
        }
    }
}
