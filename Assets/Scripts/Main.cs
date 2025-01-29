using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Main : MonoBehaviour{
    private CreateEnvironment _cE;
    private GenerateTetromino _gT;
    
    
    public GameObject cubePrefab;
    public int gridSize = 5; // cube-grid size
    public Material transparentMaterial;

    public void Show(){
        _cE = gameObject.AddComponent<CreateEnvironment>();
        _cE.Init(cubePrefab, transparentMaterial, gridSize);
        _cE.ShowEnvironment();
        
        _gT = gameObject.AddComponent<GenerateTetromino>();
        Tetromino tetromino = _gT.GetTetromino();
        
    }
    
    public void Hide(){
        _cE = gameObject.AddComponent<CreateEnvironment>();
        _cE.HideEnvironment();
    }
    
    
}