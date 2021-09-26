using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Response;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // добавлять сопоставления в таком стиле нужно для всех объектов 
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<ObjMetric, ObjMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
        }
    }
}

