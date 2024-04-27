    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject optionsUI;

    public void StartButton()
    {
        SceneManager.LoadScene("FurkanScene");
    }   
    public void OptionsButton()
    {
        mainMenuUI.SetActive(false);
        optionsUI.SetActive(true);
    }
    public void BackButton()
    {
        mainMenuUI.SetActive(true);
        optionsUI.SetActive(false);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
