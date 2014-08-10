using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1)) {
			Application.LoadLevel("MainScene");		
		}
	}
}
