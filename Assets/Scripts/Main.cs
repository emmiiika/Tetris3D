using UnityEngine;

public class Main : MonoBehaviour{
    private CreateEnvironment _cE;

    private Cube[,,,] _grid;
    private ScoreCounter _scoreCounter;
    
    
    public GameObject cubePrefab;
    public int gridSize = 5; // cube-grid size
    public Material transparentMaterial;
    
    
    public void Awake(){
        _cE = gameObject.AddComponent<CreateEnvironment>();
        _cE.Init(cubePrefab, transparentMaterial, gridSize);
        
        _grid = _cE.Grid;
        
        /*if (_grid != null || true)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    for (int z = 0; z < gridSize; z++)
                    {
                        // Process each cube in the grid
                        _grid[x, y, z, 0].SetMaterial(transparentMaterial);
                        _grid[x, y, z, 0].SetActive(true);
                        // Add your processing logic here
                    }
                }
            }
        }*/
        _scoreCounter = gameObject.GetComponent<ScoreCounter>();
        _scoreCounter.InitScoreCounter();
        
        GameObject tetromino = gameObject.transform.parent.Find("Tetromino").gameObject;
    }

    public void Show(){
        _cE.ShowEnvironment();
    }
    
    public void Hide(){
        _cE.HideEnvironment();
    }
    
    
}