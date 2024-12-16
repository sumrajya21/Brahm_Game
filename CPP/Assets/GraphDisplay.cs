using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphNode : MonoBehaviour
{
    public List<Vector2> nodeValue = new List<Vector2>();
    public List<Vector2> neighbours = new List<Vector2>();
    private Renderer Renderer;

    public float vote;

    private static int red = 0;
    private static int blue = 0;

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
        float col = Random.Range(0.0f, 1.0f);
        if (col > 0.5f)
        {
            Renderer.material.color = Color.blue;
            blue++;
        }
        else
        {
            Renderer.material.color = Color.red;
            red++;
        }
        Renderer.material.color = new Color(1 - col, 0, col, 1);
        vote = col;
    }

    private void Update()
    {
        /*Debug.Log("red voters");
        Debug.Log(red);
        Debug.Log("Blue voters");
        Debug.Log(blue);*/
        //RedVoter.text = red.ToString();
        //BlueVoter.text = blue.ToString();
    }

}

public class GraphDisplay : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject wall;
    public GraphNode GraphNode;
    public float nodeSpacing = 2.0f;
    public int height = 2;
    public int width = 2;
    public Text RedVoter;
    public Text BlueVoter;



    private GameObject[] nodes;

    private List<List<int>> permutations = new List<List<int>>();


    private int[,] adjacencyMatrix = {
    {0, 1, 1, 0},
    {1, 0, 0, 1},
    {1, 0, 0, 1},
    {0, 1, 1, 0}
};

    void Start()
    {
        nodes = new GameObject[width * height];
        int index = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 nodePosition = new Vector3(x * nodeSpacing, y * nodeSpacing, 0);
                nodes[index] = Instantiate(nodePrefab, nodePosition, Quaternion.identity);
                nodes[index].AddComponent<GraphNode>().nodeValue.Add(new Vector2(x, y));
                // Add neighbors by index
                if (x < width - 1)
                    nodes[index].GetComponent<GraphNode>().neighbours.Add(new Vector2(x + 1, y)); // Right neighbor
                if (x > 0)
                    nodes[index].GetComponent<GraphNode>().neighbours.Add(new Vector2(x - 1, y)); // Left neighbor
                if (y < height - 1)
                    nodes[index].GetComponent<GraphNode>().neighbours.Add(new Vector2(x, y + 1)); // Top neighbor
                if (y > 0)
                    nodes[index].GetComponent<GraphNode>().neighbours.Add(new Vector2(x, y - 1)); // Bottom neighbor

                index++;

            }

        }




        for (int i = 0; i < nodes.Length; i++)
        {
            GraphNode nodeScript = nodes[i].GetComponent<GraphNode>();
            foreach (Vector2 neighborIndex in nodeScript.neighbours)
            {
                int neighbor = (int)neighborIndex.x * height + (int)neighborIndex.y;
                DrawConnection(nodes[i].transform.position, nodes[neighbor].transform.position);
            }
        }
        GeneratePermutations(new List<int>(), 0);

        foreach (var perm in permutations)
        {
            string permStr = "";
            foreach (var item in perm)
            {
                permStr += item + " ";
            }
            Debug.Log(permStr);
        }

    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(wall, new Vector3((i * 4f) + 1, 5, -1), Quaternion.identity);
            }

        }
    }

    void DrawConnection(Vector3 from, Vector3 to)
    {
        GameObject line = new GameObject("Connection");
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.material = Resources.Load<Material>("Materials/Blue");
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }

    void GeneratePermutations(List<int> currentPermutation, int index)
    {
        if (index == nodes.Length)
        {
            permutations.Add(new List<int>(currentPermutation));
            return;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (!currentPermutation.Contains(i))
            {
                currentPermutation.Add(i);
                GeneratePermutations(currentPermutation, index + 1);
                currentPermutation.Remove(i);
            }
        }
    }


}

