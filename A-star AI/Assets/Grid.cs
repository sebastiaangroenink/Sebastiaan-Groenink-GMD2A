using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Vector2 gridSize;

    public float nodeRadius;

    public LayerMask unwalkableMask;

    Nodes[,] grid;

    public float nodeDiameter;
    public int gridSizeX;
    public int gridSizeY;

    /// <summary>
    /// Sets grid size to create nodes for A* to work with.
    /// </summary>
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        CreateGrid();
    }

    /// <summary>
    /// Simple script to create a grid of nodes for A*. Also checks if tiles are blocked by obstacles and preventing them to be 'active'nodes.
    /// </summary>
    void CreateGrid()
    {
        grid = new Nodes[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[i, y] = new Nodes(walkable, worldPoint, i, y);
            }
        }
    }
    /// <summary>
    /// Adds all near active nodes to a list for later use.
    /// </summary>
    /// <param name="node">List of all active nodes. </param>
    /// <returns></returns>
    public List<Nodes> GetNeighbours(Nodes node)
    {
        List<Nodes> neighbours = new List<Nodes>();

        for (int i = -1; i <= 1; i++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (i == 0 && y == 0)

                    continue;

                int checkX = node.gridX + i;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSize.y)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }

            }
        }
        return neighbours;
    }
    /// <summary>
    /// Clamps all nodes to not make them go out of bounds with the grid.
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns> returns the grid's clamped value </returns>
    public Nodes NodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridSize.x / 2) / gridSize.x;
        float percentY = (worldPos.z + gridSize.y / 2) / gridSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Nodes> path;

    /// <summary>
    /// Visual tool to assist in checking whether the nodes work and are placed correctly or not.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));

        if (grid != null)
        {
            foreach (Nodes node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if (path != null)

                    if (path.Contains(node))

                        Gizmos.color = Color.black;
                Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
