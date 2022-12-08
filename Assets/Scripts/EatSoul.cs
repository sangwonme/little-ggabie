using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSoul : MonoBehaviour
{
    private BoxCollider eatCollider;
    private MonsterBody monsterBody;
    private Timer timer;
    public float holdingTime;

    // Start is called before the first frame update
    void Start()
    {
        eatCollider = GetComponent<BoxCollider>();
        monsterBody = transform.parent.gameObject.GetComponent<MonsterBody>();
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        holdingTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingTime > 5.0f){
            monsterBody.state = "dead";
        }
    }
    
    // eat event
    private void OnTriggerStay(Collider other) {
        if(timer.isDay){
            if(other.tag == "Player"){
                Debug.Log(Input.GetKey(KeyCode.X));
                if(Input.GetKey(KeyCode.X)) holdingTime += Time.deltaTime;
                else holdingTime = 0.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") holdingTime = 0.0f;
    }
}
