using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Collections;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class TableHelper
	{
		public TableHelper()
		{
			db = new Database();
		}
		public void InsertProfileItem(string title, string content)
		{
			db.InsertProfileItem (new ProfileItems{Title = title, Content = content});
		}

		public TableSource CreateSource<T>(IEnumerable<T> data, Func<T, int> getId, Func<T, string> getHeading)
		{
			var tableItems = new List<TableItem> ();

			foreach (var dataItem in data) {
				var id = getId (dataItem);
				var heading = getHeading (dataItem);
				tableItems.Add(new TableItem(id, heading));
			}

			return new TableSource(tableItems);
		}

		private Database db;
		public TableSource tableSource;
	}
}

