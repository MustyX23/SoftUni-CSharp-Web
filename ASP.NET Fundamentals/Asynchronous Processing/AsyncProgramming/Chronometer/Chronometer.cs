using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private Stopwatch _stopwatch;
        private List<string> _laps;

        public Chronometer()
        {
            _stopwatch = new Stopwatch();
            _laps = new List<string>();
        }

        public string GetTime => _stopwatch.Elapsed.ToString(@"mm\:ss\.ffff");

        public List<string> Laps => _laps;

        public void Start()
        {
            _stopwatch.Start();
        }
        public void Reset()
        {
            _stopwatch.Restart();
            _laps.Clear();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public string Lap()
        {
            string lap = GetTime;
            _laps.Add(lap);
            return lap;
        }
       
    }
}
