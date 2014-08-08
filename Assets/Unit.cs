using UnityEngine;
using System.Collections;

public enum Team { Racoons, Cats };


public class Unit : MonoBehaviour {
    
    public enum State { Idling, Moving, Mining, Hissing, Fighting }
    public State state;

    public Team team;
    public bool Selected = false;
    //public bool carrying = false;

    //private bool moving = false;
    public Animator anim;
    public GarbagePiece garbage;



    public bool carrying
    {
        get
        {
            return (garbage != null);
            //return (garbage != null) ? true : false;
            //if (garbage != null) return true;
            //else return false;
        }
    }

    //public Vector3? target = null;  //nullable!
    public Vector3? target;
    //private bool movingTowardsTarget = false;

    private const float regSpeed = 5f;
    private const float minSpeed = 0.001f;
    private float speed = regSpeed;
    private bool tooClose;

	// Use this for initialization
	void Start () {
        
        target = null;
        speed = regSpeed;
        tooClose = false;
        anim = GetComponent<Animator>();
        state = State.Idling;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (target != null)
        {
            state = State.Moving;
            anim.SetBool("moving", true);

            //if arrives at target then { movingTowardsTarget = false; target = null; }
            float step = speed * Time.deltaTime;
            float distAway = Vector3.Distance(transform.position, (Vector3)target);


            if (distAway < 1 && speed > minSpeed)
            {
                tooClose = true;
                speed /= 2;
            }

            transform.position = Vector3.MoveTowards(transform.position, (Vector3)target, step);
            
            if (tooClose)
            {
                state = State.Idling;
                anim.SetBool("moving", false);
                Start();
                //movingTowardsTarget = true;
            }

        }
	}

    void Update()
    {
        //right click
        if (Input.GetMouseButtonDown(1) && Selected)
        {
            Vector3 tempTarget = Selection.GetWorldPositionAtDepth(Input.mousePosition, 0f);
            target = new Vector3(tempTarget.x, tempTarget.y, 0);
         }
    }

}
