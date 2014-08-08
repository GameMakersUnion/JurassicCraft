using UnityEngine;
using System.Collections;


public enum GarbageType { Bone,Pizza,Banana };


public class GarbagePiece : MonoBehaviour {

    public GarbageType garbage;
    public const int points = 1;
    public bool needsMining = false;  //for mining garbage cans and maybe stealing from other bases' HQ's.


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Unit u = other.gameObject.GetComponent<Unit>();
        //if (u == null)
        //{
        //    throw new System.NullReferenceException("u");
        //} else 
        if (u != null && !u.carrying && !needsMining)
        {
          //  Transform Mouth = other.transform.Find("Head").Find("Mouth").transform;
          //  transform.position = Mouth.position;
          //  transform.parent = Mouth;
          //  u.garbage = this;
            XferGarbageOwner(transform, other.transform);
        }
         
    }

    //transfer
    void XferGarbageOwner(Transform from, Transform to)
    {
        Transform toMouth = to.transform.Find("Head").Find("Mouth").transform;
        from.position = toMouth.position;
        from.parent = toMouth;
        to.gameObject.GetComponent<Unit>().garbage = from.gameObject.GetComponent<GarbagePiece>();
        //u.garbage = this;
    }
}
