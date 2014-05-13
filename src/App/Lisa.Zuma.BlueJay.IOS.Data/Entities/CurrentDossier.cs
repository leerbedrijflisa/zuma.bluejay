using SQLite;
using System;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class CurrentDossier
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public int currentDossier { get; set; }
	}
}

