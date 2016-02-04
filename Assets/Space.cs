/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections.Generic;

public class Space<T> : ISpace<T> {

    public T Value {
        get { return value; }
    } protected T value;

    public virtual bool IsEmpty { get { return Value==null; } }

}
