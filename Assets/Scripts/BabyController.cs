using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public Animator animator;
    public MissionGenerator missionGenerator;
    public int babyIdx;
    public string state;
    public string mission;


    // anim
    private bool isCry;
    private bool isEat;
    private bool isPlay;
    private bool isSleep;
    private bool isSoul;


    public void morphSoul(){
        state = "soul";
    }
    // Start is called before the first frame update
    void Start()
    {
        // state & mission
        state = "idle";

        // anim
        isCry = false;
        isEat = false;
        isPlay = false;
        isSleep = false;
        isSoul = false;
    }

    // Update is called once per frame
    void Update()
    {
        // update current mission
        mission = missionGenerator.getPrioryMission(babyIdx);

        // set cry
        if(mission != "none" && state == "idle"){
            state = "cry";
        }

        // state animation
        isCry = (state == "cry");
        isEat = (state == "eat");
        isPlay = (state == "play");
        isSleep = (state == "sleep");
        isSoul = (state == "soul");

        switch(state){
            case "cry":
                break;
            case "eat":
                break;
            case "play":

                break;
            case "sleep":

                break;
            case "soul":
                break;
            default:
                break;
        }

        animator.SetBool("isCry", isCry);
        animator.SetBool("isEat", isEat);
        animator.SetBool("isPlay", isPlay);
        animator.SetBool("isSleep", isSleep);
        animator.SetBool("isSoul", isSoul);
    }

    public void LateUpdate(){
        if(isSoul){
            Vector3 tragetPos = player.GetComponent<PlayerController>().backPosition();
            transform.position = Vector3.Lerp(transform.position, tragetPos, Time.deltaTime * speed); 
        }
    }
}
