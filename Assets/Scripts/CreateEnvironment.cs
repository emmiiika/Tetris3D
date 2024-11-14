using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class CreateEnvironment : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject wallPrefab;
    public Camera mainCamera;
    
    public int gridSize = 5; // size of cubeSpace
    
    private float _cubeSize; // size of one cube
    private float _roomSpace; // how much space is between the wall and cubeSpace
    
    private List<GameObject> _cubes;
    private List<GameObject> _walls;
    private List<Vector3> _wallOffsets;
    private List<Vector3> _wallRotations;

    private Transform roomChild;
    private Transform gridChild;

    
    void GenerateGrid(float cubeSize){
        gridChild.position = new Vector3(gridSize / 2.0f, gridSize / 2.0f, gridSize / 2.0f); // move cubeSpace object to origin
        
        // generate cubes in cubeSpace
        for (int x = 0; x < gridSize; x++){
            for (int y = 0; y < gridSize; y++){
                for (int z = 0; z < gridSize; z++){
                    _cubes.Add(Instantiate(cubePrefab, new Vector3(x + cubeSize / 2.0f, y + cubeSize / 2.0f, z + cubeSize / 2.0f), Quaternion.identity, gridChild));
                }
            }
        }
    }
    

    void GenerateWalls(List<Vector3> wallVectors, List<Vector3> wallRotations){
        float planeSize = (5.0f + gridSize) / 10.0f;
        for (int i = 0; i < 6; i++) {
            Vector3 vector = wallVectors[i];
            Vector3 rotation = wallRotations[i];
                
            GameObject wall = Instantiate(wallPrefab, vector, Quaternion.identity, roomChild);
            wall.transform.Rotate(rotation);
            wall.transform.localScale = new Vector3(planeSize, planeSize, planeSize);
            _walls.Add(wall);
        }
    }

    void SetCameraPosition() {
        mainCamera.transform.position = new Vector3(gridSize / 2.0f, gridSize / 2.0f, 0 - gridSize - 2);

    }
    
    // Start is called before the first frame update
    void Start(){
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
        
        _cubeSize = cubePrefab.transform.localScale.x;
        GenerateGrid(_cubeSize);
        GenerateWalls(_wallOffsets, _wallRotations);
        SetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
