using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    public GameObject monsterPrefab;
    private Timer timer;
    float spawnPeriod;

    // Start is called before the first frame update
    void Start()
    {
        setSpawnPeriod();
        timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    Vector3 initPos(){
        float x = Random.Range(-17, 17);
        float z = Random.Range(-3.5f, 5f);
        return new Vector3(x, 2.26f, z);
    }

    void setSpawnPeriod(){
        spawnPeriod = (Random.Range(3.0f, 7.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.isDay){
            // timecount
            spawnPeriod -= Time.deltaTime;
            // spawn monster
            if(spawnPeriod < 0){
                Instantiate(monsterPrefab, initPos(), Quaternion.identity, transform);
                setSpawnPeriod();
            }
        }
    }
}
