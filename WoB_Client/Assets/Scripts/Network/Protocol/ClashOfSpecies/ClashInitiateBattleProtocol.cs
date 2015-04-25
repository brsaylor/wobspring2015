using System;
using System.Collections.Generic;
using System.IO;

public class ClashInitiateBattleProtocol {
	
	public static NetworkRequest Prepare(int otherPlayerID, List<int> species) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_INITIATE_BATTLE);
		request.AddInt32(otherPlayerID);
		request.AddInt32(species.Count);
		foreach(int s in species){
			request.AddInt32(s);
		}
		
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashInitiateBattle response = new ResponseClashInitiateBattle();

		response.valid = DataReader.ReadBool(dataStream);
		/*
		short status = DataReader.ReadShort(dataStream);
		response.status = status;
		if(response.status == 0){
			//battle begun
		}else{
			//player already in battle, refuse to begin
		}//*/
		return response;
	}
}

public class ResponseClashInitiateBattle : NetworkResponse {

	//public short status {get; set;}
	public bool valid {get; set;}

	public ResponseClashInitiateBattle() {
		protocol_id = NetworkCode.CLASH_INITIATE_BATTLE;
	}
}
