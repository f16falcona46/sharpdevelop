/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 12/8/2016
 * Time: 13:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;

namespace OfflineDbBrowser
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			SQLiteConnection db;
			SQLiteCommand cmd;
			
			if (!File.Exists("test.db")) {
				SQLiteConnection.CreateFile("test.db");
				db = new SQLiteConnection("Data Source=test.db;Version=3;");
				db.Open();
				using (cmd = new SQLiteCommand("CREATE TABLE People (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name STRING NOT NULL, Description STRING)", db)) {
					cmd.ExecuteNonQuery();
				}
				db.Close();
			}
			db = new SQLiteConnection("Data Source=test.db;Version=3;");
			db.Open();
			string s = "";
			using (cmd = new SQLiteCommand("SELECT Id,Name,Description FROM People WHERE Name LIKE ?;", db)) {
				SQLiteParameter param = new SQLiteParameter();
				cmd.Parameters.Add(param);
				param.Value = "%Smith";
				
				SQLiteDataReader reader = cmd.ExecuteReader();
				while (reader.Read()) {
					s += String.Format("Id: {0}, Name: {1}, Description: {2}\n", reader["Id"], reader["Name"], reader["Description"]);
				}
			}
			MessageBox.Show(s, "Result");
			db.Close();
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
