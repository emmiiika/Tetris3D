using UnityEngine;

public class Main : MonoBehaviour{
    private CreateEnvironment _cE;

    private Cube[,,,] _grid;
    private ScoreCounter _scoreCounter;
    
    
    public GameObject cubePrefab;
    public int gridSize = 5; // cube-grid size
    public Material transparentMaterial;
    
    
    public void Awake(){
        _cE = gameObject.AddComponent<CreateEnvironment>();
        _cE.Init(cubePrefab, transparentMaterial, gridSize);
        _grid = _cE.Grid;
        
        _scoreCounter = gameObject.GetComponent<ScoreCounter>();
        _scoreCounter.InitScoreCounter();
        
        GameObject tetromino = gameObject.transform.parent.Find("Tetromino").gameObject;
    }

    public void Show(){
        _cE.ShowEnvironment();
    }
    
    public void Hide(){
        _cE.HideEnvironment();
    }
    
    
}