using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject hazard1;
    public GameObject hazard2;
    public GameObject hazard3;
    public GameObject enemy;
    public GameObject endboss;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float hazardSpeed;
    public float firstHazardSpeed;
    public float playerLives;
    public float wavesUntilEndboss;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
	public GUIText Waves;
    public GUIText endbossText;

    private int wavesCounted;
    private bool gameOver;
    private bool restart;
    public int points;
    public float endbossLifepoints;

    public static GameController instance;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
		Waves.text = "";
        endbossText.text = "";
        UpdateScore();
        StartCoroutine(SpawnWaves());
        instance = this;
		wavesCounted = 1;
        firstHazardSpeed = hazardSpeed;
}

    void Update()
    {
        if (restart)
        {
            restartText.text = "Press 'R' for Restart";
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (!gameOver)
            restartText.text = "Lives: " + playerLives;
        else
        {
            restart = true;
        }

        if (wavesCounted == wavesUntilEndboss)
        {
            Instantiate(endboss, new Vector3(0, 0, 12), endboss.transform.rotation);
            wavesCounted++;
        }

        if (wavesCounted == wavesUntilEndboss+1)
        {
            endbossText.text = "Endboss-Lifepoints: " + endbossLifepoints;
            if (endbossLifepoints <= 0)
            {
                endbossText.text = "Endboss-Lifepoints: " + endbossLifepoints;
                gameOverText.text = "You won!";
                restart = true;
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
				int j = Random.Range(0, 4);
                if (j == 0) Instantiate(hazard1, spawnPosition, spawnRotation);
                if (j == 1) Instantiate(hazard2, spawnPosition, spawnRotation);
				if (j == 2) Instantiate(hazard3, spawnPosition, spawnRotation);
                if (j == 3) Instantiate(enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

			yield return new WaitForSeconds(waveWait);

            wavesCounted++;
            hazardSpeed += 2;
            if (wavesCounted % 3 == 0)
            {
                hazardCount++;
            }

            if (wavesCounted == wavesUntilEndboss)
            {
                Waves.text = "Last Stage: Endboss!";
                yield return new WaitForSeconds(2);
                Waves.text = "";
                break;
            }

            if (gameOver)
            {
                break;
            }

            Waves.text = "Wave " + (wavesCounted) + "!";
			yield return new WaitForSeconds (2);
			Waves.text = "";
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