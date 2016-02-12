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

	public Dir Direction {
		get { return CurrentSpace.Direction; }
		set { if (CurrentSpace==null) return;
			CurrentSpace.Direction = value;
		}
	}

	public bool HasWater {
		get { return (CurrentSpace!=null && CurrentSpace.HasWater); }
		set { if (CurrentSpace==null) return;
			CurrentSpace.HasWater = value;
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

    public IEnumerator MakeMoveCoroutine() {
        if (wait) yield break;
        wait = true;

		// BUG: What if player clicks really fast and changes IconSelector.Current before this function can run?
		// TODO: Check if IconSelector really has anything meaningful selected


		MakeMove();
        yield return new WaitForSeconds(0.05f);
        wait = false;
    }

	public void MakeMove() {
		// BUG: What if player clicks really fast and changes IconSelector.Current before this function can run?
		// TODO: Check if IconSelector really has anything meaningful selected


		Tiles oldTileVal = CurrentSpace.Value;
		CurrentSpace.Value = IconSelector.Current;



		Debug.Log ("About to check if board is valid!");

		/* Check that move is valid */
		if (board.board.IsBoardValid()) {
			if (oldTileVal == CurrentSpace.Value) {
				CurrentSpace.Direction = (Dir)(((int)CurrentSpace.Direction + 1) % 4);
				Debug.Log ("Dir changed! new dir is:");
				Debug.Log (CurrentSpace.Direction);
				var rotation = new Vector3 (0, 90f, 0);
				CurrentTile.transform.Rotate (rotation);
			} else {
				var newTile = IconSelector.CreateTile(IconSelector.Current);
				CurrentTile = newTile;
				//CurrentSpace.Direction = IconSelector.CurrentSelectDir;
				newTile.transform.parent = this.transform;
				newTile.transform.localPosition = Vector3.zero;
			}

			board.board.UpdateWater ();

			Debug.Log ("Valid move made!");
		} else {
			CurrentSpace.Value = oldTileVal;
			Debug.Log ("That was not a valid move!");
		}

		if (clip)
			GetComponent<AudioSource>().PlayOneShot(clip);
	}


    public void OnMouseOver() {
		if (Input.GetButtonUp ("Fire1")) MakeMove();

//        while (!wait) {
//			if (Input.GetButtonUp ("Fire1")) {
//				Debug.Log ("Button press!");
//				yield return StartCoroutine (MakeMoveCoroutine ());
//			}
//            else yield return new WaitForEndOfFrame();
//        }
    }

    public void OnMouseExit() {
        StopAllCoroutines();
    }
}
