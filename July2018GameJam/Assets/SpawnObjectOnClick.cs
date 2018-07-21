using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectOnClick : MonoBehaviour {

    public GameObject prefab;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            Instantiate(prefab, new Vector3(p.x, p.y, 0.0f), Quaternion.identity);
        }
	}
}
