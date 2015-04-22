using System;
using System.Collections.Generic;
using System.IO;

public class ClashDefenseSetupProtocol {
	
	public static NetworkRequest Prepare(int terrainID, List<UnitData> setup) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_DEFENSE_SETUP);
		request.AddInt32(terrainID);
		request.AddInt32(setup.Count);
		foreach(UnitData ud in setup){
			request.AddInt32(ud.species_id);
			request.AddFloat(ud.location.x);
			request.AddFloat(ud.location.y);
		}
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashDefenseSetup response = new ResponseClashDefenseSetup();

		response.valid = DataReader.ReadBool(dataStream);

		return response;
	}
}

public class ResponseClashDefenseSetup : NetworkResponse {

	public bool valid {get; set;}

	public ResponseClashDefenseSetup() {
		protocol_id = NetworkCode.CLASH_DEFENSE_SETUP;
	}
}
