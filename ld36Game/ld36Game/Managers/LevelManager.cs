using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ld36Game.Managers
{
    public class LevelManager
    {
        struct Path
        {
            public int start;
            public int end;
            public int type;

            public Path(int s, int e, int t)
            {
                start = s;
                end = e;
                type = t;
            }
        }

        int[] mapTiles;
        List<Path> mapPaths;
        List<string> spawnList;

        int[] choices;
        int spawns;

        const int mapWidth = 20;
        const int mapHeight = 15;

        public LevelManager()
        {
            mapPaths = new List<Path>();
            spawnList = new List<string>();

            spawns = 0;
            choices = new int[mapWidth * mapHeight];

            mapTiles = new int[mapWidth * mapHeight];
            for(int i = 0; i < mapWidth * mapHeight; ++i)
            {
                mapTiles[i] = 0;
                choices[i] = 0;
            }
        }

        public int[] getMap()
        {
            return mapTiles;
        }

        public int getDestTile(int pathId)
        {
            return mapPaths[pathId].end;
        }

        public int getPathIdFromStart(int startTile)
        {
            List<int> possiblePathIds = new List<int>();
            for(int i = 0; i < mapPaths.Count; ++i)
            {
                if (mapPaths[i].start == startTile) possiblePathIds.Add(i);
            }

            int currentPathChoice = choices[startTile];
            choices[startTile]++;
            if (choices[startTile] >= possiblePathIds.Count) choices[startTile] = 0;

            return possiblePathIds[currentPathChoice];
        }

        public int getSpawnPoint()
        {
            List<int> possiblePathIds = new List<int>();
            for (int i = 0; i < mapPaths.Count; ++i)
            {
                if (mapPaths[i].type == 170 /*AA*/ || mapPaths[i].type == 175 /*AF*/) possiblePathIds.Add(mapPaths[i].start);
            }

            return possiblePathIds[0];
        }

        public bool isEndPoint(int id)
        {
            if (mapPaths[id].type == 255 /*FF*/ || mapPaths[id].type == 175 /*AF*/) return true;
            else return false;
        }

        public List<string> getSpawnList()
        {
            return spawnList;
        }

        public void loadLevel(string levelFileName)
        {
            string mapFileAsString = "";
            string pathsAsString = "";
            string spawnListAsString = "";

            using (var levelfile = TitleContainer.OpenStream(@"Data/" + levelFileName))
            {
                using (var sr = new StreamReader(levelfile))
                {
                    string line = sr.ReadLine();
                    while(line != "xxxx")
                    {
                        //add line to string
                        mapFileAsString += line;
                        line = sr.ReadLine();
                    }

                    line = sr.ReadLine();
                    while (line != "xxxx")
                    {
                        //add line to string
                        pathsAsString += line;
                        line = sr.ReadLine();
                    }

                    line = sr.ReadLine();
                    while (line != null)
                    {
                        //add line to string
                        spawnListAsString += line;
                        line = sr.ReadLine();
                    }
                }
            }

            //process the map string
            if (mapFileAsString != "")
            {
                int index = 0;
                for(int i = 0; i < mapFileAsString.Length; i+=2)
                {
                    string tileId = mapFileAsString.Substring(i, 2);
                    mapTiles[index] = Convert.ToInt32(tileId, 16);
                    index++;
                }
            }

            //process path string
            if (pathsAsString != "")
            {
                int count = 0;
                while(count < pathsAsString.Length)
                {
                    //grab the first 8
                    try
                    {
                        string path = pathsAsString.Substring(count, 8);
                        count += 8;

                        string tileIdStart = path.Substring(0, 3);
                        string tileIdEnd = path.Substring(3, 3);
                        string pathType = path.Substring(6, 2);

                        mapPaths.Add(new Path(Convert.ToInt32(tileIdStart, 16), Convert.ToInt32(tileIdEnd, 16), Convert.ToInt32(pathType, 16)));
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        //oops, don't have 8 chars
                    }
                }
            }

            //process enemy string
            if (spawnListAsString != "")
            {
                string tickTime = spawnListAsString.Substring(0, 3);
                spawnList.Add(tickTime);
                for (int i = 3; i < spawnListAsString.Length; ++i)
                {
                    string ent = spawnListAsString.Substring(i, 1);
                    if(ent == "0")
                    {
                        string numBlanks = spawnListAsString.Substring(i+1,3);
                        int numBlanksAsInt = Convert.ToInt32(numBlanks, 16);
                        for(int k = 0; k < numBlanksAsInt; ++k)
                        {
                            spawnList.Add(ent);
                        }
                        i += 3;
                    }
                    spawnList.Add(ent);
                }
            }
        }
    }
}
