using Dapper;
using System.Data;
using System;

namespace MetricsAgent.DAL
{
    // задаем хэндлер для парсинга значений в TimeSpan если таковые попадутся в наших классах моделей
    public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object Value)
            => TimeSpan.FromSeconds(Convert.ToInt64(Value));

        public override void SetValue(IDbDataParameter parameter, TimeSpan Value)
            => parameter.Value = Value;
    }
}
