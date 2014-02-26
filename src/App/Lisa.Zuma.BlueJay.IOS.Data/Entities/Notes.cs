using System;
using System.Collections.Generic;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Notes
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int OwnerID{ get; set; } 
		public string Text { get; set; }	
		public string Media { get; set; }

	}
}

