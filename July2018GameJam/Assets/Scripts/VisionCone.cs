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
	            Debug.Log(mask);
                RaycastHit2D directLine = Physics2D.Raycast(this.transform.position, hit.transform.position - this.transform.position, fov.viewRadius,
	                mask );
	            if (directLine.collider == null) continue;
	            if (directLine.transform.gameObject.GetComponent<GrandmaController>() != null)
	            {
	                Vector2 guardToPlayer = (Vector2) (hit.transform.position - this.transform.position);

	                float guardToPlayerAngle = Vector2.SignedAngle(guardToPlayer, Vector2.right);

	                float guardAngle = this.transform.rotation.eulerAngles.z;

	                if (Math.Abs(FOVDraw.AngleDifference(guardAngle, guardToPlayerAngle)) <= fov.viewAngle / 2 && guardToPlayer.magnitude < fov.viewRadius)
	                {
	                    GrannyVisible = true;
                    }
	               
                }
	            //other detectable objects go here
	        }

	    }
        
	}
}
