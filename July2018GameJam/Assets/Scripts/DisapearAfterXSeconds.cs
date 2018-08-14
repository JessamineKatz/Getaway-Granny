using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearAfterXSeconds : MonoBehaviour {

    public int destroyTime = 5;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(WaitThenDie());
	}
	
	IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
