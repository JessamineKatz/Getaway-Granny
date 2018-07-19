using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class VisionCone : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    private FOVDraw fov;
    public float rotSpeed;

	// Use this for initialization
	void Start ()
	{
	    fov = GetComponent<FOVDraw>();

	}

    private float LastVisionCheck = -100;

    private float VisionCheckInterval = 0.2f;
    // Update is called once per frame

    private bool GrannyVisible = false;

    public bool CanSeePlayer()
    {
        return GrannyVisible;
    }


    void LateUpdate()
    {
        
    }

    void Update ()
	{
        //check visibility
	    if (Time.time - LastVisionCheck > VisionCheckInterval)
	    {
	        GrannyVisible = false;
	        LastVisionCheck = Time.time;

	        Collider2D[] hits = Physics2D.OverlapCircleAll((Vector2) this.transform.position, fov.viewRadius);

	        foreach (Collider2D hit in hits)
	        {
	            int mask = LayerMask.GetMask("Walls", "Interactables");

                RaycastHit2D directLine = Physics2D.Raycast(this.transform.position, hit.transform.position - this.transform.position,
	                mask );

	            if (!directLine)
	            {

	                continue;
                }
	            else
	            {
	                Debug.Log(hit.name + ": " + (bool) directLine);
	            }
	            if (hit.GetComponent<GrandmaController>() != null)
	            {


	                Vector2 guardToPlayer = (Vector2) (hit.transform.position - this.transform.position);

	                float guardToPlayerAngle = Vector2.Angle(guardToPlayer, Vector2.right);

	                float guardAngle = this.transform.rotation.eulerAngles.z;

                    


	                if (Mathf.Abs(guardToPlayerAngle - guardAngle) <= fov.viewAngle && guardToPlayer.magnitude < fov.viewRadius)
	                {
	                    GrannyVisible = true;
                    }
	                

	                if (GrannyVisible) Debug.Log("I can see Granny!");
                }
	            //other detectable objects go here
	        }

	    }
        
	}
}
