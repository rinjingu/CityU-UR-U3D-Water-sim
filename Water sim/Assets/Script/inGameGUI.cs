using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameGUI : MonoBehaviour
{
    public GameObject tabGUI;
    public GameObject pauseMenu;
    public bool isGamePause;
    public float timeScale;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        tabGUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        generalGUI();
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
}
