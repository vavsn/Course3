using System;
using System.Collections.Generic;

namespace WebCelsius.Controllers
{
    public class ValuesHolder
    {

        public List<Temper> Values { set; get; } = new List<Temper>();
    }

    /// <summary>
    /// класс для хранения данных по температуре и дате регистрации температуры
    /// </summary>
    public class Temper
    { 
        public string _temp { get; set; } // температура
        public DateTime _time { get; set; } // дата регистрцаии температуры

    }

}