using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TraficMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Trafic trafic = new Trafic();
            CountManager _countMgr = new CountManager(trafic);
            _countMgr.StartTrafic();
            _countMgr.requestTrafic();
            Console.ReadLine();

        }
    }

    public class CountManager
    {
        private ITrafic _trafic;
        public CountManager(ITrafic trafic)
        {
            _trafic = trafic;
        }
        public void StartTrafic()
        {
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(HitTrafic);
            myTimer.Interval = 300; // 1000 ms is one second
            myTimer.Start();

        }

        public void requestTrafic()
        {
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(RequestTrafic);
            myTimer.Interval = 600; // 1000 ms is one second
            myTimer.Start();

        }

        private void HitTrafic(object source, ElapsedEventArgs e)
        {
            _trafic.Hit();
            Console.WriteLine("Trafic Hit " + DateTime.Now);
        }

        private void RequestTrafic(object source, ElapsedEventArgs e)
        {
            int result = _trafic.HitCount();
            Console.WriteLine("Trafic Request " + DateTime.Now + " " + result);
        }


    }
}
