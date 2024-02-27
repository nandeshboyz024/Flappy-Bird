using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{   
    public Player player;
    public GameObject scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject creater;
    public GameObject bronze;
    public GameObject silver;
    public GameObject gold;
    public GameObject instruction;

    private int score = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        instruction.SetActive(false);
        bronze.SetActive(false);
        silver.SetActive(false);
        gold.SetActive(false);

        player.audioSource.clip = player.play;
        player.audioSource.Play();

        score = 0;

        playButton.SetActive(false);
        gameOver.SetActive(false);
        creater.SetActive(false);
        
        scoreText.GetComponent<TextMeshProUGUI>().text=score.ToString();
        
        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        foreach (Pipes pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
    }

    public void Pause()
    {   
        player.audioSource.clip=player.pause;
        player.audioSource.Play();
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        if (score >= 20) { gold.SetActive(true); }
        else if(score >=10) { silver.SetActive(true); }
        else if(score >=1) { bronze.SetActive(true); }
        gameOver.SetActive(true);
        creater.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

}
