using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class play : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }
    public void exit()
    {
        print("exit");
        Application.Quit();
        
    }
}