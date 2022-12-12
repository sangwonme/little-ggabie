using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSoul : MonoBehaviour
{
    private BoxCollider eatCollider;
    private MonsterBody monsterBody;
    private UIMonster ui;
    private Timer timer;
    private PlayerController player;
    private Inventory inven;
    public float holdingTime;

    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.Find("GameController").GetComponent<Inventory>();
        eatCollider = GetComponent<BoxCollider>();
        monsterBody = transform.parent.gameObject.GetComponent<MonsterBody>();
        ui = transform.parent.gameObject.GetComponent<UIMonster>();
        ui.setUIKey(false);
        ui.setUIMission(false);
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        holdingTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingTime > 3.0f){
            Destroy(transform.parent.gameObject.transform.parent.gameObject);
            player.finishWorking();
            inven.earnFlower();
        }

        // turn off ui when night
        if(!timer.isDay && player.getClosestMonster() == gameObject){
            ui.setUIKey(false);
            player.setWorkable(false);
            ui.setUIMission(false);
        }

        // update ui mission length
        if(timer.isDay){
            ui.setMissionLength(holdingTime);
            if(holdingTime > 0.0f){
                ui.setUIMission(true);
            }else{
                ui.setUIMission(false);
            }
        }
    }
    
    // eat event
    private void OnTriggerStay(Collider other) {
        if(timer.isDay){
            if(other.tag == "Player"){
                ui.setUIKey(holdingTime == 0.0f && gameObject == player.getClosestMonster());
                player.setWorkable(true);
                player.setWorkingDirection(transform.position.z - 1.05f);
                if(gameObject == player.getClosestMonster()){
                    if(player.checkWorking() && !timer.pause) holdingTime += Time.deltaTime;
                    else holdingTime = 0.0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            ui.setUIKey(false);
            player.setWorkable(false);
            holdingTime = 0.0f;
        }
    }
}
