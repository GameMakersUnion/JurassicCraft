using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HQ : MonoBehaviour {

    public Team team;
    int points;

    private List<GarbagePiece> garbages;

	// Use this for initialization
	void Start () {
        points = 0;
        garbages = new List<GarbagePiece>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        Unit u = other.gameObject.GetComponent<Unit>();

        if (u != null && u.carrying && u.team == team)
        {
            //Vector3 offset = new Vector3(Random.Range(2f, -2f), Random.Range(2f, -2f), 0f); 
            //u.garbage.transform.position = transform.position + offset;
            //u.garbage.transform.parent = transform;
            //GarbagePiece g = u.garbage;
            //g.needsMining = true;
            //u.garbage = null;
            //points += g.points;

            XferGarbageOwner(other.transform, transform);
        }

        
    }

    //transfer owner
    void XferGarbageOwner(Transform from, Transform to)
    {
        Unit uFrom = from.gameObject.GetComponent<Unit>();
        HQ uTo = to.gameObject.GetComponent<HQ>();
        Vector3 offset = new Vector3(Random.Range(2f, -2f), Random.Range(2f, -2f), 0f); 
        uFrom.garbage.transform.position = uTo.transform.position + offset;
        uFrom.garbage.transform.parent = uTo.transform;
        GarbagePiece g = uFrom.garbage;
        g.needsMining = true;
        uFrom.garbage = null;
        points += GarbagePiece.points;
        uTo.garbages.Add(g);
    }
    
}
