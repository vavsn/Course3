using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;
using MetricsAgent.Request;
using MetricsAgent.DAL;
using MetricsAgent.Response;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjMetricsController : ControllerBase
    {
        private IObjMetricsRepository repository;
        private IMapper mapper;
        public ObjMetricsController(IObjMetricsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ObjMetricCreateRequest request)
        {

            repository.Create(new ObjMetric
            {
                Time = TimeSpan.FromSeconds(request.Time),
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            // задаем конфигурацию для мапера. Первый обобщенный параметр -- тип объекта источника, второй -- тип объекта в который перетекут данные из источника

            IList<ObjMetric> metrics = repository.GetAll();

            var response = new AllObjMetricsResponse()
            {
                Metrics = new List<ObjMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<ObjMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] TimePeriod respond)
        {
            IList<ObjMetric> metrics = repository.GetByTimePeriod(respond);

            var response = new AllObjMetricsResponse()
            {
                Metrics = new List<ObjMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<ObjMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

    }
}
