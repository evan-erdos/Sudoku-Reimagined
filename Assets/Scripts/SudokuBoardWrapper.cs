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

	public GameObject[] prefabs;

	public IList<GameObject> spaces = new List<GameObject>();

	int[,] normalBoard = {
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
	};

	int[,] tutorialBoard = {
		{0,5,5,5},
		{0,5,5,5},
		{0,6,0,5},
		{0,0,0,5},
	};
		

		
	SpaceWrapper CreateSpaceWrapper(int x, int y, Tiles value) {
		var instance = Object.Instantiate(prefab,
			transform.position+new Vector3(x*size+size,0f,y*size+size),
			Quaternion.identity) as GameObject;
		instance.transform.parent = this.transform;
		// TODO: This is where we create the default space
		instance.GetComponent<SpaceWrapper>().Value = value;
		var tile = Object.Instantiate(prefabs[(int)value],
			transform.position+new Vector3(x*size+size,0f,y*size+size),
			Quaternion.identity) as GameObject;
		tile.transform.parent = instance.transform;
		tile.transform.localPosition = Vector3.zero;
		instance.GetComponent<SpaceWrapper>().CurrentTile = tile;
		instance.GetComponent<SpaceWrapper>().board = this;
//		for (var i=0; i<Random.Range(0,3); ++i)
//			instance.GetComponent<SpaceWrapper>().RotateTile();
		return instance.GetComponent<SpaceWrapper>();
	}

	GameObject RandomSpace() {
		return prefabs[Random.Range(0,prefabs.Length)]; }


	void Awake() {

		Debug.Log ("Awake on Sudoku board wrapper!");


		int[,] boardArr;
		switch (SceneManager.GetActiveScene().name) {
		case "Tutorial":
			boardArr = tutorialBoard;
			break;
		case "MainGame":
		default:
			Debug.Log ("name is " + SceneManager.GetActiveScene ().name);
			boardArr = normalBoard;
			break;
		}

		if (prefab==null)
			throw new System.Exception("missing spacewrapper prefab");
		var spaceArr = new ISpace<Tiles>[dims, dims];
		for (var i = 0; i < dims; ++i)
			for (var j = 0; j < dims; ++j) {
				spaceArr [i, j] = CreateSpaceWrapper (i, j, (Tiles)boardArr [i, j]);
				Debug.Log ("Set Tile " + boardArr [i, j].ToString () + " at position" + i.ToString () + ", " + j.ToString());

			}
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







