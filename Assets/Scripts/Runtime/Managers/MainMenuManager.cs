using Mirror;
using UnityEngine;
using UnityEngine.Audio;
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
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button creditsBackButton;

        [Header("UI Elements")]
        [SerializeField] private NetworkManagerHUD connectionUI;
        [SerializeField] private GameObject startUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject optionsUI;
        [SerializeField] private GameObject creditsUI;

        [Header("Sliders")]
        [SerializeField] private Slider SFXSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sensitivitySlider;

        [Header("Audio Mixer")] [SerializeField] private AudioMixer audioMixer;

        private void Start()
        {
            if (startButton != null) startButton.onClick.AddListener(StartButton);
            if (startBackButton != null) startBackButton.onClick.AddListener(StartBackButton);

            optionsButton.onClick.AddListener(OptionsButton);
            optionsBackButton.onClick.AddListener(OptionsBackButton);
            quitButton.onClick.AddListener(QuitButton);
            creditsButton.onClick.AddListener(CreditsButton);
            creditsBackButton.onClick.AddListener(CreditsBackButton);

            //Init player prefs
            if (!PlayerPrefs.HasKey("SFX")) PlayerPrefs.SetFloat("SFX", 1);
            if (!PlayerPrefs.HasKey("Music")) PlayerPrefs.SetFloat("Music", 1);
            if (!PlayerPrefs.HasKey("Sensitivity")) PlayerPrefs.SetFloat("Sensitivity", 1);

            SFXSlider.value = PlayerPrefs.GetFloat("SFX");
            musicSlider.value = PlayerPrefs.GetFloat("Music");
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");

            SFXSlider.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("SFX", value);
                audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);

            });

            musicSlider.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("Music", value);
                audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
            });

            sensitivitySlider.onValueChanged.AddListener((value) => PlayerPrefs.SetFloat("Sensitivity", value));
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

        private void CreditsButton()
        {
            mainMenuUI.SetActive(false);
            creditsUI.SetActive(true);
        }

        private void CreditsBackButton()
        {
            mainMenuUI.SetActive(true);
            creditsUI.SetActive(false);
        }

        private void QuitButton()
        {
            Application.Quit();
        }
    }
}
