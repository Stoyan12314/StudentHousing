using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    [Serializable()]
    public class BuildingsManager
    {
        public List<Building> buildings = new List<Building>();
        public BuildingsManager()
        {
            buildings.Add(new Building("BuildingA"));
            buildings.Add(new Building("BuildingB"));
            buildings.Add(new Building("BuildingC"));      
        }
        public Building ReturnBuildingByName(string name)
        {
            foreach (Building building in buildings)
            {
                if (building.BuildingName == name)
                {
                    return building;
                }
            }
            return null;
        }
        public List<Building> Buildings()
        {
            return this.buildings;
        }
    }
}
