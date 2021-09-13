using System;

namespace MetricsAgent.DAL.Request
{
    public class CpuMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}