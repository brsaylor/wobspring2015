using System;
using System.Collections.Generic;
using System.IO;

public class ClashPlayerListProtocol{
	
	public static NetworkRequest Prepare() {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_PLAYER_LIST);
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {

		//same deal as ClashSpeciesListProtocol
		ResponseClashPlayerList response = new ResponseClashPlayerList();
		int count = DataReader.ReadInt(dataStream);
		for(int i = 0; i < count; i++){
			int pid = DataReader.ReadInt(dataStream);
			string pname = DataReader.ReadString(dataStream);
			response.players.Add(pid, pname);
		}

		return response;
	}
}

public class ResponseClashPlayerList : NetworkResponse {
	public Dictionary<int, string> players {get; set;}

	public void addPlayer(int player_id, string player_name){
		players.Add(player_id, player_name);
	}

	public ResponseClashPlayerList() {
		protocol_id = NetworkCode.CLASH_PLAYER_LIST;
		players = new Dictionary<int, string >();
	}
}
