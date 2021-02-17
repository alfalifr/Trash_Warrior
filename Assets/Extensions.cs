using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public static class Extensions
    {
        public static bool isLayerName(this GameObject it, string name) => 
            it.layer == LayerMask.NameToLayer(name);
        public static int countIf<T>(this IEnumerable<T> it, Func<T, bool> predicate) {
            var count = 0;
            foreach (T e in it)
                if (predicate(e))
                    count++;    
            return count;
        }
        public static IList<R> map<T, R>(this IEnumerable<T> it, Func<T, R> mapper) {
            var ls = new ArrayList();
            foreach(T e in it) 
                ls.Add(mapper(e));
            return (IList<R>) ls;
        }
    }
}
