/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections.Generic;

public class IntSpaceWrapper : MonoBehaviour, ISpace<int> {

	public ISpace<int> space = new Space<int>(0);

	TextMesh textMesh;

    public int Value {
    	get { return space.Value; }
    	set { if (space.Value==value) return;
    		space.Value = value;
    		SetSpace(space.Value);
    	}
    }

    public bool IsEmpty { get { return space.IsEmpty; } }

    void Awake() {
    	textMesh = GetComponentInChildren<TextMesh>();
    	if (textMesh==null)
    		throw new System.Exception("No render text");
    	SetSpace(space.Value);
    }

    public void SetSpace(int n) {
    	space.Value = n;
    	textMesh.text = space.Value.ToString();
    }
}
