using UnityEngine;
using Firebase.Database;
using DB.Schema.User;
using DB.Schema.Terrain;

namespace DB.CRUD
{
    public class DBCRUDOperations : MonoBehaviour
    {
        DatabaseReference dbReference;
        DBUser currentUser;
        [System.Obsolete]
        private void Start()
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        }
        public void CreateUser(string name, string password)
        {
            currentUser = new DBUser(name, password);
            string jsonData = JsonUtility.ToJson(new DBUser("A","A"));
            dbReference.Child("users").Child(currentUser.GetHashCode().ToString()).SetRawJsonValueAsync(jsonData)
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Data posted successfully to Firebase!");
                    CreateTerrain();
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error posting data to Firebase: " + task.Exception);
                }
                
            });
            
            
        }

        public void CreateTerrain()
        {
            DBTerrain terrain = new DBTerrain(currentUser, "aaaXDeergrg","Guatemala", 20);
            Debug.Log(terrain.terrainStringRepresentation);
            string jsonData = JsonUtility.ToJson(terrain);

            dbReference.Child("terrain").Child(terrain.ownerName + ";"+ terrain.name).SetRawJsonValueAsync(jsonData)
            .ContinueWith(task =>
            {
                //add creating terrain popup
                if (task.IsCompleted)
                {
                    Debug.Log("Data posted AAAA to Firebase!");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error posting data to Firebase: " + task.Exception);
                }
            });
        }
    }
}
