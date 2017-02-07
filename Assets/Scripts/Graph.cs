using UnityEngine;
using System.Collections.Generic;

public interface Graph{
	bool addNode (int a);
	bool addEdge (int a, int b);

	List<int> nodes();
	List<int> neighbours (int a);
}

public class AdjacencyListGraph : Graph {
	Dictionary<int, List<int>> waypoints= new Dictionary<int, List<int>>();

	public bool addNode(int a){
		if (waypoints.ContainsKey(a)) {
			return false;
		} else {
			waypoints.Add (a, new List<int> ());
			return true;
		}
	}

	public bool addEdge(int a, int b){
		if (waypoints[a].Contains(b)) {
			return false;
		} else {
			waypoints[a].Add(b);
			return true;
		}
	}

	public List<int> nodes(){
		List<int> temp = new List<int>();
		foreach(KeyValuePair<int, List<int>> element in waypoints){
			temp.Add (element.Key);	
		}
		return temp;
	}

	public List<int> neighbours(int a){
		return waypoints[a];
	}

}
