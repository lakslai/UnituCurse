using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
        SaveData();
        Debug.Log("Save");
        LoadData();
        Debug.Log("lsssog");
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void LosdMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");

    }
    // Ссылка на экземпляр класса Data
    public Data dataScript;

    private void Start()
    {
        // Присвоение dataScript через поиск по тегу (предположим, что у вас есть объект с тегом "Data")
        dataScript = GameObject.FindGameObjectWithTag("Data").GetComponent<Data>();
    }

    public void SaveData()
    {
        // Вызов метода Save из скрипта Data
        dataScript.Save();
    }

    public void LoadData()
    {
        // Вызов метода Load из скрипта Data
        dataScript.Load();
    }

}
