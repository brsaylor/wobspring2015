using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Used on first entering the Clash of Species game from lobby
/// </summary>

public class ClashEntryProtocol {
	/// <summary>
	/// Prepares the request to send
	/// </summary>
	public static NetworkRequest Prepare() {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_ENTRY);

		return request;
	}

	/// <summary>
	/// Creates a response object containg the data from the server
	/// </summary>
	/// <param name="dataStream">The input stream</param>
	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashEntry response = new ResponseClashEntry();

		response.firstTime = DataReader.ReadBool(dataStream);
		if(response.firstTime){
			
		}else{
			//read in data on own defense setup
			response.terrainID = DataReader.ReadInt(dataStream);
			int defenseElementCount = DataReader.ReadInt(dataStream);
			for(int i = 0; i < defenseElementCount; i++){
				int species_id = DataReader.ReadInt(dataStream);
				float x = DataReader.ReadFloat(dataStream);
				float y = DataReader.ReadFloat(dataStream);

				ClashUnitData unit = new ClashUnitData();
				unit.species_id = species_id;
				unit.location = new Vector3(x, y, 0); //
				response.config.Add(unit);
			}
		}

		return response;
	}
}

/// <summary>
/// Container for data sent by the server
/// </summary>
public class ResponseClashEntry : NetworkResponse {

	/// <summary>
	/// Whether the player has a defense set up for Clash of Species
	/// </summary>
	/// <value><c>true</c> if no defense; otherwise, <c>false</c>.</value>
	public bool firstTime {get; set;}

	/// <summary>
	/// Gets and sets the terrain ID
	/// </summary>
	/// <value>The id of the terrain in the defense setup, if one exists</value>
	public int terrainID {get; set;}

	/// <summary>
	/// Gets and sets the list of species
	/// </summary>
	/// <value>The list of species in the defense setup, if one exists</value>
	public List<ClashUnitData> config {get; set;}

	public ResponseClashEntry() {
		protocol_id = NetworkCode.CLASH_ENTRY;
		config = new List<ClashUnitData>();
	}
}
