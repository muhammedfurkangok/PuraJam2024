using System;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button startBackButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button optionsBackButton;
        [SerializeField] private Button quitButton;

        [Header("UI Elements")]
        [SerializeField] private NetworkManagerHUD connectionUI;
        [SerializeField] private GameObject startUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject optionsUI;

        private void Start()
        {
            startButton.onClick.AddListener(StartButton);
            startBackButton.onClick.AddListener(StartBackButton);
            optionsButton.onClick.AddListener(OptionsButton);
            optionsBackButton.onClick.AddListener(OptionsBackButton);
            quitButton.onClick.AddListener(QuitButton);
        }

        private void StartButton()
        {
            connectionUI.enabled = true;
            startUI.SetActive(true);
            mainMenuUI.SetActive(false);
        }

        private void StartBackButton()
        {
            connectionUI.enabled = false;
            startUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }

        private void OptionsButton()
        {
            mainMenuUI.SetActive(false);
            optionsUI.SetActive(true);
        }

        private void OptionsBackButton()
        {
            mainMenuUI.SetActive(true);
            optionsUI.SetActive(false);
        }

        private void QuitButton()
        {
            Application.Quit();
        }
    }
}
