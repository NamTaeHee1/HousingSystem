using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static TilemapManager Tilemap
    {
        get
        {
            if(_tilemap == null)
                _tilemap = FindObjectOfType<TilemapManager>();

            return _tilemap;
        }
    }
    private static TilemapManager _tilemap;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        Tilemap.Init();
    }
}
