/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections.Generic;

public class IntSpaceWrapper : MonoBehaviour, ISpace<int> {

	ISpace<int> space = new Space<int>(0);

	TextMesh textMesh;

    public int Value {
    	get { return space.Value; }
    	set { space.Value = value;
    		SetSpace(space.Value);
    	}
    }

    public bool IsEmpty { get { return space.IsEmpty; } }

    void Awake() {
    	textMesh = GetComponentInChildren<TextMesh>();
    	if (textMesh==null)
    		throw new System.Exception("No render text");
    }

    public void SetSpace(int n) {
    	space.Value = n;
    	textMesh.gameObject.GetComponent<Renderer>().enabled = (space.Value>0);
    	textMesh.text = space.Value.ToString();
    }
}
