using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pauseUI;
    public bool gameIsPaused;

    [Header("EndGame")]
    public GameObject endGameUI;
    public int currentPontuation;
    public TMP_Text pontuationTxt;

    private Pontuation pontuation;
    private CursorController cursorController;

    private void Start()
    {
        pontuation = FindObjectOfType<Pontuation>();
        cursorController = FindObjectOfType<CursorController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void EndGame()
    {
        currentPontuation = pontuation.currentPontuation;
        pontuationTxt.text = currentPontuation.ToString();
        cursorController.SetCursorVisible();
        endGameUI.SetActive(true);
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        cursorController.SetCursorInvisible();
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        cursorController.SetCursorVisible();
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        cursorController.SetCursorVisible();
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
