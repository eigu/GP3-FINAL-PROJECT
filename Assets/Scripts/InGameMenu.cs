using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import Unity UI namespace for using UI components

public class InGameMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;

    public AudioSource audioSource; // Declare AudioSource variable
    public Slider volumeSlider; // Declare Slider variable for volume control

    void Start()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);

        if (menuPanel.activeSelf)
            settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainManu");
    }

    public void SetVolume()
    {
        // Ensure that AudioSource and Slider references are assigned in the Unity Editor
        if (audioSource != null && volumeSlider != null)
        {
            // Adjust the volume of the AudioSource based on the value of the volumeSlider
            audioSource.volume = volumeSlider.value;
        }
        else
        {
            Debug.LogWarning("AudioSource or VolumeSlider is not assigned!");
        }
    }
}
