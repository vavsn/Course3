using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _clientFactory = clientFactory;
        }

        [HttpGet("agent/{Id}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:51353/cpumetrics/getbyid/245");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <CpuMetricDto>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
                return Ok(metricsResponse);
            }
            else
            {
                // ошибка при получении ответа
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:51353/CpuMetrics/");
            ///http://localhost:51353/api/cpumetrics/from/1/to/999999?var=val&var1=val1);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <AllCpuMetricsResponse>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
                return Ok(metricsResponse);
            }
            else
            {
                // ошибка при получении ответа
            }
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

    }

}
