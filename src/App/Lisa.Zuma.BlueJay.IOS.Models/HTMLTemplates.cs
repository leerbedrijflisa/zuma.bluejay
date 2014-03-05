using System;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class HTMLTemplates
	{
		private string MainTemplateHTML;
		private string NoteHTMLContent;
		private string NoteMedia;
		private string direction;
		private string role;

		private Database db;
	
		private string MainTemplate(string content)
		{
			MainTemplateHTML = " <!doctype html>" +
			"<html>" +
			"<head>   " +
			"<meta charset='utf-8'>" +
			"<title></title>" +
			"<link href='all.css' media='screen' rel='stylesheet' type='text/css' />" +
			"<link href='timeline.css' media='screen' rel='stylesheet' type='text/css' />" +
			"</head>" +
			"<body class='index'>" +
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
					"<div class='imageUnit'>"+
					"<a href='#'><img src='"+profileimage+"' width='50' height='50' alt=''></a>"+
					"<div class='imageUnit-content'>"+
					"<h4><a href='#'>"+PosterName+"</a></h4>"+
					"<p class='roll'>"+role+"</p>"+
					"<p class='time'> "+date+"</p>"+
					"</div>"+
					"</div>"+

					content

					+"</div>"+

					"</div>"+
					"</li>";

			return NoteHTMLContent;
		}

		private string NoteMediaHTML(string text, string media){

			NoteMedia = "";

			if (text != null && text != "") {
				NoteMedia += "<p>" + text + "</p>";
			}

			if (media != null && media != "") {
				NoteMedia += "<video width='320' height='240' controls><source src='" + media + "' type='video/mp4'>Your browser does not support the video tag.</video>";
			}

			return NoteMedia;

		}

		public string ParseTimeLine(int id)
		{
			db = new Database ();

			string TemporaryNote = null;
			string TemporaryMedia;

			var Note = db.GetNotesFromDosier (id);

			foreach (var note in Note) {

				var ItemOwner = db.GetUserById (note.OwnerID);

				TemporaryMedia = this.NoteMediaHTML (note.Text, note.Media);

				if (ItemOwner.Role == 1) {
					direction = "left";
					role = "Begeleider";
				}

				if (ItemOwner.Role == 2) {
					direction = "right";
					role = "Ouder";
				}

				TemporaryNote += this.NoteHTML (direction, null, ItemOwner.Name, role, "nu", TemporaryMedia);


			}

			return MainTemplate (TemporaryNote);
		}

	}
}
