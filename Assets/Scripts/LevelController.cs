using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject WallPrefab;


    class LevelData
    {
        public Vector2 size;
        public string layout;
        
        public LevelData(Vector2 ls, string ll)
        {
            size = ls;
            layout = ll.Replace("\n", "");
        }
    }

    LevelData[] levels =
    {
        new LevelData(new Vector2(20,20), testLevel)
    };

    static string testLevel = @"
W W W W W W W W W W W W W W W W W W W W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W                                     W 
W W W W W W W W W W W W W W W W W W W W ";
	// Use this for initialization
	void Start () {
        LoadLevel(levels[0]);
	}

    void LoadLevel(LevelData level)
    {
        for(int x = 0; x < level.size.x; ++x)
        {
            for (int y = 0; y < level.size.y; ++y)
            {
                char tile0 = level.layout[y * (int)level.size.x * 2 + x * 2];
                switch(tile0)
                {
                    case 'W': //GUOL
                        InstantiateOn(x, (int)level.size.y-y-1, WallPrefab);
                        break;
                    default:
                        break;

                };
                //char tile1 = level.layout[y * (int)level.size.x * 2 + x * 2 + 1];

            }
        }
    }

    GameObject InstantiateOn(int x, int y, GameObject toInstantiate)
    {
        GameObject obj = Instantiate(toInstantiate);
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = new Vector3(x, y, 0);

        return obj;
    }

}
