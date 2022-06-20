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
        public List<Building> buildings { get; set; }
        public BuildingsManager()
        {
            buildings = new List<Building>()
            {
                new Building("BuildingA"),
                new Building("BuildingB"),
                new Building("BuildingC")
            };
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
