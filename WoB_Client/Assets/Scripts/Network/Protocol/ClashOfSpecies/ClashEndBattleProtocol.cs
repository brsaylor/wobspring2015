using System;
using System.Collections.Generic;
using System.IO;

public class ClashEndBattleProtocol {
	
	public static NetworkRequest Prepare(bool won) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_END_BATTLE);
		request.AddBool(won);
		
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
