using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpriteManager: MonoBehaviour
{
    [SerializeField]
    List<Sprite> TileSprites = new List<Sprite>();
    public  Stack TileSpritesStacked = new Stack();
    void Awake()
    {
        FillSpritesStack();
    }
    void FillSpritesStack()
    {
        TileSprites.Reverse();
        foreach(Sprite s in TileSprites)
        {
            TileSpritesStacked.Push(s);
        }
    }
    public Stack GetTileSpritesStack()
    {
        return (Stack)TileSpritesStacked.Clone();
    }
}
