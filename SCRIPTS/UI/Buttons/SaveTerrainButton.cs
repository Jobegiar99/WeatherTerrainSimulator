using DB.Manager;
using UnityEngine;

namespace UI.Buttons.SaveTerrain
{
    public class SaveTerrainButton : MonoBehaviour
    {
        #region Script Reference
        [SerializeField] DBManager db;
        #endregion
        #region Shell Screens
        [SerializeField] GameObject savingTerrainShell;
        [SerializeField] GameObject errorSavingShell;
        #endregion
        public void SaveTerrain()
        {
            savingTerrainShell.SetActive(true);
            db.SaveTerrain(couldSave =>
            {
                savingTerrainShell.SetActive(false);
                if (!couldSave)
                {
                    errorSavingShell.SetActive(true);
                }
            });
        }
    }
}
