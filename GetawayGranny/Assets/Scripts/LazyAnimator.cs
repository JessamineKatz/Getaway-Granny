using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyAnimator : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer renderer;
    public Sprite up;
    public Sprite left;
    public Sprite right;
    public Sprite down;


	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();
	    renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        

	    if (Input.GetAxisRaw("Horizontal") > 0)
	    {
	        renderer.sprite = right;
        }

	    if (Input.GetAxisRaw("Horizontal") < 0)
	    {
	        renderer.sprite = left;
	    }

	    if (Input.GetAxisRaw("Vertical") > 0)
	    {
	        renderer.sprite = up;
	    }

	    if (Input.GetAxisRaw("Vertical") < 0)
	    {
	        renderer.sprite = down;
	    }
	}
}
