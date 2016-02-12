/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * TileSudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class TileSudokuBoard : SudokuBoard<ISpace<Tiles>> {

	public int[] startPos;

	public int[] endPos;

	public TileSudokuBoard() : base(9) { }

    public TileSudokuBoard(int size) : base(size) { }

    public TileSudokuBoard(int size, ISpace<Tiles>[,] array)
    				: base(size,array) {
		startPos = new int[] {0,0};
		endPos = new int[] {3,3};
	}

    public override bool IsRowValid(int n) {
		return IsValid(GetRow(n));
    }

    public override bool IsColValid(int n) {
		return IsValid(GetCol(n));
    }

    public override bool IsBlockValid(int n) {
		return false;//IsValid(GetBlock(n));
    }

    public override bool IsValid(IList<ISpace<Tiles>> list) {

		if (list.Count != Size) return false;

		/* Check that there is no more than 1 of each special piece in line */
		var uniqueSeen = new HashSet<Tiles>();
		foreach (var space in list) {
			if (space.Value != Tiles.Default) {
				if (uniqueSeen.Contains(space.Value))
					return false;
				uniqueSeen.Add(space.Value);
			}
		}

        return true;
    }

	public override bool IsBoardValid() {

		for (int i = 0; i < Size; i++) {
			if (!(IsRowValid (i) && IsColValid (i)))
				return false;
		}

		return  true;
	}

	private bool WaterCanFlow(Tiles currTile, Tiles nextTile) {

		switch (nextTile) {
		case Tiles.Default:
			return false;
		case Tiles.Raise:
			return currTile == Tiles.Spout;
		case Tiles.Level:
			return currTile == Tiles.Raise || currTile == Tiles.Spout;
		case Tiles.Lower:
		case Tiles.Spout:
			return true;
		}
		return false;
	}

	private void ClearWaterFromBoard() {

		for (int i = 0; i < Size; i++)
			for (int j = 0; j < Size; j++)
				board [i, j].HasWater = false;
	}

	public override void UpdateWater() {

		ClearWaterFromBoard ();

		//bool waterStreamContinue = false;
		int x = startPos[0], y = startPos[1];
		ISpace<Tiles> currSpace = this[x,y];

		/* For every iteration of this loop, assume:
		 * * currSpace alredy has water
		 * * All previous spaces had water
		 */
		//int n = 1600;
		while (currSpace != null && currSpace.Value != Tiles.Default &&
				!currSpace.HasWater) {
			//n--;
			//if (n<1)
			//	throw new System.Exception("holy shit!");
			currSpace.HasWater = true;
			Debug.Log("Space with coords("+ x + "," +
				y + ") has water! (and dir is:");
			Debug.Log(currSpace.Direction);
			Debug.Log(currSpace.Value);

			int[] next = GetNextSpaceCoords(x, y, currSpace.Direction);
			Debug.Log("Got next coordinates. They are ("+ next[0] + "," +
				next[1] + ")");

			ISpace<Tiles> nextSpace = this[next[0],next[1]];
			if (nextSpace==null) break; //out of bounds

			Debug.Log(nextSpace.Value);

			if (WaterCanFlow(currSpace.Value, nextSpace.Value)) {
				currSpace = nextSpace;
				x = next[0];
				y = next[1];
			} else break;
		}
		Debug.Log ("Done Updating water");

	}

	public override int Score() {
//		int totalScore = 0;
//
//		for (int i = 0; i < Size; i++) {
//			if (IsRowValid (i))
//				totalScore++;
//			if (IsColValid (i))
//				totalScore++;
//			if (IsBlockValid (i))
//				totalScore++;
//		}
//
//		return totalScore;
		return 0;
	}
}






