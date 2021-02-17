using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class TrashController: MonoBehaviour
    {
        public enum Kind {
            ORGANIC,
            ANORGANIC,
            RECYCLABLE,
            //B3, in future
        }

        public static readonly string STR_ORGANIC = "T-Organic";
        public static readonly string STR_ANORGANIC = "T-Anorganic";
        public static readonly string STR_RECYCLABLE = "T-Recyclable";

        public Kind kind { get; private set; }

        private void Start()
        {
            switch (gameObject.tag) {
                case "T-Organic": kind = Kind.ORGANIC; break;
                case "T-Anorganic": kind = Kind.ANORGANIC; break;
                case "T-Recyclable": kind = Kind.RECYCLABLE; break;
                default: Debug.LogError($"gameObject.tag ({gameObject.tag}) tidak sesuai untuk sampah");
                    break;
            }
        }
    }
}
