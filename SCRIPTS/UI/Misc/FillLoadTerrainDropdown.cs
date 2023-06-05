using System.Collections.Generic;
using UnityEngine;
using DB.Manager;
using TMPro;

namespace UI.Misc.FillLoadTerrainDropdown
{
    public class FillLoadTerrainDropdown : MonoBehaviour
    {
        [SerializeField] DBManager db;
        [SerializeField] TMP_Dropdown dropdown;
        private void OnEnable()
        {
            List<TMP_Dropdown.OptionData> terrainNames = new List<TMP_Dropdown.OptionData>();
            foreach(string name in db.GetTerrainNames())
            {
                terrainNames.Add(new TMP_Dropdown.OptionData(name));
            }
            dropdown.options = terrainNames;
        }
    }
}
