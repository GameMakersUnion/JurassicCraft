using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public GameObject enemies;
	public GameObject friends;

	private int targetUnit;

	// Use this for initialization
	void Start () {
		targetUnit = 0;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform enemy in enemies.transform)
		{
			Vector3 target = Vector3.zero;
			try {
				target = friends.transform.GetChild(0).position;
			}
			catch(UnityException e){
				Engine.loss = true;
			}
			enemy.GetComponent<Unit>().ChooseNewTarget(target);
			Debug.Log(enemy.name);
			
		}
	}


}
