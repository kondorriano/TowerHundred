using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject WallPrefab;

    public CollisionMap map;
    [Header("Gizmos")]
    public Color gizmoColor = Color.black;
    
	// Use this for initialization
	void Start () {

	}

    public void InstantiateVisualColliders()
    {
        for(int x = 0; x < map.width; ++x)
        {
            for (int y = 0; y < map.height; ++y)
            {
                switch (map.columns[x].rows[y]) {
                    case CollisionMap.CollisionTile.Full:
                        InstantiateOn(x + map.origin.x, map.origin.y + y, WallPrefab);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    GameObject InstantiateOn(float x, float y, GameObject toInstantiate)
    {
        GameObject obj = Instantiate(toInstantiate);
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = new Vector3(x, y, 0);

        return obj;
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) {
            Gizmos.color = gizmoColor;

            if (map != null && map.columns != null)
            {
                for (int x = 0; x < map.width; ++x)
                {
                    for (int y = 0; y < map.height; ++y)
                    {
                        switch (map.columns[x].rows[y])
                        {
                            case CollisionMap.CollisionTile.Full:
                                Gizmos.DrawCube(new Vector3(x + map.origin.x, map.origin.y + y, 0), Vector3.one);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
