using UnityEngine;
using System.Collections;

public enum Team { Saurischians, Ornithischians };


public class Unit : MonoBehaviour {

    public enum State { Idling, Moving, Fighting, Eating }
    public State state;

    public Team team;
    public bool Selected = false;
    //public bool carrying = false;

    //private bool moving = false;
    public Animator anim;

    public int health = 100;
    //const float timeLimit = 1.0f; // 1 seconds
    //float timeAmount = timeLimit;
    //bool timerActive = false;

    private float timerAttack;
    public static float attackDelayTime = .2f;

    //const float txrexh = 7.5f;
    float dinoMidpointHeight;


    //public Vector3? target = null;  //nullable!
    public Vector3 target;
    private const float moveSpeed = 20f;
    private bool tooClose;
    private const float rotateSpeed = 5f;

	// Use this for initialization
	void Start () {
        tooClose = false;
        anim = GetComponent<Animator>();
        state = State.Idling;
        dinoMidpointHeight = collider.bounds.size.y/2;
        Debug.Log(team + " : " + dinoMidpointHeight);

        //initialize timer
        timerAttack = Time.time;

	}


    void FixedUpdate()
    {
//		//timer ticks
//		float deltaTick = Time.time - timerAttack;
//		if (deltaTick > attackDelayTime){
//			timerAttack += deltaTick;
//			Damage();
//		}
    }


    void Update()
    {
        
        //float ty = Terrain.activeTerrain.SampleHeight(new Vector3(transform.position.x, 0, transform.position.z));
        //transform.position = new Vector3(transform.position.x, ty + dinoMidpointHeight, transform.position.z);

        RightClick ();

       if (state == State.Moving) 
       {
			Movement();
	   }
//
//            if (tooClose)
//            {
//                state = State.Idling;
//                //anim.SetBool("moving", false);
//                Start();
//                //movingTowardsTarget = true;
//            }



        //if (timerActive && timeAmount > 0)
        //{
        //    timeAmount -= Time.deltaTime;
        //}
        //else if (timeAmount <= 0) 
        //{
        //    timerActive = false;
        //    timeAmount = timeLimit;
        //}
    }

	void RightClick ()
	{
		//right click
		if (Input.GetMouseButtonDown (1) && Selected) {
			Vector3 tempTarget = Selection.GetWorldPositionAtHeight (Input.mousePosition, 0f);

			ChooseNewTarget (tempTarget);
		}
	}

    // Update is called once per frame
    void LateUpdate()
    {


    }

	public void ChooseNewTarget (Vector3 tempTarget)
	{
		target = new Vector3 (tempTarget.x, 0, tempTarget.z);
		state = State.Moving;
	}

	void Movement ()
	{	
        CharacterController controller = GetComponent<CharacterController>();


		//animation moving stuff

		//anim.SetBool("moving", true);
		//if arrives at target then { movingTowardsTarget = false; target = null; }
		/*float distAway = Vector3.Distance (transform.position, (Vector3)target);
		if (distAway < 1 && moveSpeed > minSpeed) {
			tooClose = true;
			moveSpeed /= 2;
		}*/

		//controller.SimpleMove (moveSpeed * (target - transform.position));
        controller.SimpleMove(moveSpeed * (target - transform.position).normalized);
        Debug.Log(team.ToString() + "moving");
	    //rotation stuff
	    //http://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
	    Vector3 targetDir = (Vector3)target - transform.position;
	    targetDir = new Vector3(targetDir.x,  0, targetDir.z);
	    float turnStep = rotateSpeed * Time.deltaTime;
	    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnStep, 0.0F);
	    Debug.DrawRay(transform.position, newDir, Color.red);
	    transform.rotation = Quaternion.LookRotation(newDir);

	}

    public void Damage()
    {
        if (health - 10 >= 0)
        {
            health -= 10;
        }
        
    }

    void OnTriggerStay(Collider other)
    {	
        Unit u = other.gameObject.GetComponent<Unit>();

        if (u != null && u.team != team )
        {
            u.Damage();
            Debug.Log(u.gameObject.name + " health: " + u.health);
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        Debug.Log("COLLIDE OTHER: " + other.gameObject.name);
    }

}
