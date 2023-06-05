using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        public string latitude;
        public string longitude;
        public byte size;
        public string terrainStringRepresentation;


        private byte INITIAL_TIDE_CODE = 0;

        public DBTerrain(string ID, string name, string latitude, string longitude, byte size)
        {
            this.ID = ID;
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
            this.size = size;
            terrainData = new List<List<List<byte>>>();

            //2 because the first one is for the ground
            //and the second one for the decoration
            AddTerrainMatrixLevel();
            AddTerrainMatrixLevel();
            terrainStringRepresentation = MatrixToString();
                       
        }

        public DBTerrain(string ID, string name, string latitude,string longitude, string terrainStringRepresentation,byte size)
        {
            this.ID = ID;
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
            this.size = size;
            this.terrainStringRepresentation = terrainStringRepresentation;
            terrainData = new List<List<List<byte>>>();
        }

        /// <summary>
        /// Adds a Level into the matrix
        /// </summary>
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
        public string MatrixToString()
        {
            string tempMatrix = "";
            for(int y = 0; y < 2; y++)
            {
                for(int x = 0; x < size;x++)
                {
                    for(int z = 0; z < size; z++)
                    {
                        tempMatrix += terrainData[y][x][z].ToString() + ";";
                    }
                }
            }
            terrainStringRepresentation = tempMatrix;
            return tempMatrix;
        }

        public void StringToMatrix()
        {
            List<string> terrainCells = terrainStringRepresentation.Split(';').ToList<string>();
            int terrainCellindex = 0;
            terrainData = new List<List<List<byte>>>();
            AddTerrainMatrixLevel();
            AddTerrainMatrixLevel();
            for (int y = 0; y < terrainData.Count; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        terrainData[y][x][z] = byte.Parse(terrainCells[terrainCellindex]);
                        terrainCellindex++;
                    }
                }
            }
        }

        public string TerrainToJSON()
        {
            string terrainName = "__TERRAIN_NAME_BODY__" + name + "__TERRAIN_NAME_BODY__";
            string terrainLatitude = "__TERRAIN_LATITUDE_BODY__" + latitude + "__TERRAIN_LATITUDE_BODY__";
            string terrainLongitude = "__TERRAIN_LONGITUDE_BODY__" + longitude + "__TERRAIN_LONGITUDE_BODY__";
            string strSize = "__TERRAIN_SIZE__" + size.ToString() + "__TERRAIN_SIZE__";
            string strTerrain = "__TERRAIN_STRING__" + terrainStringRepresentation + "__TERRAIN_STRING__";
            string jsonData = $"{{ \"name\": \"{terrainName}\",\"latitude\":\"{terrainLatitude}\",\"longitude\":\"{terrainLongitude}\",\"size\":\"{strSize}\",\"terrain\":\"{strTerrain}\" }}";
            return jsonData;

        }
    }
}
