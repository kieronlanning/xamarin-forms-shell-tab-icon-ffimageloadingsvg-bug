using System;
using System.Collections.Generic;
using TabIconSvgImageIssue.ViewModels;
using Xamarin.Forms;

namespace TabIconSvgImageIssue
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();

			BindingContext = new BaseViewModel();
		}
	}
}
