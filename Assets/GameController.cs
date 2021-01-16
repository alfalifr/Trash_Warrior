using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //private GameObject[] 
    [SerializeField] private Text winTxt;
    public int trashCount { private set; get; }

    void Start()
    {
        trashCount = GameObject.FindGameObjectsWithTag("Collectible").Length;
        print("trashCount= " + trashCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decTrash() {
        if (--trashCount <= 0) {
            winTxt.gameObject.SetActive(true);
            print("winTxt.enabled= " + winTxt.enabled);
        }
        print("decTrash.trashCount= " + trashCount);
    }
}
