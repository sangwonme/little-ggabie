using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShop : MonoBehaviour
{
    private Timer timer;
    private Inventory inven;
    private int meatBuy;
    private int flowerBuy;
    private int price;
    public TMP_Text btnText;
    public TMP_Text meatBuyText;
    public TMP_Text flowerBuyText;
    

    private void more(){
        if(inven.getFlowerNum() >= flowerBuy + price){
            meatBuy ++;
            flowerBuy += price;
        }
    }

    private void less(){
        if(meatBuy > 0){
            meatBuy --;
            flowerBuy -= price;
        }
    }

    private void trade(){
        for(int i = 0; i < meatBuy; i++){
            inven.buyMeat(price);
        }
        gameObject.SetActive(false);
        meatBuy = 0;
        flowerBuy = 0;
        timer.makeResume();
    }

    public void onButton(string btn){
        switch(btn){
            case "more":
                more();
                break;

            case "less":
                less();
                break;

            case "trade":
                trade();
                break;

            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        price = 1;
        inven = GameObject.Find("GameController").GetComponent<Inventory>();
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        btnText.text = "QUIT";
        meatBuyText.text = "0";
        flowerBuyText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // change button text
        btnText.text = meatBuy == 0 ? "QUIT" : "TRADE";

        // update buy num text
        meatBuyText.text = meatBuy.ToString();
        flowerBuyText.text = flowerBuy.ToString();

        // key pressed
        if(Input.GetKeyDown(KeyCode.Return)){
            trade();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)){
            gameObject.SetActive(false);
            meatBuy = 0;
            flowerBuy = 0;
            timer.makeResume();
            
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow)){
            more();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
            less();
        }
        
    }
}
