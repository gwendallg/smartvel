using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SmartVel.Ui.Utils;

namespace SmartVel.Utils
{
    public static class Insight
    {
        /// <summary>
        /// initialise les composants Insight
        /// </summary>
        /// <param name="options"></param>
        public static void Init(InsightOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            
            var task = Task.Factory.StartNew(async () =>
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"android={options.AndroidID ?? "{Your Android App secret here}"};");
                stringBuilder.Append($"ios={options.IosID ?? "{Your iOS App secret here}"};");
                stringBuilder.Append($"uwp={options.UwpID ?? "{Your Uwp App secret here}"}");
                
                AppCenter.Start(stringBuilder.ToString(), typeof(Analytics), typeof(Crashes));
                AppCenter.LogLevel = options.Level;

                Crashes.ShouldProcessErrorReport = report => true;
                Crashes.ShouldAwaitUserConfirmation = () => false;
                Crashes.FailedToSendErrorReport += (s, e) =>
                {
                    Crashes.TrackError((Exception)e.Exception,
                        new Dictionary<string, string>() { { "Crashes_FailedToSendErrorReport", e.Report.Id } });
                };

                await AppCenter.SetEnabledAsync(true);
                await Crashes.SetEnabledAsync(true);
                await Analytics.SetEnabledAsync(true);

                //gestion des CRASHs de la session précédente
                await Task.Factory.StartNew(async () =>
                {
                    var didAppCrash = await Crashes.HasCrashedInLastSessionAsync();
                    if (didAppCrash)
                    {
                        var lastErrorReport = await Crashes.GetLastSessionCrashReportAsync();
                        var detail = "Crash initial date: " +
                                     lastErrorReport.AppErrorTime.ToString("dd/MM/yyyy HH:mm:ss");
                        Crashes.TrackError(lastErrorReport.Exception,
                            new Dictionary<string, string>() { { "LastSessionCrash", detail } });
                    }
                });
            });
            task.Wait();
        }

        /// <summary>
        /// initialise les composants Insight
        /// </summary>
        /// <param name="insightOptionsBuilderAction"></param>
        public static void Init(Action<InsightOptionsBuilder> insightOptionsBuilderAction)
        {
            var insightOptionsBuilder = new InsightOptionsBuilder();
            insightOptionsBuilderAction?.Invoke(insightOptionsBuilder);
            var options = insightOptionsBuilder.Build();
            Init(options);
        }

       /// <summary>
       /// rapporte une erreur
       /// </summary>
       /// <param name="ex">exception soulevée</param>
       /// <param name="properties">propriétés liées à l'execption</param>
        public static void TrackError(Exception ex, IDictionary<string, string> properties = null)
        {
            if (ex == null) throw new ArgumentNullException(nameof(ex));
            if (AppCenter.Configured)
            {
                Crashes.TrackError(ex,properties);
            }
            else
            {
                Debug.WriteLine($"INSIGHT ReportError : {ex}");
            }
        }

        /// <summary>
        /// rapporte un évènement
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        public static void TrackEvent([CallerMemberName] string eventName = "",
            IDictionary<string, string> properties = null)
        {
            if (string.IsNullOrWhiteSpace(eventName)) throw new ArgumentNullException(nameof(eventName));
            if (AppCenter.Configured)
            {
                Analytics.TrackEvent(eventName, properties);
            }
            else
            {
                Debug.WriteLine($"INSIGHT TrackEvent : {eventName}");
            }
        }
    }
}
