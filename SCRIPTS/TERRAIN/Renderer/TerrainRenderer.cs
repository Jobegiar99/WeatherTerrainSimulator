using DB.Manager;
using DB.Schema.Terrain;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Terrain.Renderer
{
    public class TerrainRenderer : MonoBehaviour
    {
        #region Script References
        [Header("Script References")]
        [SerializeField] DBManager db;
        #endregion

        #region Tileset
        [Space(10)]
        [Header("Tilesets")]
        [SerializeField] Tilemap floor;
        [SerializeField] Tilemap decoration;
        #endregion

        #region Tiles Data
        [Space(10)]
        [Header("Tilebase")]
        [SerializeField] TileBase grass;
        #endregion

        private Dictionary<byte, Tilemap> tilemapLevel;

        private Dictionary<byte, TileBase> tileBaseMap;

        private void Start()
        {
            tilemapLevel = new Dictionary<byte, Tilemap>();
            tileBaseMap = new Dictionary<byte, TileBase>();


            tilemapLevel[0] = floor;
            tilemapLevel[1] = decoration;


        }

        public void RenderTerrain()
        {

            DBTerrain currentTerrain = db.GetCurrentTerrain();
            for (byte y = 0; y < 2; y++)
            {
                for(byte x = 0; x < currentTerrain.size; x++)
                {
                    for(byte z = 0; z < currentTerrain.size; z++)
                    {
                        tilemapLevel[y].SetTile(new Vector3Int(x, z,0), grass);
                        Debug.Log("here" + x + " " + z + " " + y);
                    }
                }
            }
        }
    }
}
