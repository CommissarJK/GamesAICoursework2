using UnityEngine;
using System.Text;
using System.Collections.Generic;

public abstract class Pathfinder
{
    public Graph navGraph;
    public abstract List<int> findPath(int a, int b);
}

public class PathAgent : MonoBehaviour
{

    // Set from inspector
    public GameObject waypointSet;

    // Waypoints
    protected WaypointGraph waypoints;
    protected int? current = 420;

    protected List<int> path;
    protected Pathfinder pathfinder;
    protected GridCreator gridchecker;

    public float speed;
    protected static float NEARBY = 0.2f;
    protected static System.Random rnd = new System.Random();

    protected int counter = 0;

    void Start()
    {
        gridchecker = waypointSet.GetComponent<GridCreator>();
        //while (!gridchecker.finished) { }
        waypoints = new WaypointGraph(waypointSet);
        path = new List<int>();
        pathfinder = createPathfinder();
        pathfinder.navGraph = waypoints.navGraph;
    }

    void Update()
    {

        if (path.Count == 0)
        {
            // We don't know where to go next
            counter++;
            if (counter > 200)
            {
                counter = 0;
                generateNewPath();
            }
        }
        else {
            // Get the next waypoint position
            GameObject next = waypoints[path[0]];
            Vector3 there = next.transform.position;
            Vector3 here = transform.position;

            // Are we there yet?
            float distance = Vector3.Distance(here, there);
            if (distance < NEARBY)
            {
                // We're here
                current = path[0];
                path.RemoveAt(0);

                Debug.Log("Arrived at waypoint " + current);
            }
        }
    }

    void FixedUpdate()
    {
        if (path.Count > 0)
        {
            GameObject next = waypoints[path[0]];
            Vector3 position = next.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, position, speed);
        }
    }

    protected void generateNewPath()
    {

        if (current != null)
        {
            // We know where are
            List<int> nodes = waypoints.navGraph.nodes();

            if (nodes.Count > 0)
            {
                // Pick a random node to aim for
                int target = nodes[rnd.Next(nodes.Count)];
                Debug.Log("New target: " + target);
                // Find a path from here to there
                path = pathfinder.findPath(current.Value, target);
                Debug.Log("New path: " + writePath(path));

            }
            else {
                // There are zero nodes
                Debug.Log("No waypoints - can't select new target");
            }

        }
        else {
            // We don't know where we are

            // Find the nearest waypoint
            int? target = waypoints.findNearest(transform.position);

            if (target != null)
            {
                // Go to target
                path.Clear();
                path.Add(target.Value);

                Debug.Log("Heading for nearest waypoint: " + target);
            }
            else {
                // Couldn't find a waypoint
                Debug.Log("Can't find nearby waypoint to target");
            }

        }
    }

    public static string writePath(List<int> path)
    {
        var s = new StringBuilder();
        bool first = true;
        foreach (int t in path)
        {
            if (first)
            {
                first = false;
            }
            else {
                s.Append(", ");
            }
            s.Append(t);
        }
        return s.ToString();
    }

    public Pathfinder createPathfinder()
    {

        Heuristic h = new Heuristic((int a, int b) => { return Vector3.Distance(waypoints[a].transform.position, waypoints[b].transform.position); });
        return new AStarPathfinder(h);
    }

}



