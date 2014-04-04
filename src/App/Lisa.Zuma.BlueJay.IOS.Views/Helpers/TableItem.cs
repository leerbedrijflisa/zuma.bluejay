using System;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS
{
	public class TableItem {
		public string Heading { get; set; }
		
		public string SubHeading { get; set; }
		
		public string ImageName { get; set; }

		public int Id { get; set; }
		
		public UITableViewCellStyle CellStyle
		{
			get { return cellStyle; }
			set { cellStyle = value; }
		}
		protected UITableViewCellStyle cellStyle = UITableViewCellStyle.Default;
		
		public UITableViewCellAccessory CellAccessory
		{
			get { return cellAccessory; }
			set { cellAccessory = value; }
		}
		protected UITableViewCellAccessory cellAccessory = UITableViewCellAccessory.None;

		public TableItem () { }
		
		public TableItem (int id, string heading)
		{ Id = id; Heading = heading;}

		public TableItem (int id, string heading, string image)
		{ Id = id; Heading = heading; ImageName = image;}
	}
}