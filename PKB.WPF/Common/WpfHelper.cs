using System.Windows;
using System.Windows.Media;
using PKB.Utility;

namespace PKB.WPF.Common
{
    public static class WpfHelper
    {
        public static Maybe<T> FindParentByType<T>(this DependencyObject dependencyObject)
            where T : DependencyObject
        {

            var current = dependencyObject;
            while (true)
            {
                current = VisualTreeHelper.GetParent(current);

                if (current == null)
                    return Maybe<T>.Nothing;

                var result = current as T;

                if (result != null)
                    return new Maybe<T>(result);
            }
        }
    }
}
