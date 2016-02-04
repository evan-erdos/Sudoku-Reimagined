/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class IntSudokuBoard : SudokuBoard<int> {

    public override bool IsRowValid(int n) {
        return false; //@TODO
    }

    public override bool IsColValid(int n) {
        return false; //@TODO
    }

    public override bool IsBlockValid(int n) {
        return false; //@TODO
    }

    public override bool IsValid(IList<ISpace<int>> list) {
        return false; //@TODO
    }

    public override bool IsMoveValid(Move<int> move) {
        return false; //@TODO
    }
}







