using System;
using Lisa.Zuma.BlueJay.IOS.Data;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public class DataHelper
	{
		private Database database;

		public DataHelper ()
		{
			database = new Database ();
		}

		public void SignIn(int id)
		{
			database.DummyLoggedIn (id);
		}
	}
}

