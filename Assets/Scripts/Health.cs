using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;
    public float fullHp;

    public void init(){
        fullHp = 100.0f;
        hp = fullHp;
    }

    // void meet monster
    public void reduceHP(){
        hp -= 1.0f * Time.deltaTime;
    }

    // mission fail
    public void missionFail(){
        hp -= 7.0f;
    }

    // check if gameover
    public bool isDead(){
        return hp < 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
