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
            new int[] {0,0,0,0,4,0,0,9,0},
            new int[] {5,2,9,3,0,0,7,4,1},
            new int[] {0,7,0,0,0,5,6,2,8},
            new int[] {0,1,0,5,6,0,0,0,0},
            new int[] {0,0,0,1,0,0,2,0,3},
            new int[] {9,0,7,2,0,0,0,1,6},
            new int[] {4,0,0,8,0,0,0,0,0},
            new int[] {0,3,0,0,0,0,0,6,0},
            new int[] {0,0,0,0,9,3,5,7,0}};
		if (prefab==null)
			throw new System.Exception("missing space prefab");
		board = new IntSudokuBoard(9, array);
		var remaining = GetRemainingTiles(board.Size, array);
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







