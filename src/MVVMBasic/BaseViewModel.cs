using MVVMBasic.Commands;
using MVVMBasic.Services;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMBasic
{
    [DataContract]
    public abstract class BaseViewModel : ObservableModel
    {
        private bool isBusy;
        [DataMember]
        public virtual bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                NotifyChanged();
            }
        }

        private bool isDataLoaded;
        [DataMember]
        public virtual bool IsDataLoaded
        {
            get { return this.isDataLoaded; }
            set
            {
                this.isDataLoaded = value;
                NotifyChanged();
            }
        }

        private ICommand _loadDataCommand;
        public ICommand LoadDataCommand
        {
            get { return _loadDataCommand; }
        }

        public BaseViewModel()
        {
            _loadDataCommand = new RelayCommandAsync(LoadData);
        }

        public virtual Task LoadData(object arg)
        {
            IsDataLoaded = true;
            return null;
        }
    }
}
