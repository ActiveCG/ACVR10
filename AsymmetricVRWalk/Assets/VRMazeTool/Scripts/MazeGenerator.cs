using System.Collections;
using UnityEngine;

/*public class Tile{
	public enum Fill{
		EMTPY,
		FULL
	}
	Fill fill = Fill.EMTPY;
}

public class FloorTile : Tile{
	bool transition
}

public class WallTile : Tile{

}*/
public enum TileFill{
	REMOVE,
	FLOOR
}

public enum TileType{
	FLOOR,
	WALL1,
	WALL2
}

public class MazeGenerator : MonoBehaviour {



	public Transform tilePrefab;
	public Transform wallTilePrefab;
	[HideInInspector]
	public Vector2 gridSize;
	[HideInInspector]
	public Vector2 sizeInMeters;
	[HideInInspector]
	public float wallWidthInMeters;
	[HideInInspector]
	public Vector2 tileSize;


	private TileFill[,] tiles;
	private GameObject[,] tileMesh;

	/*void Start(){
		GenerateCell ();
	}*/

	public void SetUpStructure(int gridSizeX, int gridSizeY, Vector2 physicalArea, GameObject floor, GameObject wall){
		gridSize = new Vector2 (gridSizeX, gridSizeY);
		sizeInMeters = physicalArea;
		wallWidthInMeters = 0.1f;
		tileSize = new Vector2 (sizeInMeters.x / gridSize.x, sizeInMeters.y / gridSize.y);
		tilePrefab = floor.transform;
		wallTilePrefab = wall.transform;
	}

	public void GenerateCell(){

		string holderName = "GeneratedCell";
		if (transform.FindChild (holderName)) {
			DestroyImmediate (transform.FindChild (holderName).gameObject);
		}

		Transform cellHolder = new GameObject (holderName).transform;
		cellHolder.parent = transform;

		tiles = new TileFill[(int)gridSize.x,(int)gridSize.y];
		tileMesh = new GameObject[(int)gridSize.x,(int)gridSize.y];
		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				tiles [x, y] = TileFill.REMOVE;
				tileMesh [x, y] = null;
			}
		}

		/*for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				Vector3 tilePosition = new Vector3 ((-gridSize.x / 2 + 0.5f + x) * tileSize.x, 0, (-gridSize.y/2 + 0.5f + y) * tileSize.y);
				Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right*90)) as Transform;
				//Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
				newTile.localScale = new Vector3 (tileSize.x - wallWidthInMeters / 2f, tileSize.y - wallWidthInMeters / 2f, tileSize.y - wallWidthInMeters / 2f);
				newTile.parent = cellHolder;
			}
		}


		for (int x = 0; x <= gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				Vector3 wallTilePosition = new Vector3 ((-gridSize.x / 2 + x) * tileSize.x,
					0, (-gridSize.y/2 + 0.5f + y) * tileSize.y);
				Transform newWallTile = Instantiate(wallTilePrefab, wallTilePosition, Quaternion.Euler(Vector3.right*90)) as Transform;
				//Transform newWallTile = Instantiate(wallTilePrefab, wallTilePosition, Quaternion.Euler(*Vector3.up*90)) as Transform;
				newWallTile.localScale = new Vector3 (wallWidthInMeters, tileSize.x - wallWidthInMeters/2f, 1);
				newWallTile.parent = cellHolder;
			}
		}
		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y <= gridSize.y; y++) {
				Vector3 wallTilePosition2 = new Vector3 ((-gridSize.x / 2 + 0.5f + x) * tileSize.x,
					                            0, (-gridSize.y / 2 + y) * tileSize.y);
				Transform newWallTile2 = Instantiate (wallTilePrefab, wallTilePosition2, Quaternion.Euler (Vector3.right*90)) as Transform;
				//Transform newWallTile2 = Instantiate (wallTilePrefab, wallTilePosition2, Quaternion.Euler (Vector3.right)) as Transform;
				newWallTile2.localScale = new Vector3 (tileSize.y - wallWidthInMeters/2f, wallWidthInMeters, 1);
				newWallTile2.parent = cellHolder;
			}
		}*/
	}

	public Vector3 getLocalPos(int x, int y, TileType tile){
		if(tile == TileType.FLOOR)
			return new Vector3 ((-gridSize.x / 2 + 0.5f + x) * tileSize.x, 0, (-gridSize.y/2 + 0.5f + y) * tileSize.y);
		if(tile == TileType.WALL1)
			return new Vector3 ((-gridSize.x / 2 + x) * tileSize.x, 0, (-gridSize.y/2 + 0.5f + y) * tileSize.y);
		if(tile == TileType.WALL2)
			return new Vector3 ((-gridSize.x / 2 + 0.5f + x) * tileSize.x, 0, (-gridSize.y / 2 + y) * tileSize.y);
		return Vector3.zero;
	}

	public void DrawOnTile(int x, int y, TileType tile, TileFill selectedTool){
		if (selectedTool == TileFill.FLOOR) {
			if (tiles [x, y] == TileFill.REMOVE) {
				Vector3 tilePosition = getLocalPos (x, y, tile);
				Transform newTile = Instantiate (tilePrefab, tilePosition, Quaternion.Euler (Vector3.right)) as Transform;
				newTile.parent = transform.FindChild ("GeneratedCell");
				tiles [x, y] = TileFill.FLOOR;
				tileMesh [x, y] = newTile.gameObject;
				Debug.Log ("drawing");
			}
		} else {
			if (tiles [x, y] == TileFill.FLOOR) {
				DestroyImmediate(tileMesh[x,y]);
				tiles [x, y] = TileFill.REMOVE;
				tileMesh [x, y] = null;
				Debug.Log ("removing shit");
			}
		}
	}
}
