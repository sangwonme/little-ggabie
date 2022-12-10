using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour
{

    public Animator animator;
    public Vector2 idleRange;
    public float idleTime;
    public float cryTime;
    public string state;
    public string mission;

    public float timeCount;


    // anim
    private bool isCry;
    private bool isEat;
    private bool isPlay;
    private bool isSleep;

    private void setRandomIdleTime(){
        idleTime = Random.Range(idleRange.x, idleRange.y);
    }

    private void giveNewMission(){
        float dice = Random.Range(0f, 0.9f);
        if(dice < 0.3f){
            mission = "eat";
        }
        else if(dice < 0.6f){
            mission = "play";
        }
        else if(dice < 0.9f){
            mission = "sleep";
        }
        state = "cry";
        setRandomIdleTime();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = "idle";
        mission = "none";
        setRandomIdleTime();
        timeCount = 0.0f;

        // anim
        isCry = false;
        isEat = false;
        isPlay = false;
        isSleep = false;
    }

    // Update is called once per frame
    void Update()
    {
        // time update
        timeCount += Time.deltaTime;

        // do mission
        switch(mission){
            // no mission state -> generate mission
            case "none":
                if(timeCount > idleTime){
                    timeCount = 0.0f;
                    giveNewMission();
                }
                break;
            case "eat":
                // fail
                if(timeCount > cryTime){
                    timeCount = 0.0f;
                    // healt --
                    mission = "none";
                    state = "idle";
                }
                // doing mission


                break;
            case "play":
                // fail
                if(timeCount > cryTime){
                    timeCount = 0.0f;
                    // healt --
                    mission = "none";
                    state = "idle";
                }

                break;
            case "sleep":
                // fail
                if(timeCount > cryTime){
                    timeCount = 0.0f;
                    // healt --
                    mission = "none";
                    state = "idle";
                }

                break;
        }

        // state animation
        isCry = (state == "cry");
        isEat = (state == "eat");
        isPlay = (state == "play");
        isSleep = (state == "sleep");

        switch(state){
            case "cry":
                break;
            case "eat":
                break;
            case "play":

                break;
            case "sleep":

                break;
            default:
                break;
        }

        animator.SetBool("isCry", isCry);
        animator.SetBool("isEat", isEat);
        animator.SetBool("isPlay", isPlay);
        animator.SetBool("isSleep", isSleep);
    }
}
