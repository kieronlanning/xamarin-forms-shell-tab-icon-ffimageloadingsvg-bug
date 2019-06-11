using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using TabIconSvgImageIssue.Models;
using TabIconSvgImageIssue.Services;
using System.IO;
using FFImageLoading.Svg.Forms;

namespace TabIconSvgImageIssue.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

		public event PropertyChangedEventHandler PropertyChanged;

		readonly Lazy<ImageSource> _homeImage;
		
		bool _isBusy = false;
		string _title = string.Empty;

		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value);
		}

		public ImageSource HomeImage { get { return _homeImage.Value; } }

		public BaseViewModel()
		{
			_homeImage = new Lazy<ImageSource>(() =>
			{
				// Loading the SVG this way just to prove the resource is correctly loading from the manifest stream.
				var resources = typeof(BaseViewModel).Assembly.GetManifestResourceStream("TabIconSvgImageIssue.Resources.home.svg");
				string content;
				using (var reader = new StreamReader(resources))
					content = reader.ReadToEnd();

				var image = SvgImageSource.FromSvgString(content, vectorHeight: 30, vectorWidth: 32);

				return image.ImageSource;
			});
		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName]string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
