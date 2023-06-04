using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Navigation.MainMenu;
using DB.Manager;
using TMPro;

namespace UI.Buttons.MainMenu.Login
{
    public class LoginButton : MonoBehaviour
    {
        #region Shell Screens
        [SerializeField] GameObject loadingShell;
        [SerializeField] GameObject badLoginShell;
        #endregion

        #region Script References
        [SerializeField] MainMenuNavigation mainMenuNavigation;
        [SerializeField] DBManager db;
        #endregion

        #region InputFields
        [SerializeField] TMP_InputField username;
        [SerializeField] TMP_InputField password;
        #endregion

        public void AttemptLogin()
        {
            loadingShell.SetActive(true);
            db.CanLogin(username.text, password.text, canLogin =>
            {
                
                if (canLogin)
                {
                    db.LoadTerrains(terrainsLoaded =>
                    {
                        return;
                    });
                    loadingShell.SetActive(false);
                    mainMenuNavigation.GoToCreateOrLoadTerrainShell();
                }
                else
                {
                    loadingShell.SetActive(false);
                    badLoginShell.SetActive(true);
                }
            });

        }
    }
}
