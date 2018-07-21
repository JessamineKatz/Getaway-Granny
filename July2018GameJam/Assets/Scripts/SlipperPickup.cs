using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GrandmaController granny = other.GetComponent<GrandmaController>();

        if (granny != null)
        {
            Destroy(this.gameObject);
            granny.slipperCount++;
        }
    }

}
