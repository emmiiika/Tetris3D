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
        
        Tetromino tetromino1 = _gT.RandomTetromino();
        Location localLocation1 = new Location(0, 0, 0);
        Debug.Log("1: " + tetromino1.Name);
        
        Tetromino tetromino2 = _gT.RandomTetromino();
        Location localLocation2 = new Location(2, 0, 0);
        Debug.Log("2: " + tetromino2.Name);
        
        Tetromino tetromino3 = _gT.RandomTetromino();
        Location localLocation3 = new Location(2, 1, 2);
        Debug.Log("3: " + tetromino3.Name);


        _tP.OnPlace(_grid, tetromino1, localLocation1);
        Debug.Log("mid1");
        _tP.ShowTetrominoPreview(_grid, tetromino2, localLocation2);
        Debug.Log("mid2");
        _tP.OnPlace(_grid, tetromino3, localLocation3);

    }
    
    public void Hide(){
        _cE = gameObject.GetComponent<CreateEnvironment>();
        _cE.HideEnvironment();
    }
    
    
}