using System;

namespace MetricsAgent.DAL.Request
{
    public class NetworkMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}