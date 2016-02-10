/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * TileSudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class TileSudokuBoard : SudokuBoard<ISpace<Tiles>> {


	public TileSudokuBoard() : base(9) { }

    public TileSudokuBoard(int size) : base(size) { }

    public TileSudokuBoard(int size, IList<IList<ISpace<Tiles>>> board)
    	: base(size,board) { }

    public TileSudokuBoard(int size, ISpace<Tiles>[][] array)
    	: base(size,array) { }

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

		/* Convert to a list of ints to make it easier to valid */
		var tiles = new List<ISpace<Tiles>>();
		foreach (var space in list)
			tiles.Add(space);
#if OLD
		tiles.Sort();
		for (int i = 0; i < Size; i++) {
			if (tiles [i] != i + 1)
				return false;
		}
#endif
        return true;
    }

	public override int Score() {
		int totalScore = 0;

		for (int i = 0; i < Size; i++) {
			if (IsRowValid (i))
				totalScore++;
			if (IsColValid (i))
				totalScore++;
			if (IsBlockValid (i))
				totalScore++;
		}

		return totalScore;
	}

    public override bool IsMoveValid(Move<ISpace<Tiles>> move) {
		return board [move.x] [move.y].IsEmpty;
    }
}






