using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ClashMainMenu : MonoBehaviour {
    private ClashGameManager manager;

	public VerticalLayoutGroup playerListGroup;
	public List<Player> playerList = new List<Player>();
	public Transform contentPanel;
    public GameObject playerItemPrefab;

    private Player selectedPlayer = null;
    private ToggleGroup toggleGroup;

	void Awake() {
        manager = GameObject.Find("MainObject").GetComponent<ClashGameManager>();
    }

	void Start() {
        NetworkManager.Send(ClashPlayerListProtocol.Prepare(), (res) => {
            var response = res as ResponseClashPlayerList;

            foreach (var pair in response.players) {
                Player player = new Player(pair.Key);
                player.name = pair.Value;
                playerList.Add(player);

                var item = Instantiate(playerItemPrefab) as GameObject;
                item.transform.SetParent(playerListGroup.transform);
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0.0f);
                item.transform.localScale = Vector3.one;

                item.GetComponentInChildren<Text>().text = player.name;

                var toggle = item.GetComponentInChildren<Toggle>().group = playerListGroup.GetComponent<ToggleGroup>();
                item.GetComponentInChildren<Toggle>().onValueChanged.AddListener((val) => {
                    if (val) {
                        selectedPlayer = player;
                    } else {
                        selectedPlayer = null;
                    }
                });
            }
        });
    }

	void Update() {}

	//protocol does this
	//gets only the player name and terrain name from the valid defense table
	IEnumerator RetrievePlayerList() {
        bool done = false;

        while (!done) yield return null;
	}

    public void ReturnToLobby() {
        // TODO: Find where this should go.
        // Game.LoadScene("Lobby");
    }

    public void EditDefense() {
        Game.LoadScene("ClashShop");
    }

}
