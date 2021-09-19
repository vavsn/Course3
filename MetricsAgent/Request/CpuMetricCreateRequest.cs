using System;

namespace MetricsAgent.Request
{
    public class CpuMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}