using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Text coinsText;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite isLife, nonLife;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject invPan;
    [SerializeField] private SoudEffect soundEffect;
    [SerializeField] private AudioSource musicSource, soundSource;
    public void ReloadLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        musicSource.volume = (float)PlayerPrefs.GetInt("MusicVolume") / 9;
        soundSource.volume = (float)PlayerPrefs.GetInt("SoundVolume") / 9;
    }
    public void Update()
    {
        coinsText.text = player.GetCoins().ToString();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (player.GetHp() > i)
                hearts[i].sprite = isLife;
            else
                hearts[i].sprite = nonLife;
        }
    }
    public void PauseOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        pauseScreen.SetActive(true);
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        pauseScreen.SetActive(false);
    }
    public void Win()
    {
        soundEffect.PlayWinSound();
        Time.timeScale = 0f;
        player.enabled = false;
        winScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);

        if (PlayerPrefs.HasKey("coins"))
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + player.GetCoins());
        else
            PlayerPrefs.SetInt("coins", player.GetCoins());
        invPan.SetActive(false);
        GetComponent<Inventory>().RecountItems();
    }
    public void Lose()
    {
        soundEffect.PlayLoseSound();
        Time.timeScale = 0f;
        player.enabled = false;
        loseScreen.SetActive(true);
        invPan.SetActive(false);
        GetComponent<Inventory>().RecountItems();
    }
    public void MenuLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene("Menu");
    }
    public void NextLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
