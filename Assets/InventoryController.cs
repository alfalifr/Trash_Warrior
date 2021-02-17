using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

namespace Assets
{
    public class InventoryController : MonoBehaviour
    {
        private GameObject[] crates;
        private GameObject[] crateFills;
        private Sprite[] sprites;
        private TrashController.Kind?[] trashKind;
        public int emptySpriteCount { private set; get; } = 0;
        private int cratePointer = 0;
        public int capacity { private set; get; } = 2;

        private void Start()
        {
            crates = GameObject.FindGameObjectsWithTag("InvCrate");
            crateFills = GameObject.FindGameObjectsWithTag("InvChange");
            sprites = new Sprite[crates.Length];
            trashKind = new TrashController.Kind?[crates.Length];
            emptySpriteCount = crates.Length;

            //print($"crateFills.Length = {crateFills.Length} emptySpriteCount= {emptySpriteCount}");
        }

        public void set(GameObject obj, int? index = null) {
            var index_ = index ?? cratePointer;

            var s = obj.GetComponent<SpriteRenderer>().sprite;
            TrashController.Kind? trashKind = null;

            switch (obj.tag) {
                case "T-Organic":
                    trashKind = TrashController.Kind.ORGANIC;
                    break;
                case "T-Anorganic":
                    trashKind = TrashController.Kind.ANORGANIC;
                    break;
                case "T-Recyclable":
                    trashKind = TrashController.Kind.RECYCLABLE;
                    break;
            }

            //print("index_ = " + index_  + " ssprites.Length= " + sprites.Length + " crateFills.Length= " + crateFills.Length);
            crateFills[index_].GetComponent<SpriteRenderer>().sprite = s;
            this.trashKind[index_] = trashKind;
/*
            try
            {
                
            }
            catch (MissingComponentException e)
            {
                crateFills[index_].GetComponent<TilemapRenderer>().sprite = s;
            }
*/
            sprites[index_] = s;
            if (s != null && cratePointer < sprites.Length)
            {
                cratePointer++;
                emptySpriteCount--;

                while (emptySpriteCount > 0 && cratePointer < sprites.Length && sprites[cratePointer] != null)
                {
                    cratePointer++;
                }
            }
            //print("cratePointer= " + cratePointer +" s= " +s);
        }

        public Tuple<Sprite, TrashController.Kind> remove(int index) {
            var comp = crateFills[index].GetComponent<SpriteRenderer>();
            var old = comp.sprite;
            var oldKind = (TrashController.Kind) trashKind[index];
            comp.sprite = null;
            sprites[index] = null;
            if (old != null)
                emptySpriteCount++;
            if (index < cratePointer)
                cratePointer = index;
            return Tuple.Create(old, oldKind);
        }

        public Tuple<Sprite, TrashController.Kind> get(int index) => Tuple.Create(sprites[index], (TrashController.Kind) trashKind[index]);
    }
}
