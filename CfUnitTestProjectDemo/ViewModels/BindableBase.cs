using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CfUnitTestProjectDemoUI.ViewModels
{
    public abstract class BindableBaseOld : INotifyPropertyChanged
    {
        #region Fields



        #endregion

        #region Properties

        [Browsable(false)]


 

        #endregion

        #region Events and Delegates

 

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Interface Implementations

 

        #endregion

        #region Methods

        protected virtual void OnErrorsChanged()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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