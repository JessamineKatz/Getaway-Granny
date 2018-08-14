using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<GrandmaController>() != null)
        {
            SceneManager.LoadScene("WonScene");
        }
    }
}
