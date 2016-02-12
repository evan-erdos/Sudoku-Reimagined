/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * SudokuBoard */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class SudokuBoard<T> : ISudokuBoard<T>, IEnumerable<IList<T>>
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

    public IList<IList<T>> Board {
        get { return board; }
    } protected IList<IList<T>> board;

    public T this[int x, int y] {
        get { return (IsValidSpace(x,y)?
            (board[y][x]):(default (T))); }
        set { if (!IsValidSpace(x,y)) return;
            board[x][y] = value;
        }
    }

    public SudokuBoard() : this(9) { }

    public SudokuBoard(int size) {
        this.size = (uint) size;
        board = new List<IList<T>>(Size);
        for (var i=0; i<Size; ++i) {
            var list = new List<T>(Size);
            board.Add(list);
            //for (var j=0; j<Size; ++j)
            //    list.Add(new T());
        }
    }

//    public SudokuBoard(int size, IList<IList<T>> board) {
//        this.size = (uint) size;
//        this.board = board;
//    }

    public SudokuBoard(int size, T[,] array) {
        this.size = (uint) size;
        this.board = new List<IList<T>>(Size);
        for (var i=0; i<Size; ++i) {
            var list = new List<T>(Size);
            this.board.Add(list);
			for (var j=0; j<Size; ++j)
                list.Add(array[i,j]);
        }
    }

	public SudokuBoard(int size, T[] playSequence) {
		this.size = (uint) size;
        foreach (var n in playSequence)
            this.playSequence.Enqueue(n);
		//this.playSequence = playSequence;
		// TODO I think maybe we don't need to initialize things to 0
		this.movesCompleted = 0;
		board = new List<IList<T>>(Size);
		for (var i=0; i<Size; ++i) {
			var list = new List<T>(Size);
			board.Add(list);
			//for (var j=0; j<Size; ++j)
			//	list.Add(new Space<T>());
		}
	}

    public bool IsValidSpace(int x, int y) {
        return ((0>=x && x<Size) && (0>=y && y<Size) && !board[y][x].IsEmpty); }

	public T GetSpace(Coordinates coords) {
		if (!( 0 <= coords.x && coords.x < Size && 
			0 <= coords.y && coords.y < Size))
			return default(T);
		
		return board[coords.x][coords.y];
	}

	public T GetNextSpace(Coordinates coords, Dir dir) {
		switch (dir) {
		case Dir.Up:
			return board [coords.x] [coords.y - 1];
		case Dir.Down:
			return board [coords.x] [coords.y + 1];
		case Dir.Left:
			return board [coords.x - 1] [coords.y];
		case Dir.Right:
			return board [coords.x + 1] [coords.y];
		}
		return default(T);
	}

	public Coordinates GetNextSpaceCoords(Coordinates coords, Dir dir) {
		switch (dir) {
		case Dir.Up:
			return new Coordinates (coords.x, coords.y - 1);
		case Dir.Down:
			return new Coordinates (coords.x, coords.y + 1);
		case Dir.Left:
			return new Coordinates (coords.x - 1, coords.y);
		case Dir.Right:
			return new Coordinates (coords.x + 1, coords.y);
		}
		return null;
	}

    public IList<T> GetRow(int n) {
        if (0>n || n>=Size)
            throw new System.Exception("Bad row index");
        IList<T> list = new List<T>();
        foreach (var space in board[n])
            list.Add(space);
        return list;
    }


    public IList<T> GetCol(int n) {
        if (0>n || n>=Size)
            throw new System.Exception("Bad col index");
        IList<T> list = new List<T>();
        foreach (var row in board)
            list.Add(row[n]);
        return list;
    }

    public IList<T> GetBlock(int n) {
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
        return new List<T>();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return ((IEnumerator) board.GetEnumerator());
    }

    public IEnumerator<IList<T>> GetEnumerator() {
        return ((IEnumerator<IList<T>>) board.GetEnumerator());
    }

    public T GetNext() {
        return PlaySequence.Dequeue();
    }

	public abstract void UpdateWater ();

    public abstract bool IsValid(IList<T> list);

    public abstract bool IsRowValid(int i);

    public abstract bool IsColValid(int i);

    public abstract bool IsBlockValid(int i);

	public abstract bool IsBoardValid ();

    public abstract int Score();
}







