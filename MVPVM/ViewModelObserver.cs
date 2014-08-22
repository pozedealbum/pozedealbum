//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq.Expressions;
//using PKB.Utility;

//namespace MVPVM
//{
//    public class ViewModelObserver<TPresenter, TViewModel>
//        where TPresenter : PresenterBase<TViewModel>
//        where TViewModel : class, INotifyPropertyChanged, new()
//    {
//        private readonly SortedDictionary<string, Action<TPresenter, TViewModel>> _handlers
//             = new SortedDictionary<string, Action<TPresenter, TViewModel>>();

//        public ViewModelObserver<TPresenter, TViewModel> Bind(
//            Expression<Func<TViewModel, object>> expression,
//            Action<TPresenter, TViewModel> handler)
//        {
//            string propertyName = StaticReflection.GetMember(expression).Name;

//            if (_handlers.ContainsKey(propertyName))
//                _handlers[propertyName] += handler;
//            else
//                _handlers.Add(propertyName, handler);

//            return this;
//        }

//        public void Handle(TPresenter presenter, TViewModel sender, PropertyChangedEventArgs args)
//        {
//            Action<TPresenter, TViewModel> handler;
//            if (_handlers.TryGetValue(args.PropertyName, out handler))
//                handler.Invoke(presenter, sender);
//        }




//    }
//}
