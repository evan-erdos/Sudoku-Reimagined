/* Ben Scott * bescott@andrew.cmu.edu * 2016-02-03 * Move */

using UnityEngine;


public struct Move<T> {

	public int x,y;

	public T value;

	public Move(int x, int y, T value) {
		this.x = x;
		this.y = y;
		this.value = value;
	}
}
