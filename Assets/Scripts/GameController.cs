using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject hazard1;
    public GameObject hazard2;
    public GameObject hazard3;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
	public GUIText Waves;

    private int wavesCounted;
    private bool gameOver;
    private bool restart;
    public int points;
	public int EnemiesLives;

    private DestroyByContact destroyByContact;
    public static GameController instance;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
		Waves.text = "";
        points = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        instance = this;
		wavesCounted = 1;
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
		yield return new WaitForSeconds(startWait);
		Waves.text = "Wave " + (wavesCounted) + "!";
		yield return new WaitForSeconds(2);
		Waves.text = "";

        while (true)
        {
			
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
				int j = Random.Range(0, 3);
                if (j == 0) Instantiate(hazard1, spawnPosition, spawnRotation);
                if (j == 1) Instantiate(hazard2, spawnPosition, spawnRotation);
				if (j == 2) Instantiate(hazard3, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

			wavesCounted++;
			yield return new WaitForSeconds(waveWait);

			Waves.text = "Wave " + (wavesCounted) + "!";
			yield return new WaitForSeconds (2);
			Waves.text = "";
            
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        points += newScoreValue;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Points: " + points;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}