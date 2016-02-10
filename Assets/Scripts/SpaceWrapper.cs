/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Space */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class SpaceWrapper : MonoBehaviour, ISpace<Tiles> {

    public GameObject prefab;

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
        GetComponent<Rigidbody>().isKinematic = true;
        var tile = Object.Instantiate(prefab) as GameObject;
        tile.transform.parent = this.transform;
        tile.transform.localPosition = Vector3.zero;
        Current = tile;
    }

    public IEnumerator MakeMove() {
        if (wait) yield break;
        wait = true;
        var tile = IconSelector.CreateTile(IconSelector.Current);
        tile.transform.parent = this.transform;
        tile.transform.localPosition = Vector3.zero;
        Current = tile;
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
