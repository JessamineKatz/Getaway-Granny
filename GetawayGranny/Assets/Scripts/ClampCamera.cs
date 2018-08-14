using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampCamera : MonoBehaviour
{


    public float minX, maxX, minY, maxY;
    public Camera cam;
    


	// Use this for initialization
	void Start () {
	    
    }
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    cam.transform.localPosition = new Vector3(0,0,-20);

	    float height = 2f * cam.orthographicSize / 2;
	    float width = height * cam.aspect;

	    Vector3 camLoc = cam.transform.position;
	    cam.transform.position = new Vector3(Mathf.Clamp(camLoc.x, minX + width, maxX - width), Mathf.Clamp(camLoc.y, minY + height, maxY - height), camLoc.z);

	}
}
