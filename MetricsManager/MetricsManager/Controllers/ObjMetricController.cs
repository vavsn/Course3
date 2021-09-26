using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ObjMetricsController : ControllerBase
    {
        private readonly ILogger<ObjMetricsController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ObjMetricsController(ILogger<ObjMetricsController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в ObjMetricsController");
            _clientFactory = clientFactory;
        }

        [HttpGet("agent/{Id}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:51353/objmetrics/getbyid/245");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <ObjMetricDto>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
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
            "http://localhost:51353/ObjMetrics/");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <AllObjMetricsResponse>(responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result;
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

