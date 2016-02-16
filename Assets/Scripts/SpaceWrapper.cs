/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class SpaceWrapper : MonoBehaviour, ISpace<Tiles> {

    bool wait;
    public float speed = 4f;
    public AudioClip clip;
    public AudioClip failClip;
    public SudokuBoardWrapper board;
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

    public Tiles Value {
        get { return CurrentSpace.Value; }
        set { if (CurrentSpace==null) return;
            CurrentSpace.Value = value;
        }
    }

    public Quaternion TargetRotation {get;set;}

	public Dir Direction {
		get { return CurrentSpace.Direction; }
		set { if (CurrentSpace==null) return;
			CurrentSpace.Direction = value;
            TargetRotation = Quaternion.Euler(0f,(float) value,0f);
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

    void FixedUpdate() {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,TargetRotation,Time.deltaTime*speed);
    }


    public IEnumerator MakingMove() {
        if (wait) yield break;
        wait = true;
		MakeMove();
        yield return new WaitForSeconds(0.05f);
        wait = false;
    }

	public void MakeMove() {
		Tiles oldTileVal = CurrentSpace.Value;
		CurrentSpace.Value = IconSelector.Current;
		if (board.board.IsBoardValid()) {
			if (oldTileVal == CurrentSpace.Value) {
				Direction = (Dir)(((int)Direction+90)%360);
				//Debug.Log ("Dir changed! new dir is:");
				//Debug.Log (CurrentSpace.Direction);
				//var rotation = new Vector3(0,(float) -90,0);
				//CurrentTile.transform.Rotate(rotation);
			} else {
				var newTile = IconSelector.CreateTile(IconSelector.Current);
				CurrentTile = newTile;
				//CurrentSpace.Direction = IconSelector.CurrentSelectDir;
				newTile.transform.parent = this.transform;
				newTile.transform.localPosition = Vector3.zero;
			}

			board.board.UpdateWater();
            if (clip)
                GetComponent<AudioSource>().PlayOneShot(clip);
		} else {
            if (failClip)
                GetComponent<AudioSource>().PlayOneShot(failClip,0.7f);
			CurrentSpace.Value = oldTileVal;
		}
	}


    public void OnMouseOver() {
		if (Input.GetButtonUp ("Fire1")) MakeMove();
//        while (!wait) {
//			if (Input.GetButtonUp("Fire1"))
//				yield return StartCoroutine(MakingMove());
//            else yield return new WaitForEndOfFrame();
//        }
    }

    public void OnMouseExit() {
        StopAllCoroutines();
    }
}
