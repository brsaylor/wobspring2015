using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProtocolTest : MonoBehaviour {

	private bool testLock = true;

	void TestResult(string suite, bool passed) {
		Debug.Log(suite + " <b>" + (passed ? "PASSED" : "FAILED") + "</b>");
	}

	IEnumerator Start() {

		// Appropriately detecting a new user.
		yield return StartCoroutine(Execute(ClashEntryProtocol.Prepare(), (res) => {
			var response = res as ResponseClashEntry;
			var passed = response.firstTime;
			TestResult("New User", passed);
		}));

		// Getting list of available species.
		yield return StartCoroutine(Execute(ClashSpeciesListProtocol.Prepare(), (res) => {
			var response = res as ResponseClashSpeciesList;
			var passed = (response.speciesList != null && response.speciesList.Count == 12);
			TestResult("Species List", passed);
		}));

		// Invalid defense config setup.
		var request = ClashDefenseSetupProtocol.Prepare(2, new Dictionary<int, Vector2>() {
			{ 6, new Vector2(0.5f, 0.5f) },
			{ 3, new Vector2(0.25f, 0.25f) }
		});
		yield return StartCoroutine(Execute(request, (res) => {
			var response = res as ResponseClashDefenseSetup;
			TestResult("Invalid Defense", !response.valid);
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
			TestResult("Valid Defense", response.valid);
		}));

		// Player list request.
		yield return StartCoroutine(Execute(ClashPlayerListProtocol.Prepare(), (res) => {
			var response = res as ResponseClashPlayerList;
			var passed = (response.players.Count >= 6);
			TestResult("Player List", passed);
		}));

		//Player view request
		yield return StartCoroutine(Execute(ClashPlayerViewProtocol.Prepare(1), (res) => {
			var response = res as ResponseClashPlayerView;
			Debug.Log(response.TerrainID);
			var passed = (response.TerrainID != 0 &&
			              response.defenseSpecies.Count == 5);

			TestResult("Player View", passed);
			Debug.Log("config created at " + response.Timestamp);
		}));

		//initiate failure
		request = ClashInitiateBattleProtocol.Prepare(3, new List<int> {
			4, 5, 8, 10, 11, 9
		});
		yield return StartCoroutine(Execute(request, (res) => {
			var response = res as ResponseClashInitiateBattle;
		}));

		//initiate success
		request = ClashInitiateBattleProtocol.Prepare(3, new List<int> {
			4, 5, 8, 10, 11
		});
		yield return StartCoroutine(Execute(request, (res) => {
			var response = res as ResponseClashInitiateBattle;
			TestResult("Initiate Battle", response.valid);
		}));

		//battle end
		yield return StartCoroutine(Execute(ClashEndBattleProtocol.Prepare(true), (res) => {
			var response = res as ResponseClashEndBattle;
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
