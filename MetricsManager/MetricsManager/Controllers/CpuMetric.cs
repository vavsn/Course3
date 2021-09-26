using System;

namespace MetricsManager.Controllers
{
    internal class CpuMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTime Time { get; set; }
    }
}