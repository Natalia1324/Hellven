using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;
using static UnityEngine.Terrain;

public class ItemsDB : MonoBehaviour
{
    public List<Item> Player = new List<Item>();
    public List<Item> InGame = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        StartsItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addItemPlayer(int Id, Texture2D texture, string name, string Description, int quantity, ItemType type, bool eatable)
    {
        Player.Add(new Item(Id, texture, name, Description, quantity, type, eatable));
    }
    public void addFoodPlayer(int Id, Texture2D texture, string name, string Description, int quantity, int calories, bool eatable)
    {
        Player.Add(new Food(Id, texture, name, Description, quantity, calories, eatable));
    }
    public void addWeaponPlayer(int Id, Texture2D texture, string name, string Description, int quantity, int damage, bool eatable)
    {
        Player.Add(new Weapon(Id, texture, name, Description, quantity, damage, eatable));
    }
    public void addMaterialsPlayer(int Id, Texture2D texture, string name, string Description, int quantity, string materialType, bool eatable)
    {
        Player.Add(new Materials(Id, texture, name, Description, quantity, materialType, eatable));
    }
    public void addClothesPlayer(int Id, Texture2D texture, string name, string Description, int quantity, string size, int defence, bool eatable)
    {
        Player.Add(new Clothes(Id, texture, name, Description, quantity, size, defence, eatable));
    }
    public void StartsItem() {
        
    }
    //public struct pomoc {
    //    public ItemType Type;

    //    public int Id;
    //    public Texture2D Tekstura;
    //    public string Name;
    //    public string Description;
    //    public int Quantity;
    //    public bool Eatable;
    //    public pomoc(int Id, Texture2D texture, string name, string Description, int quantity, ItemType type, bool eatable)
    //    {
    //        this.Id = Id;
    //        this.Tekstura = texture;
    //        this.Name = name;
    //        this.Description = Description;
    //        this.Quantity = quantity;
    //        this.Type = type;
    //        this.Eatable = eatable;
    //    }
    //}

}
