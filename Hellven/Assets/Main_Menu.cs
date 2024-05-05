using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{
    public int volume = 50;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
    public void VolumeeGame(int volume)
    {
        this.volume = volume;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
