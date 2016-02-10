/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SudokuBoardWrapper : MonoBehaviour {

	public bool wait;

	public uint dimensions = 4;

	public float size = 1f;

	public SudokuBoard<ISpace<Tiles>> board;

	public GameObject prefab;

	public IList<GameObject> spaces = new List<GameObject>();


	SpaceWrapper CreateWrapper(int x,int y) {
		var instance = (Object.Instantiate(
			prefab, transform.position + new Vector3(x*size+size,0f,y*size+size),
			Quaternion.identity) as GameObject);
		instance.transform.parent = this.transform;
		instance.GetComponent<SpaceWrapper>().Value = Tiles.Default;
		instance.GetComponent<SpaceWrapper>().board = this;
		return instance.GetComponent<SpaceWrapper>();
	}


	void Awake() {
		if (prefab==null)
			throw new System.Exception("missing spacewrapper prefab");
		var arr = new ISpace<Tiles>[dimensions, dimensions];
		for (var i=0; i<dimensions; ++i)
			for (var j=0; j<dimensions; ++j)
				arr[i,j] = CreateWrapper(i,j);
		board = new TileSudokuBoard((int) dimensions);
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

	public static IList<ISpace<Tiles>> GetRemainingTiles(int size, int[][] array) {
#if IMPL
        var list = new List<ISpace<Tiles>>();
        var dict = new Dictionary<ISpace<Tiles>,int>();
        for (var i=0; i<=size; ++i) dict[i] = 9;
        foreach (var arr in array)
            foreach (var n in arr) dict[n]--;
        foreach (var kvp in dict)
            for (var i=0; i<kvp.Value; ++i)
            	list.Add(kvp.Key);
        return list;
#else
		return new List<ISpace<Tiles>>();
#endif
    }
}







