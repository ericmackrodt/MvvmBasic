using MVVMBasic.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVVMBasic
{
    public delegate Task AsyncExecutionEventHandler(object parameter);

    [DataContract]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected event AsyncExecutionEventHandler OnLoad;

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

        public BaseViewModel()
        {

        }

        public async virtual Task Load(object arg)
        {
            if (OnLoad != null)
                await OnLoad(arg);
        }

        public virtual void NotifyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [DataContract]
    public abstract class ConnectionVerifyBaseViewModel : BaseViewModel
    {
        protected IConnectionVerify connection;

        public ConnectionVerifyBaseViewModel(IConnectionVerify connectionVerify)
        {
            connection = connectionVerify;
        }

        protected async Task<TResult> ConnectionVerifyCall<TResult, TInput>(Func<TInput, Task<TResult>> call, TInput input, Action catchAction = null)
        {
            if (!connection.HasInternetConnection())
                return default(TResult);

            Exception exception = null;

            try
            {
                return await call(input);
            }
            catch (Exception ex)
            {
                exception = ex;
                if (catchAction != null)
                    catchAction();
            }

            if (exception != null)
            {
                await connection.VerifyConnectionException(exception);
            }

            return default(TResult);
        }

        protected async Task<T> ConnectionVerifyCall<T>(Func<Task<T>> call, Action catchAction = null)
        {
            if (!connection.HasInternetConnection())
                return default(T);

            Exception exception = null;

            try
            {
                return await call();
            }
            catch (Exception ex)
            {
                exception = ex;
                if (catchAction != null)
                    catchAction();
            }

            if (exception != null)
            {
                await connection.VerifyConnectionException(exception);
            }

            return default(T);
        }

        protected async Task ConnectionVerifyCall(Func<Task> call, Action catchAction = null)
        {
            if (!connection.HasInternetConnection())
                return;

            Exception exception = null;

            try
            {
                await call();
            }
            catch (Exception ex)
            {
                exception = ex;
                if (catchAction != null)
                    catchAction();
            }

            if (exception != null)
            {
                await connection.VerifyConnectionException(exception);
            }
        }

        protected async Task ConnectionVerifyCall<T>(Func<T, Task> call, T input, Action catchAction = null)
        {
            if (!connection.HasInternetConnection())
                return;

            Exception exception = null;

            try
            {
                await call(input);
            }
            catch (Exception ex)
            {
                exception = ex;
                if (catchAction != null)
                    catchAction();
            }

            if (exception != null)
            {
                await connection.VerifyConnectionException(exception);
            }
        }
    }
}
