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
			db = new Database();
		}

		public TableSource DataForList()
		{

			List<TableItem> tableItems = new List<TableItem>();

			var dosier = db.GetDosiers (1);

			foreach (var dosiers in dosier) 
			{
				tableItems.Add (new TableItem(dosiers.ID, dosiers.Name));
			}
		
			return new TableSource(tableItems);

		}

		public TableSource DataForProfileList()
		{
			List<TableItem> tableItem = new List<TableItem>();

			var ProfileItems = db.GetProfileItemsByProfileID (1);

			foreach (var profileItem in ProfileItems) 
			{
				tableItem.Add (new TableItem(profileItem.ID, profileItem.Title));
			}

			return new TableSource(tableItem);
		}

		public void InsertProfileItem(string title, string content)
		{
			db.InsertProfileItem (new ProfileItems{Title = title, Content = content});
		} 
	}
}

