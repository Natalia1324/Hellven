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

    // Common properties that all items might have
    public string Name { get; set; }
    public int Quantity { get; set; }

    // Constructor
    public Item(string name, int quantity, ItemType type)
    {
        Name = name;
        Quantity = quantity;
        Type = type;
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

    public Food(string name, int quantity, int calories)
        : base(name, quantity, ItemType.Food)
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

    public Weapon(string name, int quantity, int damage)
        : base(name, quantity, ItemType.Weapon)
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

    public Materials(string name, int quantity, string materialType)
        : base(name, quantity, ItemType.Materials)
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


    public Clothes(string name, int quantity, string size)
        : base(name, quantity, ItemType.Clothes)
    {
        Size = size;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        //Console.WriteLine($"Size: {Size}");
    }
}