using UnityEngine;
using TMPro;

namespace UI.DataVerification.Name
{
    public class UINameInputVerification : MonoBehaviour
    {
        [SerializeField] public TMP_InputField textInput;
        public void VerifyUsername()
        {
            string validUsername = "";
            for (int i = 0; i < textInput.text.Length; i++)
            {
                if (IsValidCharacter(textInput.text[i]))
                    validUsername += textInput.text[i];
            }
            textInput.text = validUsername;
        }

        /// <summary>
        /// Checks if the current character is a valid one for the username
        /// </summary>
        /// <param name="currentChar">The current character of the username input</param>
        /// <returns>a bool value that represents if the current character is a valid one</returns>
        public  bool IsValidCharacter(char currentChar)
        {
            bool isLowerCase = currentChar >= 65 && currentChar <= 90;
            bool isUpperCase = currentChar >= 97 && currentChar <= 122;
            bool isSpace = currentChar == 32;
            
            return isLowerCase || isUpperCase || isSpace;
        }
        
    }
}
