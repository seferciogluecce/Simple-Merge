using UnityEngine;
using System.Collections.Generic;

public class TileMap : MonoBehaviour
{
    List<TileElement> FullTiles = new List<TileElement>();
    List<TileElement> EmptyTiles = new List<TileElement>();
    // Use this for initialization
    void Start()
    {
        foreach (Transform childT in transform)
        {
            childT.GetChild(0).GetComponent<TileElement>().InitTileElement();
            EmptyTiles.Add(childT.GetChild(0).GetComponent<TileElement>());
        }

        InvokeRepeating("CreateNewTile", 0.0f, 2.0f);
    }

    void CreateNewTile()
    {
        if (EmptyTiles.Count == 0)
        {
            Debug.Log("Game Ended!");
        }
        else
        {
            int idx = Random.Range(0, EmptyTiles.Count);
            FillTile(EmptyTiles[idx]);
        }
    }
    public void EmptyTile(TileElement t)
    {
        FullTiles.Remove(t);
        EmptyTiles.Add(t);
    }
    public void FillTile(TileElement t)
    {
        FullTiles.Add(t);
        EmptyTiles.Remove(t);
        t.Appear(true);
    }
}
