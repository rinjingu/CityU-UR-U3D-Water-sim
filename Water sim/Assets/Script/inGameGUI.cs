using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameGUI : MonoBehaviour
{
    public GameObject tabGUI;
    public GameObject pauseMenu;
    public GameObject m_mainMenu;
    public GameObject m_startUpMenu;
    public GameObject m_settingMenu;
    public bool isGamePause;
    private bool isGameStart;
    public float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        m_mainMenu.SetActive(true);
        m_startUpMenu.SetActive(false);
        m_settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        tabGUI.SetActive(false);
        isGameStart = false;
        isGamePause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            generalGUI();
        }
    }

    public void generalGUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            tabGUI.SetActive(false);
            if (isGamePause)
            {
                pauseMenu.SetActive(false);
                resumeGame();
            }
            else
            {
                pauseMenu.SetActive(true);
                pauseGame();
            }
        }
        else if (!isGamePause)
        {
            tabGUI.SetActive(Input.GetKey(KeyCode.Tab));
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0.0f;
        isGamePause = true;
    }

    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        resumeGame();
    }
    public void resumeGame()
    {
        Time.timeScale = timeScale;
        isGamePause = false;
    }
    public void menuBack()
    {
        if (!isGameStart)
        {
            m_mainMenu.SetActive(true);
            m_startUpMenu.SetActive(false);
            m_settingMenu.SetActive(false);
        }
        else
        {
            tabGUI.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 0.0f;
            isGameStart = false;
            isGamePause = false;
            m_mainMenu.SetActive(true);
        }
    }

    public void startUpMenu()
    {
        m_mainMenu.SetActive(false);
        m_startUpMenu.SetActive(true);
        m_settingMenu.SetActive(false);
    }

    public void settingMenu()
    {
        m_mainMenu.SetActive(false);
        m_startUpMenu.SetActive(false);
        m_settingMenu.SetActive(true);
    }

    public void startGame()
    {

        m_startUpMenu.SetActive(false);

        //to be added scene generation
        isGameStart = true;
        Time.timeScale = timeScale;
    }

    public void killGame()
    {
        Application.Quit();
    }
}
