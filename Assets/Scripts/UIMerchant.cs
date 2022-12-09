using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMerchant : MonoBehaviour
{
    private GameObject uiKey;
    private GameObject uiShop;

    public void setUIKey(bool turn){
        uiKey.SetActive(turn);
    }
    public void setUIShop(bool turn){
        uiShop.SetActive(turn);
    }



    public void turnOffUI(){
        uiKey.SetActive(false);
        uiShop.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        turnOffUI();
    }

    // Update is called once per frame
    void Update()
    {
        uiKey = gameObject.transform.GetChild(0).gameObject;
        uiShop = GameObject.Find("ShopUI");
    }
}
