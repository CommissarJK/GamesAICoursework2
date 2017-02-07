using UnityEngine;
using System.Collections.Generic;
using Priority_Queue;

public delegate float Heuristic(int a, int b);

public class AStarPathfinder : Pathfinder
{

    protected Heuristic guessCost;

    public AStarPathfinder(Heuristic h)
    {
        guessCost = h;
    }

    public override List<int> findPath(int start, int goal)
    {
        SimplePriorityQueue<int> frontier = new SimplePriorityQueue<int>();
        Dictionary<int, int> visitedFrom = new Dictionary<int, int>();
        Dictionary<int, int> costSoFar = new Dictionary<int, int>();
        frontier.Enqueue(start, 0);
        visitedFrom[start] = -1;
        costSoFar[start] = 0;
        while (frontier.Count > 0)
        {
            int current = frontier.Dequeue();
            if (current == goal) break;
            List<int> neighbours = navGraph.neighbours(current);
            foreach (int next in neighbours)
            {
                int nextCost = costSoFar[current];

                if (!costSoFar.ContainsKey(next) || nextCost < costSoFar[next])
                {
                    frontier.Enqueue(next, nextCost + guessCost(next, goal));
                    visitedFrom[next] = current;
                    costSoFar[next] = nextCost;
                }
            }
        }
        List<int> path = new List<int>();
        int nxt = goal;
        while (nxt != start)
        {
            path.Insert(0, nxt);
            nxt = visitedFrom[nxt];
        }

        //Debug.Log ("giuydskijhdsakjh");
        return path;
    }
}