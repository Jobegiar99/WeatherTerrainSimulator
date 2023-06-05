using DB.Manager;
using UnityEngine;

namespace UI.Buttons.Save
{
    public class SaveButton : MonoBehaviour
    {
        #region Shell popup
        [SerializeField] GameObject LoadingShell;
        [SerializeField] GameObject ErrorSavingShell;
        #endregion
        #region Script Reference
        [SerializeField] DBManager db;
        #endregion

        public void SaveTerrain()
        {
            LoadingShell.SetActive(true);
            db.SaveTerrain(result =>
            {
                LoadingShell.SetActive(false);
                if (!result)
                    ErrorSavingShell.SetActive(true);
            });
        }
    }
}
