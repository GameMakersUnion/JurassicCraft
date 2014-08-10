using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {
	void Awake() {
		Application.targetFrameRate = 60;
	}
}
