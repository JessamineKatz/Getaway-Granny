using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectOnClick : MonoBehaviour {

    public GameObject prefab;
    public Text slipperCountText;

    private int slipperCount = 0;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && slipperCount > 0)
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            Instantiate(prefab, new Vector3(p.x, p.y, 0.0f), Quaternion.identity);
            slipperCount--;
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            slipperCount++;
        }

        slipperCountText.GetComponent<Text>().text = "Slipper count: " + slipperCount;

    }
}
