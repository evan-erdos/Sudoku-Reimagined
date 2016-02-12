/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SudokuBoardWrapper : MonoBehaviour {

	public bool wait;

	public uint dims = 4;

	public float size = 1f;

	public SudokuBoard<ISpace<Tiles>> board;

	public GameObject prefab;

	public IList<GameObject> spaces = new List<GameObject>();


	SpaceWrapper CreateSpaceWrapper(int x,int y) {
		var instance = Object.Instantiate(
			prefab,
			transform.position+new Vector3(x*size+size,0f,y*size+size),
			Quaternion.identity) as GameObject;
		instance.transform.parent = this.transform;
		instance.GetComponent<SpaceWrapper>().Value = Tiles.Default;
		instance.GetComponent<SpaceWrapper>().board = this;
		return instance.GetComponent<SpaceWrapper>();
	}


	void Awake() {
		if (prefab==null)
			throw new System.Exception("missing spacewrapper prefab");
		var spaceArr = new ISpace<Tiles>[dims, dims];
		for (var i=0; i<dims; ++i)
			for (var j=0; j<dims; ++j)
				spaceArr[i,j] = CreateSpaceWrapper(i,j);
		board = new TileSudokuBoard((int) dims, spaceArr);
	}

	public IEnumerator Restarting() {
		if (wait) yield break;
		wait = true;
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene("Sudoku");
		wait = false;
	}

	public void Restart() {
		StartCoroutine(Restarting()); }

	public ISpace<Tiles> GetNext() {
        return board.GetNext();
    }
}







