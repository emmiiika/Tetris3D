using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>CreateEnvironment</c> creates the game environment - cube-grid.
/// </summary>
public class CreateEnvironment : MonoBehaviour
{
    private GameObject _cubePrefab;
    private int _gridSize; // size of cube-grid
    private Material _transparentMaterial;
    private float _cubeSize; // size of one cube
    
    private List<Cube> _cubes; // a list of all cubes
    private Cube[,,] _grid; // 3D array of Cubes in cube-grid

    private bool _isShown = false;
    private GenerateTetromino _gb;
    private TetrominoPlacement _bp;

    /// <summary>
    /// Method <c>Init</c> sets the needed private parameters.
    /// </summary>
    /// <param name="cubePrefab">prefab of a cube.</param>
    /// <param name="transparentMaterial">transparent material.</param>
    /// <param name="gridSize">size of the cube-grid (preset to 5).</param>
    /// <param name="cubeSize">size of one side of a cube (preset to 0).</param>
    public void Init(GameObject cubePrefab, Material transparentMaterial, int gridSize = 5, float cubeSize = 0){
        this._cubePrefab = cubePrefab;
        this._gridSize = gridSize;
        this._transparentMaterial = transparentMaterial;

        if (cubeSize == 0){
            this._cubeSize = _cubePrefab.transform.localScale.x;
        }
        else{
            this._cubeSize = cubeSize;
        }
}
    
    
    /// <summary>
    /// Method <c>GenerateGrid</c> generates the cube-grid in world space.
    /// </summary>
    /// <param name="cubeSize">size of one side of a cube.</param>
    /// <param name="autoIncrement">id counter.</param>
    public void GenerateGrid(float cubeSize, AutoIncrement autoIncrement){
        // generate cubes in cubeSpace
        for (float x = 0; x < _gridSize * _cubeSize; x += _cubeSize){
            for (float y = 0; y < _gridSize * _cubeSize; y += _cubeSize){
                for (float z = 0; z < _gridSize * _cubeSize; z += _cubeSize){
                    
                    // location in the cube-grid
                    int i = (int)(x / _cubeSize);
                    int j = (int)(y / _cubeSize);
                    int k = (int)(z / _cubeSize);
                    
                    Location localLocation = new Location((int)(2 * x * 10), (int)(2 * y * 10), (int)(2 * z * 10));
                    Location globalLocation = new Location(x + cubeSize / 2.0f - _gridSize * cubeSize / 2.0f, y + cubeSize / 2.0f, z + cubeSize / 2.0f  - _gridSize * cubeSize / 2.0f);

                    // create and draw the cube
                    Cube cube = new Cube(localLocation, globalLocation, _transparentMaterial, _transparentMaterial, autoIncrement);
                    cube.GenerateCube(this.transform, _cubePrefab);
                    
                    // save the cube
                    _cubes.Add(cube);
                    _grid[i, j, k] = cube;
                }
            }
        }
    }

    /// <summary>
    /// Method <c>DrawGrid</c> "redraws" the cube-grid if it was already generated after the start and lost.
    /// </summary>
    void DrawGrid(){
        if (_cubes.Count != 0){
            foreach (Cube cube in _cubes){
                cube.GenerateCube(this.transform, _cubePrefab);
            }
        }
    }
    
    /// <summary>
    /// Method <c>ShowEnvironment</c> show the cube-grid in world space.
    /// </summary>
    public void ShowEnvironment(){
        if (!_isShown){ 
            _isShown = true;
            AutoIncrement autoIncrement = new AutoIncrement(); 
            
            // create cube-grid
            if (_cubes.Count == 0){
                GenerateGrid(_cubeSize, autoIncrement);
            }
            else{ // draw existing
                DrawGrid();
            }
        }
    }

    /// <summary>
    /// Method <c>HideEnvironment</c> hides the cube-grid from world space.
    /// </summary>
    public void HideEnvironment(){
        if (_isShown){
            _isShown = false;
            
            // hide all cubes from grid
            foreach (Cube cube in _cubes){
                Destroy(cube.cubeObject);
            }
        }
    }
 
    public void Start(){
        _grid = new Cube[_gridSize, _gridSize, _gridSize];
        _cubes = new List<Cube>();
    }
}
