using System.Collections;
using UnityEditor;
using UnityEngine;

public class GenerateMap : EditorWindow {

	int gridSizeX = 4;
	int gridSizeY = 4;
	Vector2 physicalSize = new Vector2 (2.5f , 2.5f);
	GameObject wallPrefab, floorPrefab;

	//show in the menu bar
	[MenuItem("Framework/GenerateMap")]
	public static void ShowWindow () {
		//GetWindow (typeof(GenerateMap));
		GenerateMap window = (GenerateMap)EditorWindow.GetWindow(typeof(GenerateMap));
		window.minSize = new Vector2 (400, 300);
		window.maxSize = new Vector2 (450,300);
	}

	// constructor
	public GenerateMap() {
		titleContent = new GUIContent ("Generate Map");
	}

	// content of the GUI is put here
	void OnGUI(){
		GUILayout.BeginVertical ();

		GUILayout.Space (10);
		GUI.skin.label.fontSize = 18;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUILayout.Label ("Self-overlap Structure Generator");
		GUI.skin.label.fontSize = 12;
		GUI.skin.label.alignment = TextAnchor.UpperLeft;


		GUILayout.Space (20);
		GUI.skin.label.fontStyle = FontStyle.Bold;
		GUILayout.Label ("Physical area (m)");
		GUI.skin.label.fontStyle = FontStyle.Normal;
		//GUILayout.FlexibleSpace ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("X:", GUILayout.MaxWidth(30));
		physicalSize.x = EditorGUILayout.FloatField (physicalSize.x);
		GUILayout.Label ("Y:", GUILayout.MaxWidth(30));
		physicalSize.y = EditorGUILayout.FloatField (physicalSize.y);
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();

		GUILayout.Space (15);
		GUI.skin.label.fontStyle = FontStyle.Bold;
		GUILayout.Label ("Size of Grid");
		GUI.skin.label.fontStyle = FontStyle.Normal;

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("X:", GUILayout.MaxWidth(30));
		gridSizeX = EditorGUILayout.IntField (gridSizeX);
		GUILayout.Label ("Y:", GUILayout.MaxWidth(30));
		gridSizeY = EditorGUILayout.IntField (gridSizeY);
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();

		GUILayout.Space (20);
		GUI.skin.label.fontStyle = FontStyle.Bold;
		GUILayout.Label ("Prefabs");
		GUI.skin.label.fontStyle = FontStyle.Normal;

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Wall:", GUILayout.MaxWidth(60));
		wallPrefab = (GameObject)EditorGUILayout.ObjectField (wallPrefab, typeof(GameObject), true);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Floor:", GUILayout.MaxWidth(60));
		floorPrefab = (GameObject)EditorGUILayout.ObjectField (floorPrefab, typeof(GameObject), true);
		GUILayout.EndHorizontal ();

		GUILayout.Space (20);
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		if(GUILayout.Button("Generate Structure", GUILayout.MaxWidth(200))){
			GenerateStructure ();
		}
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();
	}

	void GenerateStructure(){
		Debug.Log("Generating!!!");
		GameObject structure = new GameObject ("VRMaze");
		MazeGenerator generator = structure.AddComponent<MazeGenerator> ();
		generator.SetUpStructure (gridSizeX, gridSizeY, physicalSize, floorPrefab, wallPrefab);
		generator.GenerateCell ();
		this.Close ();
	}
}
