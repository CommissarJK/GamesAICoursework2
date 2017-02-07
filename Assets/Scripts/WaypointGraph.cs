using UnityEngine;
using System.Collections.Generic;

public class WaypointGraph : MonoBehaviour {
	public Graph navGraph;
	public List<GameObject> waypoints;

	public GameObject this[int i] {
		get { return waypoints [i]; }
		set { waypoints [i] = value; }
	}


	public WaypointGraph(GameObject waypointSet) {

		waypoints = new List<GameObject> ();
		navGraph = new AdjacencyListGraph ();

		findWaypoints (waypointSet);
		buildGraph ();
	}

	private void findWaypoints(GameObject waypointSet) {
        Debug.Log("Waypoint");
		if (waypointSet != null) {
			foreach (Transform t in waypointSet.transform) {
				waypoints.Add (t.gameObject);
			}
			Debug.Log("Found " + waypoints.Count + " waypoints.");

		} else {
			Debug.Log ("No waypoints found.");

		}
	}

	private void buildGraph(){
		//GameObject temp = GameObject.Find ("GridCreator");//GetComponent<GridCreator>.getWidth();
		//int width = temp.GetComponent<GameObject>().width;
		int width = 40;
		int height = 20;
		int n = waypoints.Count;

		navGraph = new AdjacencyListGraph();
		for (int i = 0; i < n; i++) {
			navGraph.addNode (i);
		}

		int side = 0;
		for (int i = 0; i < n; i++) {
			Vector2 pos = getVector (i,width);
			if (pos.y % 2 == 0) {
				side = -1;
			} else {
				side = 1;
			}
			if (pos.x - 1 >= 0){ // add left edge
				navGraph.addEdge (i,getNo(pos.x-1f,pos.y, width));
			}
			if (pos.x + 1 < width){ // add right edge
				navGraph.addEdge (i,getNo(pos.x+1f,pos.y, width));
			}
			if (pos.y - 1 >= 0) {// add directly below edge
				navGraph.addEdge (i,getNo(pos.x,pos.y-1f, width));
			}
			if (pos.y + 1 < height) { // add directly above edge
				navGraph.addEdge (i,getNo(pos.x,pos.y+1f, width));
			}
			if (pos.y -1 >= 0 && pos.x + side >= 0 && pos.x + side < width) { // add below to the side edge
				navGraph.addEdge (i,getNo(pos.x+side,pos.y-1f, width));
			}
			if (pos.y +1 < height && pos.x + side >= 0 && pos.x + side < width) { // add above to the side edge
				navGraph.addEdge (i,getNo(pos.x+side,pos.y+1f, width));
			}

		}
	}


	protected int getX(int point, int gridWidth){
		return point - (gridWidth * getY (point, gridWidth));
	}

	protected int getY(int point, int gridWidth){
		return point / gridWidth;
	}

	protected Vector2 getVector(int point, int gridWidth){
		return new Vector2 (getX(point, gridWidth),getY(point, gridWidth));
	}

	protected int getNo(float _x, float _y, int _width){
		return (int)((_y*_width)+_x);
	}

    public int? findNearest(Vector3 here)
    {
        int? nearest = null;

        if (waypoints.Count > 0)
        {
            nearest = 0;
            Vector3 there = waypoints[0].transform.position;
            float minDistance = Vector3.Distance(here, there);

            for (int i = 1; i < waypoints.Count; i++)
            {
                there = waypoints[i].transform.position;
                float distance = Vector3.Distance(here, there);
                if (distance < minDistance)
                {
                    nearest = i;
                }
            }
        }
        return nearest;
    }
}
