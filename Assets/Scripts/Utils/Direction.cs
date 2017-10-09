using System;
using UnityEngine;

public enum Direction {
	LEFT, UP, RIGHT, DOWN, NONE
}

public static class DirectionMethods {
	public static Vector3 GetTranslation (this Direction dir) {
		int x = 0;
		int y = 0;
		switch (dir) {
		case Direction.LEFT:
			x = -1;
			break;
		case Direction.UP:
			y = 1;
			break;
		case Direction.RIGHT:
			x = 1;
			break;
		case Direction.DOWN:
			y = -1;
			break;
		case Direction.NONE:
			return Vector3.zero;
		}
		return new Vector3 (x, y, 0) ;
	}

	public static int GetRotation (this Direction dir) {
		switch (dir) {
		case Direction.LEFT:
			return 180;
		case Direction.UP:
			return 90;
		case Direction.RIGHT:
			return 0;
		case Direction.DOWN:
			return -90;
		case Direction.NONE:
			return 0;
		}
		return 0;
	}
}

