/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class SudokuBoard<T> : ISudokuBoard<T>, IEnumerable<T>
                where T : ISpace<Tiles> {

    public int Size {
        get { return (int) size; }
    } protected uint size;

    public T Total {
        get { return total; }
    } protected T total;

    public bool IsFinished {
        get { return PlaySequence.Count<=0; } }

	public Queue<T> PlaySequence {
		get { return playSequence; }
        set { playSequence = value; }
	} protected Queue<T> playSequence = new Queue<T>();

	public int MovesCompleted {
		get { return movesCompleted; }
	} protected int movesCompleted;

    public T[,] Board {
        get { return board; }
    } protected T[,] board;

	public int[] EndPos {
		get { return endPos; }
		set { endPos = value; }
	} protected int [] endPos;

    public T this[int x, int y] {
        get { return (IsValidSpace(x,y)?
            (board[x,y]):(default (T))); }
        set { if (!IsValidSpace(x,y)) return;
            board[x,y] = value;
        }
    }

    public SudokuBoard() : this(9) { }

    public SudokuBoard(int size) : this(size, new T[size,size]) { }

    public SudokuBoard(int size, T[,] array) {
        this.size = (uint) size;
        this.board = array;
    }

    public bool IsValidSpace(int x, int y) {
        return ((0<=x && x<Size)
            && (0<=y && y<Size)
            && !board[x,y].IsEmpty); }


	public T GetNextSpace(int x, int y, Dir dir) {
		switch (dir) {
		case Dir.North: return board[x,y+1];
		case Dir.South: return board[x,y-1];
		case Dir.East: return board[x+1,y];
		case Dir.West: return board[x-1,y];
        default: return default(T);
		}
	}

	public int[] GetNextSpaceCoords(int x, int y, Dir dir) {
		switch (dir) {
    		case Dir.East: return new int[] {x+1,y};
    		case Dir.West: return new int[] {x-1,y};
    		case Dir.South: return new int[] {x,y-1};
    		case Dir.North: return new int[] {x,y+1};
            default: return new int[0];
		}
	}

    public IList<T> GetRow(int n) {
        if (0>n || n>=Size)
            throw new System.Exception("Bad row index");
        IList<T> list = new List<T>();
        for (var i=0; i<Size; ++i)
            list.Add(board[i,n]);
        return list;
    }


    public IList<T> GetCol(int n) {
        if (0>n || n>=Size)
            throw new System.Exception("Bad col index");
        IList<T> list = new List<T>();
        for (int i=0; i<Size; ++i)
            list.Add(board[n,i]);
        return list;
    }


    public IList<T> GetBlock(int n) {
        if (0>n || n>=Size*Size)
            throw new System.Exception("Bad block index");
        var x = n / Size;
        var y = n % Size;
        var list = new List<T>();
        for (var i=0; i<Size/2; ++i)
            for (var j=0; i<Size/2; ++j)
                list.Add(board[i+x,j+y]);
        return list;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return ((IEnumerator) board.GetEnumerator());
    }

    public IEnumerator<T> GetEnumerator() {
        return ((IEnumerator<T>) board.GetEnumerator());
    }

    public T GetNext() {
        return PlaySequence.Dequeue();
    }

	public abstract bool Solved();

	public abstract void UpdateWater();

    public abstract bool IsValid(IList<T> list);

    public abstract bool IsRowValid(int i);

    public abstract bool IsColValid(int i);

    public abstract bool IsBlockValid(int i);

	public abstract bool IsBoardValid();

    public abstract int Score();
}







