using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class InventoryController : MonoBehaviour
    {
        private GameObject[] crates;
        private GameObject[] crateFills;
        private Sprite[] sprites;
        private int emptySpriteCount = 0;
        private int cratePointer = 0;

        private void Start()
        {
            crates = GameObject.FindGameObjectsWithTag("InvCrate");
            crateFills = GameObject.FindGameObjectsWithTag("InvChange");
            sprites = new Sprite[crates.Length];
            emptySpriteCount = crates.Length;
        }

        public void set(Sprite s, int? index = null) {
            var index_ = index ?? cratePointer;
            
            print("index_ = " + index_  + " ssprites.Length= " + sprites.Length + " crateFills.Length= " + crateFills.Length);
            crateFills[index_].GetComponent<SpriteRenderer>().sprite = s;
            sprites[index_] = s;
            if (s != null && cratePointer < sprites.Length -1)
            {
                cratePointer++;
                emptySpriteCount--;

                while (emptySpriteCount > 0 && cratePointer < sprites.Length && sprites[cratePointer] != null)
                {
                    cratePointer++;
                }
            }
            print("cratePointer= " + cratePointer +" s= " +s);
        }

        public Sprite remove(int index) {
            var comp = crateFills[index].GetComponent<SpriteRenderer>();
            var old = comp.sprite;
            comp.sprite = null;
            if (index < cratePointer)
                cratePointer = index;
            return old;
        }

        public Sprite get(int index) => sprites[index];
    }
}
