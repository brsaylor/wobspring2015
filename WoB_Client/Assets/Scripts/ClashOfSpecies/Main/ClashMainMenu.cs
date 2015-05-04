using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ClashMainMenu : MonoBehaviour {
    private ClashGameManager manager;

	public VerticalLayoutGroup playerListGroup;
	public List<Player> playerList = new List<Player>();
	public Transform previewPanel;
    public GameObject playerItemPrefab;

    private Player selectedPlayer = null;
    private ToggleGroup toggleGroup;

	void Awake() {
        manager = GameObject.Find("MainObject").GetComponent<ClashGameManager>();
    }

	void Start() {
		toggleGroup = playerListGroup.transform.GetComponent<ToggleGroup> ();
        NetworkManager.Send(ClashPlayerListProtocol.Prepare(), (res) => {
            var response = res as ResponseClashPlayerList;

            foreach (var pair in response.players) {
                Player player = new Player(pair.Key);
                player.name = pair.Value;
                playerList.Add(player);

                var item = Instantiate(playerItemPrefab) as GameObject;
                item.transform.SetParent(playerListGroup.transform);
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0.0f);
                item.transform.localScale = Vector3.zero;

                var toggleComp = item.GetComponent<ClashPlayerToggle>();
                toggleComp.id = player.GetID();
                toggleComp.label.text = player.name;
				toggleComp.toggle.group = toggleGroup;
                toggleComp.toggle.onValueChanged.AddListener((val) => {
					previewPanel.GetComponentInChildren<Text>().enabled = !val;
					if(val) {
						selectedPlayer = player;
						NetworkManager.Send (ClashPlayerViewProtocol.Prepare(player.GetID()), (resView) => {
							var responseView = resView as ResponseClashPlayerView;
							previewPanel.GetComponent<RawImage>().texture = Resources.Load("Images/ClashOfSpecies/"+responseView.terrain) as Texture;
							manager.lastDefenseConfig.owner = player;
							manager.lastDefenseConfig.terrain = responseView.terrain;

							//manager.lastDefenseConfig.layout = responseView.layout;
						});
					} else {
						selectedPlayer = null;
						previewPanel.GetComponent<RawImage>().texture = null;
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
		Destroy (manager);
        // Game.LoadScene("Lobby");
    }

    public void EditDefense() {
        Game.LoadScene("ClashShop");
    }

	public void Attack() {
		if (selectedPlayer != null) {
			Game.LoadScene("ClashAttack");
		}
	}
}
