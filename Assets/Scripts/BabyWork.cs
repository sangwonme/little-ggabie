using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyWork : MonoBehaviour
{
    private BoxCollider workCollider;
    private PlayerController player;
    private Inventory inven;
    private UIMonster ui;
    public BabyController baby;
    public string mission;
    public string state;

    public float holdingTime;

    void updateBabyState(){
        mission = baby.mission;
        state = baby.state;
    }

    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.Find("GameController").GetComponent<Inventory>();
        workCollider = GetComponent<BoxCollider>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        ui = transform.parent.gameObject.GetComponent<UIMonster>();
        ui.setUIKey(false);
        ui.setUIMission(false);
        holdingTime = 0.0f;
        updateBabyState();
    }

    // Update is called once per frame
    void Update()
    {
        // update baby state
        updateBabyState();

        // work done
        if(holdingTime > 3.0f){
            holdingTime = 0.0f;
            switch(mission){
                case "play":
                    player.finishWorking();
                    // make soul
                    if(state == "soul"){
                        baby.morphBaby();
                    }else{
                        baby.morphSoul();
                    }
                    break;
                case "sleep":
                    player.finishPlayingMusic();
                    baby.makeSleep();
                    break;
            }
        }

        // update ui mission length
        if(state == "cry" || state == "soul" || state == "idle"){
            ui.setMissionLength(holdingTime);
            if(holdingTime > 0.0f){
                ui.setUIMission(true);
            }else{
                ui.setUIMission(false);
            }
        }

    }

    // hold baby or give food baby
    private void OnTriggerStay(Collider other) {
        updateBabyState();
        if(other.tag == "Player"){
            if(mission == "eat" && state == "cry"){
                // x button
                if(inven.meatExist()){
                    ui.setKeyImg("x");
                    ui.setUIKey(gameObject == player.getClosestMonster());
                    if(gameObject == player.getClosestMonster()){
                        if(Input.GetKey(KeyCode.X)){
                            ui.setUIKey(false);
                            baby.eatMeat();
                        }
                    }
                }else{
                    ui.setUIKey(false);
                }
            }
            else if(mission == "sleep" && state == "cry"){
                // c button
                ui.setKeyImg("c");
                ui.setUIKey(holdingTime == 0.0f && gameObject == player.getClosestMonster());
                if(gameObject == player.getClosestMonster()){
                    if(player.checkPlayingMusic()) holdingTime += Time.deltaTime;
                    else holdingTime = 0.0f;
                }
            }
            else if(
                mission == "play" && state == "cry" ||
                mission == "play" && state == "soul" && player.GetComponent<PlayerController>().getPlace()=="playground" ||
                mission == "none" && state == "soul"
            ){
                // z button
                ui.setKeyImg("z");
                ui.setUIKey(holdingTime == 0.0f && gameObject == player.getClosestMonster());
                player.setWorkable(true);
                player.setWorkingDirection(transform.position.z);
                if(gameObject == player.getClosestMonster()){
                    if(player.checkWorking()) holdingTime += Time.deltaTime;
                    else holdingTime = 0.0f;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other) {
        updateBabyState();
        if(other.tag == "Player"){
            ui.setUIKey(false);
            player.setWorkable(false);
            holdingTime = 0.0f;
        }
    }
}
