using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBody : MonoBehaviour
{
    public Animator animator;
    public float xSpeed;
    private Timer timer;
    // state change
    public string state;
    float stateChangeTimeLeft = 0.0f;
    float timeCount = 0.0f;
    // walk
    private SpriteRenderer sprite;
    private Rigidbody theRb;
    // collider
    private BoxCollider boxCollider;
    private SphereCollider sphereCollider;
    // monster Light
    public GameObject monsterLight;

    // set random time left to state change
    void setRandomTimeLeft(){
        stateChangeTimeLeft = Random.Range(3.0f, 5.0f);
    }

    // change Random State
    void setRandomState(){
        // if monster was moving left or right -> set idle
        if(state == "left" || state == "right"){
            state = "idle";
        }
        // else choose random direction
        else if(state == "idle"){
            float tmp = Random.Range(0.0f, 1.0f);
            if(tmp < 0.5f){
                state = "left";
            }
            else{
                state = "right";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // state
        state = "idle";
        setRandomTimeLeft();
        // walk
        sprite = GetComponent<SpriteRenderer>();
        theRb = GetComponent<Rigidbody>();
        // collider
        boxCollider = GetComponent<BoxCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        // timer
        timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        // dead
        if(state == "dead"){
            boxCollider.enabled = false;
            sphereCollider.enabled = false;
            sprite.enabled = false;
        }

        // box collider
        boxCollider.enabled = !(timer.isDay);

        // sleep or spawn check
        animator.SetBool("isSpawn", !timer.isDay);
        animator.SetBool("isSleep", timer.isDay);

        // set idle when day
        if(timer.isDay) state = "idle";
        
        // walk
        animator.SetBool("isWalk", (state == "left" || state == "right"));
        if(!timer.isDay){
            if(state == "left"){
                sprite.flipX = false;
                theRb.velocity = new Vector3(-xSpeed, 0, 0);
            }
            else if(state == "right"){
                sprite.flipX = true;
                theRb.velocity = new Vector3(xSpeed, 0, 0);
            }
            else{
                theRb.velocity = new Vector3(0, 0, 0);
            }
        }else{
            theRb.velocity = new Vector3(0, 0, 0);
        }
        
        // state change
        if(!timer.isDay){
            stateChangeTimeLeft -= Time.deltaTime;
            if(stateChangeTimeLeft < 0){
                setRandomTimeLeft();
                setRandomState();
            }
        }

        // light
        monsterLight.transform.position = new Vector3(transform.position.x, transform.position.y-1.02f, transform.position.z-1.23f);
        if(!timer.isDay && monsterLight.GetComponent<Light>().intensity < 2.0f){
            monsterLight.GetComponent<Light>().intensity += 0.05f;
        }
        else if(timer.isDay && monsterLight.GetComponent<Light>().intensity > 0.0f){
            monsterLight.GetComponent<Light>().intensity -= 0.05f;
        }
    }

    // scream
    private void OnTriggerEnter(Collider other) {
        // scream
        if(other.tag == "Player" && !timer.isDay){
            state = "scream";
            animator.SetBool("isScream", true);
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player" && !timer.isDay){
            state = "scream";
            animator.SetBool("isScream", true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && !timer.isDay){
            state = "idle";
            animator.SetBool("isScream", false);
        }
    }
}
