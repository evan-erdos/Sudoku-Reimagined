/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections.Generic;

public class Space<T> : MonoBehaviour, ISpace<T> {

	public GameObject water;

    public T Value {get;set;}

	public Dir Direction {get;set;}

	public bool HasWater {
		get { return hasWater; }
		set { if (hasWater==value) return;
			hasWater = value;
			water.SetActive(hasWater);
		}
	} bool hasWater;

    public virtual bool IsEmpty { get { return Value==null; } }

    public virtual void Awake() {
    	if (!water)
    		throw new System.Exception("No water!");
    	water.SetActive(false);
    }
}
