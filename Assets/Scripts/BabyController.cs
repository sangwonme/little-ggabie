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
    public Vector3 home;

    // light
    public Light cryingLight;
    private int lightBrighter;

    // cooltime
    private UIMonster ui;
    private float missionTime = 5.0f;
    private float timeCount;


    // inven
    private Inventory inven;

    // anim
    private bool isCry;
    private bool isEat;
    private bool isPlay;
    private bool isSleep;
    private bool isSoul;

    
    public void morphSoul(){
        state = "soul";
    }

    public void morphBaby(){
        // set hold time to 0 if baby is not playing
        if(state != "play") timeCount = 0.0f;
        // if baby is at playground -> state = play / mission clear
        state = player.GetComponent<PlayerController>().getPlace()=="playground" ? "play" : "idle";
        if(state == "play"){
            missionGenerator.clearMissionOfBaby(babyIdx);
        }
    }

    public void makeSleep(){
        timeCount = 0.0f;
        state = "sleep";
        missionGenerator.clearMissionOfBaby(babyIdx);
    }

    public void eatMeat(){
        inven.eatMeat();
        timeCount = 0.0f;
        state = "eat";
        missionGenerator.clearMissionOfBaby(babyIdx);
    }

    public void init(){
        // state & mission
        state = "idle";

        // anim
        isCry = false;
        isEat = false;
        isPlay = false;
        isSleep = false;
        isSoul = false;

        // cooltime
        timeCount = 0.0f;

        // light
        lightBrighter = 1;

        // pos
        transform.position = home;
    }

    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.Find("GameController").GetComponent<Inventory>();
        ui = GetComponent<UIMonster>();
        
        init();
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

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
                if(timeCount > missionTime){
                    timeCount = 0.0f;
                    ui.setUIMission(false);
                    state = "idle";
                }else{
                    ui.setUIMission(true);
                    ui.setMissionLength(timeCount, missionTime);
                }
                break;
            case "play":
                if(timeCount > missionTime){
                    timeCount = 0.0f;
                    ui.setUIMission(false);
                    state = "idle";
                    transform.position = home;
                }else{
                    ui.setUIMission(true);
                    ui.setMissionLength(timeCount, missionTime);
                }
                break;
            case "sleep":
                if(timeCount > missionTime){
                    timeCount = 0.0f;
                    ui.setUIMission(false);
                    state = "idle";
                }else{
                    ui.setUIMission(true);
                    ui.setMissionLength(timeCount, missionTime);
                }
                break;
            case "soul":
                // if out of home even though no mission
                if(mission == "none"){
                    state = "idle";
                    transform.position = home;
                }
                break;
            default:
                break;
        }

        // crying light
        if(state == "soul" || state == "cry"){
            if(cryingLight.intensity <= 0.0f){
                lightBrighter = 1;
            }
            else if(cryingLight.intensity >= 2.0f){
                lightBrighter = -1;
            }
            cryingLight.intensity += lightBrighter * 0.05f;
        }

        animator.SetBool("isCry", isCry);
        animator.SetBool("isEat", isEat);
        animator.SetBool("isPlay", isPlay);
        animator.SetBool("isSleep", isSleep);
        animator.SetBool("isSoul", isSoul);
    }

    public void LateUpdate(){
        if(isSoul){
            Vector3 tragetPos = player.GetComponent<PlayerController>().backPosition(transform.position.y);
            transform.position = Vector3.Lerp(transform.position, tragetPos, Time.deltaTime * speed); 
        }
    }

}
