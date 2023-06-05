using DB.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Navigation.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.MainMenu.CreateTerrain
{
    public class CreateTerrainButton : MonoBehaviour
    {
        #region Shell Screens
        [Header("Shell Screens")]
        [SerializeField] GameObject loadingShell;
        [SerializeField] GameObject terrainNameExistsShell;
        [SerializeField] GameObject emptyFieldShell;
        #endregion

        #region Script References
        [Space(10)]
        [Header("Script References")]
        [SerializeField] MainMenuNavigation mainMenuNavigation;
        [SerializeField] DBManager db;
        #endregion

        #region InputFields
        [Space(10)]
        [Header("Input Fields")]
        [SerializeField] TMP_InputField terrainNameInput;
        [SerializeField] TMP_InputField latitudeInput;
        [SerializeField] TMP_InputField longitudeInput;
        [SerializeField] Slider terrainSize;

        #endregion
        
        public void AttemptToCreateTerrain()
        {
            if(terrainNameInput.text.Length == 0 || latitudeInput.text.Length == 0 || longitudeInput.text.Length == 0)
            {
                emptyFieldShell.SetActive(true);
                return;
            }

            loadingShell.SetActive(true);
            foreach(string name in db.GetTerrainNames())
            {
                if(name == terrainNameInput.text)
                {
                    loadingShell.SetActive(false);
                    terrainNameExistsShell.SetActive(true);
                    return;
                }
            }
            db.CreateTerrain
                (db.GetUsername(), 
                terrainNameInput.text, 
                latitudeInput.text, 
                longitudeInput.text, 
                (byte)terrainSize.value, 
                createdTerrain =>
                {
                   
                    loadingShell.SetActive(false);
                    mainMenuNavigation.GoToGameplayFootShell();

                });


        }
    }
}
