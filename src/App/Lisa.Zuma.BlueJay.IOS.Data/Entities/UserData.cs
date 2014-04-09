using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class UserData
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string AccesToken { get; set; }
		public string RefreshToken { get; set; }
	}
}

