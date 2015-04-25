using System;
using System.Collections.Generic;
using System.IO;

public class ClashEndBattleProtocol {

	public enum BattleResult{
		WIN = 0,
		LOSS,
		DRAW
	}
	
	public static NetworkRequest Prepare(BattleResult res) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_END_BATTLE);
		request.AddInt32(res);

		
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashEndBattle response = new ResponseClashEndBattle();

		return response;
	}
}

public class ResponseClashEndBattle : NetworkResponse {
	
	public ResponseClashEndBattle() {
		protocol_id = NetworkCode.CLASH_END_BATTLE;
	}


}
