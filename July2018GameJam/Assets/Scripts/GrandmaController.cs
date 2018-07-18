using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    private Rigidbody2D Rigidbody;

    public float BaseSpeed = 5; //public variables are exposed in the Unity inspector.

    private static GameObject granny;

    public static GameObject GetInstance()
    {
        return granny;
    }

	// Use this for initialization
	void Start ()
	{
	    granny = this.gameObject;
	    Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    float GetSpeed() //exists so it's easy to add movement slowing effect later.
    {
        return BaseSpeed; 
    }


	// Update is called once per frame
	void Update () {
	    float xInput = 0;
	    float yInput = 0;

	    yInput = Input.GetAxisRaw("Vertical");
	    xInput = Input.GetAxisRaw("Horizontal");

        Rigidbody.velocity = new Vector2(xInput, yInput);
    }
}
