using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private int firstGameplaySceneIndex = 1;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(firstGameplaySceneIndex);
    }
}
