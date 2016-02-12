/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * TileSudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class TileSudokuBoard : SudokuBoard<ISpace<Tiles>> {

	public Coordinates StartPos;

	public Coordinates EndPos;

	public TileSudokuBoard() : base(9) { }

    public TileSudokuBoard(int size) : base(size) { }

//    public TileSudokuBoard(int size, IList<IList<ISpace<Tiles>>> board)
//    	: base(size,board) { }

    public TileSudokuBoard(int size, ISpace<Tiles>[,] array)
    	: base(size,array) { 
		StartPos = new Coordinates (0, 0);
		EndPos = new Coordinates (3, 3);
	}

	public TileSudokuBoard(int size, ISpace<Tiles>[] playSequence)
		: base(size, playSequence) { }

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
		var uniqueSeen = new HashSet<Tiles> ();
		foreach (var space in list) {
			if (space.Value != Tiles.Default) {
				if (uniqueSeen.Contains(space.Value))
					return false;
				uniqueSeen.Add (space.Value);
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

		//TODO Throw error here
	}

	public override void UpdateWater() {

		//bool waterStreamContinue = false;
		Coordinates currSpaceCoords = new Coordinates(StartPos.x, StartPos.y);
		ISpace<Tiles> currSpace = GetSpace (currSpaceCoords);

		/* For every iteration of this loop, assume:
		 * * currSpace alredy has water
		 * * All previous spaces had water
		 */
		while (currSpace != null && currSpace.Value != Tiles.Default) {
			
			currSpace.HasWater = true;
			Debug.Log ("Space with coords("+ currSpaceCoords.x.ToString() + "," +
				currSpaceCoords.y.ToString() + ") has water! (and dir is:");
			Debug.Log(currSpace.Direction);
			Debug.Log(currSpace.Value);



			Coordinates nextSpaceCoords = GetNextSpaceCoords (currSpaceCoords, currSpace.Direction);
			Debug.Log ("Got next coordinates. They are ("+ nextSpaceCoords.x.ToString() + "," +
				nextSpaceCoords.y.ToString() + ")");
			
			ISpace<Tiles> nextSpace = GetSpace (nextSpaceCoords); // GetNextSpace (currentSpaceCoords, currSpace.Direction);

//			Debug.Log ("Got next space");


			if (nextSpace == null) // Out of bounds
				break;

			Debug.Log(nextSpace.Value);

			
			if (WaterCanFlow (currSpace.Value, nextSpace.Value)) {
				currSpace = nextSpace;
				currSpaceCoords = nextSpaceCoords;
			} else {
				break;
			}
		
		}

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






