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

    int health = 100;
    const float timeLimit = 1.0f; // 1 seconds
    float timeAmount = timeLimit;
    bool timerActive = false;

    //const float txrexh = 7.5f;
    float trex_ext;

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

    private const float regMoveSpeed = 25f;
    private const float minSpeed = 0.001f;
    private float moveSpeed = regMoveSpeed;
    private bool tooClose;


    private const float regTurnSpeed = 5f;
    private float turnSpeed = regTurnSpeed;

	// Use this for initialization
	void Start () {
        
        target = null;
        moveSpeed = regMoveSpeed;
        tooClose = false;
        anim = GetComponent<Animator>();
        state = State.Idling;
        trex_ext = collider.bounds.extents.y;

	}
	


    void Update()
    {
        
        float ty = Terrain.activeTerrain.SampleHeight(new Vector3(transform.position.x, 0, transform.position.z));
        transform.position = new Vector3(transform.position.x, ty + trex_ext, transform.position.z);

        //right click
        if (Input.GetMouseButtonDown(1) && Selected)
        {
            Vector3 tempTarget = Selection.GetWorldPositionAtHeight(Input.mousePosition, 0f);
         
            target = new Vector3(tempTarget.x, ty, tempTarget.z);
        }


        if (target != null)
        {


            //rotation stuff
            //http://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
            Vector3 targetDir = (Vector3)target - transform.position;
            targetDir = new Vector3(targetDir.x,  0, targetDir.z);
            float turnStep = turnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnStep, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);


            //moving stuff
            state = State.Moving;
            //anim.SetBool("moving", true);

            //if arrives at target then { movingTowardsTarget = false; target = null; }
            float step = moveSpeed * Time.deltaTime;
            float distAway = Vector3.Distance(transform.position, (Vector3)target);


            if (distAway < 1 && moveSpeed > minSpeed)
            {
                tooClose = true;
                moveSpeed /= 2;
            }

            transform.position = Vector3.MoveTowards(transform.position, (Vector3)target, step);

     


            if (tooClose)
            {
                state = State.Idling;
                //anim.SetBool("moving", false);
                Start();
                //movingTowardsTarget = true;
            }




        }



        if (timerActive && timeAmount > 0)
        {
            timeAmount -= Time.deltaTime;
        }
        else if (timeAmount <= 0) 
        {
            timerActive = false;
            timeAmount = timeLimit;
        }





    }

    // Update is called once per frame
    void LateUpdate()
    {


    }

    public void Damage()
    {
        if (health - 10 >= 0)
        {
            health -= 10;
        }
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Unit u = other.gameObject.GetComponent<Unit>();

        if (u != null && u.team != team && timeAmount >= timeLimit )
        {
            u.Damage();
            Debug.Log(u.gameObject.name + " health: " + u.health);
            timerActive = true;
        }
    }

}
