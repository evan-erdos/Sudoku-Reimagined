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


	/** `Width` : **`int`**
	 *
	 * width of the board
	 **/
	int Width {get;}


	/** `Height` : **`int`**
	 *
	 * height of the board
	 **/
	int Height {get;}


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

	bool IsMoveValid(Move<T> move);
}