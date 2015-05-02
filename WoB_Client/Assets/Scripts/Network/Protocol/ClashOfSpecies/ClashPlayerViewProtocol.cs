using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

/// <summary>
/// Gets detailed data about a specific player relevant to Clash of Species
/// </summary>
public class ClashPlayerViewProtocol {

	/// <summary>
	/// Creates a request for the player data
	/// </summary>
	/// <param name="player_id">The id of the player requested.</param>
	public static NetworkRequest Prepare(int player_id) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_PLAYER_VIEW);
		request.AddInt32(player_id);
		
		return request;
	}

	/// <summary>
	/// Fills the response object with data about the player
	/// </summary>
	/// <param name="dataStream">The input stream</param>
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashPlayerView response = new ResponseClashPlayerView();

		response.DefenseConfigID = DataReader.ReadInt(dataStream);
		response.TerrainID = DataReader.ReadInt(dataStream);
		response.PlayerID = DataReader.ReadInt(dataStream);

		string timeString = DataReader.ReadString(dataStream);

		long timeLong = 0;
		long.TryParse(timeString, out timeLong);

		DateTime time = JavaLongToCSharpLong(timeLong);

		response.Timestamp = time;

		int count = DataReader.ReadInt(dataStream);
		for(int i = 0; i < count; i++){
			int species = DataReader.ReadInt(dataStream);
			float x = DataReader.ReadFloat(dataStream);
			float y = DataReader.ReadFloat(dataStream);

			ClashUnitData unit = new ClashUnitData();
			unit.species_id = species; //weird, but UnitData expects string type
			unit.location = new Vector3(x, y, 0); //
			response.defenseSpecies.Add(unit);
		}

		return response;
	}

	/// <summary>
	/// Converts a Java timestamp (milliseconds since 00:00:00 1/1/1970)
	/// into a C# DateTime
	/// </summary>
	/// <returns>The DateTime object</returns>
	/// <param name="javaLong">milliseconds since since 00:00:00 1/1/1970</param>
	static DateTime JavaLongToCSharpLong(long javaLong)
	{
		TimeSpan ss = TimeSpan.FromMilliseconds(javaLong);
		DateTime Jan1st1970 = 
			new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime ddd = Jan1st1970.Add(ss);
		DateTime final = ddd.ToUniversalTime();
		
		return final;
	}
}

/// <summary>
/// Stores data about the requested player's defense
/// </summary>
public class ResponseClashPlayerView : NetworkResponse {

	/// <summary>
	/// Defense config ID.
	/// </summary>
	public int DefenseConfigID {get; set;}

	/// <summary>
	/// Terrain ID
	/// </summary>
	public int TerrainID {get; set;}

	/// <summary>
	/// Player ID
	/// </summary>
	public int PlayerID {get; set;}

	/// <summary>
	/// When the defense was created
	/// </summary>
	public DateTime Timestamp {get; set;}

	/// <summary>
	/// Species in defense config
	/// </summary>
	public List<ClashUnitData> defenseSpecies {get; set;}

	public ResponseClashPlayerView() {
		protocol_id = NetworkCode.CLASH_PLAYER_VIEW;
		defenseSpecies = new List<ClashUnitData>();
	}
}
