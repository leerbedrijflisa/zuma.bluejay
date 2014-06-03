using System;
using System.Collections.Generic;
using SQLite;

namespace Lisa.Zuma.BlueJay.IOS.Data
{
	public class NotesData
	{
		public NotesData(){
			Media = new List<Media> ();
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string OwnerID{ get; set; } 
		public int noteId{ get; set;}
		public int DosierDataID { get; set; }
		public string Text { get; set; }	
		public DateTime Date{ get; set;}
		[Ignore]
		public List<Media> Media { get; set; }

		public string ParsedMedia {
			get { 
				string Result = "";

				foreach (var Results in Media) {
					Result += Results.Location + "*"+ Results.mediaId +"@";
				}

				return Result;
			}

			set {
				var SplitValue = value.Split (new char[]{ '@' }, StringSplitOptions.RemoveEmptyEntries);

				for (var i = 0; i < SplitValue.Length; i++) {

					var SplitValueDash = SplitValue[i].Split (new char[]{ '*' }, StringSplitOptions.RemoveEmptyEntries);

						var model = new Media { Location = SplitValueDash [0], mediaId = int.Parse (SplitValueDash [1]) };
						Media.Add (model);

				}

			}
		}

	}
}

