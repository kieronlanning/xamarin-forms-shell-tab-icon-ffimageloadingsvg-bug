using System;

using TabIconSvgImageIssue.Models;

namespace TabIconSvgImageIssue.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Item Item { get; set; }
		public ItemDetailViewModel(Item item = null)
		{
			Title = item?.Text;
			Item = item;
		}
	}
}
