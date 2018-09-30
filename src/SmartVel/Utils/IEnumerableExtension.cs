using System.Collections.ObjectModel;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    public static class EnumerableExtension
    {
        public static ObservableCollection<TViewModel> ToObservable<TModel, TViewModel>(this IEnumerable<TModel> source,
            Func<TModel, TViewModel> convertAction)
        {
            var items = source.Select(convertAction);
            return new ObservableCollection<TViewModel>(items);
        }
    }
}
