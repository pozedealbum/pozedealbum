using Microsoft.Practices.Prism.Commands;
using MVPVM;
using PKB.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PKB.WPF.Common
{
    public class Presenter<TViewModel> : IPresenter
        where TViewModel : class, new()
    {
        private readonly Lazy<IList<DelegateCommandBase>> _commandsLazy;


        public Presenter()
        {
            ViewModel = new TViewModel();
            _commandsLazy = new Lazy<IList<DelegateCommandBase>>(LoadCommands);
            var propertyChanged = ViewModel as INotifyPropertyChanged;
            if (propertyChanged != null)
                propertyChanged.PropertyChanged += OnViewModelPropertyChanged;
        }

        
        private IEnumerable<DelegateCommandBase> Commands
        {
            get { return _commandsLazy.Value; }
        }

 

        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Commands.ForEach(x => x.RaiseCanExecuteChanged());
        }

        private IList<DelegateCommandBase> LoadCommands()
        {
            return GetType()
                .GetProperties()
                .Where(x => typeof(DelegateCommandBase).IsAssignableFrom(x.PropertyType))
                .Select(x => x.GetValue(this))
                .Cast<DelegateCommandBase>()
                .ToArray();
        }

  

        public virtual void AttachView(object view)
        {
            View = view;
        }

        public TViewModel ViewModel { get; private set; }

        object IPresenter.ViewModel { get { return ViewModel; } }

        public object View { get; private set; }
    }
}