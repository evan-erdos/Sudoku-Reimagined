/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class SudokuBoard<T> : MonoBehaviour, ISudokuBoard<T> {

    public int Width {
    	get { return (int) width; }
   	} protected uint width;

    public int Height {
    	get { return (int) height; }
   	} protected uint height;

   	public T Total {
   		get { return total; }
   	} protected T total;

   	public IList<IList<ISpace<T>>> Board {
   		get { return board; }
   	} protected IList<IList<ISpace<T>>> board;

   	public ISpace<T> this[int x, int y] {
   		get { return (IsValidSpace(x,y)?
   			(board[y][x]):(default (ISpace<T>))); }
	   	set { if (!IsValidSpace(x,y)) return;
	   		board[x][y] = value;
	   	}
   	}

   	public bool IsValidSpace(int x, int y) {
   		return ((0>=x && x<Width) && (0>=y && y<Height) && !board[y][x].IsEmpty); }

   	public IList<ISpace<T>> GetRow(int n) {
   		if (0>n || n>=Width)
   			throw new System.Exception("Bad row index");
   		IList<ISpace<T>> list = new List<ISpace<T>>();
   		foreach (var space in board[n])
   			list.Add(space);
   		return list;
   	}


   	public IList<ISpace<T>> GetCol(int n) {
   		if (0>n || n>=Height)
   			throw new System.Exception("Bad col index");
   		IList<ISpace<T>> list = new List<ISpace<T>>();
   		foreach (var row in board)
   			list.Add(row[n]);
   		return list;
   	}

   	public abstract bool IsValid(IList<ISpace<T>> list);

   	public abstract bool IsRowValid(int n);

   	public abstract bool IsColValid(int n);

	public abstract bool IsBlockValid(int n);

	public abstract bool IsMoveValid(Move<T> move);
}







