/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
public class SpaceWrapper : MonoBehaviour, ISpace<Tiles> {

	ISpace<Tiles> space = new Space<Tiles>();

	bool wait, isRevealed;

    public AudioClip clip;

	public SudokuBoardWrapper board;

	TextMesh textMesh;

    public Tiles Value {
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
        if (GetComponentInChildren<Renderer>()==null)
            throw new System.Exception("No renderer");
    }

    public void SetSpace(Tiles tile) {
    	space.Value = tile;
    	textMesh.gameObject.GetComponent<Renderer>().enabled = (isRevealed);
    	textMesh.text = space.Value.ToString();
    }

    public void ApplyPlayerMaterial(Material material) {
        GetComponentInChildren<Renderer>().material = material;
    }


    public IEnumerator MakeMove() {
    	if (wait) yield break;
    	wait = true;
    	isRevealed = true;
    	SetSpace(board.board.GetNext().Value);
        board.PrintNext();
        if (clip)
            AudioSource.PlayClipAtPoint(clip, new Vector3(0f,0f,-10f));
        ApplyPlayerMaterial(board.SwitchPlayer());
        if (board.board.IsFinished) board.Restart();
        yield return new WaitForSeconds(0.05f);
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
