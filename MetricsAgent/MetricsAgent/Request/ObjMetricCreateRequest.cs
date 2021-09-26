using System;

namespace MetricsAgent.Request
{
    public class ObjMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}