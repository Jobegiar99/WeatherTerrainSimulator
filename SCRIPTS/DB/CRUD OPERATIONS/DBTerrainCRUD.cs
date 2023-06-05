using DB.Schema.Terrain;
using Proyecto26;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DB.CRUD.Terrain
{
    public class DBTerrainCRUD 
    {
        private static string url = "me";

        public DBTerrain currentTerrain;
        public List<DBTerrain> terrainList;

        public DBTerrainCRUD()
        {
            terrainList = new List<DBTerrain>();
        }

        /// <summary>
        /// Creates the user in the database and assigns it to an instance of DBUser.
        /// </summary>
        /// <param name="ownerUsername">the username that the user wants the account to have</param>
        /// <param name="terrainName">the password for the account</param>
        /// <param name="location">The real world location of this terrain</param>
        /// <param name="size">number of rows and columns</param>
        /// <returns>The instance of the recently created username</returns>
        public void CreateTerrain(string owner,string terrainName, string latitude, string longitude,byte size, Action<bool> callback)
        {
            currentTerrain = new DBTerrain("NONE",terrainName, latitude,longitude, size);
            RestClient.Post($"{url}terrain/{owner}.json", currentTerrain.TerrainToJSON())
                .Then(response =>
                {
                    currentTerrain.ID = response.Text.Split(":")[1].Split('"')[1];
                    callback?.Invoke(true);
                    return;
                });
        }

        /// <summary>
        /// Fills the terrain list with  data from the DB for the current user.
        /// </summary>
        /// <param name="ownerUsername">Owner of the terrains</param>
        public void ReadTerrains(string owner, Action<bool> callback)
        {
            terrainList.Clear();
            string query = $"{url}/terrain/{owner}.json";
            RestClient.Get(query).Then(response =>
            {
                if(response.Text == "null")
                {
                    callback?.Invoke(true);
                    return;
                }
                List<string> terrainData = response.Text.Split('{').ToList<string>();
 
                string terrainID = terrainData[1].Substring(1, terrainData[1].Length-3);
                string terrainName = "";
                string terrainLatitude = "";
                string terrainLongitude = "";
                string terrainMatrix = "";
                string terrainSize = "";

                for (int i = 2; i < terrainData.Count; i++) 
                {
                    List<string> splitData = terrainData[i].Split(':').ToList<string>();
                    foreach(string s in splitData)
                    {
                        ReadTerrainsHelper(s, "__TERRAIN_NAME_BODY__",ref terrainName);
                        ReadTerrainsHelper(s, "__TERRAIN_LATITUDE_BODY__", ref terrainLatitude);
                        ReadTerrainsHelper(s, "__TERRAIN_LONGITUDE_BODY__", ref terrainLongitude);
                        ReadTerrainsHelper(s, "__TERRAIN_SIZE__", ref terrainSize );
                        ReadTerrainsHelper(s, "__TERRAIN_STRING__", ref terrainMatrix);
                    }
                    terrainList.Add(new DBTerrain(terrainID, terrainName, terrainLatitude,terrainLongitude, terrainMatrix, byte.Parse(terrainSize)));
                    if( i < terrainData.Count-1 ) 
                        terrainID = terrainData[i].Split('}')[1].Split(',')[1].Split('"')[1];
                }
                callback?.Invoke(true);

            }).Catch(err =>
            {
                Debug.LogError("Error: " + err.Message);
                callback?.Invoke(false);
            });
        }
        /// <summary>
        /// Checks if the given substring of the LoadTerrain Get response matches the separator to assign it to the given attribute
        /// </summary>
        /// <param name="currentString">The current substring of the response</param>
        /// <param name="separator">The target separator in case it is a substring of the currentString parameter</param>
        /// <param name="attribute">the attribute to assign the value of the current substring if there's a match</param>

        private void ReadTerrainsHelper(string currentString, string separator, ref string attribute)
        {
            if (currentString.Contains(separator))
            {
                List<string> strings = currentString.Split(separator).ToList<string>();
                attribute = strings[1];
            }
        }


        /// <summary>
        /// Updates the given terrain in the database
        /// </summary>
        /// <param name="updatedTerrain">updates version of the terrain</param>
        /// <param name="username">owner of the terrain</param>
        public void UpdateTerrain(string owner, Action<bool> callback)
        {
            currentTerrain.terrainStringRepresentation = currentTerrain.MatrixToString();
            Debug.Log(currentTerrain.ID);

            Debug.Log(currentTerrain.TerrainToJSON());
            string query = $"{url}/terrain/{owner}/{currentTerrain.ID}.json";
            RestClient.Put(query, currentTerrain.TerrainToJSON())
                .Then(response =>
                {
                    callback?.Invoke(true);
                })
                .Catch(err =>
                {
                    Debug.LogError(err.Message);
                    callback?.Invoke(false);
                });
        }

        /// <summary>
        /// Removes the terrain from the database.
        /// </summary>
        public void DeleteTerrain(string owner)
        {
            string query = $"{url}/terrain/{owner}/{currentTerrain.ID}.json";
            RestClient.Delete(query)
                .Then(response =>
                {
                    Debug.Log("SUCCESS");
                })
                .Catch(err =>
                {
                    Debug.LogError(err.Message);
                });
        }


     
    }
}
