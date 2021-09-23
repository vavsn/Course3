using System;

namespace MetricsAgent.Request
{
    public class DotNetMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}