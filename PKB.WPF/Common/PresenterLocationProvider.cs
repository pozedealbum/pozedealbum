// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Navigation;
using MVPVM;

namespace PKB.WPF.Common
{

    public static class PresenterLocationProvider
    {

        public static Func<Type, IPresenter> PresenterFactory { get; set; }
        public static Func<Type, Type> ViewTypeToPresenterTypeResolver { get; set; }

        static PresenterLocationProvider()
        {
            PresenterFactory = (presenterType) =>
                (IPresenter)Activator.CreateInstance(presenterType);

            ViewTypeToPresenterTypeResolver = viewType =>
                 {
                     var viewName = GetViewName(viewType);
                     var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                     var viewModelName = string.Format(
                         CultureInfo.InvariantCulture, "{0}Presenter, {1}",
                         viewName, viewAssemblyName);

                     return Type.GetType(viewModelName);
                 };
        }

        private static string GetViewName(Type viewType)
        {
            var viewName = viewType.FullName;
            if (viewName.EndsWith("View"))
                viewName = viewName.Remove(viewName.Length - "View".Length);

            return viewName;
        }


        public static IPresenter CreatePresenterFromView(Type viewType)
        {
            var presenterType = ViewTypeToPresenterTypeResolver(viewType);
            if (presenterType == null) return null;
            return PresenterFactory(presenterType);
        }

    }
}
