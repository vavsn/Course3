using System;

namespace MetricsAgent.DAL.Request
{
    public class HddMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}