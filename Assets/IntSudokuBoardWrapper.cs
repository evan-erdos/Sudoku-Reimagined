/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class IntSudokuBoardWrapper : MonoBehaviour {

	public IntSudokuBoard board;

	public float size = 1f;

	public IList<GameObject> spaces = new List<GameObject>();

	public GameObject prefab;

	void Awake() {
		int[][] array = new int[][] {
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0},
			new int[] {0,0,0,0,0,0,0,0,0}};
		if (prefab==null)
			throw new System.Exception("missing space prefab");
		board = new IntSudokuBoard(9, array);
	}

	void Start() {
		var x=0f;
		foreach (var list in board) {
			x += size;
			var y=0f;
			foreach (var space in list) {
				y += size;
				var position = transform.position + new Vector3(x,y,0f);
				var instance = (Object.Instantiate(
					prefab, position,
					Quaternion.identity) as GameObject);
				instance.transform.parent = this.transform;
				instance.GetComponent<IntSpaceWrapper>().space = space;
				spaces.Add(instance);
			}
		}
	}
}







