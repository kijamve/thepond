using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausticAnimation : MonoBehaviour {

    public Sprite[] images;

    public float duration;

    private float elapsedTime;

    private SpriteRenderer spriteRender;
	// Use this for initialization
	void Start () {
        elapsedTime = 0.0f;

        spriteRender = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        int frame = (int)((elapsedTime / duration) * (float)images.Length);
        elapsedTime += Time.deltaTime;
        
        spriteRender.sprite = images[frame];
        if (elapsedTime >= duration) { 
            elapsedTime = duration - elapsedTime;
        }

    }
}
