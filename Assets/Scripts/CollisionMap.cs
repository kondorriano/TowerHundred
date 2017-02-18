using UnityEngine;

[CreateAssetMenu(fileName ="Level Collision", menuName = "Level Collision Map")]
public class CollisionMap : ScriptableObject {

    public enum CollisionTile
    {
        Empty = 0,
        Full = 1,
        Length = 2
    }

    [System.Serializable]
    public struct CollisionRow {
        public CollisionTile[] rows;
    }

    [SerializeField]
    public CollisionRow[] columns;
    public Vector2 origin;

    public int width {
        get
        {
            if (columns != null) return columns.Length;
            else return 0;
        }
    }

    public int height
    {
        get
        {
            if (columns != null) return columns[0].rows.Length;
            else return 0;
        }
    }
}
