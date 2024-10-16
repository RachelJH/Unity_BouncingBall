using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System.IO;

public class MapDataSave : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFileName;
    [SerializeField]
    private TileMap2D tilemap2D;

    private void Awake()
    {
        inputFileName.text = "NoName.json";
    }
    public void Save()
    {
        MapData mapData = tilemap2D.GetMapData();

        string fileName = inputFileName.text;

        if(fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        fileName = Path.Combine("MapData/", fileName);

        string toJson = JsonConvert.SerializeObject(mapData, Formatting.Indented);

        File.WriteAllText(fileName, toJson);    
    }
}
