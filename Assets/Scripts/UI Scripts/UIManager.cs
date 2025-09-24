using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject EndScreen;

    private Canvas StartMenu;
    private Canvas EndMenu;


    public void Init()
    {
        StartMenu = StartScreen.GetComponent<Canvas>();
        EndMenu = StartScreen.GetComponent<Canvas>();

        StartMenu.enabled = true;
    }

    private void LoadGame()
    {
        
    }

    public void ShowGameOver()
    {
        EndMenu.enabled = true;
    }

    public void OnStartClick()
    {
        StartMenu.enabled = false;
        LoadGame();
    }

    public void OnReplayClick()
    {
        EndMenu.enabled = false;
        LoadGame();
    }

    public void OnQuitClick()
    {
        // Exit the game
        Application.Quit();
    }
    void Start()
    {
        Init();
    }

}