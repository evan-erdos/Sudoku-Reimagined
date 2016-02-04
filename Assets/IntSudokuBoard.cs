/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class IntSudokuBoard : SudokuBoard<int> {


	public IntSudokuBoard() : base(9) { }

    public IntSudokuBoard(int size) : base(size) { }

    public IntSudokuBoard(int size, IList<IList<ISpace<int>>> board)
    	: base(size,board) { }

    public IntSudokuBoard(int size, int[][] array)
    	: base(size,array) { }

	public IntSudokuBoard(int size, int[] playSequence)
		: base(size, playSequence) { }

    public override bool IsRowValid(int n) {
		return IsValid(GetRow(n));
    }

    public override bool IsColValid(int n) {
		return IsValid(GetCol(n));
    }

    public override bool IsBlockValid(int n) {
		return IsValid(GetBlock(n));
    }

    public override bool IsValid(IList<ISpace<int>> list) {
		if (list.Count != Size) return false;

		/* Convert to a list of ints to make it easier to valid */
		var intList = new List<int>();
		foreach (var space in list)
			intList.Add(space.Value);

		intList.Sort();
		for (int i = 0; i < Size; i++) {
			if (intList [i] != i + 1)
				return false;
		}

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

    public override bool IsMoveValid(Move<int> move) {
		return board [move.x] [move.y].IsEmpty;
    }
}







