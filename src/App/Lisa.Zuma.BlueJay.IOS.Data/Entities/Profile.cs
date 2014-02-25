using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Profile
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int DossierID { get; set; }

	}
}

