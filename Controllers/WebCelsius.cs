using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// In SDK-style projects such as this one, several assembly attributes that were historically
// defined in this file are now automatically added during build and populated with
// values defined in project properties. For details of which attributes are included
// and how to customise this process see: https://aka.ms/assembly-info-properties


// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("f0595c4b-40dc-4f3d-b34f-84e4a5434d18")]



namespace WebCelsius.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebCelsiusController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        /// <summary>
        /// метод формирует объект для хранения информации
        /// </summary>
        /// <param name="holder"></param>
        public WebCelsiusController(ValuesHolder holder)
        {
            _holder = holder;
        }

        /// <summary>
        /// метод сохраняет данные в памяти
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] Temper input)
        {
            _holder.Values.Add(input);
            return Ok();
        }

        /// <summary>
        /// метод получает данные из памяти
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Read()
        {
            return Ok(_holder.Values);
        }

        /// <summary>
        /// метод обновляет данные в памяти
        /// </summary>
        /// <param name="stringsToUpdate"></param>
        /// искомое значение
        /// <param name="newValue"></param>
        /// новое значение для замены
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromQuery] string stringsToUpdate, [FromQuery] string newValue)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (String.Compare(_holder.Values[i]._temp, stringsToUpdate) == 0)
                    _holder.Values[i]._temp = newValue;
            }

            return Ok();
        }

        /// <summary>
        /// метод удаляет данные из памяти, если температура совпадает с переданным искомым значением
        /// </summary>
        /// <param name="stringsToDelete"></param>
        /// искомое значение
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            _holder.Values = _holder.Values.Where(w => (String.Compare(w._temp, stringsToDelete) != 0)).ToList();
            return Ok();
        }

        /// <summary>
        /// метод ищет данные в заданном периоде
        /// </summary>
        /// <param name="DateBeg"></param>
        /// начальная дата периода
        /// <param name="DateEnd"></param>
        /// конечная дата периода
        /// <returns></returns>
        [HttpPatch("patch")]
        public IActionResult Patch([FromQuery] DateTime DateBeg, [FromQuery] DateTime DateEnd)
        {
            ValuesHolder _holder1 = new ValuesHolder();
            _holder1.Values = _holder.Values.Where(w => (w._time >= DateBeg && w._time < DateEnd)).ToList();
            return Ok(_holder1.Values);
        }

    }
}