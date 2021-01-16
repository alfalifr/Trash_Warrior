using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class PlayerController : KinematicObject
{
    public InventoryController inventoryC;
    public GrabController grabC;

    private bool isTouchingBin = false;
    //public GameObject inventory;


    protected new void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q) && isTouchingBin && grabC.currSprite != null) {
            inventoryC.remove(grabC.currSpriteIndex);
            grabC.grabOnHand(-1);
            gameC.decTrash();
            gameC.playAudio(GameController.Audio.THROW);
        }
    }

    protected new void OnTriggerEnter2D(Collider2D hit)
    {
        base.OnTriggerEnter2D(hit);
        switch (hit.gameObject.tag) {
            case "Collectible": {
                    if (inventoryC.emptySpriteCount > 0) {
                        var spriteR = hit.gameObject.GetComponent<SpriteRenderer>();
                        grabToInventory(spriteR);
                        gameC.playAudio(GameController.Audio.COLLECT);
                    }
                    break;
            }
            case "Bin": 
                {
                    isTouchingBin = true;
                    break;
                }
        }
        print("hit.gameObject.tag= " + hit.gameObject.tag + " isTouchingBin= " + isTouchingBin);
    }

    protected new void OnTriggerExit2D(Collider2D hit) {
        base.OnTriggerExit2D(hit);
        switch (hit.gameObject.tag)
        {
            case "Bin":
                {
                    isTouchingBin = isInside(hit);
                    break;
                }
        }
    }

    void grabToInventory(SpriteRenderer obj) {
        var sprite = obj.sprite;
        if (sprite != null) {
            inventoryC.set(sprite);
            Destroy(obj);
        }
    }
}
