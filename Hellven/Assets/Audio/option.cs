using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject Option_UI;
    private bool opensett;
    // Start is called before the first frame update
    void Start()
    {
        opensett = false;
        Option_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("E key was pressed.");
        if (Input.GetKeyDown(KeyCode.O))
        {
            Option_UI.SetActive(!Option_UI.activeInHierarchy);
           // opensett = !opensett;
        }
      //  if (opensett == true)
      //  {
      //      Time.timeScale = 0;
      //  }
      //  else
      //  {
      //      Time.timeScale = 1;
      //  }
    }
}