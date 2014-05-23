using System;
using System.Collections.Generic;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS.Views
{

	public delegate void RowClickedEventHandler(object sender, RowClickedEventArgs e);

	public class TableSource : UITableViewSource {
		List<TableItem> tableItems;
		string cellIdentifier = "TableCell";
		DataHelper dataHelper;

		public TableSource (List<TableItem> items)
		{
			tableItems = items;

		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Count;
		}

		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, false);
			var args = new RowClickedEventArgs ();
			args.ClickedItem = tableItems[indexPath.Row];
			OnRowClicked (args);
		}

		public event RowClickedEventHandler RowClicked;

		protected void OnRowClicked(RowClickedEventArgs e)
		{
			DataHelper dataHelper = new DataHelper();

			if (RowClicked != null) 
			{
				dataHelper.insertNewCurrentDossier (e.ClickedItem.Id);
				RowClicked (this, e);
			}
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

			// UNCOMMENT one of these to use that style
			//			var cellStyle = UITableViewCellStyle.Default;
			var cellStyle = UITableViewCellStyle.Subtitle;
			//			var cellStyle = UITableViewCellStyle.Value1;
			//			var cellStyle = UITableViewCellStyle.Value2;

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (cellStyle, cellIdentifier);
			}

			cell.TextLabel.Text = tableItems[indexPath.Row].Heading;

			// Default style doesn't support Subtitle
			if (cellStyle == UITableViewCellStyle.Subtitle 
			    || cellStyle == UITableViewCellStyle.Value1
			    || cellStyle == UITableViewCellStyle.Value2) {
				cell.DetailTextLabel.Text = tableItems[indexPath.Row].SubHeading;
			}

			// Value2 style doesn't support an image
			if (cellStyle != UITableViewCellStyle.Value2)
				cell.ImageView.Image = UIImage.FromFile ("Images/" +tableItems[indexPath.Row].ImageName);

			return cell;
		}

		#region -= editing methods =-

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				Console.WriteLine (tableItems [indexPath.Row].Id);
				break;
			case UITableViewCellEditingStyle.None:
					Console.WriteLine ("CommitEditingStyle:None called");
				break;

			}
		}

		/// <summary>
		/// Called by the table view to determine whether or not the row is editable
		/// </summary>
		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}

		/// <summary>
		/// Called by the table view to determine whether or not the row is moveable
		/// </summary>
		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return indexPath.Row < tableView.NumberOfRowsInSection (0) - 1;
		}

		/// <summary>
		/// Custom text for delete button
		/// </summary>
		public override string TitleForDeleteConfirmation (UITableView tableView, NSIndexPath indexPath)
		{
			return "Verwijderen";
		}

		/// <summary>
		/// Called by the table view to determine whether the editing control should be an insert
		/// or a delete.
		/// </summary>
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.Editing) {
				if (indexPath.Row == tableView.NumberOfRowsInSection (0)-1)
					return UITableViewCellEditingStyle.Insert;
				else
					return UITableViewCellEditingStyle.Delete;
			} else  // not in editing mode, enable swipe-to-delete for all rows
				return UITableViewCellEditingStyle.Delete;
		}
		public override NSIndexPath CustomizeMoveTarget (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath proposedIndexPath)
		{
			var numRows = tableView.NumberOfRowsInSection (0) - 1; // less the (add new) one
			Console.WriteLine (proposedIndexPath.Row + " " + numRows);
			if (proposedIndexPath.Row >= numRows)
				return NSIndexPath.FromRowSection(numRows-1, 0);
			else
				return proposedIndexPath;
		}
		/// <summary>
		/// called by the table view when a row is moved.
		/// </summary>
		public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
		{
			//---- get a reference to the item
			var item = tableItems[sourceIndexPath.Row];
			int deleteAt = sourceIndexPath.Row;
			int insertAt = destinationIndexPath.Row;

			//---- if we're moving within the same section, and we're inserting it before
			if ((sourceIndexPath.Section == destinationIndexPath.Section) && (destinationIndexPath.Row < sourceIndexPath.Row))
			{
				//---- add one to where we delete, because we're increasing the index by inserting
				deleteAt += 1;
			} else {
				insertAt += 1;
			}

			//---- copy the item to the new location
			tableItems.Insert (destinationIndexPath.Row, item);

			//---- remove from the old
			tableItems.RemoveAt (deleteAt);
		}
		/// <summary>
		/// Called manually when the table goes into edit mode
		/// </summary>
		public void WillBeginTableEditing (UITableView tableView, int id, string name)
		{
			//---- start animations
			tableView.BeginUpdates ();

			//---- insert a new row in the table
			tableView.InsertRows (new NSIndexPath[] { 
				NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0), 0) 
			}, UITableViewRowAnimation.Fade);
			//---- create a new item and add it to our underlying data
//			tableItems.Add (new TableItem (id,name,"../Documents/"+id.ToString()+"/photo1.png"));

			//---- end animations
			tableView.EndUpdates ();
		}

		/// <summary>
		/// Called manually when the table leaves edit mode
		/// </summary>
		public void DidFinishTableEditing (UITableView tableView)
		{
			//---- start animations
			tableView.BeginUpdates ();
			//---- remove our row from the underlying data
			tableItems.RemoveAt (tableView.NumberOfRowsInSection (0) - 1); // zero based :)
			//---- remove the row from the table
			tableView.DeleteRows (new NSIndexPath[] { NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0) - 1, 0) }, UITableViewRowAnimation.Fade);

			Console.WriteLine ("ja");

			//---- finish animations
			tableView.EndUpdates ();
		}

		#endregion
	}
}