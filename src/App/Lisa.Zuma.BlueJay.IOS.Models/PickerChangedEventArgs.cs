using System;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class PickerChangedEventArgs : EventArgs
	{
		public string SelectedValue {get; set;}
	}
}