using System;
using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class AllObjMetricsResponse
    {
        public List<ObjMetricDto> Metrics { get; set; }
    }
    
    public class ObjMetricDto
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}