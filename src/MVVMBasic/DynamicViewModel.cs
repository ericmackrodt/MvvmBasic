using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMBasic
{
    public class NotifyChangeAttribute : Attribute
    {

    }

    public abstract class DynamicViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DynamicViewModel()
        {
            WirePropertiesNotification();
        }

        protected virtual void WirePropertiesNotification()
        {
            var properties = this.GetType().GetTypeInfo().DeclaredProperties
                .Where(o => o.GetCustomAttribute<NotifyChangeAttribute>() != null);

            var currentContext = SynchronizationContext.Current;

            foreach (var property in properties)
            {
                var realPropertyType = property.PropertyType.GenericTypeArguments.Single();
                var propertyType = typeof(Property<>).MakeGenericType(realPropertyType);
                var propertyInstance = Activator.CreateInstance(propertyType, currentContext, property.Name);
                property.SetValue(this, propertyInstance);
                (propertyInstance as INotifyPropertyChanged).PropertyChanged += (s, e) => currentContext.Post(PostOnChanged, e);
            }
        }

        private void PostOnChanged(object state)
        {
            var e = state as PropertyChangedEventArgs;
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }

    public interface IProperty
    {
        object Value { get; }
    }

    public class Property<T> : IProperty, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public T Value { get { return _Value; } set { _Value = value; SyncContext.Post(PostOnChanged, _Name); } }

        string _Name;
        T _Value;
        SynchronizationContext SyncContext;

        public Property(SynchronizationContext syncContext, string name)
        {
            SyncContext = syncContext;
            _Name = name;
        }

        void PostOnChanged(object state)
        {
            var name = (string)state;
            var p = PropertyChanged;
            if (p != null)
            {
                p(this, new PropertyChangedEventArgs(name));
            }
        }

        object IProperty.Value
        {
            get { return Value; }
        }
    }
}
