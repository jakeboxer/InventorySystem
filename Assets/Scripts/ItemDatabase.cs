using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
    private List<Item> items = new List<Item>();
    private JsonData itemData;

    void Start() {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        SetUpItems();
    }

    void SetUpItems() {
        foreach (JsonData elem in itemData) {
            items.Add(new Item(elem));
        }
    }

    public Item FindItemById(int id) {
        foreach (Item item in items) {
            if (item.ID == id) {
                return item;
            }
        }

        return null;
    }
}

public class Item {
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defense { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item() {
        ID = -1;
    }

    public Item(JsonData data) {
        ID = (int)data["id"];
        Title = data["title"].ToString();
        Value = (int)data["value"];
        Power = (int)data["stats"]["power"];
        Defense = (int)data["stats"]["defense"];
        Vitality = (int)data["stats"]["vitality"];
        Description = data["description"].ToString();
        Stackable = (bool)data["stackable"];
        Rarity = (int)data["rarity"];
        Slug = data["slug"].ToString();

        Sprite = Resources.Load<Sprite>("Sprites/Items/" + Slug);
    }
}