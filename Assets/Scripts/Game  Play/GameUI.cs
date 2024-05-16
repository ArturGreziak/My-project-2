using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Frruit Counter")]
    [SerializeField] private TextMeshProUGUI fruitCounterText;

    [Header("Game Over Screen")]
    [SerializeField] private GameObject gameOverScreenObject;
    [SerializeField] private Button gameOverRestartButton;

    [Header("Complitete Screen")]
    [SerializeField] private GameObject levelCompleteScreenObject;
    [SerializeField] private Button nextLevelButton;

    [Header("Game Complete Screen")]
    [SerializeField] private GameObject gameCompleteScreen;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private int _firstSceneIndex;

    private Player player;
    private PlayerInventory playerInventory;
    private int fruitsOnMap;

    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerInventory = FindObjectOfType<PlayerInventory>();

        var fruits = FindObjectsOfType<Fruit>();
        fruitsOnMap = fruits.Length;

        gameOverScreenObject.SetActive(false);
        levelCompleteScreenObject.SetActive(false);
        gameCompleteScreen.SetActive(false);




        player.OnLose += DisplayGameOverScreen;

        gameOverRestartButton.onClick.AddListener(RestartScreen);
        nextLevelButton.onClick.AddListener(MoveToNextLevel);

        restartGameButton.onClick.AddListener(RestartGame);
        backToMenuButton.onClick.AddListener(BackToMenu);
        exitGameButton.onClick.AddListener(ExitGameMenu);
    }

    private void ExitGameMenu()
    {
        Application.Quit();
    }

    private void BackToMenu()
    {
        
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void MoveToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DisplayGameOverScreen()
    {
        gameOverScreenObject.SetActive(true);
    }

    private void Update()
    {
        fruitCounterText.text = $"{playerInventory.GetCollectedFruits()}/{fruitsOnMap}";
        TryDisplayLevelCompleteScreen();
    }

    private void TryDisplayLevelCompleteScreen()
    {
        if (playerInventory.GetCollectedFruits() == fruitsOnMap && !levelCompleteScreenObject.activeSelf)
        {
            player.gameObject.SetActive(false);

            if(Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1)) 
            {
                levelCompleteScreenObject.SetActive(true);
            } else
            {
                gameCompleteScreen.SetActive(true);
            }

            
        }
    }
}
