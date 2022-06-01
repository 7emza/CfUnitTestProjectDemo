using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CfUnitTestProjectDemo.ViewModels
{
    public abstract class BindableBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Fields

        private readonly HashSet<string> _validationInitialized = new HashSet<string>();

        private readonly ConcurrentDictionary<string, ICollection<object>> _validationErrors =
            new ConcurrentDictionary<string, ICollection<object>>();

        #endregion

        #region Properties

        [Browsable(false)]
        public bool HasErrors => _validationErrors.Count > 0;

        [Browsable(false)]
        public bool HasChanged { get; set; } = false;


        [Browsable(false)]
        public IReadOnlyDictionary<string, IReadOnlyCollection<object>> ValidationErrors =>
            new ReadOnlyDictionary<string, IReadOnlyCollection<object>>(
                this._validationErrors
                    .ToDictionary(
                        x => x.Key,
                        x => (IReadOnlyCollection<object>)new ReadOnlyCollection<object>(x.Value.ToList())));

        #endregion

        #region Events and Delegates

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Interface Implementations

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName) ||
                this._validationErrors.ContainsKey(propertyName) == false)
            {
                return null;
            }

            return _validationErrors[propertyName];
        }

        #endregion

        #region Methods

        protected virtual void OnErrorsChanged()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

 
        protected void RaiseErrorsChanged([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                foreach (var error in this._validationErrors.Keys.ToArray())
                {
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(error));
                }
            }
            else
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            this.OnErrorsChanged();
        }

        [Obsolete("use OnPropertyChanged instead")]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged(propertyName);
        }

        protected void SetPropertyValue<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            property = value;
            OnPropertyChanged(propertyName);
        }




        #endregion
    }
}