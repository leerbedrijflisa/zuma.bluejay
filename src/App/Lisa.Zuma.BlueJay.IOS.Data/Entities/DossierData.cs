using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class DossierData
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set;}
		public int DossierId { get; set; }
	}
}