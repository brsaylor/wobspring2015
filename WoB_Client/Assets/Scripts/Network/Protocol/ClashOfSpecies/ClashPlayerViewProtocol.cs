using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class ClashPlayerViewProtocol {
	
	public static NetworkRequest Prepare(int playerId) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_PLAYER_VIEW);
		request.AddInt32(playerId);
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashPlayerView response = new ResponseClashPlayerView();

		var defenseId = DataReader.ReadInt(dataStream);
		response.terrain = DataReader.ReadInt(dataStream);
        response.playerId = DataReader.ReadInt(dataStream);

		string timeString = DataReader.ReadString(dataStream);
		long timeLong = 0;
		long.TryParse(timeString, out timeLong);
        TimeSpan ts = TimeSpan.FromMilliseconds(timeLong);
		DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		response.createdAt = epoch.Add(ts).ToUniversalTime();

		int count = DataReader.ReadInt(dataStream);
		for(int i = 0; i < count; i++){
			int species = DataReader.ReadInt(dataStream);
			float x = DataReader.ReadFloat(dataStream);
			float y = DataReader.ReadFloat(dataStream);

            response.layout.Add(species, new Vector2(x, y));
		}

		return response;
	}
}

public class ResponseClashPlayerView : NetworkResponse {
	//public Player player { get; set; }
    public int playerId;
    public int terrain;
    public DateTime createdAt;
    public Dictionary<int, Vector2> layout = new Dictionary<int,Vector2>();

	public ResponseClashPlayerView() {
		protocol_id = NetworkCode.CLASH_PLAYER_VIEW;
	}
}
