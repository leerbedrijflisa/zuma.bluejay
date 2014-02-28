using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class TemplateParser
	{
		private Database db;
		private List<Notes> NoteItem;
		private string ReturnHTML;

		public TemplateParser ()
		{
			db = new Database ();
		}

		public string ParseTimelineFromDosier(int id)
		{
			NoteItem = db.GetNotesFromDosier (1);


			var ParentHTML = System.IO.File.ReadAllLines("HTML/timeline/index.html");

//			var ReturnHTML = ParentHTML.Replace("###TIMELINE-ITEMS###", ParseTimelineItems());

			return ReturnHTML;
		}

		private string ParseTimelineItems()
		{
			string ItemRole = "";
			string ItemSide = ""; 



			NoteItem = db.GetNotesFromDosier(1);

			foreach(var note in NoteItem){

				var OwnerRole = 1;//db.GetUserById(note.OwnerID());
				var ItemHTML = System.IO.File.ReadAllText("HTML/timeline/item.html");

				switch (OwnerRole) {
					case 1:
						ItemRole = "Moeder";
						ItemSide = "right";
						break;

					case 2:
						ItemRole = "Begeleider";
						ItemSide = "right";
						break;
				}

				ItemHTML = ItemHTML.Replace("###ITEM_DIRECTION###", ItemSide);
				ItemHTML = ItemHTML.Replace("###ITEM_PROFILENAME###", OwnerRole.ToString());
				ItemHTML = ItemHTML.Replace("###ITEM_PROFILEROLE###", ItemRole);
				ItemHTML = ItemHTML.Replace("###ITEM_POSTDATE###", "20 minuten geleden");
				ItemHTML = ItemHTML.Replace ("###ITEM_CONTENT###",  ParseItemMedia(note.ID));
			}

			return ReturnHTML;
		}

		private string ParseItemMedia(int id)
		{
			Notes MediaString = db.GetMediaFromNoteByID (id);

			var MediaHTML = System.IO.File.ReadAllText("HTML/timeline/media.html");
			string ReturnHTML = "";

			var MediaTags = MediaHTML.Split('$');

			ReturnHTML += MediaTags[1].Replace ("###ITEM_CONTENT###", MediaString.Text);	
			ReturnHTML += MediaTags [2].Replace ("###ITEM_CONTENT###", MediaString.Media);	

			return ReturnHTML;
		}

	}
}

