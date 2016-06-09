using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Data.sqlite;
using System.Runtime.Serialization.Formatters.Binary;
using Mono.Data.Sqlite;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;


public static class DBConnector  {
	private static SqliteConnection sqlite_conn = new SqliteConnection("Data Source=/Users/shahabrazavi/nightHawk.sqlite;Version=3;");


	public static void setDBFilePath (string path){
		sqlite_conn = new SqliteConnection("Data Source="+path+";Version=3;");
	}

	public static void saveToDB<dataType>(object game){ 
		sqlite_conn.Open ();
		XmlSerializer ser = new XmlSerializer (typeof(dataType)); 
		StringWriter strWriter = new StringWriter();
		ser.Serialize (strWriter, (dataType) game); 
		string serializedGame = strWriter.ToString ();
		//string update_query = "UPDATE saved_games SET game= \'" + serializedGame + "\' WHERE uid = 1;";

		DateTime myDateTime = DateTime.Now;
		string sqlFormattedNow = myDateTime.ToString("G");

		string update_query = "INSERT INTO all_saved_games (time,game) VALUES (\'"+sqlFormattedNow+"\',\'" + serializedGame + "\');";
		Debug.Log (update_query);

		SqliteCommand update_command = new SqliteCommand (update_query, sqlite_conn);
		int resulted_changes = update_command.ExecuteNonQuery ();
		sqlite_conn.Close ();
	}

	public static List<dataType> loadFromDB<dataType>(){
		sqlite_conn.Open ();
		//string get_query = "SELECT game FROM saved_games WHERE uid=1;";
		string get_query = "SELECT * FROM all_saved_games;";
	
		SqliteCommand get_command = new SqliteCommand (get_query, sqlite_conn);
		SqliteDataReader reader = get_command.ExecuteReader ();

		XmlSerializer ser = new XmlSerializer (typeof(dataType));
		StringReader strReader;

		List<dataType> games = new List<dataType> ();

		while (reader.Read ()) {
			strReader = new StringReader((string)reader ["game"]);
			games.Add((dataType) ser.Deserialize (strReader));
		}
	
		sqlite_conn.Close ();
		return games;
	}

}
