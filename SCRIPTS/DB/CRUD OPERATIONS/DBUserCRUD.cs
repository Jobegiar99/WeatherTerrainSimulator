using UnityEngine;
using DB.Schema.User;
using Proyecto26;

namespace DB.CRUD.User
{
    public class DBUserCRUD
    {
        private static string url = "AUTHORIZED_ACCESS_ONLY";


        /// <summary>
        /// Creates the user in the database and assigns it to an instance of DBUser.
        /// </summary>
        /// <param name="username">the username that the user wants the account to have</param>
        /// <param name="password">the password for the account</param>
        /// <returns>The instance of the recently created username</returns>
        public DBUser CreateUser(string username, string password)
        {
            password = "__PASSWORD_BODY__" + password + "__PASSWORD_BODY__";
            DBUser currentUser = new DBUser(username, password);
            string jsonData = $"{{ \"password\": \"{password}\" }}";
            RestClient.Post($"https://terrainweathersimulator-default-rtdb.firebaseio.com/user/{username}.json", jsonData);
            return currentUser;
        }

        /// <summary>
        /// Function that checks if the user can login
        /// </summary>
        /// <param name="usernameInput">username provided by the user</param>
        /// <param name="passwordInput">password provided by the user</param>
        /// <returns>An instance of the recently logged in user or null if it was not possible to log in</returns>
        public DBUser Login(string usernameInput, string passwordInput)
        {
            DBUser currentUser = new DBUser(usernameInput, passwordInput);
            string query = $"{url}/user/{usernameInput}.json";
            bool canLogin = true;
            RestClient.Get(query).Then(response =>
            {
                if (response.Text.Length == 0)
                {
                    canLogin = false;
                    return;
                }
                string[] jsonData = response.Text.Split(':');
                string[] passwordData = jsonData[2].Split("__PASSWORD_BODY__");
                canLogin = passwordData[1] == passwordInput;


            }).Catch(err =>
            {
                Debug.LogError("Error: " + err.Message);
            });
            if (canLogin)
                return currentUser;
            return null;
        }


        /// <summary>
        /// Checks if the given username exists
        /// </summary>
        /// <param name="usernameInput">the username provided by the user</param>
        /// <returns>A bool that is true if the username exists and false if not</returns>
        public bool DoesUserExists(string usernameInput)
        {
            string query = $"{url}/user/{usernameInput}.json";
            bool userExists = false;
            RestClient.Get(query).Then(response =>
            {
                userExists = response.Text.Length != 0;

            }).Catch(err =>
            {
                // Handle any errors that occur
                Debug.LogError("Error: " + err.Message);
            });
            return userExists;
        }
    }
}
