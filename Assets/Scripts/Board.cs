using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
    public static int CurrentLevel = 0;

    public MenuManager menu;

    public int pointsPerCell = 50;

    public Timer timer;

    public Sprite[] close_box_sprites;

    public GameObject box_prefav;
    public int side_w = 7;
    public int side_h = 4;
    public float size_scale_position = 1.08f;

    private static GameObject[,] cellMatrix;
    private int[][] neighbours = {                       new int[] {  0, -1 },
                                   new int[] { -1,  0 },                       new int[] {  1,  0 },
                                                         new int[] {  0,  1 }                       };

    private Cell movingCell;
    private bool acceptsMovement = true;

    private bool canPlay = true;

    public RecycleCan[] recycleCans;
    public Sprite empty_box_sprite;

    private Player player;
    private TrashFactory trashFactory;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        trashFactory = GetComponent<TrashFactory>();

        Restart();
    }

    public void StartGame()
    {
        canPlay = true;
        timer.StartTimer();
    }

    public void PauseGame()
    {
        canPlay = false;
        timer.Pause();
    }

    public void ResumeGame()
    {
        canPlay = true;
        timer.Resume();
    }

    public void PrepareBoard()
    {
    }

    public void Tap(Vector2 touch)
    {
        if (canPlay)
        {
            if (acceptsMovement)
            {
                bool touchFound = false;
                bool emptyFound = false;
                for (int i = 0; !touchFound && i < side_w; i++)
                {
                    for (int j = 0; !touchFound && j < side_h; j++)
                    {
                        GameObject cell_obj = cellMatrix[i, j];
                        if (cell_obj != null && cell_obj.collider2D.OverlapPoint(touch))
                        {
                            touchFound = true;
                            Cell cell = cell_obj.GetComponent<Cell>();
                            if (cell.getCellType() != Cell.BOX_CLEAN)
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

                                        Bounds boundz = ((SpriteRenderer)cell_obj.renderer).sprite.bounds;
                                        Vector3 zize = (boundz.min - boundz.max) * size_scale_position;
                                        float w = zize.x;
                                        float h = zize.y;

                                        cell.Move(neig_x, neig_y, neig_x * w, neig_y * h);

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
                for (int i = 0; !touchFound && i < side_w; i++)
                {
                    for (int j = 0; !touchFound && j < side_h; j++)
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

        Sprite[] trashes = trashFactory.GetTrash(cells.Count, cellType);
        for (int i = 0; i < cells.Count; i++)
        {
            GameObject box_object = (GameObject)cells[i];
            Cell box_cell = box_object.GetComponent<Cell>();

            box_cell.setCellType(Cell.BOX_CLEAN);
            if (trashes[i] != null)
            {
                recycleCans[cellType].AddToTrash(1);
                ((SpriteRenderer)box_cell.renderer).sprite = trashes[i];
            }
            else
            {
                ((SpriteRenderer)box_cell.renderer).sprite = empty_box_sprite;
            }
        }
    }

    private void CalculatePoints(ArrayList cells)
    {
        player.AddPoints(cells.Count * pointsPerCell);
    }

    private bool InBoard(int x, int y)
    {
        return x >= 0 && y >= 0 && x < side_w && y < side_h;
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
        if (cellMatrix != null)
        {
            foreach (GameObject child in cellMatrix)
            {
                if (child != null)
                    GameObject.Destroy(child.gameObject);
            }
        }

        cellMatrix = new GameObject[side_w, side_h];

        int emptyX = Random.Range(0, side_w);
        int emptyY = Random.Range(0, side_h);

        for (int i = 0; i < side_w; i++)
        {
            for (int j = 0; j < side_h; j++)
            {
                if (i != emptyX || j != emptyY)
                {
                    GameObject new_box;
                    int rand = Random.Range(0, recycleCans.Length);
                    Sprite box_sprite = close_box_sprites[rand];
                    new_box = (GameObject)GameObject.Instantiate(box_prefav);
                    ((SpriteRenderer)new_box.renderer).sprite = box_sprite;
                    Cell cell = new_box.GetComponent<Cell>();
                    cell.setCellType(rand);

                    new_box.transform.parent = gameObject.transform;

                    Vector3 zize = (box_sprite.bounds.min - box_sprite.bounds.max) * size_scale_position;
                    float w = zize.x;
                    float h = zize.y;

                    new_box.transform.localPosition = new Vector3(i * w, j * h);

                    cell.cell_x = i;
                    cell.cell_y = j;

                    cellMatrix[i, j] = new_box;
                }
            }
        }
    }

    public void TimeOut()
    {
        canPlay = false;
        player.SubLife();
        menu.OpenGameMenu();
    }
}
