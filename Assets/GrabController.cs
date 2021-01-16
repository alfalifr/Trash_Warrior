using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class GrabController : MonoBehaviour
{
    private SpriteRenderer content;
    public InventoryController inventoryC;
    public Sprite currSprite { private set => content.sprite = value; get => content.sprite; }
    public int currSpriteIndex { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponentInChildren<SpriteRenderer>();
        currSpriteIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            grabOnHand(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            grabOnHand(1);
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            grabOnHand(-1);
        }
    }

    public void grabOnHand(int index) {
        if (index >= 0)
        {
            currSprite = inventoryC.get(index);
            currSpriteIndex = index;
        }
        else {
            currSprite = null;
            currSpriteIndex = -1;
        }
    }
}
