using System;

namespace MetricsAgent.Request
{
    public class RamMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}