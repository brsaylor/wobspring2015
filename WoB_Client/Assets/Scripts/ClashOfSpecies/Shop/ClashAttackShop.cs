using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SpeciesType = ClashSpecies.SpeciesType;

public class ClashAttackShop : MonoBehaviour {

	private ClashGameManager manager;
    private ClashDefenseConfig pending = new ClashDefenseConfig();

	public GridLayoutGroup carnivoreGroup;
    public GridLayoutGroup herbivoreGroup;
	public GridLayoutGroup omnivoreGroup;
	public GridLayoutGroup plantGroup;

	public HorizontalLayoutGroup selectedGroup;

    public Image previewImage;
    public Text previewText;

	public GameObject shopElementPrefab;
	public GameObject selectedUnitPrefab;

	void Awake() {
        manager = GameObject.Find("MainObject").GetComponent<ClashGameManager>();
;
        foreach (var species in manager.availableSpecies) {
            var item = (Instantiate(shopElementPrefab) as GameObject).GetComponent<ClashShopItem>();
            item.displayText.text = species.name;

            var texture = Resources.Load<Texture2D>("Images/" + species.name);
            item.displayImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            item.addButton.onClick.AddListener(() => {
                // If item exists in the list already, don't add.
                foreach (ClashSelectedUnit existing in selectedGroup.GetComponentsInChildren<ClashSelectedUnit>()) {
                    if (existing.label.text == item.displayText.text) {
                        return;
                    }
                }

                // If the selected list already contains 5 units, don't add.
                if (selectedGroup.transform.childCount == 5) {
                    return;
                }

                // Instantiated a selected item prefab and configure it.
                var selected = (Instantiate(selectedUnitPrefab) as GameObject).GetComponent<ClashSelectedUnit>();
                selected.transform.SetParent(selectedGroup.transform);
                selected.image.sprite = item.displayImage.sprite;
                selected.transform.localScale = Vector3.one;
                selected.label.text = item.displayText.text;
                selected.remove.onClick.AddListener(() => {
                    Destroy(selected.gameObject);
                });
            });

            var description = species.description;
            item.previewButton.onClick.AddListener(() => {
                previewImage.sprite = item.displayImage.sprite;
                previewText.text = description;
            }); 

            switch (species.type) {
                case SpeciesType.CARNIVORE:
                    item.transform.SetParent(carnivoreGroup.transform);
                    break;
                case SpeciesType.HERBIVORE:
                    item.transform.SetParent(herbivoreGroup.transform);
                    break;
                case SpeciesType.OMNIVORE: 
                    item.transform.SetParent(omnivoreGroup.transform);
                    break;
                case SpeciesType.PLANT: 
                    item.transform.SetParent(plantGroup.transform);
                    break;
                default: break;
            }

            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0.0f);
            item.transform.localScale = Vector3.one;
        }

        // If the player has an existing attack configuration, populate the selected unit panel.
        if (manager.attackConfig != null) {
            foreach (var species in manager.attackConfig.layout) {
                var selected = (Instantiate(selectedUnitPrefab) as GameObject).GetComponent<ClashSelectedUnit>();
                var texture = Resources.Load<Texture2D>("Images/" + species.name);
                selected.transform.SetParent(selectedGroup.transform);
                selected.image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                selected.transform.localScale = Vector3.one;
                selected.label.text = species.name;
                selected.remove.onClick.AddListener(() => {
                    Destroy(selected.gameObject);
                });
            }
        }
	}

    // Use this for initialization
    void Start() {}

    public void Engage() {
        if (selectedGroup.transform.childCount == 5) {
            if (manager.attackConfig == null) {
                manager.attackConfig = new ClashAttackConfig();
            }
			manager.attackConfig.owner = manager.currentPlayer;
			manager.attackConfig.layout = new List<ClashSpecies>();
            foreach (ClashSelectedUnit csu in selectedGroup.GetComponentsInChildren<ClashSelectedUnit>()) {
                var species = manager.availableSpecies.Single(x => x.name == csu.label.text);
				manager.attackConfig.layout.Add(species);
            }
            Game.LoadScene("ClashBattle");
        }
    }

    public void BackToMain() {
		Game.LoadScene ("ClashMain");
    }
}
