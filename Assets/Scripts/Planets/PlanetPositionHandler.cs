using System.Collections.Generic; 
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetPositionHandler
{
    private readonly Grid grid;
    private Transform lastPlanet;
    public Layout Layout { get; private set; }
    private readonly float minDistanceBetweenPlanets;
    
   public PlanetPositionHandler(in float distanceBetweenPlanets, in Grid grid)
   {
        Layout = grid.Layout;
        this.grid = grid;
        minDistanceBetweenPlanets = distanceBetweenPlanets;

   }

   public void SetRandomPlanetPosition(Planet planet)
   {
       if (lastPlanet == null)
       {
           var yMid = Layout.YSpawnLines[0] + (Layout.YSpawnLines[1] - Layout.YSpawnLines[0]) / 2;
           var xMid = Layout.Left + (Layout.Right - Layout.Left) / 2;
           planet.transform.position = new Vector3(xMid, yMid, 0);
       }
       else
       {
           planet.transform.position = lastPlanet.position;
           GetCorrectedPosition(planet);
       }

       lastPlanet = planet.transform;
   }
   
   private Vector3 GetCorrectedPosition(Planet planet)
   {
       if (GetDistanceBetweenPlanetsSurfaces(planet.transform) <= minDistanceBetweenPlanets)
       {
           planet.transform.position =  GetNewPosition();
           var newPosition = GetCorrectedPosition(planet);
           if (newPosition != Vector3.back)
           {
               planet.transform.position = newPosition;
           }
       }
       return Vector3.back;
   }

   private Vector3 GetNewPosition()
   {
       var x = Random.Range(Layout.Left * 100, Layout.Right * 100) / 100;

       List<float> screenYSpawnLines = Layout.YSpawnLines,
           outOfScreenYSpawnLines = grid.GetOutOfScreenYSpawnLines();

       for(int i = 1; i < screenYSpawnLines.Count - 1; i++)
       {
           if (lastPlanet.position.y <= screenYSpawnLines[i])
           {
               return new Vector3(x, Random.Range(Layout.YSpawnLines[i], Layout.YSpawnLines[i + 1]), 0);
           }
       }

       for (int i = 0; i < outOfScreenYSpawnLines.Count; i++)
       {
           if (lastPlanet.position.y <= outOfScreenYSpawnLines[i])
           {
               return new Vector3(x, Random.Range(outOfScreenYSpawnLines[i], outOfScreenYSpawnLines[i + 1]), 0);
           }
       }
       
       return Vector3.zero;
   }
   
   private float GetDistanceBetweenPlanetsSurfaces(Transform currentPlanet)
   {
       var x = Mathf.Abs(lastPlanet.position.x - currentPlanet.position.x);
       var y = Mathf.Abs(lastPlanet.position.y - currentPlanet.position.y);
       var distanceBetweenCenters = Mathf.Sqrt(x * x + y * y);
       return distanceBetweenCenters - lastPlanet.localScale.x / 2 - currentPlanet.localScale.x / 2;
   }
}