using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
    public int pointsPerCell = 50;

    private GameObject truck_go;
    public Truck truck;

    private Timer timer;

    public GameObject cell_prefav;
    public int side = 5;

    private float w = 20;
    private float init_x;
    private float init_y;

    private static GameObject[,] cellMatrix;
    private int[][] neighbours = {                       new int[] {  0, -1 },
                                   new int[] { -1,  0 },                       new int[] {  1,  0 },
                                                         new int[] {  0,  1 }                       };

    private Cell movingCell;
    private bool acceptsMovement = true;

    private bool canPlay = true;

    public RecycleCanText[] trashCans;

    private Player player;
    private TrashFactory trashFactory;

    // Use this for initialization
    void Start()
    {
        timer = GetComponent<Timer>();
        player = GetComponent<Player>();
        trashFactory = GetComponent<TrashFactory>();

        truck.Init();
        truck_go = truck.gameObject;

        Restart();

        truck.GoIn();
    }

    public void StartGame()
    {
        canPlay = true;
        timer.StartTimer();
    }

    public void Tap(Vector2 touch)
    {
        if (canPlay)
        {
            if (acceptsMovement)
            {
                bool touchFound = false;
                bool emptyFound = false;
                for (int i = 0; !touchFound && i < side; i++)
                {
                    for (int j = 0; !touchFound && j < side; j++)
                    {
                        GameObject cell_obj = cellMatrix[i, j];
                        if (cell_obj != null && cell_obj.collider2D.OverlapPoint(touch))
                        {
                            touchFound = true;
                            Cell cell = cell_obj.GetComponent<Cell>();
                            if (cell.getCellType() != Cell.CELL_TYPE_FREE)
                            {
                                int neig_x;
                                int neig_y;
                                for (int t = 0; !emptyFound && t < neighbours.Length; t++)
                                {
                                    neig_x = cell.cell_x + neighbours[t][0];
                                    neig_y = cell.cell_y + neighbours[t][1];
                                    if (InBoard(neig_x, neig_y) && cellMatrix[neig_x, neig_y] == null)
                                    {
                                        emptyFound = true;
                                        cellMatrix[i, j] = null;
                                        cellMatrix[neig_x, neig_y] = cell_obj;
                                        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(init_x + (neig_x * w), init_y + (neig_y * w)));

                                        cell.Move(neig_x, neig_y, pos.x, pos.y);

                                        movingCell = cell;
                                        acceptsMovement = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void LongTap(Vector2 touch)
    {
        if (canPlay)
        {
            if (acceptsMovement)
            {
                bool touchFound = false;
                bool emptyFound = false;
                for (int i = 0; !touchFound && i < side; i++)
                {
                    for (int j = 0; !touchFound && j < side; j++)
                    {
                        GameObject cell_obj = cellMatrix[i, j];
                        if (cell_obj != null && cell_obj.collider2D.OverlapPoint(touch))
                        {
                            touchFound = true;
                            int originType = cell_obj.GetComponent<Cell>().getCellType();

                            Queue toCheck = new Queue();
                            toCheck.Enqueue(cell_obj);
                            ArrayList alreadyCheked = new ArrayList();
                            ArrayList cellToChange = new ArrayList();
                            int neig_x;
                            int neig_y;
                            Cell currentCell;
                            Cell neigCell;

                            GameObject current;
                            while (toCheck.Count > 0)
                            {
                                current = (GameObject)toCheck.Dequeue();
                                if (!alreadyCheked.Contains(current))
                                {
                                    alreadyCheked.Add(current);
                                    currentCell = current.GetComponent<Cell>();

                                    if (currentCell.getCellType() == originType)
                                    {
                                        cellToChange.Add(current);
                                        for (int t = 0; !emptyFound && t < neighbours.Length; t++)
                                        {
                                            neig_x = currentCell.cell_x + neighbours[t][0];
                                            neig_y = currentCell.cell_y + neighbours[t][1];
                                            if (InBoard(neig_x, neig_y) && cellMatrix[neig_x, neig_y] != null)
                                            {
                                                neigCell = cellMatrix[neig_x, neig_y].GetComponent<Cell>();
                                                toCheck.Enqueue(cellMatrix[neig_x, neig_y]);
                                            }
                                        }
                                    }
                                }
                            }
                            ChangeCells(cellToChange, originType);
                        }
                    }
                }
            }
        }
    }

    private void ChangeCells(ArrayList cells, int cellType)
    {
        CalculatePoints(cells);

        GameObject[] trashes = trashFactory.GetTrash(cells.Count, 0);
        for (int i = 0; i < cells.Count; i++)
        {
            trashCans[((GameObject)cells[i]).GetComponent<Cell>().getCellType()].AddToTrash(1);
            ((GameObject)cells[i]).GetComponent<Cell>().setCellType(Cell.CELL_TYPE_FREE);
            if (trashes[i] != null)
            {
                trashes[i].transform.position = ((GameObject)cells[i]).transform.position;
            }
        }
    }

    private void CalculatePoints(ArrayList cells)
    {
        player.AddPoints(cells.Count * pointsPerCell);
    }

    private bool InBoard(int x, int y)
    {
        return x >= 0 && y >= 0 && x < side && y < side;
    }

    // Update is called once per frame
    void Update()
    {
        if (!acceptsMovement)
        {
            acceptsMovement = !movingCell.isMoving();
        }
    }

    public void Restart()
    {
        foreach (Cell child in transform.GetComponentsInChildren<Cell>())
        {
            GameObject.Destroy(child.gameObject);
        }

        init_x = 80;//(truck_go.transform.localScale.x - (side * w)) / 2 + w / 2;
        init_y = 30;// (Screen.height - (side * w)) / 2 + w / 2;

        cellMatrix = new GameObject[side, side];

        int emptyX = Random.Range(0, side);
        int emptyY = Random.Range(0, side);

        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                if (i != emptyX || j != emptyY)
                {
                    GameObject newCell = (GameObject)GameObject.Instantiate(cell_prefav);
                    Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(init_x + i * w, init_y + j * w));
                    pos.z = 0;
                    newCell.transform.position = pos;

                    Cell cell = newCell.GetComponent<Cell>();
                    float rand = Random.Range(0, 1f);
                    if (rand < 0.2)
                    {
                        cell.setCellType(Cell.CELL_TYPE_1);
                    }
                    else if (rand < 0.4)
                    {
                        cell.setCellType(Cell.CELL_TYPE_2);
                    }
                    else if (rand < 0.6)
                    {
                        cell.setCellType(Cell.CELL_TYPE_3);
                    }
                    else if (rand < 0.8)
                    {
                        cell.setCellType(Cell.CELL_TYPE_4);
                    }
                    else
                    {
                        cell.setCellType(Cell.CELL_TYPE_5);
                    }
                    cell.cell_x = i;
                    cell.cell_y = j;

                    cellMatrix[i, j] = newCell;
                }
            }
        }
    }

    public void TimeOut()
    {
        canPlay = false;
        truck.GoOut();
    }
}
