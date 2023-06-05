using UnityEngine;
using DB.CRUD.Terrain;
using DB.Schema.Terrain;
using System.Collections;

namespace UI.Navigation.MainMenu
{
    public class MainMenuNavigation : MonoBehaviour
    {
        #region UI Elements
        [SerializeField] GameObject LoginShell;
        [SerializeField] GameObject SignUpShell;
        [SerializeField] GameObject LogedInHeaderShell;
        [SerializeField] GameObject CreateOrLoadTerrainShell;
        [SerializeField] GameObject CreateTerrainShell;
        [SerializeField] GameObject LoadTerrainShell;
        [SerializeField] GameObject GameplayHeaderShell;
        [SerializeField] GameObject GameplayFootShell;
        private GameObject currentActiveElement;
        [Space(10)]
        #endregion
        #region DB
        [SerializeField] DBTerrainCRUD db;
        
        #endregion

        [System.Obsolete]
        private void Start()
        {
            currentActiveElement = LoginShell;
            
        }

        public void GoToSignUpShell()
        {
            SwitchShell(SignUpShell);
        }

        public void GoToLoginShell()
        {
            LogedInHeaderShell.SetActive(false);
            SwitchShell(LoginShell);
        }


        public void GoToCreateTerrainShell()
        {
            SwitchShell(CreateTerrainShell);
        }

        public void GoToLoadTerrainShell()
        {
            SwitchShell(LoadTerrainShell);
        }

        public void GoToCreateOrLoadTerrainShell()
        {
            LogedInHeaderShell.SetActive(true);
            SwitchShell(CreateOrLoadTerrainShell);

        }

        public void GoToGameplayFootShell()
        {
            LogedInHeaderShell.SetActive(false);
            GameplayHeaderShell.SetActive(true);
            SwitchShell(GameplayFootShell);
        }

        private void SwitchShell(GameObject nextUiElement) 
        {
            currentActiveElement.SetActive(false);
            nextUiElement.SetActive(true);
            currentActiveElement=nextUiElement;
        }

    }
}
