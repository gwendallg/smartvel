using Microsoft.AppCenter;

namespace SmartVel.Ui.Utils
{
    public class InsightOptions
    {
        /// <summary>
        /// android application ID
        /// </summary>
        public string AndroidID { get; set; }

        /// <summary>
        /// iOS application ID
        /// </summary>
        public string IosID { get; set; }

        /// <summary>
        /// windows universal app application ID
        /// </summary>
        public string UwpID { get; set; }

        /// <summary>
        /// log level 
        /// </summary>
        public LogLevel Level { get; set; }
    }
}
