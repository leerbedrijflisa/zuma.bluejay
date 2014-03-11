using System;
using System.Collections.Generic;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Notes
	{
		public Notes(){
			Media = new List<Media> ();
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int OwnerID{ get; set; } 
		public int DosierID { get; set; }
		public string Text { get; set; }	
		[Ignore]
		public List<Media> Media { get; set; }

		public string ParsedMedia 
		{
			 get
			{ 
				string Result = "";

				foreach (var Results in Media) {
					Result += Results.Location + "@";
				}

				return Result;
			}

			set
			{
				var SplitValue = value.Split (new char[]{'@'});

				foreach (var x in SplitValue) {
					var model = new Media {Location = x};
					Media.Add (model);
				}
			}
		}

	}
}

