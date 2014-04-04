using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Collections;

namespace Lisa.Zuma.BlueJay.IOS
{
	public class TableHelper
	{
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

		public TableSource CreateSource<T>(IEnumerable<T> data, Func<T, int> getId, Func<T, string> getHeading, Func<T, string> getImage)
		{
			var tableItems = new List<TableItem> ();

			foreach (var dataItem in data) {
				var id = getId (dataItem);
				var heading = getHeading (dataItem);
				var image = getImage(dataItem);
				tableItems.Add(new TableItem(id, heading, image));
			}
			return new TableSource(tableItems);
		}

		public TableSource tableSource;

	}
}

