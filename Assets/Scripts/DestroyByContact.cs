using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
    public GameObject endbossExplosion;
    public int scoreValue;
	private GameController gameController;
    public static DestroyByContact instance;

    void Start()
	{
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
			
        instance = this;
	}

	void OnTriggerEnter(Collider other) 
	{
        if(CompareTag("Bolt"))
        {
            if (other.CompareTag("Endboss"))
            {
                if (GameController.instance.endbossLifepoints <= 50)
                {
                    GameController.instance.endbossLifepoints -= 50;
                    Instantiate(endbossExplosion, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                else
                    GameController.instance.endbossLifepoints -= 50;
                    Destroy(gameObject);
            }
        }

        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Hazard") || other.CompareTag("Endboss"))
		{
			return;
		}  

        if(explosion != null)
            Instantiate(explosion, transform.position, transform.rotation);

            if (other.CompareTag("Player"))
        {
            if (GameController.instance.playerLives == 1)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                GameController.instance.playerLives--;
                gameController.GameOver();
            }
            else
            {
                GameController.instance.playerLives--;
                Destroy(gameObject);
                PlayerController.instance.blink();
                return;
            }
        }
        gameController.AddScore (scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}