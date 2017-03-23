using System.Collections;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor (typeof (MazeGenerator))]
public class MazeEditor : Editor {

	/*[MenuItem ("WalkableMaze/Start New Maze")]
	static void DoSomething () {
		Debug.Log ("Starting new maze...");
	}*/

	private static GUIStyle _selectedButton = null;

	private static GUIStyle selectedButton
	{
		get
		{
			if (_selectedButton == null)
			{
				_selectedButton = new GUIStyle(GUI.skin.button);
				_selectedButton.normal.background = _selectedButton.active.background;
				_selectedButton.normal.textColor = _selectedButton.active.textColor;
			}
			return _selectedButton;
		}
	}

	protected TileFill selectedTile = TileFill.REMOVE;

	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();

		MazeGenerator maze = target as MazeGenerator;

		#region Board

		EditorGUILayout.BeginHorizontal();
		foreach (TileFill tile in Enum.GetValues(typeof (TileFill)))
		{
			if (GUILayout.Button(tile.ToString(), (tile == selectedTile ? selectedButton : GUI.skin.button)))
			{
				selectedTile = tile;
			}
		}
		EditorGUILayout.EndHorizontal();

		#endregion
		//maze.GenerateCell ();
	}

	void OnSceneGUI()
	{

		MazeGenerator maze = target as MazeGenerator;


		// mouse attempt
		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		Event e = Event.current;
		Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = ray.origin;





		// cast to world
		/*GameObject gameObj;
		Transform pfb = maze.tilePrefab;// grid.tilePrefab;

		float gridWidth = maze.gridSize.x * maze.tileSize.x;
		float gridHeight = maze.gridSize.y * maze.tileSize.y;

		if(pfb){
			Undo.IncrementCurrentGroup ();
			gameObj = (GameObject)PrefabUtility.InstantiatePrefab (pfb.gameObject);
			Vector3 aligned = new Vector3 (Mathf.Floor (mousePos.x / gridWidth) * gridWidth + gridWidth / 2.0f,
				Mathf.Floor (mousePos.y / gridHeight) * gridHeight + gridHeight / 2.0f, 0.0f);
			gameObj.transform.position = mousePos; //aligned;
			gameObj.transform.parent = GameObject.Find ("GeneratedCell").transform; //grid.transform;
			Undo.RegisterCreatedObjectUndo (gameObj, "Create"+ gameObj.name);
		}

		if (e.isMouse && e.type == EventType.mouseDown) {
			GUIUtility.hotControl = controlID;
			e.Use ();
			Debug.Log ("mouse was down");
		}

		if(e.isMouse && e.type == EventType.MouseUp){
			GUIUtility.hotControl = 0;
			Debug.Log ("mouse was up");
		}*/

		/*Plane plane = new Plane ((GameObject.Find ("Plane")).transform.forward, Vector3.zero);
		float distance;

		if (plane.Raycast (ray, out distance)) {
			Vector3 hitPoint = ray.GetPoint (distance);
			gameObj = (GameObject)PrefabUtility.InstantiatePrefab (pfb.gameObject);
			gameObj.transform.position = hitPoint; //aligned;
			gameObj.transform.parent = GameObject.Find ("GeneratedCell").transform; //grid.transform;
		}*/



		float wallBtnPercent = 0.3f; //to increase the wall button and decrease floor button

		float buttonSize = Mathf.Min(maze.tileSize.x*(1f-wallBtnPercent), maze.tileSize.y*(1f-wallBtnPercent))/2;

		Color handlesColor = Handles.color;

		Handles.color = new Color(66f/256f, 152f/256f, 180f/256f, 0.9f); //(0.3f, 0.3f, 0.3f, 0.3f);

		// FOR FLOOR TILES HOOVERING AND CLICKING
		for (int x = 0; x < maze.gridSize.x; x++)
		{
			for (int y = 0; y < maze.gridSize.y; y++)
			{
				Vector3 position = maze.transform.position + maze.getLocalPos(x, y, TileType.FLOOR);
				if(Handles.Button(position, Quaternion.Euler(Vector3.right*90), buttonSize, buttonSize,
					Handles.SelectionFrame))
				{
					//maze.SetTileValue(x, y, selectedTile);
					maze.DrawOnTile(x,y, TileType.FLOOR, selectedTile);
					Debug.Log ("changing tile " + x + ", " + y);
				}
			}
		}

		// FOR WALL TILES HOOVERING AND CLICKING (2 sections for 2 kinds of walls)
		float buttonSizeWall = Mathf.Min(maze.tileSize.x*wallBtnPercent, maze.tileSize.y*wallBtnPercent)/2;
		Handles.color = new Color(51f/256f, 178f/256f, 21f/256f, 0.9f); 
		for (int x = 0; x <= maze.gridSize.x; x++)
		{
			for (int y = 0; y < maze.gridSize.y; y++)
			{
				Vector3 position = maze.transform.position + maze.getLocalPos(x, y, TileType.WALL1);
				if(Handles.Button(position, Quaternion.Euler(Vector3.right*90), buttonSizeWall, buttonSizeWall,
					Handles.SelectionFrame))
				{
					//maze.SetTileValue(x, y, selectedTile);
					//maze.DrawOnTile(x,y, TileType.WALL1, selectedTile);
					Debug.Log ("changing tile " + x + ", " + y);
				}
			}
		}

		for (int x = 0; x < maze.gridSize.x; x++)
		{
			for (int y = 0; y <= maze.gridSize.y; y++)
			{
				Vector3 position = maze.transform.position + maze.getLocalPos(x, y, TileType.WALL2);
				if(Handles.Button(position, Quaternion.Euler(Vector3.right*90), buttonSizeWall, buttonSizeWall,
					Handles.SelectionFrame))
				{
					//maze.SetTileValue(x, y, selectedTile);
					//maze.DrawOnTile(x,y, TileType.WALL2, selectedTile);
					Debug.Log ("changing tile " + x + ", " + y);
				}
			}
		}

		Handles.color = handlesColor;
	}
}
