using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Prism.Mvvm;

namespace Zodiacon.WPF {
	/// <summary>
	/// Propagates property change from one object to another
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TTarget"></typeparam>
	public sealed class PropertyFollower<TSource, TTarget> : IDisposable
		 where TSource : INotifyPropertyChanged
		 where TTarget : BindableBase {				// ensures that RaisePropertyChanged method exists
		HashSet<string> _propertyNames = new HashSet<string>();
		TSource _source;
		TTarget _target;
		MethodInfo _raisePropertyChangedMethod;
		Dictionary<string, Action<string>> _extraWork;

		// factory to allow 'var' usage
		public static PropertyFollower<TSource1, TTarget1> Create<TSource1, TTarget1>(TSource1 source, TTarget1 target, params string[] propertyNames)
			where TSource1 : INotifyPropertyChanged
			where TTarget1 : BindableBase {
			return new PropertyFollower<TSource1, TTarget1>(source, target, propertyNames);
		}

		public PropertyFollower(TSource source, TTarget target, params string[] propertyNames) {
			Debug.Assert(source != null && target != null);

			if(source == null)
				throw new ArgumentNullException(nameof(source));

			if(target == null)
				throw new ArgumentNullException(nameof(target));

			source.PropertyChanged += source_PropertyChanged;
			_source = source;
			_target = target;
			_raisePropertyChangedMethod = _target.GetType().GetMethod("RaisePropertyChanged", 
				BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(string) }, null);
			if(propertyNames != null) {
				foreach(var name in propertyNames)
					_propertyNames.Add(name);
			}
		}

		void source_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			if(_propertyNames.Contains(e.PropertyName)) {
				_raisePropertyChangedMethod.Invoke(_target, new object[] { e.PropertyName });  
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
