using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Test = System.Diagnostics.Debug;

public class ProtocolTest : MonoBehaviour {

	private bool testLock = true;

	IEnumerator Start() {

		// Appropriately detecting a new user.
		yield return StartCoroutine(Execute(ClashEntryProtocol.Prepare(2), (res) => {
			var response = res as ResponseClashEntry;
			Test.Assert(response.firstTime);
			Test.Assert(response.config == null);
		}));

		// Getting list of available species.
		yield return StartCoroutine(Execute(ClashSpeciesListProtocol.Prepare(), (res) => {
			var response = res as ResponseSpeciesList;
			Test.Assert(response.speciesList != null);
			Test.Assert(response.speciesList.Count == 12);
		}));

		// Invalid defense config setup.
		var request = ClashDefenseSetupProtocol.Prepare(2, new Dictionary<int, Vector2>() {
			{ 6, new Vector2(0.5f, 0.5f) },
			{ 3, new Vector2(0.25f, 0.25f) }
		});
		yield return StartCoroutine(Execute(request, (res) => {
			var response = res as ResponseClashDefenseSetup;
			Test.Assert(!response.valid);
		}));

		// Valid defense config setup.
		request = ClashDefenseSetupProtocol.Prepare(2, new Dictionary<int, Vector2>() {
			{ 6, new Vector2(0.5f, 0.5f) },
			{ 3, new Vector2(0.25f, 0.25f) },
			{ 7, new Vector2(0.3f, 0.3f) },
			{ 12, new Vector2(0.75f, 0.75f) },
			{ 13, new Vector2(0.15f, 0.15f) }
		});
		yield return StartCoroutine(Execute(request, (res) => {
			var response = res as ResponseClashDefenseSetup;
			Test.Assert(response.valid);
		}));

		// Player list request.
		yield return StartCoroutine(Execute(ClashPlayerListProtocol.Prepare(), (res) => {
			var response = res as ResponseClashPlayerList;
			Test.Assert(response.players.Count != 0);
		}));

		//Player view request
		yield return StartCoroutine(Execute(ClashPlayerViewProtocol.Prepare(2), (res) => {
			var response = res as ResponseClashPlayerView;
			Test.Assert(response.TerrainID != 0);
			Test.Assert(response.defenseSpecies != null);
			Test.Assert(response.defenseSpecies.Count != 0);
		}));

		//initiate failure
		request = ClashInitiateBattleProtocol.Prepare(3, new List<int> {
			4, 5, 8, 10, 11, 9
		});
		yield return StartCoroutine(Execute(request, (res) => {
			var response = res as ResponseClashInitiateBattle;
			Test.Assert(response.status == 2);
		}));

		//initiate success
		request = ClashInitiateBattleProtocol.Prepare(3, new List<int> {
			4, 5, 8, 10, 11
		});
		yield return StartCoroutine(Execute(request, (res) => {
			var response = res as ResponseClashInitiateBattle;
			Test.Assert(response.status == 0);
		}));

		//battle end
		yield return StartCoroutine(Execute(ClashEndBattleProtocol.Prepare(true), (res) => {
			var response = res as ResponseClashEndBattle;
			Test.Assert(response != null);
		}));
	}

	// Use this for initialization
	IEnumerator Execute(NetworkRequest req, NetworkManager.Callback cb) {
		testLock = false;
		NetworkManager.Send(req, (res) => {
			cb(res);
			testLock = true;
		});

		while (!testLock) yield return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
