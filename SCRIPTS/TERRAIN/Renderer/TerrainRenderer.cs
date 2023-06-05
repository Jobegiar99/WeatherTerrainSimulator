using DB.Manager;
using DB.Schema.Terrain;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Tilemaps;
using WeatherAPI.Manager;
using TMPro;
using UnityEngine.UI;

namespace Terrain.Renderer
{
    public class TerrainRenderer : MonoBehaviour
    {
        #region Script References
        [Header("Script References")]
        [SerializeField] DBManager db;
        [SerializeField] WeatherApiManager weatherAPI;
        #endregion

        #region UI Elements
        [Header("UI Elements")]
        [SerializeField] TMP_InputField latitudeInput;
        [SerializeField] TMP_InputField longitudeInput;
        [SerializeField] TextMeshProUGUI temperature;
        [SerializeField] Slider hourSlider;
        [SerializeField] GameObject rain;
        [SerializeField] GameObject snow;
        #endregion

        #region Tileset
        [Space(10)]
        [Header("UI elements")]
        [SerializeField] Tilemap floor;
        [SerializeField] Tilemap decoration;
        #endregion

        #region Tiles Data
        [Space(10)]
        [Header("Tilebase")]
        [SerializeField] TileBase grass;
        [SerializeField] TileBase ground;
        [SerializeField] TileBase rockPath;
        [SerializeField] TileBase water;
        [SerializeField] TileBase tree;
        #endregion

        private Dictionary<byte, Tilemap> tilemapLevel;

        private Dictionary<byte, TileBase> tileBaseMap;

        private void Start()
        {
            tilemapLevel = new Dictionary<byte, Tilemap>();
            tileBaseMap = new Dictionary<byte, TileBase>();


            tilemapLevel[0] = floor;
            tilemapLevel[1] = decoration;

            tileBaseMap[0] = grass;
            tileBaseMap[1] = ground;
            tileBaseMap[2] = rockPath;
            tileBaseMap[3] = water;


        }

        public void RenderTerrain()
        {

            DBTerrain currentTerrain = db.GetCurrentTerrain();
            weatherAPI.GetAPIData(float.Parse(currentTerrain.latitude), float.Parse(currentTerrain.longitude), finishLoadingData =>
            {
                for (byte y = 0; y < 2; y++)
                {
                    for (byte x = 0; x < currentTerrain.size; x++)
                    {
                        for (byte z = 0; z < currentTerrain.size; z++)
                        {
                            byte tileCode = currentTerrain.terrainData[y][x][z];
                            if (y == 0)
                                tilemapLevel[0].SetTile(new Vector3Int(x, z, 0), tileBaseMap[tileCode]);
                            else if (tileCode == 1)
                            {
                                tilemapLevel[1].SetTile(new Vector3Int(x, z, 0), tree);
                            }
                        }
                    }
                }
                latitudeInput.text = currentTerrain.latitude;
                longitudeInput.text = currentTerrain.longitude;
                SetTemperature();
            });
        }

        public void UpdateTile(Vector3 position, byte code)
        {
            Vector3Int pos = new Vector3Int((int)Mathf.Floor(position.x), (int)Mathf.Floor(position.y), 0);
            floor.SetTile(pos, tileBaseMap[code]);
            db.GetCurrentTerrain().terrainData[0][pos.x][pos.y] = code;
        }

        public void PlantTree(Vector3Int position)
        {
            decoration.SetTile(position, tree);
            db.GetCurrentTerrain().terrainData[1][position.x][position.y] = 1;
        }

        public void RemoveTree(Vector3Int position)
        {
            decoration.SetTile(position, null);
            db.GetCurrentTerrain().terrainData[1][position.x][position.y] = 0;
        }

        public void GetWeatherCode()
        {
            byte code = (byte) weatherAPI.GetWeatherCode((byte) hourSlider.value);
            rain.SetActive(code == 1);
            snow.SetActive(code == 2);  
        }

        public void SetTemperature()
        {
            temperature.text = weatherAPI.GetTemperature((byte)hourSlider.value).ToString();
        }

        public void UpdateWeatherAPI()
        {
            weatherAPI.GetAPIData(float.Parse(latitudeInput.text), float.Parse(longitudeInput.text),result =>
            {
                GetWeatherCode();
                SetTemperature();
            });
            DBTerrain terrain = db.GetCurrentTerrain();
            terrain.longitude = longitudeInput.text;
            terrain.latitude = latitudeInput.text;
        }
    }
}
