using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HQ : Damagable {

    //public Team team;
    public GameObject allUnits; //on this team

    int points;
    const int spawnMax = 20;
    int spawned = 0;
    
	// Use this for initialization
	public override void Start () {

        base.Start();

        points = 0;
        if (allUnits != null)
        {
            foreach (Transform unit in allUnits.transform)
            {

            }
        }
        else
        {
            Debug.LogWarning("Nothing attached to variable 'allUnits' in " + this.name +". Please fix.");
        }

        health = 100;


	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        Unit u = other.gameObject.GetComponent<Unit>();

        if (u != null && u.team == team)
        {

        }

        
    }


    
}
