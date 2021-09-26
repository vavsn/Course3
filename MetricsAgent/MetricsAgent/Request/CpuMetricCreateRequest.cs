using System;

namespace MetricsAgent.Request
{
    public class CpuMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}