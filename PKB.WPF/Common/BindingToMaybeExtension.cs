using System;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using MVPVM;
using PKB.WPF.Shared.Converters;

namespace PKB.WPF.Common
{
    [MarkupExtensionReturnType(typeof(object))]
    public class BindingToMaybeExtension : CustomBindingBase
    {
        private static readonly MaybeToObjectConverter MaybeToObjectConverter = new MaybeToObjectConverter();

        public BindingToMaybeExtension()
        {
        }


        public BindingToMaybeExtension(string path)
            : base(path)
        {
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            if (Converter != null)
                throw new InvalidOperationException();

            if (Path == null || Path.Path == null)
                return base.ProvideValue(provider);

            string path = Path.Path;
            if (path.EndsWith("?"))
            {
                path = path.Remove(path.Length - 1, 1);
                Converter = MaybeToObjectConverter;
            }

            Path.Path = path.Replace("?", ".NullableValue");
            return base.ProvideValue(provider);
        }
    }
}
