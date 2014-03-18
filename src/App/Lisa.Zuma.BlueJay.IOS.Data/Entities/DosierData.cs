using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class DosierData
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set;}

	}
}

