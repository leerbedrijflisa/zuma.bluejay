using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int Role { get; set; }
		public string Name { get; set; }
	}
}

