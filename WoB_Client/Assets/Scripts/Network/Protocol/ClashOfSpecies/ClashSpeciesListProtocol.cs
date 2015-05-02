using System;
using System.Collections.Generic;
using System.IO;

public class ClashSpeciesListProtocol {
	
	public static NetworkRequest Prepare() {
		NetworkRequest request = new NetworkRequest(NetworkCode.CLASH_SPECIES_LIST);
		return request;
	}

	public static NetworkResponse Parse(MemoryStream dataStream) {
		ResponseClashSpeciesList response = new ResponseClashSpeciesList();

		int count = DataReader.ReadInt(dataStream);
		for(int i = 0; i < count; i++) {
            ClashSpecies s = new ClashSpecies();
            s.id = DataReader.ReadInt(dataStream);
            s.name = DataReader.ReadString(dataStream);
			s.cost = DataReader.ReadInt(dataStream);
			s.type = (ClashSpecies.SpeciesType)DataReader.ReadInt(dataStream);
			s.description = DataReader.ReadString(dataStream);
			s.attack = DataReader.ReadInt(dataStream);
			s.hp = DataReader.ReadInt(dataStream);
			s.moveSpeed = DataReader.ReadInt(dataStream);
			s.attackSpeed = DataReader.ReadInt(dataStream);

			response.speciesList.Add(s);
		}
		return response;
	}
}

public class ResponseClashSpeciesList : NetworkResponse {
	public List<ClashSpecies> speciesList = new List<ClashSpecies>();

	public ResponseClashSpeciesList() {
	    protocol_id = NetworkCode.CLASH_SPECIES_LIST;
	}
}
