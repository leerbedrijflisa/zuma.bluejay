using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Media
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
	}
}

