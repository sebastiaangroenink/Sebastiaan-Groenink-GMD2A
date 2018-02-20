using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Grid grid;

    public Transform target;

    public Transform seeker;

    public AIStats aiStats;

    public PathFinding pathFinding;

    public bool found;


    /// <summary>
    /// Grabs the object's own grid.
    /// </summary>
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }


    /// <summary>
    /// If the NPC has a target, start searching for the path towards it.
    /// </summary>
    private void Update()
    {
        if (target != null)
        {
            FindPath(seeker.position, target.position);
        }
    }
    /// <summary>
    ///  Once a target has been set, start by scanning down the area with the use of the node grid creates. Follows basic A* algorithm by checking a node's neighbours and 
    ///  Checking if one is closer to the target or not then continue with the node that's the nearest to the target IF not checked yet.
    /// </summary>
    /// <param name="startPos">Currently investigated node. </param>
    /// <param name="targetPos">The target's node. </param>
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Nodes startNode = grid.NodeFromWorldPoint(startPos);
        Nodes targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Nodes> openSet = new List<Nodes>();
        HashSet<Nodes> closedSet = new HashSet<Nodes>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Nodes currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);

                if (aiStats.busy)
                {
                    aiStats.busy = false;
                    aiStats.TaskReward();
                }
                return;
            }
            foreach (Nodes neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + getDist(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDist(currentNode, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }

            }
        }
    }

    
    /// <summary>
    /// Once a path has been found retraces the path for the player to walk to the target.
    /// Simple way of getting the distance between the currently checked node and the target node.
    /// </summary>
    /// <param name="starterNode">Currently investigated node. </param>
    /// <param name="endNode">The target's node. </param>
    /// 
    void RetracePath(Nodes starterNode, Nodes endNode)
    {
        List<Nodes> path = new List<Nodes>();
        Nodes currentNode = endNode;

        while (currentNode != starterNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
            
        }
        path.Reverse();

        grid.path = path;
    }   
    int getDist(Nodes a, Nodes b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridY);
        int distY = Mathf.Abs(a.gridY - b.gridX);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }
}
