using System.Collections.Generic;
using Unity.VisualScripting;
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
    private AutoIncrement _autoIncrement;
    
    private List<Cube> _cubes = new List<Cube>(); // a list of all cubes
    private Cube[,,,] _grid; // 3D array of [actualCube, previewCube] in cube-grid

    private bool _isShown;
    private TetrominoGenerating _gb;
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

        this._grid = new Cube[_gridSize, _gridSize, _gridSize, 2];
        this._cubes = new List<Cube>();
        
        if (cubeSize == 0){
            this._cubeSize = _cubePrefab.transform.localScale.x;
        }
        else{
            this._cubeSize = cubeSize;
        }
        
        this._autoIncrement = new AutoIncrement();
        this._isShown = false;
}
    
    /// <summary>
    /// Method <c>GenerateGrid</c> generates the cube-grid in world space.
    /// </summary>
    /// <param name="cubeSize">size of one side of a cube.</param>
    /// <param name="autoIncrement">id counter.</param>
    public void GenerateGrid(float cubeSize, AutoIncrement autoIncrement){
        
        _cubes = new List<Cube>();
        
        // generate cubes in cube-grid
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
                    Cube actualCube = new Cube(localLocation, globalLocation, _transparentMaterial, _transparentMaterial, autoIncrement);
                    actualCube.GenerateCube(this.transform, _cubePrefab); // draw the actual cube
                    _cubes.Add(actualCube);
                    
                    Cube previewCube = new Cube(localLocation, globalLocation, _transparentMaterial, _transparentMaterial, autoIncrement);
                    previewCube.GenerateCube(this.transform, _cubePrefab, false); // draw the actual cube
                    
                    // save the cube
                    _grid[i, j, k, 0] = actualCube;
                    _grid[i, j, k, 1] = previewCube;
                }
            }
        }
    }

    /// <summary>
    /// Method <c>DrawGrid</c> "redraws" the cube-grid if it was already generated after the start and lost.
    /// </summary>
    void DrawGrid(){
        foreach (Cube cube in _cubes){
            cube.SetActive(true);

        }
    }
    
    /// <summary>
    /// Method <c>ShowEnvironment</c> show the cube-grid in world space.
    /// </summary>
    /// <returns>A 3D cube-grid with cells containing two values - the actual
    /// saved cube in the cell and a preview of a cube, which can be placed
    /// there.
    /// </returns>
    public Cube[,,,] ShowEnvironment(){
        if (!_isShown){
            _isShown = true;

            // create cube-grid
            if (_cubes.Count == 0){
                GenerateGrid(_cubeSize, _autoIncrement);
            }
            else{
                // draw existing
                DrawGrid();
            }
            return _grid;
        }
        return null;
    }

    /// <summary>
    /// Method <c>HideEnvironment</c> hides the cube-grid from world space.
    /// </summary>
    public void HideEnvironment(){
        if (_isShown){
            _isShown = false;
            
            // hide all cubes from grid
            foreach (Cube cube in _cubes){
                cube.SetActive(false);
            }
        }
    }
}
