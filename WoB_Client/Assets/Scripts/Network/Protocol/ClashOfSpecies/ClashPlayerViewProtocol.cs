using System;
using System.Collections.Generic;
using System.IO;

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
		int count = DataReader.ReadInt(dataStream);
		for(int i = 0; i < count; i++){
			int species = DataReader.ReadInt(dataStream);
			float x = DataReader.ReadFloat(dataStream);
			float y = DataReader.ReadFloat(dataStream);

			UnitData unit = new UnitData();
			unit.species_id = "" + species; //weird, but UnitData expects string type
			unit.location = new Vector3(x, y, 0); //
			response.defenseSpecies.Add(unit);
		}

		return response;
	}
}

public class ResponseClashPlayerView : NetworkResponse {

	//public Player player { get; set; }
	public int DefenseConfigID {get; set;}
	public int TerrainID {get; set;}
	public List<UnitData> defenseSpecies {get; set;}

	public ResponseClashPlayerView() {
		protocol_id = NetworkCode.CLASH_PLAYER_VIEW;
		defenseSpecies = new List<UnitData>();
	}
}
