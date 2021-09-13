using System;

namespace MetricsAgent.DAL.Request
{
    public class DotNetMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}