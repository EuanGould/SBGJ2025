using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject EndScreen;

    private Canvas StartMenu;

    public void Init()
    {
        StartMenu = StartScreen.GetComponent<Canvas>();
        StartMenu.enabled = true;
    }
    public void OnStartClick()
    {
        StartMenu.enabled = false;
    }

    public void OnQuitClick()
    {
        // Exit the game
    }
    void Start()
    {
        Init();
    }

}