using System;

namespace MetricsAgent.DAL.Request
{
    public class ObjMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}