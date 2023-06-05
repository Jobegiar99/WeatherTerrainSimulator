using DB.Manager;
using Terrain.Renderer;
using TMPro;
using UI.Navigation.MainMenu;
using UnityEngine;

namespace UI.Buttons.LoadTerrain
{
    public class LoadTerrainButton : MonoBehaviour
    {
        #region ScriptReferences
        [SerializeField] DBManager db;
        [SerializeField] MainMenuNavigation uiNavigation;
        [SerializeField] TerrainRenderer terrainRenderer;
        #endregion
        #region UI Element
        [Space(10)]
        [Header("UI Element")]
        [SerializeField] TMP_Dropdown terrainDropdown;
        #endregion
        #region
        [Space(10)]
        [Header("Shell Screens")]
        [SerializeField] GameObject LoadTerrainShell;
        #endregion

        public void LoadTerrain()
        {
            LoadTerrainShell.SetActive(true);
            db.SetCurrentTerrain(terrainDropdown.value);
            terrainRenderer.RenderTerrain();
            LoadTerrainShell.SetActive(false);
            uiNavigation.GoToGameplayFootShell();
        }

    }
}