using Xamarin.Forms;

namespace SmartVel.Controls
{
    public class ViewCellBase : ViewCell
    {
#pragma warning disable S1104 // Fields should not have public accessibility
#pragma warning disable S2223 // Non-constant static fields should not be visible
        public static BindableProperty ParentBindingContextProperty = BindableProperty.Create(nameof(ParentBindingContext),typeof(object), typeof(ViewCellBase),null, BindingMode.TwoWay, propertyChanged: OnParentBindingContextPropertyChanged);
#pragma warning restore S2223 // Non-constant static fields should not be visible
#pragma warning restore S1104 // Fields should not have public accessibility

        public object ParentBindingContext
        {
            get => GetValue(ParentBindingContextProperty);
            set => SetValue(ParentBindingContextProperty, value);
        }

        private static void OnParentBindingContextPropertyChanged(BindableObject bindable, object oldValue,
            object newValue)
        {
            if (!(bindable is ViewCellBase viewCellBase)) return;
            if (newValue != oldValue && newValue != null)
            {
                viewCellBase.ParentBindingContext = newValue;
            }
        }
    }
}
