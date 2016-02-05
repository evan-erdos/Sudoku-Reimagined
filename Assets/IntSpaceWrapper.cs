/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class IntSpaceWrapper : MonoBehaviour, ISpace<int> {

	ISpace<int> space = new Space<int>(0);

	bool wait, isRevealed;

	public IntSudokuBoardWrapper board;

	TextMesh textMesh;

    public int Value {
    	get { return space.Value; }
    	set { space.Value = value;
            SetSpace(space.Value);
    	}
    }

    public bool IsEmpty { get { return space.IsEmpty; } }

    void Awake() {
    	gameObject.GetComponent<Rigidbody>().isKinematic = true;
    	textMesh = GetComponentInChildren<TextMesh>();
    	if (textMesh==null)
    		throw new System.Exception("No render text");
    }

    public void SetSpace(int n) {
    	space.Value = n;
    	textMesh.gameObject.GetComponent<Renderer>().enabled =
    		(isRevealed && space.Value>0);
    	textMesh.text = space.Value.ToString();
    }


    public IEnumerator MakeMove() {
    	if (wait) yield break;
    	wait = true;
    	isRevealed = true;

		//Change material for 
		board.SwitchPlayer();

    	SetSpace(board.board.GetNext().Value);
        board.PrintNext();
        yield return new WaitForSeconds(0.1f);
    	wait = false;
    }


    public IEnumerator OnMouseOver() {
        if (isRevealed) yield break;
        while (!isRevealed) {
        	if (Input.GetButtonDown("Fire1"))
        		yield return StartCoroutine(MakeMove());
        	else yield return new WaitForEndOfFrame();
        }
    }

    public void OnMouseExit() {
    	StopAllCoroutines();
    }
}
