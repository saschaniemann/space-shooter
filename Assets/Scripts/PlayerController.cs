using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	private AudioSource ad;
	private Rigidbody rb;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
    public GameObject jet_flare;
    public GameObject jet_core;
    public Transform shotSpawn;
    public double fireRate;

	private double nextFire;
    public static PlayerController instance;

    void Start()
	{
		ad = GetComponent<AudioSource> ();
		rb = GetComponent <Rigidbody> ();
        instance = this;
	}

	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			ad.Play ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.velocity = movement * speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

    public void blink()
    {
        StartCoroutine(blink2());
    }

    public IEnumerator blink2()
    {
        GetComponent<Collider>().enabled = false;
        jet_flare.GetComponent<ParticleSystem>().Stop();
        jet_core.GetComponent<ParticleSystem>().Stop();
        for (int i = 0; i < 3;i++)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        jet_flare.GetComponent<ParticleSystem>().Play();
        jet_core.GetComponent<ParticleSystem>().Play();
        GetComponent<Collider>().enabled = true;
    }
}