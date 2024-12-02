using System.Collections.Generic;
using UnityEngine;
using static Cube;

public class CreateEnvironment : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject wallPrefab;
    // public Camera mainCamera;
    
    public int gridSize = 5; // size of cubeSpace
    public Material transparentMaterial;
    
    private float _cubeSize; // size of one cube
    private float _roomSpace; // how much space is between the wall and cubeSpace
    
    private List<Cube> _cubes;
    // private List<GameObject> _walls;
    // private List<Vector3> _wallOffsets;
    // private List<Vector3> _wallRotations;

    // private Transform roomChild;
    private Transform gridChild;
    private Transform parent;

    private bool _isShown = false;

    
    void GenerateGrid(float cubeSize, AutoIncrement autoIncrement){
        // generate cubes in cubeSpace
        for (float x = 0; x < gridSize * _cubeSize; x += _cubeSize){
            for (float y = 0; y < gridSize * _cubeSize; y += _cubeSize){
                for (float z = 0; z < gridSize * _cubeSize; z += _cubeSize){
                    Location localLocation = new Location(x, y, z);
                    Location globalLocation = new Location(x + cubeSize / 2.0f - gridSize * cubeSize / 2.0f, y + cubeSize / 2.0f, z + cubeSize / 2.0f  - gridSize * cubeSize / 2.0f);
                    
                    Cube cube = new Cube(localLocation, globalLocation, transparentMaterial, autoIncrement);
                    cube.GenerateCube(gridChild, cubePrefab);
                    
                    cube.Print();
                    
                    _cubes.Add(cube);
                }
            }
        }
    }
    
    public void ShowEnvironment(){
        if (!_isShown){
            Debug.Log("Found it");
            _isShown = true;
            _cubes = new List<Cube>();
            gridChild = transform.GetChild(0);
            parent = transform.parent;

            _cubeSize = cubePrefab.transform.localScale.x;
            AutoIncrement autoIncrement = new AutoIncrement();
            GenerateGrid(_cubeSize, autoIncrement);
        }
    }

    public void HideEnvironment(){
        if (_isShown){
            Debug.Log("Lost it");
            _isShown = false;
            foreach (Cube cube in _cubes){
                Destroy(cube.cubeObject);
            }
            _cubes.Clear();
        }
    }
    
    // Start is called before the first frame update
    void Start(){
        // ShowEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
