using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int meatNum;
    private int flowerNum;

    public void buyMeat(int price){
        meatNum += 1;
        flowerNum -= price;
    }

    public void earnFlower(){
        flowerNum += 1;
    }

    public int getMeatNum(){
        return meatNum;
    }

    public int getFlowerNum(){
        return flowerNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        meatNum = 0;
        flowerNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
