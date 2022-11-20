using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public delegate void RadialAttack(Vector3 targetPosition);
	public static RadialAttack OnRadialAttack;

	public delegate void AngularAttack(Vector3 targetPosition);
	public static AngularAttack OnAngularAttack;

	Vector2 initPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) initPosition = Input.mousePosition;

		if (Input.GetMouseButtonUp (0)) {
			if(Vector2.Distance (initPosition,Input.mousePosition)>5.0f) OnAngularAttack(Input.mousePosition);
            else OnRadialAttack(Input.mousePosition);
        }


	}
}
