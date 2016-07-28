using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Prism.Mvvm;

namespace Zodiacon.WPF {
    /// <summary>
    /// Propagates property change from one object to another
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public sealed class PropertyFollower<TSource, TTarget> : IDisposable
        where TSource : INotifyPropertyChanged
        where TTarget : BindableBase {  // ensures that OnPropertyChanged method exists
        HashSet<string> _propertyNames = new HashSet<string>();
        TSource _source;
        dynamic _target;
        Dictionary<string, Action<string>> _extraWork;

        public PropertyFollower(TSource source, TTarget target, params string[] propertyNames) {
            Debug.Assert(source != null && target != null);

            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if(target == null)
                throw new ArgumentNullException(nameof(target));

            if(propertyNames == null)
                throw new ArgumentException("At least one property must be provided", nameof(propertyNames));

            source.PropertyChanged += source_PropertyChanged;
            _source = source;
            _target = target;
            foreach(var name in propertyNames)
                _propertyNames.Add(name);

        }

        void source_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(_propertyNames.Contains(e.PropertyName)) {
                _target.OnPropertyChanged(e.PropertyName);  // dynamic is used because OnPropertyChanged is protected
                Action<string> action;
                if(_extraWork != null && _extraWork.TryGetValue(e.PropertyName, out action))
                    action(e.PropertyName);
            }
        }

        public PropertyFollower<TSource, TTarget> Add(string propertyName, Action<string> extraWork = null) {
            _propertyNames.Add(propertyName);
            if(extraWork != null) {
                if(_extraWork == null)
                    _extraWork = new Dictionary<string, Action<string>>();
                _extraWork.Add(propertyName, extraWork);
            }
            return this;
        }

        public void Dispose() {
            _source.PropertyChanged -= source_PropertyChanged;
        }
    }
}
