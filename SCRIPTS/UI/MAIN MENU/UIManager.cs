using UnityEngine;

namespace UI.MainMenu
{
    public class UIManager : MonoBehaviour
    {
        #region UI Elements
        [SerializeField] GameObject LoginShell;
        [SerializeField] GameObject SignUpShell;
        [SerializeField] GameObject LogedInHeaderShell;
        [SerializeField] GameObject CreateOrLoadTerrainShell;
        [SerializeField] GameObject CreateTerrainShell;
        [SerializeField] GameObject LoadTerrainShell;
        private GameObject currentActiveElement;
        #endregion
        
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

        private void SwitchShell(GameObject nextUiElement) 
        {
            currentActiveElement.SetActive(false);
            nextUiElement.SetActive(true);
            currentActiveElement=nextUiElement;
        }
    }
}
