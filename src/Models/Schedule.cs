using System;

namespace FileOrganizerApp.Models
{
    public class Schedule
    {
        public TimeSpan Frequency { get; set; }
        public DateTime NextScanTime { get; set; }

        public Schedule(TimeSpan frequency, DateTime nextScanTime)
        {
            Frequency = frequency;
            NextScanTime = nextScanTime;
        }
    }
}