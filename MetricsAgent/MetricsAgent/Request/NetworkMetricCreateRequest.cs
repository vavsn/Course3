using System;

namespace MetricsAgent.Request
{
    public class NetworkMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}