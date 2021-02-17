using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.SceneManagement;

public class PlayerController : KinematicObject
{
    public InventoryController inventoryC;
    public GrabController grabC;

    private bool isTouchingBin = false;
    //public GameObject inventory;
    private GameObject playerObj; 
    private GameController.Throw? throwMode;
    private TrashController.Kind? binKind;

    protected new void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q) && isTouchingBin && grabC.currSprite != null) {
            var tup = inventoryC.remove(grabC.currSpriteIndex);
            var throwMode = GameController.Throw.REGULAR;
            var trashKind = tup.Item2;
            if (trashKind == binKind) {
                if (trashKind == TrashController.Kind.RECYCLABLE)
                    throwMode = GameController.Throw.RECYCLE;
                else
                    throwMode = GameController.Throw.CORRECT;
            } else if(trashKind == TrashController.Kind.RECYCLABLE 
                && binKind == TrashController.Kind.ANORGANIC) {
                throwMode = GameController.Throw.CORRECT;
            }
            grabC.grabOnHand(-1);
            gameC.throwTrash(throwMode);
        }

        playerObj = GameObject.Find("Player");
        if (playerObj.transform.position.y <= -10){
            SceneManager.LoadScene("gameover");
            //Debug.Log("Dead");
        }
        
    }

    protected new void OnTriggerEnter2D(Collider2D hit)
    {
        base.OnTriggerEnter2D(hit);
        switch (hit.gameObject.tag)
        {
            case "T-Organic":
            case "T-Anorganic":
            case "T-Recyclable":
            case "Collectible": 
                if (inventoryC.emptySpriteCount > 0) {
                    var spriteR = hit.gameObject.GetComponent<SpriteRenderer>();
                    grabToInventory(spriteR);
                    gameC.playAudio(GameController.Audio.COLLECT);
                }
                break;
            case "Bin-Organic":
                binKind = TrashController.Kind.ORGANIC;
                goto case "Bin";
            case "Bin-Anorganic":
                binKind = TrashController.Kind.ANORGANIC;
                goto case "Bin";
            case "Bin-Recyclable":
                binKind = TrashController.Kind.RECYCLABLE;
                goto case "Bin";
            case "Bin": 
                isTouchingBin = true;
                break;
               
        }
        //print("hit.gameObject.tag= " + hit.gameObject.tag + " isTouchingBin= " + isTouchingBin);
    }

    protected new void OnTriggerExit2D(Collider2D hit) {
        base.OnTriggerExit2D(hit);
        switch (hit.gameObject.tag)
        {
            case "Bin-Organic":
            case "Bin-Anorganic":
            case "Bin-Recyclable":
            case "Bin":
                {
                    isTouchingBin = isInside(hit);
                    if (!isTouchingBin) {
                        print("OnTriggerExit2D di luar TONG bro!!!");
                        binKind = null;
                    }
                    break;
                }
        }
    }

    void grabToInventory(SpriteRenderer obj) {
        var sprite = obj.sprite;
        if (sprite != null) {
            inventoryC.set(obj.gameObject);
            Destroy(obj);
        }
    }
}
