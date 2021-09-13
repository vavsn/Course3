using System;

namespace MetricsAgent.DAL.Request
{
    public class RamMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}