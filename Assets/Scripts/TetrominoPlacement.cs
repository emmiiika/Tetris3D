using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

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
        int x = (int)localLocation.X;
        int y = (int)localLocation.Y;
        int z = (int)localLocation.Z;

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
    private bool IsCoveringOtherTetrominos(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.X;
        int y = (int)localLocation.Y;
        int z = (int)localLocation.Z;


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
    private bool CanPlaceTetromino(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
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
    private void SaveTetrominoPosition(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.X;
        int y = (int)localLocation.Y;
        int z = (int)localLocation.Z;
        
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
        CheckForWholeLayers(grid, tetromino, localLocation);
    }
    
    private void CheckForWholeLayers(Cube[,,,] grid, Tetromino tetromino, Location localLocation) {
        int gridSizeX = grid.GetLength(0); // length in x dimension
        int gridSizeY = grid.GetLength(1); // length in y dimension
        int gridSizeZ = grid.GetLength(2); // length in z dimension
        Debug.Log($"Grid Size - X: {gridSizeX}, Y: {gridSizeY}, Z: {gridSizeZ}");
        
        List<int> layersX = new List<int>();
        List<int> layersY = new List<int>();
        List<int> layersZ = new List<int>();

        for (int x = 0; x < gridSizeX; x++) {
            bool isFullLayer = true;
            for (int y = 0; y < gridSizeY; y++) {
                for (int z = 0; z < gridSizeZ; z++) {
                    if (!grid[x, y, z, 0].IsOccupied()) {
                        isFullLayer = false;
                        Debug.Log($"Not full layer at x ({x},{y},{z}): " + x);
                        break;
                    }
                }
                if (!isFullLayer) break;
            }
            if (isFullLayer) {
                Debug.Log("Full layer at x: " + x);
                layersX.Add(x);
                //return x;
            }
        }
        for (int y = 0; y < gridSizeY; y++) {
            bool isFullLayer = true;
            for (int x = 0; x < gridSizeX; x++) {
                for (int z = 0; z < gridSizeZ; z++) {
                    if (!grid[x, y, z, 0].IsOccupied()) {
                        isFullLayer = false;
                        Debug.Log($"Not full layer at y ({x},{y},{z}): " + y);
                        break;
                    }
                }
                if (!isFullLayer) break;
            }
            if (isFullLayer) {
                Debug.Log("Full layer at y: " + y);
                layersY.Add(y);
                //return y;
            }
        }

        for (int z = 0; z < gridSizeZ; z++) {
            bool isFullLayer = true;
            for (int x = 0; x < gridSizeX; x++) {
                for (int y = 0; y < gridSizeY; y++) {
                    if (!grid[x, y, z, 0].IsOccupied()) {
                        isFullLayer = false;
                        Debug.Log($"Not full layer at z ({x},{y},{z}): " + z);
                        break;
                    }
                }
                if (!isFullLayer) break;
            }
            if (isFullLayer) {
                Debug.Log("Full layer at z: " + z);
                layersZ.Add(z);
                //return z;
            }
        }
        //return -1;
        // Determine which layer list contains the most numbers
        int maxLayers = Mathf.Max(layersX.Count, layersY.Count, layersZ.Count);
        _scoreCounter.IncreaseScore(10*(maxLayers*maxLayers));
        Debug.Log("Bonus score added:" + 10*(maxLayers*maxLayers));
        if (maxLayers == layersX.Count) {
            Debug.Log("X layers contain the most numbers: " + layersX.Count);
            disappear(grid, 'x', layersX);
        } else if (maxLayers == layersY.Count) {
            Debug.Log("Y layers contain the most numbers: " + layersY.Count);
            disappear(grid, 'y', layersX);
        } else if (maxLayers == layersZ.Count) {
            Debug.Log("Z layers contain the most numbers: " + layersZ.Count);
            disappear(grid, 'z', layersX);
        }
    }
    
    private void disappear(Cube[,,,] grid, char direction, List<int> layers) {
        for (int i = 0; i < layers.Count; i++) {
            int layer = layers[i];
            switch (direction) {
                case 'x':
                    for (int y = 0; y < grid.GetLength(1); y++) {
                        for (int z = 0; z < grid.GetLength(2); z++) {
                            //grid[layer, y, z, 0].SetMaterial(trasparentMaterial);
                            grid[layer, y, z, 0].SetNotOccupied();
                        }
                    }
                    Debug.Log("Disappearing layer x: " + layer);
                    break;
                case 'y':
                    for (int x = 0; x < grid.GetLength(0); x++) {
                        for (int z = 0; z < grid.GetLength(2); z++) {
                            //grid[x, layer, z, 0].SetMaterial(trasparentMaterial);
                            grid[x, layer, z, 0].SetNotOccupied();
                        }
                    }
                    Debug.Log("Disappearing layer y: " + layer);
                    break;
                case 'z':
                    for (int x = 0; x < grid.GetLength(0); x++) {
                        for (int y = 0; y < grid.GetLength(1); y++) {
                            //grid[x, y, layer, 0].SetMaterial(trasparentMaterial);
                            grid[x, y, layer, 0].SetNotOccupied();
                        }
                    }
                    Debug.Log("Disappearing layer z: " + layer);
                    break;
            }
        }
    }
    /// <summary>
    /// Method <c>ShowTetrominoPreview</c> show a preview of a new possible tetromino on <c>localLocation</c>.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="tetromino">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public void ShowTetrominoPreview(Cube[,,,] grid, Tetromino tetromino, Location localLocation){
        int x = (int)localLocation.X;
        int y = (int)localLocation.Y;
        int z = (int)localLocation.Z;
        
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
        int x = (int)localLocation.X;
        int y = (int)localLocation.Y;
        int z = (int)localLocation.Z;

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
        Location localLocation = _tGenerating.LocalLocation;
        
        if (CanPlaceTetromino(_grid, _tetromino, localLocation)){
            SaveTetrominoPosition(_grid, _tetromino, localLocation);
            _scoreCounter.IncreaseScore(4);
            
            _tGenerating.DeleteTetromino();
            _tetromino = _tGenerating.GetTetromino();

            Debug.Log("Tetromino successfully placed.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject placeButton = GameObject.Find("PlaceButton");
            RectTransform rectTransform = placeButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.green;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
            
            ShowTetrominoPreview(_grid, _tetromino, _tGenerating.LocalLocation);
        } else {
        Debug.Log("Cannot place tetromino here.");
        // Highlight the "Place" button in pink for 0.5 seconds
        GameObject placeButton = GameObject.Find("PlaceButton");
        RectTransform rectTransform = placeButton.GetComponent<RectTransform>();
        UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

        Color originalColor = buttonImage.color;
        buttonImage.color = Color.red;

        // Spusti Coroutine na reset farby po 0.5 sekundy
        StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    }

    // Coroutine na oneskorenie zmeny farby tlačidla
    private IEnumerator ResetButtonColor(UnityEngine.UI.Image buttonImage, Color originalColor, float delay) {
        yield return new WaitForSeconds(delay);
        buttonImage.color = originalColor;
    }
    
}
