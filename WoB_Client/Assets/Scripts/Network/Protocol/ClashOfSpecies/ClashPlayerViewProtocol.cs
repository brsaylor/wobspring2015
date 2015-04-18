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

		int player_id = DataReader.ReadInt(dataStream);
		
		Player player = new Player(player_id);

		//other player data will go in here later

		response.player = player;

		return response;
	}
}

public class ResponseClashPlayerView : NetworkResponse {

	public Player player { get; set; }

	public ResponseClashPlayerView() {
		protocol_id = NetworkCode.CLASH_PLAYER_VIEW;
	}
}
