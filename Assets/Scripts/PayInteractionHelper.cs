using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayInteractionHelper : MonoBehaviour {
    public UniversalMessageController messagePanel;
    public int moneyDroppedMessageIndex = 1;
    public int successMessageIndex = 2;
    public Collider[] blockers;
    public PayTrigger payTrigger;
    private float payedMoney = 0;

    void OnCollisionEnter(Collision c){
        if(messagePanel.hasBeenShown){
            if(c.gameObject.GetComponent<Money>() != null){
                messagePanel.ShowMessage(moneyDroppedMessageIndex);
                payedMoney = c.gameObject.GetComponent<Money>().amount;
                Debug.Log("Money dropped: " + payedMoney);
            }
            else if (c.gameObject.GetComponent<Food>() != null){
                Debug.Log("Food dropped on counter; current tray cost: " + Player.MainPlayer.tray.GetContainedFoodCost());
                if(Player.MainPlayer.tray.GetContainedFoodCost() <= payedMoney){
                    messagePanel.ShowMessage(successMessageIndex);
                    messagePanel.clickEnabled = true;
                    for(int i = 0; i < blockers.Length; i++){
                        blockers[i].enabled = false;
                    }
                    payTrigger.enabled = false;
                    Player.MainPlayer.tray.SetFoodPurchased();
                }
            }
        }

    }
}
