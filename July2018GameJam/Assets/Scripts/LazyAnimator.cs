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
	    if (rigidbody.velocity.x > 0)
	    {
	        renderer.sprite = right;
        }

	    if (rigidbody.velocity.x < 0)
	    {
	        renderer.sprite = left;
	    }

	    if (rigidbody.velocity.y > 0)
	    {
	        renderer.sprite = up;
	    }

	    if (rigidbody.velocity.y < 0)
	    {
	        renderer.sprite = down;
	    }
	}
}
