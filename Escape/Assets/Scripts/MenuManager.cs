using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour{
    public static MenuManager Instance;
    public TMP_Text highscoreText;

    [SerializeField] Menu[] menus;

    void Awake(){
        Instance = this;
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].menuName == menuName){
                menus[i].Open();
            }
            else if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void LoadScene(int sceneNumber){
        SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);
    }


    public void OpenMenu(Menu menu)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}