using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoul : MonoBehaviour
{
    // light
    public Light surroundLight;
    public Light nuclearLight;
    bool goBright = true;
    // eat
    public SphereCollider eatCollider;
    // monster
    private GameObject monster;
    private GameObject monsterBody;
    private string state;



    void setLight(float intensity){
        surroundLight.intensity = intensity;
        nuclearLight.intensity = intensity * 5;
    }

    void updateMonsterState(){
        state = monsterBody.GetComponent<MonsterBody>().state;
    }

    float getLight(){
        return surroundLight.intensity;
    }

    // Start is called before the first frame update
    void Start()
    {
        monster = transform.parent.gameObject;
        monsterBody = transform.parent.transform.GetChild(0).gameObject;
        updateMonsterState();
        setLight(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // follow monsterbody
        transform.position = new Vector3(monsterBody.transform.position.x, monsterBody.transform.position.y-2.9f, monsterBody.transform.position.z-0.9f);
    
        // check if dead
        updateMonsterState();
        if(state == "dead"){
            // blinking
            if(getLight() > 2.0f) goBright = false;
            else if(getLight() < 1.0f) goBright = true;
            // set intensity
            if(goBright) setLight(getLight()+0.01f);
            else setLight(getLight()-0.01f);
        }else{
            // turn off
            setLight(0.0f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && state == "dead"){
            Destroy(monster);
        }
    }
}
