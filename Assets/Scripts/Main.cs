using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

public class Main : MonoBehaviour{
    private CreateEnvironment _cE;
    private TetrominoGenerating _gT;
    private TetrominoPlacement _tP;

    private Cube[,,,] _grid;
    
    
    public GameObject cubePrefab;
    public int gridSize = 5; // cube-grid size
    public Material transparentMaterial;
    
    
    public void Start(){
        _cE = gameObject.AddComponent<CreateEnvironment>();
        _cE.Init(cubePrefab, transparentMaterial, gridSize);
        
        GameObject tetromino = gameObject.transform.parent.Find("Tetromino").gameObject;
        _gT = tetromino.GetComponent<TetrominoGenerating>();
        _tP = tetromino.GetComponent<TetrominoPlacement>();
    }

    public void Show(){
        _grid = _cE.ShowEnvironment();
        
        Tetromino tetromino = _gT.GetTetromino();
        Location localLocation = new Location(0, 0, 0);


        // Debug.Log("Saving tetromino___________________");
        // _tP.OnPlace(_grid, tetromino, localLocation);
        // Debug.Log("Saved tetromino___________________");
        Debug.Log("Placing tetromino_____________________");
        _tP.PlaceTetromino(_grid, tetromino, localLocation);
        Debug.Log("Placed tetromino_____________________");
    }
    
    public void Hide(){
        _cE = gameObject.GetComponent<CreateEnvironment>();
        _cE.HideEnvironment();
    }
    
    
}