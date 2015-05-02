using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ClashDefenseSetupProtocol {
	
	public static NetworkRequest Prepare(string terrainName, Dictionary<int, Vector2> config) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_DEFENSE_SETUP);
		request.AddString(terrainName);
		request.AddInt32(config.Count);
		foreach(var pair in config){
			request.AddInt32(pair.Key);
			request.AddFloat(pair.Value.x);
			request.AddFloat(pair.Value.y);
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
    public bool valid;

	public ResponseClashDefenseSetup() {
		protocol_id = NetworkCode.CLASH_DEFENSE_SETUP;
	}
}
