﻿using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllObjMetricsResponse
    {
        public List<ObjMetricDto> Metrics { get; set; }
    }
    
    public class ObjMetricDto
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }

    }
}