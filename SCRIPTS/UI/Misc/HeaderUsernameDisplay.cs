using UnityEngine;
using DB.Manager;
using TMPro;

namespace UI.Misc.UsernameDisplay
{
    public class HeaderUsernameDisplay : MonoBehaviour
    {
        #region Script References
        [SerializeField] DBManager db;
        #endregion

        #region
        [Space(10)]
        [SerializeField] TextMeshProUGUI usernameText;
        #endregion

        private void OnEnable()
        {
            usernameText.text = db.GetUsername();
        }
    }
}