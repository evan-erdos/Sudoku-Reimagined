/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
public class SpaceWrapper : MonoBehaviour, ISpace<Tiles> {

    public GameObject Current {
        get { return current; }
        set { Destroy(current);
            current = value;
        }
    } GameObject current;

    public Space<Tiles> CurrentSpace {
        get { if (current==null) return default (Space<Tiles>);
            return current.GetComponent<Space<Tiles>>(); } }

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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (GetComponentInChildren<Renderer>()==null)
            throw new System.Exception("No renderer");
    }

    public IEnumerator MakeMove() {
        if (wait) yield break;
        wait = true;
        var tile = IconSelector.CreateTile(IconSelector.Current);
        tile.transform.parent = this.transform;
        tile.transform.localPosition = Vector3.zero;
        Current = tile;
        //if (clip)
        //    AudioSource.PlayClipAtPoint(clip, new Vector3(0f,0f,-10f));
        //if (board.board.IsFinished) board.Restart();
        yield return new WaitForSeconds(0.05f);
        wait = false;
    }

    public IEnumerator OnMouseOver() {
        while (!wait) {
            if (Input.GetButtonDown("Fire1"))
                yield return StartCoroutine(MakeMove());
            else yield return new WaitForEndOfFrame();
        }
    }

    public void OnMouseExit() {
        StopAllCoroutines();
    }
}
