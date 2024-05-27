using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eq : MonoBehaviour
{
    public GameObject EQ_UI;
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        EQ_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("E key was pressed.");
        if (Input.GetKeyDown(KeyCode.E))
        {
            EQ_UI.SetActive(!EQ_UI.activeInHierarchy);
            open = !open;
        }
        if (open==true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}