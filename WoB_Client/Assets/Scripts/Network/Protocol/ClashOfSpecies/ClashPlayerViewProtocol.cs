using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class ClashPlayerViewProtocol {
	
	public static NetworkRequest Prepare(int player_id) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_PLAYER_VIEW);
		request.AddInt32(player_id);
		
		return request;
	}
	
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

	static DateTime JavaLongToCSharpLong(long javaLong)
	{
		TimeSpan ss = TimeSpan.FromMilliseconds(javaLong*1000);
		DateTime Jan1st1970 = 
			new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime ddd = Jan1st1970.Add(ss);
		DateTime final = ddd.ToUniversalTime();
		
		return final;
	}
}

public class ResponseClashPlayerView : NetworkResponse {

	//public Player player { get; set; }
	public int DefenseConfigID {get; set;}
	public int TerrainID {get; set;}
	public int PlayerID {get; set;}
	public DateTime Timestamp {get; set;}
	public List<ClashUnitData> defenseSpecies {get; set;}

	public ResponseClashPlayerView() {
		protocol_id = NetworkCode.CLASH_PLAYER_VIEW;
		defenseSpecies = new List<ClashUnitData>();
	}
}
