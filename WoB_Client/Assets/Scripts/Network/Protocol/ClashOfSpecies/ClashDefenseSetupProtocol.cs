using System;
using System.Collections.Generic;
using System.IO;

public class ClashDefenseSetupProtocol {
	
	public static NetworkRequest Prepare() {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_DEFENSE_SETUP);
		request.AddString("dummy setup data");
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashDefenseSetup response = new ResponseClashDefenseSetup();


		return response;
	}
}

public class ResponseClashDefenseSetup : NetworkResponse {

	public ResponseClashDefenseSetup() {
		protocol_id = NetworkCode.CLASH_DEFENSE_SETUP;
	}
}
