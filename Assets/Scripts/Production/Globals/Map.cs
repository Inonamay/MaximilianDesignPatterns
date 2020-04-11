using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Create Map", order = 1)]
public class Map : ScriptableObject
{
    [SerializeField] TextAsset fileAsset = null;
    public TextAsset FileAsset
    {
        get
        {
            return fileAsset;
        }
    }
    [SerializeField] MapData[] prefabsAndTypes = new MapData[TileMethods.TypeAmounts];
    public MapData[] PrefabsAndTypes
    {
        get
        {
            return prefabsAndTypes;
        }
    }
}
