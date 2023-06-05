using DB.Manager;
using System.Collections;
using System.Collections.Generic;
using Terrain.Renderer;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Terrain.Interaction
{
    public class TerrainInteraction : MonoBehaviour
    {
        [Header("Tilemap")]
        [SerializeField] Tilemap tilemap;
        [Header("Script References")]
        [Space(10)]
        [SerializeField] TerrainRenderer terrainRenderer;
        [SerializeField] DBManager db;
        [Header("Shell")]
        [SerializeField] GameObject InteractionShell;


        private Vector3Int clickedTilePos;

        private void Update()
        {
            HandleTileClick();
        }

        private void HandleTileClick()
        {
            if (Input.GetMouseButtonDown(0) && tilemap.gameObject.activeInHierarchy && !InteractionShell.activeInHierarchy)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
                clickedTilePos = tilemap.WorldToCell(worldPoint);
                if (clickedTilePos.x >= 0 && clickedTilePos.x <= db.GetTerrainSize() &&  clickedTilePos.y >= 0 && clickedTilePos.y <= db.GetTerrainSize())
                {
                    InteractionShell.SetActive(true);
                }
            }
        }

        public void ChangeFloorTile(int code)
        {
            terrainRenderer.UpdateTile(clickedTilePos, (byte)code);
            InteractionShell.SetActive(false);
        }

        public void PlantTree( )
        {
            terrainRenderer.PlantTree(clickedTilePos);
            InteractionShell.SetActive(false);
        }

        public void RemoveTree()
        {
            terrainRenderer.RemoveTree(clickedTilePos);
            InteractionShell.SetActive(false);
        }
    }
}