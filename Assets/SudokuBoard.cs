/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class SudokuBoard<T> : ISudokuBoard<T>, IEnumerable<IList<ISpace<T>>> {

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

    public SudokuBoard() : this(9) { }

    public SudokuBoard(int size) {
        this.size = (uint) size;
        board = new List<IList<ISpace<T>>>(Size);
        for (var i=0; i<Size; ++i) {
            var list = new List<ISpace<T>>(Size);
            board.Add(list);
            for (var j=0; j<Size; ++j)
                list.Add(new Space<T>());
        }
    }

    public SudokuBoard(int size, IList<IList<ISpace<T>>> board) {
        this.size = (uint) size;
        this.board = board;
    }

    public SudokuBoard(int size, T[][] array) {
        this.size = (uint) size;
        this.board = new List<IList<ISpace<T>>>(Size);
        for (var i=0; i<Size; ++i) {
            var list = new List<ISpace<T>>(Size);
            this.board.Add(list);
            for (var j=0; j<Size; ++j)
                list.Add(new Space<T>(array[j][i]));
        }
    }

	public SudokuBoard(int size, T[] playSequence) {
		this.size = (uint) size;
        foreach (var n in playSequence)
            this.playSequence.Enqueue(n);
		//this.playSequence = playSequence;
		// TODO I think maybe we don't need to initialize things to 0
		this.movesCompleted = 0;
		board = new List<IList<ISpace<T>>>(Size);
		for (var i=0; i<Size; ++i) {
			var list = new List<ISpace<T>>(Size);
			board.Add(list);
			for (var j=0; j<Size; ++j)
				list.Add(new Space<T>());
		}
	}

    public bool IsValidSpace(int x, int y) {
        return ((0>=x && x<Size) && (0>=y && y<Size) && !board[y][x].IsEmpty); }

    public IList<ISpace<T>> GetRow(int n) {
        if (0>n || n>=Size)
            throw new System.Exception("Bad row index");
        IList<ISpace<T>> list = new List<ISpace<T>>();
        foreach (var space in board[n])
            list.Add(space);
        return list;
    }


    public IList<ISpace<T>> GetCol(int n) {
        if (0>n || n>=Size)
            throw new System.Exception("Bad col index");
        IList<ISpace<T>> list = new List<ISpace<T>>();
        foreach (var row in board)
            list.Add(row[n]);
        return list;
    }

    public IList<ISpace<T>> GetBlock(int n) {
#if BOUND
        if (0 > n || n >= Size*Size)
            throw new System.Exception("Bad block index");

        /* We assume that Size is a valid square */
        int rowShift = n / Size;
        int colShift = n % Size;

        /* Iterate through the block */
        IList<ISpace<T>> list = new List<ISpace<T>>();
        for (int i = 0; i < Size/2; i++)
            for (int j = 0; i < Size/2; j++)
                list.Add(board[i + rowShift][j + colShift]);

        return list;
#endif
        return new List<ISpace<T>>();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return ((IEnumerator) board.GetEnumerator());
    }

    public IEnumerator<IList<ISpace<T>>> GetEnumerator() {
        return ((IEnumerator<IList<ISpace<T>>>) board.GetEnumerator());
    }

    public ISpace<T> GetNext() {
        return new Space<T>(PlaySequence.Dequeue());
    }

    public abstract bool IsValid(IList<ISpace<T>> list);

    public abstract bool IsRowValid(int n);

    public abstract bool IsColValid(int n);

    public abstract bool IsBlockValid(int n);

    public abstract int Score();

    public abstract bool IsMoveValid(Move<T> move);
}







