using UnityEngine;
using System.Collections;

public enum Team { Saurischians, Ornithischians };


public class Unit : Damagable {

    public enum State { Idling, Moving, Fighting, Eating }
    public State state;

    public bool Selected = false;
    //public bool carrying = false;

    //private bool moving = false;
    public Animator anim;

    //const float timeLimit = 1.0f; // 1 seconds
    //float timeAmount = timeLimit;
    //bool timerActive = false;

    private int timerAttack;
    public static int attackDelay = 30;

    float dinoMidpointHeight;


    //public Vector3? target = null;  //nullable!
    public Vector3 target;
    private const float moveSpeed = 20f;
    private bool tooClose;
    private const float rotateSpeed = 5f;



	// Use this for initialization
    public override void Start()
    {
        base.Start();

        tooClose = false;
        anim = GetComponent<Animator>();
        state = State.Idling;
        dinoMidpointHeight = collider.bounds.size.y/2;
        Debug.Log(team + " : " + dinoMidpointHeight);

        //initialize timer
        timerAttack = attackDelay;



		//
		
        health = 100;
	}


    public override void Update()
    {

        base.Update();
        
        //float ty = Terrain.activeTerrain.SampleHeight(new Vector3(transform.position.x, 0, transform.position.z));
        //transform.position = new Vector3(transform.position.x, ty + dinoMidpointHeight, transform.position.z);
        

        RightClick ();

       if (state == State.Moving) 
       {
			Movement();
	   }

		if (timerAttack > 0) {
				timerAttack--;
		}


    }

	void RightClick ()
	{
		//right click
		if (Input.GetMouseButtonDown (1) && Selected) {
			Vector3 tempTarget = Selection.GetWorldPositionAtHeight (Input.mousePosition, 0f);

			ChooseNewTarget (tempTarget);
		}
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

        controller.SimpleMove(moveSpeed * (target - transform.position).normalized);
        //Debug.Log(team.ToString() + "moving");
	    //rotation stuff
	    //http://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
	    Vector3 targetDir = (Vector3)target - transform.position;
	    targetDir = new Vector3(targetDir.x,  0, targetDir.z);
	    float turnStep = rotateSpeed * Time.deltaTime;
	    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnStep, 0.0F);
	    Debug.DrawRay(transform.position, newDir, Color.red);
	    transform.rotation = Quaternion.LookRotation(newDir);

	}



    void OnTriggerStay(Collider other)
    {	
        Damagable d = other.gameObject.GetComponent<Damagable>();
        if (d != null && d.team != team && timerAttack == 0 )
        {
            d.Damage();
            Debug.Log(d.gameObject.name + " health: " + d.health);
			timerAttack=attackDelay;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        Debug.Log("COLLIDE OTHER: " + other.gameObject.name);
    }

}
