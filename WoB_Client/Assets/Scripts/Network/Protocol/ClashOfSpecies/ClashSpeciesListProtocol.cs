using System;
using System.Collections.Generic;
using System.IO;

public class ClashSpeciesListProtocol{
	
	public static NetworkRequest Prepare() {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_SPECIES_LIST);
		
		return request;
	}

	public static NetworkResponse Parse(MemoryStream dataStream) {

		//Ugly code. Better idea to have ClashSpeciesListProtocol subclass
		//SpeciesListProtocol, but that requires refactoring, splitting up 
		//puttin parse code into helper method
		ResponseSpeciesList sl = (ResponseSpeciesList) SpeciesListProtocol.Parse(dataStream);
		ResponseClashSpeciesList response = new ResponseClashSpeciesList();
		response.speciesList = sl.speciesList;
		return response;
	}
}

public class ResponseClashSpeciesList : ResponseSpeciesList {
	
	public ResponseClashSpeciesList() {
		protocol_id = NetworkCode.CLASH_SPECIES_LIST;
	}
}
