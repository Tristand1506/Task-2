using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RTS_POE
{
    class Map
    {
        //global map size variable...
        static public int mapSize = 20;

        //unit and building arrays...
        public Unit[] units;
        public Building[] buildings;


        //used to track randomly generated resource nodes...
        public string[,] grid = new string[mapSize, mapSize];

        //random number generator...
        Random rnd = new Random();


        // this is a map constructor ... 
        public Map(int startingUnits, int startingBuildings)
        {
            units = new Unit[startingUnits];
            buildings = new Building[startingBuildings];

            // thriggers all the building unite and node gens...
            nodeGen();
            unitGen(units.Length);
            buildingGen(buildings.Length);

            // this method dosent actually do anything but i left it in incase it does....
            mapUpdate();
        }

        // generates node info for the grid array
        private void nodeGen()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    string rType = "";
                    // pregens the resource density to determin the collectors efficiency
                    int rDensity = (rnd.Next(35555) % 90) + 10;
                    // selects a resource type
                    switch (rnd.Next(0, 4))
                    {
                        case 0: rType = "Oil"; break;
                        case 1: rType = "Metal"; break;
                        case 2: rType = "Stone"; break;
                        case 3: rType = "Magical Ground Energy"; break;
                    }
                    //adds node to the grid array and assigns a rndom ammout as a resource maximum...
                    grid[i,j] = rType + "," + rnd.Next(100, 10000)+","+rDensity;
                }
            }
        }

        public void buildingGen(int numBuildings)
        {
            // loops to crete new units
            for (int i = 0; i < numBuildings; i++)
            {

                // assigns random x and y values 
                int newX = rnd.Next(0, mapSize);
                int newY = rnd.Next(0, mapSize);
                int team = i % 2;

                

                // randomly gives building type
                switch (rnd.Next(0,2))
                {
                    case 0: buildings[i] = new ResourceBuilding("Extractor", newX, newY, 300,team, "♦",grid[newX,newY]); break;
                    case 1: buildings[i] = new FactoryBuilding("Factory", newX, newY, 200, team, "♥", rnd.Next(0,2), rnd.Next(5, 10)); break;

                }
            }
        }


        public void unitGen(int numUnits)
        {
            // loops to crete new units
            for (int i = 0; i < numUnits; i++)
            {

                // assigns random x and y values 
                int newX = rnd.Next(0, mapSize);
                int newY = rnd.Next(0, mapSize);
                int team = i % 2;
                int tempAttack = 0;

                // randomly assigns units damage values
                switch (rnd.Next(0, 4))
                {
                    case 0: tempAttack = 5; break;
                    case 1: tempAttack = 10; break;
                    case 2: tempAttack = 15; break;
                    case 3: tempAttack = 20; break;
                }

                // randomly gives unit type
                switch (rnd.Next(0, 2))
                {
                    case 0: units[i] = new MeleeUnit("Bruiser",newX, newY, 200, 1, tempAttack, 1, team, "♣", false); break;
                    case 1: units[i] = new RangeUnit("Ranger",newX, newY, 150, 2, tempAttack, 4, team, "♠", false); break;

                }
            }
        }

        public void mapUpdate()
        {
            // sets nuetral values for the map array
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    grid[i,j] = " • ";
                }
                
            }

            //places units
            for (int i = 0; i < units.Length; i++)
            {
               
            }
        }

        public void Save()
        {
            File.Create(Environment.CurrentDirectory + "\\MeleeUnits.txt").Close();
            File.Create(Environment.CurrentDirectory + "\\RangeUnits.txt").Close();
            File.Create(Environment.CurrentDirectory + "\\ResourcesBuildings.txt").Close();
            File.Create(Environment.CurrentDirectory + "\\FactoryBuildings.txt").Close();
            foreach (Unit u in units)
            {
                u.saveFile();
            }
            foreach (Building b in buildings)
            {
                b.saveFile();
            }
        }

        public void Load()
        {
            
        }
    }
}
