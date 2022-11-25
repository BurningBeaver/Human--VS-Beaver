using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class WaterManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap waterLoadTilemap;
    public Tilemap damTilemap;

    public TileBase waterLoadAllowTile;
    public TileBase waterTile;
    public TileBase damTile;

    private Dictionary<Vector3Int, TileBase> tiles;
    private int maxY;

    private void Awake()
    {
        tiles = new Dictionary<Vector3Int, TileBase>();
    }

    private void Start()
    {
        var bounds = tilemap.cellBounds;
        var allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                var vector3Int = new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0);
                var tile = allTiles[x + y * bounds.size.x];

                if (tile != waterTile)
                {
                    tiles.Add(vector3Int, tile);

                    waterLoadTilemap.SetTile(vector3Int, waterLoadAllowTile);
                    waterLoadTilemap.SetColor(vector3Int, Color.blue);
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
            }
        }

        maxY = tiles.Max(t => t.Key.y);
    }

    private void ReloadWaterLoad()
    {
        var visible = new Dictionary<Vector3Int, bool>();
        var queue = new Queue<Vector3Int>(tiles.Where(p => maxY == p.Key.y).Select(p => p.Key).ToList());
        var dirs = new[]
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, -1, 0)
        };
        waterLoadTilemap.ClearAllTiles();
        while (queue.Count > 0)
        {
            var temp = queue.Dequeue();
            waterLoadTilemap.SetTile(temp, waterLoadAllowTile);

            foreach (var dir in dirs)
            {
                var nextPos = temp + dir;
                if (tiles.ContainsKey(nextPos) && !damTilemap.GetTile(nextPos) && !visible.ContainsKey(nextPos))
                {
                    visible.Add(nextPos, true);
                    queue.Enqueue(nextPos);
                }
            }
        }
    }

    public bool IsWater(Vector3 position)
    {
        var cellPosition = tilemap.WorldToCell(position);
        return waterLoadTilemap.GetTile(cellPosition) != null;
    }

    public bool IsDam(Vector3 position)
    {
        var cellPosition = tilemap.WorldToCell(position);
        return damTilemap.GetTile(cellPosition) != null;
    }

    public void SetDam(Vector3 position)
    {
        var cellPosition = tilemap.WorldToCell(position);
        damTilemap.SetTile(cellPosition, damTile);
        Debug.Log($"추가 : {cellPosition}");
        ReloadWaterLoad();
    }

    public void RemoveDam(Vector3 position)
    {
        var cellPosition = tilemap.WorldToCell(position);
        damTilemap.SetTile(cellPosition, null);
        ReloadWaterLoad();
    }

    public Vector3Int GetCellPosition(Vector3 position)
    {
        return tilemap.WorldToCell(position);
    }
}