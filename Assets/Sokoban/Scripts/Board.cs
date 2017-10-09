using System;
using UnityEngine;
using System.Collections.Generic;

public class Board {

	private SokobanTile[,] board;
	public Vector2 playerPos { private get; set; }
	public Dictionary<Vector2, BoxController> boxes = new Dictionary<Vector2, BoxController> ();

	public Board (SokobanTile[,] board) {
		this.board = board;
	}

	public void AddBox (Vector2 pos, BoxController box) {
		boxes.Add (pos, box);
	}

	public bool Move (Direction dir) {
		Vector2 dirVec = (Vector2)dir.GetTranslation ();
		Vector2 newPos = playerPos + dirVec;
		SokobanTile currTile = Get (newPos);
		if (currTile.Equals (SokobanTile.WALL)) {
			return false;
		}
		if (currTile.Equals (SokobanTile.BOX_ON_TARGET) || currTile.Equals (SokobanTile.BOX)) {
			Vector2 newBoxPos = newPos + dirVec;
			if (Get (newBoxPos) == SokobanTile.WALL 
				|| Get (newBoxPos) == SokobanTile.BOX) {
				return false;
			}
			BoxController box = boxes [newPos];
			boxes.Remove (newPos);
			boxes.Add (newBoxPos, box);
			box.Move (dir);
			boxes.TryGetValue (newPos, out box);
			MoveObj (newPos, newBoxPos);
			MoveObj (playerPos, newPos);
			playerPos = newPos;
			return true;
		}
		if (currTile.Equals (SokobanTile.TARGET) || currTile.Equals (SokobanTile.FLOOR)) {
			MoveObj (playerPos, newPos);
			playerPos = newPos;
			return true;
		}
		return false;
	}

	private void MoveObj (Vector2 src, Vector2 dst) {
		MoveObj (src, dst, SokobanTile.PLAYER, SokobanTile.PLAYER_ON_TARGET);
		MoveObj (src, dst, SokobanTile.BOX, SokobanTile.BOX_ON_TARGET);
	}

	private void MoveObj (Vector2 src, Vector2 dst, SokobanTile obj, SokobanTile objOnTarget) {
		if (Get (src) == obj || Get (src) == objOnTarget) {
			if (Get (src) == objOnTarget) {
				Set (src, SokobanTile.TARGET);
			} else {
				Set (src, SokobanTile.FLOOR);
			}
			if (Get (dst) == SokobanTile.TARGET) {				
				Set (dst, objOnTarget);
			} else {
				Set (dst, obj);
			}
		}
	}

	private SokobanTile Get (Vector2 vec) {
		return board [(int)vec.y, (int)vec.x];
	}

	private void Set (Vector2 vec, SokobanTile tile) {
		board [(int)vec.y, (int)vec.x] = tile;
	}
}


