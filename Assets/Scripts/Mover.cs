using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;

	void Start ()
	{
        if(CompareTag("Hazard"))
            GetComponent<Rigidbody>().velocity = (transform.forward * speed * GameController.instance.hazardSpeed)/ GameController.instance.firstHazardSpeed;
        else
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}