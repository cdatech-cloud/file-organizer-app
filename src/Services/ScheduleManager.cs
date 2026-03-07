using System;
using System.Timers;

namespace FileOrganizerApp.Services
{
    public class ScheduleManager
    {
        private Timer _scanTimer;
        private DateTime _nextScanTime;

        public ScheduleManager()
        {
            _scanTimer = new Timer();
            _scanTimer.Elapsed += OnTimedEvent;
        }

        public void ScheduleScan(TimeSpan frequency)
        {
            _nextScanTime = DateTime.Now.Add(frequency);
            _scanTimer.Interval = frequency.TotalMilliseconds;
            _scanTimer.Start();
        }

        public void CancelScheduledScan()
        {
            _scanTimer.Stop();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // Logic to initiate scanning process
            StartScan();
        }

        private void StartScan()
        {
            // Implementation for starting the scan
        }

        public DateTime GetNextScanTime()
        {
            return _nextScanTime;
        }
    }
}