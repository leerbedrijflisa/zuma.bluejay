using System;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class signInRequestInformation
	{
		//public string .expires {get; set;}
		//public string .issued {get; set;}
		public string access_token { get; set; } 
		public string expires_in { get; set; }
		public string token_type{ get; set; }
		public string userName { get; set; } 
		public string userId { get; set; } 
	}
}

