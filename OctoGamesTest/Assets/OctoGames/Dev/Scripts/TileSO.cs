using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Tile/NewTile")]
public class TileSO : ScriptableObject
{
    public int tileId;
    public Sprite tileSprite;
}
