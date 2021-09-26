using System;
using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
    
    public class RamMetricDto
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}