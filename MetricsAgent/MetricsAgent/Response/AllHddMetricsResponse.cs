﻿using System;
using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class AllHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
    
    public class HddMetricDto
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}