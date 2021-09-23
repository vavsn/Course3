using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Response;

namespace MetricsAgent.DAL

{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface IObjMetricsRepository : IRepository<ObjMetric>
    {

    }

    public class ObjMetricsRepository : IObjMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public ObjMetricsRepository()
        {
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        public void Create(ObjMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO objmetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time.TotalSeconds
                    });
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM objmetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }

        public void Update(ObjMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }

        public IList<ObjMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // читаем при помощи Query и в шаблон подставляем тип данных
                // объект которого Dapper сам и заполнит его поля
                // в соответсвии с названиями колонок
                return connection.Query<ObjMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }
        }

        public ObjMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<ObjMetric>("SELECT Id, Time, Value FROM objmetrics WHERE id=@id",
                    new { id = id });
            }
        }
        public IList<ObjMetric> GetByTimePeriod(TimePeriod respond)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<ObjMetric>("SELECT * FROM objmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = respond.fromTime,
                        toTime = respond.toTime
                    }).ToList();
            }
        }
    }
}