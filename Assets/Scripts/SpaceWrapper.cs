/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class SpaceWrapper : MonoBehaviour, ISpace<Tiles> {

    public GameObject prefab;

    public GameObject CurrentTile {
        get { return currentTile; }
        set { Destroy(currentTile);
            currentTile = value;
        }
    } GameObject currentTile;

    public Space<Tiles> CurrentSpace {
        get { if (currentTile==null) return default (Space<Tiles>);
            return currentTile.GetComponent<Space<Tiles>>(); } }

    bool wait;

    public AudioClip clip;

    public SudokuBoardWrapper board;

    public Tiles Value {
        get { return CurrentSpace.Value; }
        set { if (CurrentSpace==null) return;
            CurrentSpace.Value = value;
        }
    }

    public bool IsEmpty {
        get { return (CurrentSpace!=null && CurrentSpace.IsEmpty); } }

    void Awake() {
        GetComponent<Rigidbody>().isKinematic = true;
        var tile = Object.Instantiate(prefab) as GameObject;
        tile.transform.parent = this.transform;
        tile.transform.localPosition = Vector3.zero;
        CurrentTile = tile;
    }

    public IEnumerator MakeMove() {
        if (wait) yield break;
        wait = true;

		// BUG: What if player clicks really fast and changes IconSelector.Current before this function can run?
		// TODO: Check if IconSelector really has anything meaningful selected


		Tiles oldTileVal = CurrentSpace.Value;
		CurrentSpace.Value = IconSelector.Current;
		/* Check that move is valid */

		Debug.Log ("About to check if board is valid!");

		if (board.board.IsBoardValid()) {
			var newTile = IconSelector.CreateTile(IconSelector.Current);
			CurrentTile = newTile;
			newTile.transform.parent = this.transform;
			newTile.transform.localPosition = Vector3.zero;
			Debug.Log ("Valid move made!");
		} else {
			CurrentSpace.Value = oldTileVal;
			Debug.Log ("That was not a valid move!");
		}


//
//
//		var tile = IconSelector.CreateTile(IconSelector.Current);
//        tile.transform.parent = this.transform;
//        tile.transform.localPosition = Vector3.zero;
       // Current = tile;
        if (clip)
            GetComponent<AudioSource>().PlayOneShot(clip);
        yield return new WaitForSeconds(0.05f);
        wait = false;
    }

    public IEnumerator OnMouseOver() {
        while (!wait) {
            if (Input.GetButtonUp("Fire1"))
                yield return StartCoroutine(MakeMove());
            else yield return new WaitForEndOfFrame();
        }
    }

    public void OnMouseExit() {
        StopAllCoroutines();
    }
}
