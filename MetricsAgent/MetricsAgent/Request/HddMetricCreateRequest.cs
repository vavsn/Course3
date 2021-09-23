using System;

namespace MetricsAgent.Request
{
    public class HddMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}