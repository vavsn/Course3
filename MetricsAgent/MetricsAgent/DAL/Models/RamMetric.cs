using System;

namespace MetricsAgent.DAL.Models
{
    public class RamMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTime Time { get; set; }
    }

}
