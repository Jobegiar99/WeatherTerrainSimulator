using DB.Manager;
using TMPro;
using UI.Navigation.MainMenu;
using UnityEngine;

namespace UI.Buttons.MainMenu.Login
{
    public class SignUpButton : MonoBehaviour
    {
        #region Shell Screens
        [SerializeField] GameObject loadingShell;
        [SerializeField] GameObject usernameExistsShell;
        [SerializeField] GameObject passwordDoNotMatchShell;
        [SerializeField] GameObject emptyFieldShell;
        #endregion

        #region Script References
        [Space(10)]
        [SerializeField] MainMenuNavigation mainMenuNavigation;
        [SerializeField] DBManager db;
        #endregion

        #region InputFields
        [Space(10)]
        [SerializeField] TMP_InputField usernameInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_InputField repeatPasswordInput;
        #endregion


        public void AttemptSignUp()
        {
            if(usernameInput.text.Length == 0 || passwordInput.text.Length == 0 || repeatPasswordInput.text.Length == 0)
            {
                emptyFieldShell.SetActive(true);
                return;
            }
            if(passwordInput.text != repeatPasswordInput.text)
            {
                passwordDoNotMatchShell.SetActive(true);
                return;
            }
            loadingShell.SetActive(true);
            db.CanCreateUser(usernameInput.text, passwordInput.text, canCreateUser =>
            {
                if (!canCreateUser)
                {
                    loadingShell.SetActive(false);
                    usernameExistsShell.SetActive(true);
                    return;
                }
                db.LoadTerrains(loadedTerrains =>
                {
                    return;
                });
                loadingShell.SetActive(false);
                mainMenuNavigation.GoToCreateOrLoadTerrainShell();
            });
        }
    }
}
