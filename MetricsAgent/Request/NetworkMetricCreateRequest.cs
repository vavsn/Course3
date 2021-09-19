using System;

namespace MetricsAgent.Request
{
    public class NetworkMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}