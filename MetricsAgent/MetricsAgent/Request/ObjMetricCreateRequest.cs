using System;

namespace MetricsAgent.Request
{
    public class ObjMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}