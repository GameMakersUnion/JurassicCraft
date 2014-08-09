using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenColliders : MonoBehaviour {

    Vector3 size;
    Vector3 pos;

	// Use this for initialization
	void Start () {
        size = this.GetComponent<Terrain>().terrainData.size;
        pos = transform.position;

        Vector2 colliderSize = new Vector2(10f, 100f);
        

        //warning: these are limited only to terrains at the origin, otherwise the positions are incorrect
        List<BoxCollider> bc = new List<BoxCollider>();

        //z-axis
        bc.Add(this.gameObject.AddComponent<BoxCollider>());
        bc[bc.Count - 1].center = new Vector3((size.x - pos.x) / 2, (size.y - pos.y), size.z + colliderSize.x / 2);
        bc[bc.Count - 1].size = new Vector3(size.x + colliderSize.x * 2, colliderSize.y, colliderSize.x);

        bc.Add(this.gameObject.AddComponent<BoxCollider>());
        bc[bc.Count - 1].center = new Vector3((size.x - pos.x) / 2, (size.y - pos.y), pos.z + colliderSize.x / 2);
        bc[bc.Count - 1].size = new Vector3(size.x + colliderSize.x * 2, colliderSize.y, colliderSize.x);

        //x-axis
        bc.Add(this.gameObject.AddComponent<BoxCollider>());
        bc[bc.Count - 1].center = new Vector3(size.x + colliderSize.x / 2, (size.y - pos.y), (size.z - pos.z) / 2);    
        bc[bc.Count - 1].size = new Vector3(colliderSize.x, colliderSize.y, size.z + colliderSize.x * 2);

        bc.Add(this.gameObject.AddComponent<BoxCollider>());
        bc[bc.Count - 1].center = new Vector3(pos.x - colliderSize.x / 2, (size.y - pos.y), (size.z - pos.z) / 2);  
        bc[bc.Count - 1].size = new Vector3(colliderSize.x, colliderSize.y, size.z + colliderSize.x * 2);

        //bc.bounds.center = size;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
