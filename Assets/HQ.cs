using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HQ : MonoBehaviour {

    public Team team;
    int points;


	// Use this for initialization
	void Start () {
        points = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        Unit u = other.gameObject.GetComponent<Unit>();

        if (u != null && u.team == team)
        {
            XferGarbageOwner(other.transform, transform);
        }

        
    }

    //transfer owner
    void XferGarbageOwner(Transform from, Transform to)
    {
        Unit uFrom = from.gameObject.GetComponent<Unit>();
        HQ uTo = to.gameObject.GetComponent<HQ>();
        Vector3 offset = new Vector3(Random.Range(2f, -2f), Random.Range(2f, -2f), 0f); 
    }
    
}
