using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody theRB;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public int jumpPower;
    public float moveSpeed;
    private Vector2 moveInput;
    private BoxCollider col;
    private bool isWorking;
    private bool isPlaying;
    private bool isWalking;


    // Start is called before the first frame update
    void Start()
    {
        isWorking = false;
        isPlaying = false;
        isWalking = false;
    }


    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        if(Input.GetKeyDown(KeyCode.Z)){
            isWorking = !isWorking;
            isPlaying = false;
            isWalking = false;
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            isPlaying = !isPlaying;
            isWorking = false;
            isWalking = false;
        }
        else if(!isWorking && !isPlaying){
            // move
            theRB.velocity = new Vector3(moveInput.x * moveSpeed, theRB.velocity.y, moveInput.y * moveSpeed);

            // sprite direction
            if(moveInput.x > 0){
                spriteRenderer.flipX = true;
            }
            else if(moveInput.x < 0){
                spriteRenderer.flipX = false;
            }

            // animator
            isWalking = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
        }

        // update anim params
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isPlaying", isPlaying);
        animator.SetBool("isWorking", isWorking);

    }
}
