using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    public GameObject monsterPrefab;
    private Timer timer;
    float spawnPeriod;

    public void destroyChildren(){
         foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        setSpawnPeriod();
        timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    Vector3 initPos(){
        float x = Random.Range(23.8f, 34.9f);
        float z = Random.Range(15f, 25f);
        return new Vector3(x, 0.38f, z);
    }

    void setSpawnPeriod(){
        spawnPeriod = (Random.Range(3.0f, 7.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.isDay){
            // timecount
            if(!timer.pause){
                spawnPeriod -= Time.deltaTime;
            }
            // spawn monster
            if(spawnPeriod < 0){
                Instantiate(monsterPrefab, initPos(), Quaternion.identity, transform);
                setSpawnPeriod();
            }
        }
    }
}
