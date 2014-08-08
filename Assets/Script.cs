using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(transform.position);
        Debug.Log(transform.name);

        GameObject go = Resources.Load<GameObject>("Trex");
        Instantiate(go, this.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
