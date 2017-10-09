using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SokobanGameLoader : MonoBehaviour {

	public GameObject floor;
	public GameObject target;
	public GameObject wall;
	public GameObject player;
	public GameObject box;
	public TextAsset level;
	public float minSpriteScale;

	private float viewPortTileWidth;
	private float viewPortTileHeight;

	private Vector2 scale;

	private float minScale;
	private Board board;

	// Use this for initialization
	void Start () {
		string[] lines = level.text.Split (new [] {Environment.NewLine}, StringSplitOptions.None);
		int height = lines.Length;
		int width = lines [0].Length;

		Sprite sprite = floor.GetComponent<SpriteRenderer> ().sprite;
		float tileWidth = sprite.textureRect.width / sprite.pixelsPerUnit;
		float tileHeight = sprite.textureRect.height / sprite.pixelsPerUnit / 2;
		float screenHeight = Camera.main.orthographicSize * 2;
		float screenWidth = screenHeight * Screen.width / Screen.height;

		this.minScale = Math.Min (Math.Min (screenWidth / tileWidth / width, screenHeight / tileHeight / height), minSpriteScale);
		this.scale = new Vector2 (minScale, minScale); 

		float hTiles = screenWidth / minScale;
		float vTiles = screenHeight / minScale;
		float maxTiles = Math.Max (hTiles, vTiles);
		int drawnTiles = (int) Math.Floor (maxTiles) + 2;

		this.viewPortTileWidth = 1 / hTiles;
		this.viewPortTileHeight = 1 / vTiles;

		int maxXTiles = drawnTiles; // min: hTiles
		int maxYTiles = drawnTiles; // min: vTiles

		SokobanTile[,] board = new SokobanTile[maxYTiles, maxYTiles];
		this.board = new Board(board);

		for (int x = 0; x < maxXTiles; x++) {
			for (int y = 0; y < maxYTiles; y++) {			
				SokobanTile tile = SokobanTile.WALL;
				int fileX = (int) Math.Ceiling (x - (maxXTiles - width) / 2f);
				int fileY = (int) Math.Ceiling (y - (maxYTiles - height) / 2f);
				if (fileX >= 0 && fileX < width && fileY >= 0 && fileY < height) {
					char c = lines [height - fileY - 1] [fileX];
					tile = Convert (c);
				}
				board[y, x] = tile;
				GameObject newObj = DrawTile (tile, x - (maxXTiles - hTiles) / 2, y - (maxYTiles - vTiles) / 2);
				if (newObj != null && newObj.tag.Equals("Player")) {
					SokobanPlayerController playerController = newObj.GetComponent <SokobanPlayerController> ();
					playerController.scale = minScale;
					playerController.board = this.board;
					this.board.playerPos = new Vector2 (x, y);
				} else if (newObj != null && newObj.tag.Equals("box")) {
					BoxController boxController = newObj.GetComponent <BoxController> ();
					boxController.scale = minScale;
					this.board.AddBox (new Vector2 (x, y), boxController);
				}
			}
		}
	}

	SokobanTile Convert (char c) {
		switch (c) {
		case '#':
			return SokobanTile.WALL;
		case '@':
			return SokobanTile.PLAYER;
		case '+':
			return SokobanTile.PLAYER_ON_TARGET;
		case '.':
			return SokobanTile.TARGET;
		case '$':
			return SokobanTile.BOX;
		case '*':
			return SokobanTile.BOX_ON_TARGET;
		case ' ':
			return SokobanTile.FLOOR;
		}
		throw new Exception ("Unknown char: " + c);
	}

	GameObject DrawTile (SokobanTile tile, float x, float y) {
		GameObject background = floor;
		GameObject obj = null;
		switch (tile) {
		case SokobanTile.WALL:
			obj = wall;
			break;
		case SokobanTile.BOX:
			obj = box;
			break;
		case SokobanTile.PLAYER:
			obj = player;
			break;
		case SokobanTile.BOX_ON_TARGET:
			obj = box;
			background = target;
			break;
		case SokobanTile.PLAYER_ON_TARGET:
			obj = player;
			background = target;
			break;
		case SokobanTile.TARGET:
			background = target;
			break;
		}

		background.transform.localScale = scale;
		Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3 (x * viewPortTileWidth + viewPortTileWidth / 2, y * viewPortTileHeight + viewPortTileHeight / 2, 0.5f));
		Instantiate (background, pos, Quaternion.identity);
		if (obj != null) {
			obj.transform.localScale = scale;
			return Instantiate (obj, pos, Quaternion.identity);
		}
		return null;
	}
}
