using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eq : MonoBehaviour
{
    public GameObject Option_UI;
    public GameObject EQ_UI;
    public GameObject Shop_UI;
    private bool open;

    void Start()
    {
        open = false;

        EQ_UI.SetActive(false);
        Option_UI.SetActive(false);
        Shop_UI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Option_UI.activeInHierarchy || Shop_UI.activeInHierarchy)
            {
                Option_UI.SetActive(false);
                Shop_UI.SetActive(false);
            }

            EQ_UI.SetActive(!EQ_UI.activeInHierarchy);
            open = EQ_UI.activeInHierarchy;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (EQ_UI.activeInHierarchy || Shop_UI.activeInHierarchy)
            {
                EQ_UI.SetActive(false);
                Shop_UI.SetActive(false);
            }

            Option_UI.SetActive(!Option_UI.activeInHierarchy);
            open = Option_UI.activeInHierarchy;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (EQ_UI.activeInHierarchy || Option_UI.activeInHierarchy)
            {
                EQ_UI.SetActive(false);
                Option_UI.SetActive(false);
            }

            Shop_UI.SetActive(!Shop_UI.activeInHierarchy);
            open = Shop_UI.activeInHierarchy;
        }

        if (open)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
