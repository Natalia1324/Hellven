using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemType
    {
        None,
        Food,
        Weapon,
        Materials,
        Clothes
    }

    public ItemType Type { get; set; }

    public int Id { get; set; }
    // Common properties that all items might have
    public Texture2D Tekstura;
    public string Name { get; set; }
    public string Description { get; set; }
    // Iloœæ
    public int Quantity { get; set; }
    //jadalnosc
    public bool Eatable { get; set; }

    // Constructor
    public Item(int Id, Texture2D texture, string name, string Description, int quantity, ItemType type, bool eatable)
    {
        this.Id = Id;
        this.Tekstura = texture;
        this.Name = name;
        this.Description = Description;
        this.Quantity = quantity;
        this.Type = type;
        this.Eatable = eatable;
    }

    // Method to display item info
    public virtual void DisplayInfo()
    {
        //Console.WriteLine($"Item: {Name}, Quantity: {Quantity}, Type: {Type}");
    }
}

// Derived class for Food items
public class Food : Item
{
    public int Calories { get; set; }

    public Food(int Id, Texture2D texture, string name, string Description, int quantity, int calories, bool eatable)
        : base(Id, texture, name, Description, quantity,  ItemType.Food, eatable)
    {
        Calories = calories;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        //Console.WriteLine($"Calories: {Calories}");
    }
}

// Derived class for Weapon items
public class Weapon : Item
{
    public int Damage { get; set; }

    public Weapon(int Id, Texture2D texture, string name, string Description, int quantity, int damage, bool eatable)
        : base(Id, texture, name, Description, quantity, ItemType.Weapon, eatable)
    {
        Damage = damage;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        //Console.WriteLine($"Damage: {Damage}");
    }
}

// Derived class for Materials items
public class Materials : Item
{
    public string MaterialType { get; set; }

    public Materials(int Id, Texture2D texture, string name, string Description, int quantity, string materialType, bool eatable)
        : base(Id, texture, name, Description, quantity, ItemType.Materials, eatable)
    {
        MaterialType = materialType;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        //Console.WriteLine($"Material Type: {MaterialType}");
    }
}

// Derived class for Clothes items
public class Clothes : Item
{
    public string Size { get; set; }
    public int Defence { get; set; }

    public Clothes(int Id, Texture2D texture, string name, string Description, int quantity, string size, int defence, bool eatable)
        : base(Id, texture, name, Description, quantity, ItemType.Clothes, eatable)
    {
        Size = size;
        Defence = defence;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        //Console.WriteLine($"Size: {Size}");
    }
}