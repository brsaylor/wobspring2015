using System;
using System.Collections.Generic;
using System.IO;

public class ClashSpeciesListProtocol{
	
	public static NetworkRequest Prepare() {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_SPECIES_LIST);
		
		return request;
	}

	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashSpeciesList response = new ResponseClashSpeciesList();

		int count = DataReader.ReadInt(dataStream);
		for(int i = 0; i < count; i++){
			ClashSpeciesData spec = new ClashSpeciesData();
			spec.species_id = DataReader.ReadInt(dataStream);
			spec.species_price = DataReader.ReadInt(dataStream);
			spec.species_type = DataReader.ReadInt(dataStream);
			spec.description = DataReader.ReadString(dataStream);
			spec.attack_points = DataReader.ReadInt(dataStream);
			spec.hit_points = DataReader.ReadInt(dataStream);
			spec.movement_speed = DataReader.ReadInt(dataStream);
			spec.attack_speed = DataReader.ReadInt(dataStream);

			response.speciesList.Add(spec);
		}

		return response;
	}
}

public class ResponseClashSpeciesList : NetworkResponse {
	public List<ClashSpeciesData> speciesList;

	public ResponseClashSpeciesList() {
		protocol_id = NetworkCode.CLASH_SPECIES_LIST;
		speciesList = new List<ClashSpeciesData>();
	}
}
