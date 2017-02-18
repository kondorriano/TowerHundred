using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelCollisionEditor : EditorWindow {

    [MenuItem("Window/Level Collision Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelCollisionEditor));
    }

    CollisionMap map;
    Vector2 deltaPosition = new Vector2(-0.5f, -0.5f);

    void OnGUI()
    {
        //GUILayout.Label("Current scene is "+ UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name, EditorStyles.boldLabel);
        map = (CollisionMap)EditorGUILayout.ObjectField("Collision map", map, typeof(CollisionMap), false);
        deltaPosition = EditorGUILayout.Vector2Field("Delta position", deltaPosition);

        if (GUILayout.Button("Process level collision")) {
            ProcessLevel();
        }
    }

    bool mapIsAsset = false;
    void RetrieveMap() {
        Object mapAsset = Resources.Load(CurrentSceneMapName());

        if (mapAsset == null)
        {
            map = (CollisionMap)ScriptableObject.CreateInstance<CollisionMap>();
            mapIsAsset = false;
        }
        else {
            map = (CollisionMap)mapAsset;
            mapIsAsset = true;
        }
    }

    void SaveMap() {
        if (!mapIsAsset) {
            AssetDatabase.CreateAsset(map, "Assets\\Resources\\"+CurrentSceneMapName() + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        EditorGUIUtility.PingObject(map);
        EditorUtility.SetDirty(map);
    }

    void ProcessLevel()
    {
        RetrieveMap();

        GameObject[] gOs = GameObject.FindGameObjectsWithTag("Level");
        Vector2[] minExtends = new Vector2[gOs.Length];
        Vector2[] maxExtends = new Vector2[gOs.Length];

        Vector2 min = new Vector2(Mathf.Infinity, Mathf.Infinity); ;
        Vector2 max = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);
        for (int i = 0; i < gOs.Length; ++i) {
            Transform t = gOs[i].transform;
            minExtends[i] = new Vector2(Mathf.Ceil(t.position.x - deltaPosition.x - t.localScale.x / 2f), Mathf.Ceil(t.position.y - deltaPosition.y - t.localScale.y / 2f));
            maxExtends[i] = new Vector2(Mathf.Floor(t.position.x - deltaPosition.x + t.localScale.x / 2f), Mathf.Floor(t.position.y - deltaPosition.y + t.localScale.y / 2f));

            if (min.x > minExtends[i].x) min.x = minExtends[i].x;
            if (min.y > minExtends[i].y) min.y = minExtends[i].y;
            if (max.x < maxExtends[i].x) max.x = maxExtends[i].x;
            if (max.y < maxExtends[i].y) max.y = maxExtends[i].y;
        }

        int width = Mathf.RoundToInt(max.x - min.x) + 1;
        int height = Mathf.RoundToInt(max.y - min.y) + 1;

        map.columns = new CollisionMap.CollisionRow[width];
        for (int i = 0; i < map.width; ++i) {
            map.columns[i].rows = new CollisionMap.CollisionTile[height];
            Utilities.InitializeArray(ref map.columns[i].rows, CollisionMap.CollisionTile.Empty);
        }
        map.origin = min;


        for (int i = 0; i < gOs.Length; ++i)
        {
            int minX = Mathf.RoundToInt(minExtends[i].x - min.x);
            int maxX = Mathf.RoundToInt(maxExtends[i].x - min.x);
            int minY = Mathf.RoundToInt(minExtends[i].y - min.y);
            int maxY = Mathf.RoundToInt(maxExtends[i].y - min.y);
            for (int x = minX; x <= maxX; ++x) {
                for (int y = minY; y <= maxY; ++y)
                {
                    map.columns[x].rows[y] = CollisionMap.CollisionTile.Full;
                }
            }
        }

        map.origin += deltaPosition;
        
        SaveMap();
    }

    string CurrentSceneMapName() {
        return "CollisionMaps\\" + UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name;
    }
}
