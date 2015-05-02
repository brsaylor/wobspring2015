using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class ClashEntryProtocol {
	
	public static NetworkRequest Prepare() {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_ENTRY);
		return request;
	}
	
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashEntry response = new ResponseClashEntry();

		response.isNew = DataReader.ReadBool(dataStream);
		if (!response.isNew) {
            response.config = new Dictionary<int, Vector2>();

			//read in data on own defense setup
			response.terrain = DataReader.ReadInt(dataStream);
			int count = DataReader.ReadInt(dataStream);
			for (int i = 0; i < count; i++) {
				int id = DataReader.ReadInt(dataStream);
				float x = DataReader.ReadFloat(dataStream);
				float y = DataReader.ReadFloat(dataStream);

                response.config.Add(id, new Vector2(x, y));
			}
		}

		return response;
	}
}

public class ResponseClashEntry : NetworkResponse {

	public bool isNew;
    public int terrain;
    public Dictionary<int, Vector2> config = null;

	public ResponseClashEntry() {
		protocol_id = NetworkCode.CLASH_ENTRY;
	}
}
