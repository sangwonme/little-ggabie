using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSoul : MonoBehaviour
{
    private BoxCollider eatCollider;
    private MonsterBody monsterBody;
    private Timer timer;
    private PlayerController player;
    public float holdingTime;

    // Start is called before the first frame update
    void Start()
    {
        eatCollider = GetComponent<BoxCollider>();
        monsterBody = transform.parent.gameObject.GetComponent<MonsterBody>();
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        holdingTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingTime > 3.0f){
            Destroy(transform.parent.gameObject.transform.parent.gameObject);
            monsterBody.state = "dead";
            player.finishWorking();
        }
    }
    
    // eat event
    private void OnTriggerStay(Collider other) {
        if(timer.isDay){
            if(other.tag == "Player"){
                if(gameObject == player.getClosestMonster()){
                    if(player.checkWorking()) holdingTime += Time.deltaTime;
                    else holdingTime = 0.0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") holdingTime = 0.0f;
    }
}
