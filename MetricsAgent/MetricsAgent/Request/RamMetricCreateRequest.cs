﻿using System;

namespace MetricsAgent.Request
{
    public class RamMetricCreateRequest
    {
        public long Time { get; set; }
        public int Value { get; set; }
    }
}