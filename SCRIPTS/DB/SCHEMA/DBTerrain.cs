using System;
using System.Collections.Generic;

namespace DB.Schema.Terrain
{
    /// <summary>
    /// This class hold the data for the terrain and asigns it to the corresponding user.
    /// </summary>
    [Serializable]
    public class DBTerrain 
    {
        public string ID;
        public string name;

        /// <summary>
        /// Holds the data for the terrain in the following way:
        ///     y (height) position
        ///     x (width)  position
        ///     z (depth) position
        /// </summary>
        public List<List<List<byte>>> terrainData;

        // current "real" world location of the terrain for weather purposes.
        public string location;
        public byte size;
        public string terrainStringRepresentation;


        private byte INITIAL_TIDE_CODE = 0;

        public DBTerrain(string ID, string name, string location, byte size)
        {
            this.ID = ID;
            this.name = name;
            this.location = location;
            this.size = size;
            terrainData = new List<List<List<byte>>>();
            AddTerrainMatrixLevel();
            AddTerrainMatrixLevel();
            AddTerrainMatrixLevel();
            AddTerrainMatrixLevel();
            terrainStringRepresentation = MatrixToString();
                       
        }

        public DBTerrain(string ID, string name, string location, string terrainStringRepresentation,byte size)
        {
            this.ID = ID;
            this.name = name;
            this.location = location;
            this.size = size;
            this.terrainStringRepresentation = terrainStringRepresentation;

        }

        private void AddTerrainMatrixLevel()
        {
            terrainData.Add(new List<List<byte>>());

            int level = terrainData.Count - 1;
            for (int x = 0; x < size; x++)
            {
                terrainData[level].Add(new List<byte>());
                for(int z = 0; z < size; z++)
                {
                    terrainData[level][x].Add(INITIAL_TIDE_CODE);
                }
            }
        }


        /// <summary>
        /// This function iterates through the matrix and converts it into a string.
        /// Each value is separated by a semicolon.
        /// </summary>
        /// <returns>A string with the matrix data per level, each cell separated by a semicolon</returns>
        private string MatrixToString()
        {
            string tempMatrix = "";
            for(int y = 0; y < terrainData.Count; y++)
            {
                for(int x = 0; x < size;x++)
                {
                    for(int z = 0; z < size; z++)
                    {
                        tempMatrix += terrainData[y][x][z].ToString() + ";";
                    }
                }
            }
            return tempMatrix;
        }

        public string TerrainToJSON()
        {
            string terrainName = "__TERRAIN_NAME_BODY__" + name + "__TERRAIN_NAME_BODY__";
            string terrainLocation = "__TERRAIN_LOCATION_BODY__" + location + "__TERRAIN_LOCATION_BODY__";
            string strSize = "__TERRAIN_SIZE__" + size.ToString() + "__TERRAIN_SIZE__";
            string strTerrain = "__TERRAIN_STRING__" + terrainStringRepresentation + "__TERRAIN_STRING__";
            string jsonData = $"{{ \"name\": \"{terrainName}\",\"location\":\"{terrainLocation}\",\"size\":\"{strSize}\",\"terrain\":\"{strTerrain}\" }}";
            return jsonData;

        }
    }
}
