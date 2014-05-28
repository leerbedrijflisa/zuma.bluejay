using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class LockScreenData
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public int IsActive { get; set; }
		public int SecurityCode { get; set; }
	}
}

