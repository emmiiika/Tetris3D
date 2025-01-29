using UnityEngine;

/// <summary>
/// Class <c>TetrominoPlacement</c> .
/// </summary>
public class TetrominoPlacement : MonoBehaviour{
    /// <summary>
    /// Method <c>IsTetrominoInsideCube</c> resolves if tetromino (<c>block</c>) is inside the cube-grid.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool IsTetrominoInsideCube(Cube[,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        int gridSize = grid.Length;

        bool isInside = true;
        // find out if every part of the tetromino is inside the cube-grid
        foreach (Transform child in tetromino.Blocks){
            float i = child.localPosition.x;
            float j = child.localPosition.y;
            float k = child.localPosition.z;

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
    public bool IsCoveringOtherTetrominos(Cube[,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        int gridSize = grid.Length;

        bool isCovering = false;
        foreach (Transform child in tetromino.Blocks){
            // if any part of the tetromino is covering any other block
            float i = child.localPosition.x;
            float j = child.localPosition.y;
            float k = child.localPosition.z;

            if (grid[(int)(x + i), (int)(y + j), (int)(z + k)].IsOccupied()){
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
    public bool CanPlaceTetromino(Cube[,,] grid, Tetromino tetromino, Location localLocation){
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
    public void SaveTetrominoPosition(Cube[,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        Renderer renderer;
        foreach (Transform child in tetromino.Blocks){
            // color relevant cube-grid's cubes with tetromino's color based on each tetromino's block position
            renderer = child.GetComponent<Renderer>();
            Material blockMaterial = renderer.sharedMaterial;

            float i = child.localPosition.x;
            float j = child.localPosition.y;
            float k = child.localPosition.z;

            grid[(int)(x + i), (int)(y + j), (int)(z + k)].SetMaterial(blockMaterial);
        }
    }
    
    
    public void ShowTetrominoPosition(Cube[,,] grid, Tetromino tetromino, Location localLocation){
            
    }
    
    
    public bool PlaceTetromino(Cube[,,] grid, Tetromino tetromino, Location localLocation){
        if (CanPlaceTetromino(grid, tetromino, localLocation)){
            SaveTetrominoPosition(grid, tetromino, localLocation);
            return true;
        }
        ShowTetrominoPosition(grid, tetromino, localLocation);
        return false; 
    }

    public void onPlace(){
        
    }
}

/*
    // generate random block
    _gb = gameObject.GetComponent<GenerateTetromino>();
    Tetromino block1 = _gb.ChooseBlock();
    Tetromino block2 = _gb.ChooseBlock();
    Tetromino block3 = _gb.ChooseBlock();
    
    // place the random block
    _bp = gameObject.GetComponent<TetrominoPlacement>();
    // bool isB1Placed =_bp.PlaceBlock(_grid, block1, new Location(-1, 0, 0));
    // Debug.Log(block1.name + " was placed: " + isB1Placed);
    bool isB2Placed = _bp.PlaceTetromino(_grid, block2, new Location(0, 0, 0));
    // Debug.Log(block2.name + " was placed: " + isB2Placed);
    // bool isB3Placed = _bp.PlaceBlock(_grid, block3, new Location(2, 0, 0));
    // Debug.Log(block3.name + " was placed: " + isB3Placed);
*/