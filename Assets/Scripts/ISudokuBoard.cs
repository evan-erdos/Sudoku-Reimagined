/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * ISudokuBoard */

using UnityEngine;
using System.Collections.Generic;


/** `ISudokuBoard<T>` : **`interface`**
 *
 * An interface to the generic `SudokuBoard<T>`.
 *
 * - `T` : **`ISpace<Tiles>`**
 *     the type to be stored in each space of the board
 *
 **/
public interface ISudokuBoard<T>
				where T : ISpace<Tiles> {


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
	IList<IList<T>> Board {get;}

	Queue<T> PlaySequence {get;set;}

	bool IsValid(IList<T> list);

	bool IsRowValid(int n);

	bool IsColValid(int n);

	bool IsBlockValid(int n);

	bool IsBoardValid ();

	int Score();

	T GetNext();
}