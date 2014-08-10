using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public GameObject enemies;
	public GameObject friends;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform enemy in enemies.transform)
		{
			Vector3 target = Vector3.zero;

			target = friends.transform.GetChild(1).position;

			enemy.GetComponent<Unit>().ChooseNewTarget(target);
			Debug.Log(enemy.name);
			
		}
	}
}
