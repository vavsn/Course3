using System;
using System.Collections.Generic;

namespace WpfAppLesson8
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }

    public class CpuMetricDto
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}