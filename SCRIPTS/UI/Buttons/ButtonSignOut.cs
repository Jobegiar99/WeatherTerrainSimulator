using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons.SignOut
{
    public class ButtonSignOut : MonoBehaviour
    {
        public void SignOut()
        {
            SceneManager.LoadScene(0);
        }
    }
}