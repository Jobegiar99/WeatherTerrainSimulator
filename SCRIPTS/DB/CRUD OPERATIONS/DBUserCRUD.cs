using UnityEngine;
using DB.Schema.User;
using Proyecto26;
using System;

namespace DB.CRUD.User
{
    public class DBUserCRUD
    {
        private static string url = "a";

        public DBUser currentUser;

        public DBUserCRUD()
        {}

        /// <summary>
        /// Creates the user in the database and assigns it to an instance of DBUser.
        /// </summary>
        /// <param name="username">the username that the user wants the account to have</param>
        /// <param name="password">the password for the account</param>
        /// <returns>The instance of the recently created username</returns>
        public void CreateUser(string username, string password)
        {
            password = "__PASSWORD_BODY__" + password + "__PASSWORD_BODY__";
            currentUser = new DBUser(username, password);
            string jsonData = $"{{ \"password\": \"{password}\" }}";
            RestClient.Post($"{url}user/{username}.json", jsonData);
        }

        /// <summary>
        /// Function that checks if the user can login
        /// </summary>
        /// <param name="usernameInput">username provided by the user</param>
        /// <param name="passwordInput">password provided by the user</param>
        /// <returns>An instance of the recently logged in user or null if it was not possible to log in</returns>
        public void Login(string usernameInput, string passwordInput, Action<bool> callback)
        {
            currentUser = new DBUser(usernameInput, passwordInput);
            string query = $"{url}user/{usernameInput}.json";
            RestClient.Get(query).Then(response =>
            {
                if (response.Text.Length == 0 || response.Text == "null")
                {
                    callback?.Invoke(false);
                    return;
                }
                else
                {
                    string[] jsonData = response.Text.Split(':');
                    string[] passwordData = jsonData[2].Split("__PASSWORD_BODY__");
                    callback?.Invoke(passwordData[1] == passwordInput);
                }


            }).Catch(err =>
            {
                callback?.Invoke(false);
            });
        }


        /// <summary>
        /// Checks if the given username exists
        /// </summary>
        /// <param name="usernameInput">the username provided by the user</param>
        /// <returns>A bool that is true if the username exists and false if not</returns>
        public void DoesUserExists(string usernameInput, Action<bool> callback)
        {
            string query = $"{url}user/{usernameInput}.json";
            RestClient.Get(query).Then(response =>
            {
                callback?.Invoke(response.Text != "null");

            }).Catch(err =>
            {
                // Handle any errors that occur
                Debug.LogError("Error: " + err.Message);
                callback?.Invoke(false);
            });
        }
    }
}
