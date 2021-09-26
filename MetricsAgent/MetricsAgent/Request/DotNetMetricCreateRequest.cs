using System;

namespace MetricsAgent.Request
{
    public class DotNetMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}