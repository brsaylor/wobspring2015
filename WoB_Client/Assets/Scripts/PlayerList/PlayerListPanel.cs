using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerListPanel : MonoBehaviour {

	private GameObject mainObject;
	private Map map;
	private Rect windowRect;
	private Vector2 scrollPosition = Vector2.zero;
	private Dictionary<int, Player> PlayerList = new Dictionary<int, Player>();

	public static Texture PlayerOnline = (Texture)Resources.Load(Constants.IMAGE_RESOURCES_PATH + "player-online");
	public static Texture PlayerOffline = (Texture)Resources.Load(Constants.IMAGE_RESOURCES_PATH + "player-offline");

	private bool isBeHidden { get; set; }
	//Window
	private int height;
	public GUISkin skin;

	// Use this for initialization
	void Awake () {
		mainObject = GameObject.Find("MainObject");
		map = GameObject.Find("Map").GetComponent<Map>();
		this.PlayerList = map.playerList;
		Hide ();
		enabled = false;
		skin = Resources.Load("Skins/DefaultSkin") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
				if (!isBeHidden) {
						height = PlayerList.Count * 20;
						GUIStyle style = new GUIStyle(skin.label);
						style.font = skin.font;
						style.fontSize = 18;
						style.alignment = TextAnchor.UpperLeft;
						Color32 color = new Color32();;

						windowRect = new Rect(25, 10, 150, 200);
						if (GUI.Button(new Rect(25, 210, 50, 30), "Close")) {
							mainObject.GetComponent<PlayerList>().Show();
							Destroy(this);
						}
				Color temp = GUI.color;
				GUI.Box(new Rect(25, 10, 150, 200), "Player List");
				scrollPosition = GUI.BeginScrollView(new Rect(25, 30, 150, 175), scrollPosition, new Rect(25, 30, 130, height)); ;
				for (int i = 1; i <= PlayerList.Count; i++) {
					
					GUI.BeginGroup (new Rect (27, 22 + (i - 1) * 20, 150, 200));
					var boxStyle = new GUIStyle(GUI.skin.box);
//					if (PlayerList[i].color != 0)
//					{
//						color = PlayerColorList[PlayerList[i].color];
//					}
//					else
//					{
//						color = Color.gray;
//					}
					GUIExtended.Label(new Rect(4, 0, 100, 100), PlayerList[i].name, style, Color.white, color);
					GUI.color = new Color(1.0f,1.0f,1.0f,0.0f);
					if (GUI.Button(new Rect(0, 4, 100, 20), ""))
					{
						var ownerid = PlayerList[i].GetID();
						var ownedtile = map.FindPlayerOwnedTile(ownerid);
						if (ownedtile != null)
						{
							var x = ownedtile.transform.position.x;
							var y = GameObject.Find("MapCamera").transform.position.y;
							var z = ownedtile.transform.position.z - 40;
							Vector3 pos = new Vector3(x, y, z);
							GameObject.Find("MapCamera").transform.position = pos;
						}
					}
					GUI.EndGroup ();
				GUI.color = temp;
				}
				GUI.EndScrollView();
				}	  
		}

	public void Show() {
		isBeHidden = false;
	}
	
	public void Hide() {
		isBeHidden = true;
	}
}
