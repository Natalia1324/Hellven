using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

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
    public void StartsItem() {
        //InGame.Add(new Clothes(4, "Assets/Cainos/Pixel Art Icon Pack - RPG/Texture/Potion/Blue Potion.png", "Okulary","Nosisz na g³owie",1,"Staki d³ugi",5));

    }
    public struct pomoc {
        public ItemType Type;

        public int Id;
        public Texture2D Tekstura;
        public string Name;
        public string Description;
        public int Quantity;
        public bool Eatable;
        public pomoc(int Id, Texture2D texture, string name, string Description, int quantity, ItemType type, bool eatable)
        {
            this.Id = Id;
            this.Tekstura = texture;
            this.Name = name;
            this.Description = Description;
            this.Quantity = quantity;
            this.Type = type;
            this.Eatable = eatable;
        }
    }

}
