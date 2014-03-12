using System;
using System.Collections.Generic;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class Notes
	{
		public Notes(){
			Media = new List<NoteMediaModel> ();
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int OwnerID{ get; set; } 
		public int DosierID { get; set; }
		public string Text { get; set; }	
		[Ignore]
		public List<NoteMediaModel> Media { get; set; }

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
				var SplitValue = value.Split (new char[]{'@'}, StringSplitOptions.RemoveEmptyEntries);

				foreach (var x in SplitValue) {
					var model = new NoteMediaModel {Location = x};
					Media.Add (model);
				}
			}
		}

	}
}

