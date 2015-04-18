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

		ResponsePlayers pl = PlayersProtocol.Parse(dataStream);
		response.playerList = pl.playerList;

		return response;
	}
}

public class ResponseClashPlayerList : ResponsePlayers {
	
	public ResponseClashPlayerList() {
		protocol_id = NetworkCode.CLASH_PLAYER_LIST;
	}
}
