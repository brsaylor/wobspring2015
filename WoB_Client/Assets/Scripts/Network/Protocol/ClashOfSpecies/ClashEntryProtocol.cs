using System;
using System.Collections.Generic;
using System.IO;

public class ClashEntryProtocol {
	
	public static NetworkRequest Prepare(int user_id) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_ENTRY);
		request.AddInt32(user_id);
		
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashEntry response = new ResponseClashEntry();

		response.firstTime = DataReader.ReadBool(dataStream);
		if(response.firstTime){
			
		}else{
			//read in data on own defense setup
			response.terrainID = DataReader.ReadInt(dataStream);
			int defenseElementCount = DataReader.ReadInt(dataStream);
			for(int i = 0; i < defenseElementCount; i++){
				int species_id = DataReader.ReadInt(dataStream);
				float x = DataReader.ReadFloat(dataStream);
				float y = DataReader.ReadFloat(dataStream);

				UnitData unit = new UnitData();
				unit.species_id = "" + species_id; //weird, but UnitData expects string type
				unit.location = new Vector3(x, y, 0); //
				response.config.Add(unit);
			}
		}

		return response;
	}
}

public class ResponseClashEntry : NetworkResponse {

	public bool firstTime {get; set;}
	public int terrainID {get; set;}
	public List<UnitData> config {get; set;}

	public ResponseClashEntry() {
		protocol_id = NetworkCode.CLASH_ENTRY;
		config = new List<UnitData>();
	}
}
