using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bin : MonoBehaviour //<T> : IEnumerable<T> where T : MonoBehaviour
{

    public bool usesSeparateLid;

    enum BinState { Open, Sealed }
    private BinState state;

    private bool isUpright;

    private Unit miner;


    private Timer timeCounter;

    private TimeSpan timePryLid;
    private TimeSpan timeDigTrashOut;

    public GameObject garbagesGO;
    public GameObject lid;

    private List<GarbagePiece> garbages;


	// Use this for initialization
	void Start () {
        state = BinState.Sealed;
        isUpright = true;
        //hasMiner = false;
        timePryLid = new TimeSpan(2 * Timer.tickSize);
        timeDigTrashOut = new TimeSpan(2 * Timer.tickSize);
        garbages = new List<GarbagePiece>();


        foreach (Transform gChild in garbagesGO.transform)
        {
            garbages.Add(gChild.transform.GetComponent<GarbagePiece>());
        }

	}
	
	// Update is called once per frame
	void Update () {

        if ( state == BinState.Sealed && timeCounter != null && timeCounter.time >= timePryLid)
        {
            lid.SetActive(false);
            state = BinState.Open;

            //reset & reuse same timer.. blah whatever
            timeCounter = new Timer();
        }

        else if ( state == BinState.Open && timeCounter.time >= timeDigTrashOut && garbages.Count > 0) //garbagesGO.transform.childCount
        {
            TakeGarbageOut();

            //reset & reuse same timer.. blah whatever
            timeCounter = new Timer();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Unit u = other.gameObject.GetComponent<Unit>();
            if (u != null && miner == null )
            {
                
                if (u.state == Unit.State.Idling || u.state == Unit.State.Moving)
                {
                    BeginsMining(u);

                    if (u.garbage != null) //has garbage, drops it to mine 
                    {
                        XferGarbageOwner(other.transform, null);
                    }

                }

                

            }
            //else
            //{
            //    Debug.Log("I'm not sure if this triggers an NRE, but if it does, and I'd like to know why");
            //}

        }

        //carried food gets dropped
    }


    //transfer owner
    void XferGarbageOwner(Transform from, Transform to)
    {
        Unit u = from.gameObject.GetComponent<Unit>();
        Bin b = from.gameObject.GetComponent<Bin>();
        GarbagePiece g = null;

        if (u != null)
        {
            u.garbage = null;    //due to BeginsMining
            g = from.gameObject.GetComponent<Unit>().garbage;
        }
        else if (b != null)
        {
            g = garbages[0];
            b.garbages.Remove(garbages[0]); //due to TakeGarbageOut
        }

        if (g != null && g.needsMining) { 
            g.needsMining = false;
            g.renderer.sortingOrder += 2;
        }

        from.parent = to; //effectively should just drop it where it is (since to is null in Bin)

    }


    void TakeGarbageOut()
    {
        //GarbagePiece g=null;//dumbbutok
        //foreach (Transform gChild in garbagesGO.transform)//dumbbutok
        //{//dumbbutok
        //    g = gChild.transform.GetComponent<GarbagePiece>();//dumbbutok
        //    break;//dumbbutok
        //}//dumbbutok

        GarbagePiece g = garbages[0];

        if (g != null)//dumbbutok

            //release a single garbage
            XferGarbageOwner(g.transform, null);

        //reset & reuse same timer.. blah whatever
        timeCounter = new Timer();
    }

    void BeginsMining(Unit u)
    {
        Debug.Log("i should mine");
        //begins mining

        miner = u;
        u.state = Unit.State.Mining;
        u.Selected = false;
        u.transform.parent = null; //temporary hack until Selection is integrated
        u.anim.SetBool("moving", true); //this is simulating mining, an animation not discussed.
        u.target = transform.position;
        //miner.transform.rotation =  LATER
        timeCounter = new Timer();


    }

}
