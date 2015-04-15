using System;
using System.Collections.Generic;
using System.IO;

public class ClashEntryProtocol {
	
	public static NetworkRequest Prepare(int user_id) {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_ENTRY);
		request.AddInt32(user_id);
		
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashEntry response = new ResponseClashEntry();

		response.firstTime = DataReader.ReadBool(dataStream);
		if(response.firstTime){
			
		}else{
			//read in data on own defense setup
		}

		return response;
	}
}

public class ResponseClashEntry : NetworkResponse {

	public bool firstTime {get; set;}

	public ResponseClashEntry() {
		protocol_id = NetworkCode.CLASH_ENTRY;
	}
}
