using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateEnvironment : MonoBehaviour
{
    public GameObject cubePrefab;
    
    public int gridSize = 5; // size of cubeSpace
    public Material transparentMaterial;
    
    private float _cubeSize; // size of one cube
    
    private List<Cube> _cubes;
    private Cube[,,] _grid;

    private Transform _gridChild;

    private bool _isShown = false;
    private GenerateBlock _gb;
    private BlockPlacement _bp;


    
    void GenerateGrid(float cubeSize, AutoIncrement autoIncrement){
        // generate cubes in cubeSpace
        for (float x = 0; x < gridSize * _cubeSize; x += _cubeSize){
            for (float y = 0; y < gridSize * _cubeSize; y += _cubeSize){
                for (float z = 0; z < gridSize * _cubeSize; z += _cubeSize){
                    // Debug.Log(gridSize * _cubeSize);
                    // Debug.Log((int)(gridSize * _cubeSize / x) + ", " + (int)(gridSize * _cubeSize / y) + ", " + (int)(gridSize * _cubeSize / z));
                    int i = (int)(x / _cubeSize);
                    int j = (int)(y / _cubeSize);
                    int k = (int)(z / _cubeSize);
                    
                    Location localLocation = new Location((int)(2 * x * 10), (int)(2 * y * 10), (int)(2 * z * 10));
                    Location globalLocation = new Location(x + cubeSize / 2.0f - gridSize * cubeSize / 2.0f, y + cubeSize / 2.0f, z + cubeSize / 2.0f  - gridSize * cubeSize / 2.0f);
                    
                    Cube cube = new Cube(localLocation, globalLocation, transparentMaterial, autoIncrement);
                    cube.GenerateCube(_gridChild, cubePrefab);
                    
                    _cubes.Add(cube);
                    _grid[i, j, k] = cube;
                }
            }
        }
    }

    public void Start(){
        _grid = new Cube[gridSize, gridSize, gridSize];
        
    }
    
    public void ShowEnvironment(){
        if (!_isShown){
            // Debug.Log("Found it");
            _isShown = true;
            _cubes = new List<Cube>();
            _gridChild = transform.GetChild(0);
            // parent = transform.parent;

            _cubeSize = cubePrefab.transform.localScale.x;
            // Debug.Log(_cubeSize);
            AutoIncrement autoIncrement = new AutoIncrement();
            GenerateGrid(_cubeSize, autoIncrement);

            _gb = gameObject.GetComponent<GenerateBlock>();
            GameObject block = _gb.GetBlock();
            
            _bp = gameObject.GetComponent<BlockPlacement>();
            _bp.PlaceBlock(_grid, block, new Location(0, 0, 0));
            
        }
    }

    public void HideEnvironment(){
        if (_isShown){
            // Debug.Log("Lost it");
            _isShown = false;
            foreach (Cube cube in _cubes){
                Destroy(cube.cubeObject);
            }
            _cubes.Clear();
            
            _gb = gameObject.GetComponent<GenerateBlock>();
            _gb.Hide();
        }
    }
 
    void Update()
    {
        
    }
}
