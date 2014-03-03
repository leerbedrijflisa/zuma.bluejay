using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class ProfileItems
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set;}
		public int ProfileID { get; set;}
		public string Title { get; set; }
		public string Content { get; set; }
	}
}

