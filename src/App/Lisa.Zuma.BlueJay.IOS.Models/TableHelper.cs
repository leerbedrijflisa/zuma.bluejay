using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class TableHelper
	{
		private Database db;
		public TableSource tableSource;

		public TableHelper()
		{

		}

		public TableSource DataForList()
		{
			db = new Database();

			List<TableItem> tableItems = new List<TableItem>();

			var dosier = db.GetDosiers (1);

			foreach (var dosiers in dosier) 
			{
				tableItems.Add (new TableItem(dosiers.ID, dosiers.Name));
			}

			return new TableSource(tableItems);

		}
	}

}

