using System.Collections.Generic;
using UnityEngine;

public class CreateEnvironment : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject wallPrefab;
    // public Camera mainCamera;
    
    public int gridSize = 5; // size of cubeSpace
    
    private float _cubeSize; // size of one cube
    private float _roomSpace; // how much space is between the wall and cubeSpace
    
    private List<GameObject> _cubes;
    private List<GameObject> _walls;
    private List<Vector3> _wallOffsets;
    private List<Vector3> _wallRotations;

    private Transform roomChild;
    private Transform gridChild;
    private Transform parent;

    private bool _isShown = false;

    
    void GenerateGrid(float cubeSize){
        // generate cubes in cubeSpace
        for (float x = 0; x < gridSize * _cubeSize; x += _cubeSize){
            for (float y = 0; y < gridSize * _cubeSize; y += _cubeSize){
                for (float z = 0; z < gridSize * _cubeSize; z += _cubeSize){
                    GameObject cube = Instantiate(cubePrefab, gridChild);
                    cube.transform.localPosition = new Vector3(x + cubeSize / 2.0f - gridSize * cubeSize / 2.0f, y + cubeSize / 2.0f, z + cubeSize / 2.0f  - gridSize * cubeSize / 2.0f);
                    _cubes.Add(cube);
                }
            }
        }
    }
    

    void GenerateWalls(List<Vector3> wallVectors, List<Vector3> wallRotations){
        roomChild.position = parent.position;
        
        float planeSize = (5.0f + gridSize) / 10.0f;
        for (int i = 0; i < 6; i++) {
            Vector3 vector = wallVectors[i];
            Vector3 rotation = wallRotations[i];
                
            GameObject wall = Instantiate(wallPrefab, roomChild);
            wall.transform.localPosition = vector;
            wall.transform.Rotate(rotation);
            wall.transform.localScale = new Vector3(planeSize, planeSize, planeSize);
            _walls.Add(wall);
        }
    }

    // void SetCameraPosition() {
    //     mainCamera.transform.position = new Vector3(gridSize / 2.0f, gridSize / 2.0f, 0 - gridSize - 2);
    // }

    public void ShowEnvironment(){
        if (!_isShown){
            _isShown = true;
            _cubes = new List<GameObject>();
            _walls = new List<GameObject>();

            _roomSpace = 2.5f;
            _wallOffsets = new List<Vector3>{
                new Vector3(gridSize / 2.0f, 0 - _roomSpace, gridSize / 2.0f), // bottom
                new Vector3(gridSize / 2.0f, gridSize / 2.0f, 0 - _roomSpace), // close
                new Vector3(0 - _roomSpace, gridSize / 2.0f, gridSize / 2.0f), // left
                new Vector3(gridSize / 2.0f, gridSize / 2.0f, gridSize + _roomSpace), // far
                new Vector3(gridSize + _roomSpace, gridSize / 2.0f, gridSize / 2.0f), // right
                new Vector3(gridSize / 2.0f, gridSize + _roomSpace, gridSize / 2.0f), // up

            };
            _wallRotations = new List<Vector3>{
                new Vector3(0, 0, 0), // bottom
                new Vector3(90, 0, 0), // close
                new Vector3(0, 0, -90), // left
                new Vector3(-90, 0, 0), // far
                new Vector3(0, 0, 90), // right
                new Vector3(0, 0, 180), // up
            };

            roomChild = transform.GetChild(0);
            gridChild = transform.GetChild(1);
            parent = transform.parent;

            _cubeSize = cubePrefab.transform.localScale.x;
            GenerateGrid(_cubeSize);
            // GenerateWalls(_wallOffsets, _wallRotations);
            // SetCameraPosition();
        }
    }

    public void HideEnvironment(){
        if (_isShown){
            _isShown = false;
            foreach (GameObject cube in _cubes){
                Destroy(cube);
            }

            foreach (GameObject wall in _walls){
                Destroy(wall);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start(){
        ShowEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
