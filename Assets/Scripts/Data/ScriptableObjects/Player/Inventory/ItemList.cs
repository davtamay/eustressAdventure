using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList<T> : ScriptableObject{

	public List<T> Items = new List<T>();
}
