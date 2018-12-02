using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public TextMeshProUGUI DeathText;

    public RectTransform InGame;
    public RectTransform DeathScreen;

    private Player _player;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void OnDeath()
    {
        SetDeathText(true, _player.NumSaved, _player.NumKilled);
        InGame.gameObject.SetActive(false);
        DeathScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnEscape()
    {
        SetDeathText(false, _player.NumSaved, _player.NumKilled);
        InGame.gameObject.SetActive(false);
        DeathScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    

    public void SetDeathText(bool sacrificedHimself, int rescued, int killed)
    {
        string text;
        if (sacrificedHimself)
        {
            if (rescued > 0)
            {
                text = "You sacrificed yourself \n" +
                       "to rescue " + rescued + " lifes";
            }
            else
            {
                text = "You sacrificed yourself \n" +
                       "but didn't save any lifes";
            }
        }
        else
        {
            if (killed == 0)
            {
                text = "You managed to escape and\n" +
                       "save everyone!\n" +
                       "Congratulations";
            }
            else
            {
                text = "You fled and sacrificed\n" +
                       killed + " lifes. You monster!";
            }
        }

        DeathText.text = text;
    }
}
