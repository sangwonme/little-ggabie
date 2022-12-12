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
    private Vector2 direction;
    // collider
    private BoxCollider boxCollider;
    private SphereCollider sphereCollider;
    // health
    private Health health;

    void setRandomDirection(){
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }

    // set random time left to state change
    void setRandomTimeLeft(){
        stateChangeTimeLeft = Random.Range(1.0f, 5.0f);
    }

    // change Random State
    void setRandomState(){
        // if monster was walk -> set idle
        if(state == "walk"){
            state = "idle";
        }
        // else make walk
        else if(state == "idle"){
            state = "walk";
            setRandomDirection();
        }
    }

    private void walk(){
        animator.SetBool("isWalk", (state == "walk"));
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        if(!timer.isDay && state == "walk"){
            if(direction.x < 0){
                sprite.flipX = false;
            }
            else if(direction.x > 0){
                sprite.flipX = true;
            }
            theRb.velocity = new Vector3(direction.x * xSpeed, 0, direction.y * xSpeed);
        }
        // sleep
        else{
            theRb.velocity = new Vector3(0, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // state
        state = "idle";
        setRandomTimeLeft();
        // walk
        setRandomDirection();
        sprite = GetComponent<SpriteRenderer>();
        theRb = GetComponent<Rigidbody>();
        // collider
        boxCollider = GetComponent<BoxCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        // timer
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        // health
        health = GameObject.Find("GameController").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        // box collider
        boxCollider.enabled = !(timer.isDay);

        // sleep or spawn check
        animator.SetBool("isSpawn", !timer.isDay);
        animator.SetBool("isSleep", timer.isDay);

        // set idle when day
        if(timer.isDay) state = "idle";
        
        // walk
        walk();
        
        // state change
        if(!timer.isDay){
            stateChangeTimeLeft -= Time.deltaTime;
            if(stateChangeTimeLeft < 0){
                setRandomTimeLeft();
                setRandomState();
            }
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
            health.reduceHP();
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && !timer.isDay){
            state = "idle";
            animator.SetBool("isScream", false);
        }
    }
}
