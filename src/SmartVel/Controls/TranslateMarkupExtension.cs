using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartVel.Controls
{
    [ContentProperty("Text")]
    public class TranslateMarkupExtension : IMarkupExtension
    {

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Text == null
                ? null
                : Application.Current.Translate(Text, Application.Current.GetCurrentCulture());
        }
    }
}
