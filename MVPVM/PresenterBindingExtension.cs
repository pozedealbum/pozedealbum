using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MVPVM
{

    [MarkupExtensionReturnType(typeof(object))]
    public class PresenterBindingExtension : CustomBindingBase
    {

        public PresenterBindingExtension()
        {
        }


        public PresenterBindingExtension(string path)
            : base(path)
        {
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            if (Source == null && ElementName == null && RelativeSource == null)
            {
                var service = (IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget));
                RelativeSource = service.TargetObject is IPresenterAware
                    ? new RelativeSource(RelativeSourceMode.Self)
                    : new RelativeSource(RelativeSourceMode.FindAncestor, typeof(IPresenterAware), 1);
            }

            Path = Path == null || String.IsNullOrEmpty(Path.Path)
                ? new PropertyPath("Presenter")
                : new PropertyPath("Presenter." + Path.Path);

            var result = base.ProvideValue(provider);
            return result;
        }

    }
}
