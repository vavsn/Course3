using System;

namespace MetricsAgent.Request
{
    public class HddMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}