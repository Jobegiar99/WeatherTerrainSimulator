using System.Collections.Generic;
using UnityEngine;
using DB.CRUD.Terrain;
using DB.CRUD.User;
using DB.Schema.Terrain;
using System;

namespace DB.Manager {
    public class DBManager : MonoBehaviour
    {
        List<string> currentTerrains = new List<string>();
        public DBTerrainCRUD terrainDB;
        public DBUserCRUD userDB;

        // Start is called before the first frame update
        void Start()
        {
            userDB = new DBUserCRUD();
            terrainDB = new DBTerrainCRUD();
        }

        public string GetUsername()
        {
            return userDB.currentUser.username;
        }

        /// <summary>
        /// Tells the UI Manager if the user was able to login
        /// </summary>
        /// <param name="username">valid username input</param>
        /// <param name="password">password input</param>
        /// <returns>a bool value indicating if the UI can proceed to the next screen</returns>
        public void CanLogin(string username, string password, Action<bool> callback)
        {
            userDB.Login(username, password,canLogin =>
                {
                    callback?.Invoke(canLogin);
                });
        }

        /// <summary>
        /// Checks if the user can be created
        /// </summary>
        /// <param name="username">valid username input</param>
        /// <param name="password">password input</param>
        /// <param name="callback">true if the user can be created, false if not</param>
        public void CanCreateUser(string username, string password, Action<bool> callback)
        {
            userDB.DoesUserExists(username, userExists =>
            {
                if (!userExists)
                {
                    userDB.CreateUser(username,password);
                    callback?.Invoke(true);
                }
                callback?.Invoke(!userExists);
            });
        }

        public void Createuser(string username, string password)
        {
            userDB.CreateUser(username, password);
        }


        /// <summary>
        /// Loads the terrains into the terrain list from the DBTerrainCRUD object
        /// </summary>
        /// <param name="callback">returns a bool value after the terrains have been loaded</param>
        public void LoadTerrains(Action<bool> callback)
        {
            currentTerrains.Clear();  
            terrainDB.ReadTerrains(userDB.currentUser.username, loadedTerrains =>
            {
                callback?.Invoke(loadedTerrains);
            });
        }

        /// <summary>
        /// Obtain all the names of the current terrains for the current user
        /// </summary>
        /// <returns>A list with the names of all the terrains the user owns.</returns>
        public List<string> GetTerrainNames()
        {
            if (currentTerrains.Count == 0)
            {
                foreach (DBTerrain terrain in terrainDB.terrainList)
                {
                    currentTerrains.Add(terrain.name);
                }
            }
            return currentTerrains;
        }

        public void CreateTerrain(string owner, string terrainName, string latitude,string longitude,byte size,Action<bool> callback)
        {
            terrainDB.CreateTerrain(owner, terrainName, latitude, longitude, size, terrainCreated =>
                {
                    callback?.Invoke(true);
                });
        }
    }
}
