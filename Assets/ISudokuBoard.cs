/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * ISudokuBoard */

using UnityEngine;
using System.Collections.Generic;


/** `ISudokuBoard<T>` : **`interface`**
 *
 * An interface to the generic `SudokuBoard<T>`.
 *
 * - `T` : **`type`**
 *     the type to be stored in each space of the board
 *
 **/
public interface ISudokuBoard<T> {


	/** `Size` : **`int`**
	 *
	 * size of the sizeXsize square board
	 **/
	int Size {get;}


	/** `Board` : **`ISpace<T>[][]`**
	 *
	 * A list of lists to represent the board. Consider this to
	 * be a "list of rows", and index into it as such.
	 **/
	IList<IList<ISpace<T>>> Board {get;}

	bool IsValid(IList<ISpace<T>> list);

	bool IsRowValid(int n);

	bool IsColValid(int n);

	bool IsBlockValid(int n);

	/** Computes current score of the board */
	int Score ();

	bool IsMoveValid(Move<T> move);
}