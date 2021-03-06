﻿using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {

	public GameObject enemies;
	public GameObject friends;
	public static bool win;
	public static bool loss;


    public Rect windowRectWin;
    public Rect windowRectLoss;

	void Start(){
        Vector2 distFromEdge = ScrollMap.thickness;
        windowRectWin = new Rect(distFromEdge.x * 2, distFromEdge.y * 2, 120, 50);
        windowRectLoss = new Rect(distFromEdge.x * 2, distFromEdge.y * 2, 120, 50);
	}

	void Update(){
		try {
			if (friends.transform.GetChild(0)==null)
				;
		}
		catch(UnityException e){
			Engine.loss = true;
			Debug.Log("Hello");
		}
		try {
			if (enemies.transform.GetChild(0)==null)
				;
		}
		catch(UnityException e){
			Engine.win = true;
			Debug.Log("Hello");
		}
	}

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void OnGUI() {
		if (win == true){
			windowRectWin = GUI.Window(0, windowRectWin, DoMyWindow, "You have Won");
			Time.timeScale = 0;
		}
		else if (loss == true){
			windowRectLoss = GUI.Window(0, windowRectLoss, DoMyWindow, "You have Lost");
			Time.timeScale = 0;
		}
	}
	
	void DoMyWindow(int windowID) {
		if (GUI.Button(new Rect(10, 20, 100, 20), "Play Again")){
			Time.timeScale = 1;
			Engine.loss = false;
			Engine.win = false;
			Application.LoadLevel("MainScene");
		}
	}

}
