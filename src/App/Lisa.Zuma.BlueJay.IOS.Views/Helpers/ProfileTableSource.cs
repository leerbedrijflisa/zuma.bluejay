using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.IO;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public class ProfileTableSource : UITableViewSource
	{
		public delegate void RowSelectedEventHandler(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath);
     	public event RowSelectedEventHandler OnRowSelected;

		protected List<TableItemGroup> TableItems;
		protected string CellIdentifier { get { return "TableCell"; } }
	
		protected ProfileTableSource()
		{
		}

		public ProfileTableSource(IEnumerable<TableItemGroup> items)
		{
			TableItems = items.ToList();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return TableItems.Count;
		}

		public override int RowsInSection(UITableView tableView, int section)
		{
			return TableItems [section].Items.Count;
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return TableItems [section].Name;
		}

		public override string TitleForFooter (UITableView tableView, int section)
		{
			return TableItems [section].Footer;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			if (OnRowSelected != null) {
				OnRowSelected (tableView, indexPath);
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CellIdentifier);
			var item = TableItems [indexPath.Section].Items [indexPath.Row];

			if (cell == null) {
				cell = new UITableViewCell (item.CellStyle, CellIdentifier);
			}

			cell.TextLabel.Text = item.Heading;

			if (item.CellStyle == UITableViewCellStyle.Subtitle ||
			    item.CellStyle == UITableViewCellStyle.Value1 ||
			    item.CellStyle == UITableViewCellStyle.Value2) {
			
				cell.DetailTextLabel.Text = item.SubHeading;
			}

			if (!string.IsNullOrEmpty (item.ImageName) && item.CellStyle != UITableViewCellStyle.Value2) {
				if (File.Exists (item.ImageName)) {
					cell.ImageView.Image = UIImage.FromBundle (item.ImageName);
				}
			}

			cell.Accessory = item.CellAccessory;

			return cell;
		}
	}

	public class TableItemGroup 
	{
		public string Name {
			get;
			set;
		}

		public string Footer { 
			get; 
			set; 
		}

		public List<TableItem> Items {
			get { return items; }
			set { items = value; }
		}

		protected List<TableItem> items = new List<TableItem>();
	}
}

