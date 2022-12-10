using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour
{

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
