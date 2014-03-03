using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Profile
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int DossierID { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
	}
}

