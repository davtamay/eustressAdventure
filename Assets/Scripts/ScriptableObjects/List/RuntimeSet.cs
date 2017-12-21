// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

	[CreateAssetMenu (fileName = "RunTimeSet", menuName = "CustomSO/RunTimeSet")]
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        public List<T> Items = new List<T>();

        public virtual void Add(T thing)
        {
            if (!Items.Contains(thing))
                Items.Add(thing);
        }

        public virtual void Remove(T thing)
        {
            if (Items.Contains(thing))
                Items.Remove(thing);
        }
    }
