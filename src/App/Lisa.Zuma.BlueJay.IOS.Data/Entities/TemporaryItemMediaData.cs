using System;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class TemporaryItemMediaData
	{
		[PrimaryKey, AutoIncrement]
		public int ID {get;set;}
		public int Type {get;set;}
		public string Path {get;set;}
		public string fileName{ get; set;}
	}
}

