using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;
using Lisa.Zuma.BlueJay.IOS.Models;


namespace Lisa.Zuma.BlueJay.IOS
{
	public class HTMLTemplates
	{
		private string MainTemplateHTML;
		private string NoteHTMLContent;
		private string NoteMedia;
		private string direction;
		private string role;
		private DataHelper dataHelper;

		private Database db;

		public HTMLTemplates ()
		{
			dataHelper = new DataHelper ();
		}
	
		private string MainTemplate(string content)
		{
			MainTemplateHTML = " <!doctype html>" +
			"<html>" +
			"<head>   " +
			"<meta charset='utf-8'>" +
			"<title></title>" +
				"<link href='../HTML/stylesheets/all.css' media='screen' rel='stylesheet' type='text/css' />" +
					"<link href='../HTML/stylesheets/timeline.css' media='screen' rel='stylesheet' type='text/css' />" +
				"<custom token='bearer "+dataHelper.token()+"'>"+
				"<script src='../HTML/javascripts/jquery-1.9.1.min.js'></script>"+
				"<script src='../HTML/javascripts/jquery.excoloSlider.js'></script>"+
				"<script src='../HTML/javascripts/all.js'></script>"+
				"<link href='../HTML/stylesheets/jquery.excoloSlider.css' rel='stylesheet' />"+
		
			"<body class='index'>" +
				"<div class='imgOverlay'>" +
				"<div class='imgDivv'></div>" +
				"</div>" +
			"<div class='container'>" +
			"<ol class='timeline clearfix'>" +
			"<li class='spine'></li>" +
			content
			+"</ol>" +
			"</div>" +
			"</body>" +
			"</html>";

			return MainTemplateHTML;
		}

		private string NoteHTML(string direction, string profileimage, string PosterName, string role, string date, string content)
		{
			NoteHTMLContent = "<li class='"+direction+"'>"+
				"<i class='pointer'></i>"+
					" <div class='unit'>"+

					"<div class='storyUnit'>"+
				content
				+"<div class='imageUnit'>"+
					"<a href='#'><img src='"+profileimage+"' width='50' height='50' alt=''></a>"+
					"<div class='imageUnit-content'>"+
					"<h4><a href='#'>"+PosterName+"</a></h4>"+
					"<p class='roll'>"+role+"</p>"+
					"</div>"+
				"<div class='imageUnit-content' style='float:right; padding-top:15px;'>"+
					"<p class='time'> "+date+"</p>"+
					"</div>"+
					"</div>"+

					
					"</div>"+

					"</div>"+
					"</li>";

			return NoteHTMLContent;
		}
		private string NoteMediaHTML(string noteId, string text, List<Media> media){

			NoteMedia = "";


			if (media.Count > 0){
				NoteMedia += "<div class='sliderDiv'>";
				foreach (var x in media) {
					if (x.Location.Contains (".png"))
					{
						NoteMedia += "<div style='max-width:419px; max-height:240px; overflow:hidden;'><img width='419' class='noteImage "+dataHelper.getCurrentDossier()+" "+noteId+" "+x.mediaId+"' style='margin-top:-40px;' src='" + x.Location + "' ></div>";
					}

					if (x.Location.Contains (".mp4"))
					{
						NoteMedia += "<div class='videoHolder "+ dataHelper.getCurrentDossier() +" "+noteId+" "+ x.mediaId +"' style=' height:240px;'><span class='play'><img src='images/play.png' width=100%></span></div>";
					}
				}
				NoteMedia += "</div>";
			}

			if (text != null && text != "") {
				NoteMedia += "<p class='text'>" + text + "</p>";
			}

			return NoteMedia;
		}

		public string ParseTimeLine(int id)
		{
			db = new Database ();

			string TemporaryNote = null;
			string TemporaryMedia;

			var Note = db.GetNotesDataFromDosierData (id);

			foreach (var note in Note) {

//				var ItemOwner = db.GetUserById (note.OwnerID);

				TemporaryMedia = this.NoteMediaHTML(note.noteId.ToString(), note.Text, note.Media);

				if (note.OwnerID == db.getCurrentUserId ()) {
					direction = "right";
				} else {
					direction = "left";
				}
					role = "Begeleider";



				TemporaryNote += this.NoteHTML (direction, null, "Gebruiker", role, note.Date.ToFancyString(), TemporaryMedia);


			}

			return MainTemplate (TemporaryNote);
		}

	}
}

