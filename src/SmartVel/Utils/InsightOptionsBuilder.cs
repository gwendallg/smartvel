using Microsoft.AppCenter;

namespace SmartVel.Ui.Utils
{
    public class InsightOptionsBuilder
    {
        private readonly InsightOptions _result;

        public InsightOptionsBuilder()
        {
            _result = new InsightOptions();
        }

        public InsightOptionsBuilder AndroidID(string id)
        {
            _result.AndroidID = id ?? "{Your Android App secret here}";
            return this;
        }

        public InsightOptionsBuilder IosID(string id)
        {
            _result.IosID = id ?? "{Your iOS App secret here}";
            return this;
        }

        public InsightOptionsBuilder UwpID(string id)
        {
            _result.UwpID = id ?? "{Your Uwp App secret here}";
            return this;
        }

        public InsightOptionsBuilder Level(LogLevel level = LogLevel.Info)
        {
            _result.Level = level;
            return this;
        }

        public InsightOptions Build()
        {
            return _result;
        }

    }
}
