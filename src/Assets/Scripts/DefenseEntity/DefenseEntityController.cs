using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEntityController : MonoBehaviour {

	bool health;

	// Use this for initialization
	void Start () {
		health = true;
		int side = UnityEngine.Random.Range (0, 1);
		if (side == 0) transform.localScale = new Vector3 (-1.0f,1.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("OnTriggerEnter2D Enemy");
        if (collider.gameObject.tag == "Enemy")
        {
            health = false;
            transform.gameObject.SetActive(false);
        }
	}
}
