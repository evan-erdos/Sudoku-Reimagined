/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class IntSudokuBoardWrapper : MonoBehaviour {

	public IntSudokuBoard board;

	public float size = 1f;

	public IList<GameObject> spaces = new List<GameObject>();

	public GameObject prefab;

	void Awake() {
        int[][] array = new int[][] {
        	new int[] {3,7,8,4,9,6,5,2,1},
			new int[] {1,5,9,2,3,7,6,4,8},
			new int[] {2,6,4,5,1,8,7,9,3},
			new int[] {7,4,5,1,6,2,3,8,9},
			new int[] {8,1,3,9,5,4,2,6,7},
			new int[] {9,2,6,8,7,3,1,5,4},
			new int[] {4,8,1,3,2,5,9,7,6},
			new int[] {5,3,7,6,8,9,4,1,2},
			new int[] {6,9,2,7,4,1,8,3,5}};
		if (prefab==null)
			throw new System.Exception("missing space prefab");
		board = new IntSudokuBoard(9, array);
		//var remaining = GetRemainingTiles(board.Size, array);
	}

	void Start() {
		var x=0f;
		foreach (var list in board.Board) {
			x += size;
			var y=0f;
			foreach (var space in list) {
				y -= size;
				var position = transform.position + new Vector3(x,y,0f);
				var instance = (Object.Instantiate(
					prefab, position,
					Quaternion.identity) as GameObject);
				instance.transform.parent = this.transform;
				instance.GetComponent<IntSpaceWrapper>().Value = space.Value;
				spaces.Add(instance);
			}
		}
	}


	public static IList<int> GetRemainingTiles(int size, int[][] array){
        var list = new List<int>();
        var dict = new Dictionary<int, int>();
        for (var i=0; i<=size; ++i) dict[i] = 9;
        foreach (var arr in array)
            foreach (var n in arr) dict[n]--;
        foreach (var kvp in dict)
            for (var i=0; i<kvp.Value; ++i)
            	list.Add(kvp.Key);
        return list;
    }
}







