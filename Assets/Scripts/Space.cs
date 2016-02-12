/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections.Generic;

public class Space<T> : MonoBehaviour, ISpace<T> {

    public T Value {get;set;}

	public Dir Direction { get; set; }

	public bool HasWater { get; set; }

    public virtual bool IsEmpty { get { return Value==null; } }
}
