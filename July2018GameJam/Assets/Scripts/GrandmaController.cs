using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaController : MonoBehaviour
{
    private Rigidbody2D Rigidbody;

    public float BaseSpeed = 5; //public variables are exposed in the Unity inspector.

    private static GameObject granny;

    public GameObject sliperPrefab;
    public Text slipperCountText;

    public int slipperCount = 0;

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


        if (Input.GetButtonDown("Fire1") && slipperCount > 0)
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            Instantiate(sliperPrefab, new Vector3(p.x, p.y, 0.0f), Quaternion.identity);
            slipperCount--;
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            slipperCount++;
        }

        slipperCountText.GetComponent<Text>().text = "Slipper count: " + slipperCount;
    }

    
}
