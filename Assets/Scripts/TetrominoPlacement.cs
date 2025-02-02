using UnityEngine;

/// <summary>
/// Class <c>TetrominoPlacement</c> holds active tetromino position.
/// </summary>
public class TetrominoPlacement : MonoBehaviour{
    
    public Material trasparentMaterial;
    public Material wrongPlacementMaterial;

    private CreateEnvironment _cE;
    private TetrominoGenerating _tGenerating;
    private Cube[,,,] _grid;
    private Tetromino _tetromino;
    private ScoreCounter _scoreCounter;

    
    public void Start(){
        GameObject grid = gameObject.transform.parent.Find("Grid").gameObject;
        _cE = grid.GetComponent<CreateEnvironment>();

        _tGenerating = gameObject.GetComponent<TetrominoGenerating>();
        _scoreCounter = grid.GetComponent<ScoreCounter>();
        
        _tetromino = _tGenerating.GetTetromino();
        _grid = _cE.Grid;
        
    }
    
    /// <summary>
    /// Method <c>IsTetrominoInsideCube</c> resolves if tetromino (<c>block</c>) is inside the cube-grid.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool IsTetrominoInsideCube(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        int gridSize = grid.GetLength(0);

        bool isInside = true;
        // find out if every part of the tetromino is inside the cube-grid
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            float i = tetromino.BlockPositions[_].x;
            float j = tetromino.BlockPositions[_].y;
            float k = tetromino.BlockPositions[_].z;
            
            if (((int)(x + i) < 0 || (int)(x + i) >= gridSize) ||
                ((int)(y + j) < 0 || (int)(y + j) >= gridSize) ||
                ((int)(z + k) < 0 || (int)(z + k) >= gridSize)){
                isInside = false;
                break;
            }
        }
        return isInside;
    }
    
    /// <summary>
    /// Method <c>IsCoveringOtherTetrominos</c> resolves if tetromino (<c>block</c>) is covering another
    /// tetromino already placed inside the cube-grid.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool IsCoveringOtherTetrominos(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;


        bool isCovering = false;
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            // if any part of the tetromino is covering any other block
            float i = tetromino.BlockPositions[_].x;
            float j = tetromino.BlockPositions[_].y;
            float k = tetromino.BlockPositions[_].z;

            if (grid[(int)(x + i), (int)(y + j), (int)(z + k), 0].IsOccupied()){
                isCovering = true;
                break;
            }
        }
        return isCovering;
    }

    /// <summary>
    /// Method <c>CanPlaceTetromino</c> resolves if tetromino (<c>block</c>) can be place on the <c>localLocation</c> position.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool CanPlaceTetromino(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        // the whole tetromino is inside the cube-grid
        bool isInside = IsTetrominoInsideCube(grid, tetromino, localLocation);
        if (isInside){
            // no part of the tetromino is covering other tetromino
            bool isCoveringOther = IsCoveringOtherTetrominos(grid, tetromino, localLocation);
            return !isCoveringOther;
        }
        return false;
    }

    
    /// <summary>
    /// Method <c>SaveTetrominoPosition</c> puts a tetromino (<c>block</c>) inside the cube-grid if possible.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public void SaveTetrominoPosition(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;
        
        Renderer renderer;
        
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            // color relevant cube-grid's cubes with tetromino's color based on each tetromino's block position
            Transform child = tetromino.Blocks[_];
            renderer = child.GetComponent<Renderer>();
            Material blockMaterial = renderer.sharedMaterial;

            float i = tetromino.BlockPositions[_].x;
            float j = tetromino.BlockPositions[_].y;
            float k = tetromino.BlockPositions[_].z;

            grid[(int)(x + i), (int)(y + j), (int)(z + k), 0].SetMaterial(blockMaterial);
            grid[(int)(x + i), (int)(y + j), (int)(z + k), 1].SetMaterial(trasparentMaterial);
        }
    }
    
    
    /// <summary>
    /// Method <c>ShowTetrominoPreview</c> show a preview of a new possible tetromino on <c>localLocation</c>.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public void ShowTetrominoPreview(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;
        
        Material material = null;
        if (IsCoveringOtherTetrominos(grid, tetromino, localLocation)){
            material = wrongPlacementMaterial;
        }
        
        Renderer renderer;
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            // color relevant cube-grid's cubes with tetromino's color based on each tetromino's block position
            Transform child = tetromino.Blocks[_];
            renderer = child.GetComponent<Renderer>();

            if (material == null){
                material = renderer.sharedMaterial;
            }

            float i = tetromino.BlockPositions[_].x;
            float j = tetromino.BlockPositions[_].y;
            float k = tetromino.BlockPositions[_].z;
            
            grid[(int)(x + i), (int)(y + j), (int)(z + k), 1].SetMaterial(material);
            grid[(int)(x + i), (int)(y + j), (int)(z + k), 1].SetActive(true);
        }
    }
    
    /// <summary>
    /// Method <c>HideTetrominoPreview</c> hide a preview of a new possible tetromino from <c>localLocation</c>.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public void HideTetrominoPreview(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            // color relevant cube-grid's cubes with tetromino's color based on each tetromino's block position

            float i = tetromino.BlockPositions[_].x;
            float j = tetromino.BlockPositions[_].y;
            float k = tetromino.BlockPositions[_].z;

            grid[(int)(x + i), (int)(y + j), (int)(z + k), 1].SetMaterial(trasparentMaterial);
            grid[(int)(x + i), (int)(y + j), (int)(z + k), 1].SetActive(false);
        }
    }

    /// <summary>
    /// Method <c>OnTetrominoPlace</c> on "Place" button click saves current tetromino on its current
    /// location and increases <c>ScoreCounter</c> by 4. 
    /// </summary>=
    public void OnTetrominoPlace(){
        // Tetromino tetromino = _tGenerating.GetTetromino();
        Location localLocation = _tGenerating.localLocation;
        
        if (CanPlaceTetromino(_grid, _tetromino, localLocation)){
            SaveTetrominoPosition(_grid, _tetromino, localLocation);
            _scoreCounter.IncreaseScore(4);
            
            _tGenerating.DeleteTetromino();
            _tetromino = _tGenerating.GetTetromino();
            ShowTetrominoPreview(_grid, _tetromino, _tGenerating.localLocation);
        }
    }
}
