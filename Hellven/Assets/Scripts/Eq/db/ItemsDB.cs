using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        InGame.Add(new Clothes(-5,"Okulary","Nosisz na g³owie",1,"Staki d³ugi",5));
    }

}
