using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    public GameObject playbutton;
    public GameObject exitButton;
    public GameObject instructions;
    public GameObject backToMenu;
    public GameObject controlsbutton;
    public GameObject audioSource; 
    public void playGame()
    {
        DontDestroyOnLoad(audioSource);
        SceneManager.LoadScene(1);
       
    }
    public void exit()
    {
        print("exit");
        Application.Quit();
        

    }
    public void controls()
    {
        playbutton.SetActive(false);
        exitButton.SetActive(false);
        controlsbutton.SetActive(false);
        instructions.SetActive(true);
        backToMenu.SetActive(true);
    }
    public void menu()
    {
        playbutton.SetActive(true);
        exitButton.SetActive(true);
        instructions.SetActive(false);
        backToMenu.SetActive(false);

        controlsbutton.SetActive(true);
    }

}