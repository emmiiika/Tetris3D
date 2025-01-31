using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class TetrominoMovement : MonoBehaviour{
    
    private CreateEnvironment _cE;
    private TetrominoGenerating _tGenerating;
    private TetrominoPlacement _tPlacement;
    private CameraPositioning _cameraPositioning;
    
    private Cube[,,,] _grid;
    
    public void Start(){
        GameObject grid = gameObject.transform.parent.Find("Grid").gameObject;
        _cE = grid.GetComponent<CreateEnvironment>();
        
        _tGenerating = gameObject.GetComponent<TetrominoGenerating>();
        _tPlacement = gameObject.GetComponent<TetrominoPlacement>();
    }

    private Tuple<Tetromino, Location> getBasicInformation(){
        _grid = _cE.Grid;
        Tetromino tetromino = _tGenerating.GetTetromino();
        Location localLocation = _tGenerating.localLocation;
        
        return new Tuple<Tetromino, Location>(tetromino, localLocation);
    }
    
    public void onMoveLeft(){
        Tuple<Tetromino, Location> basicInformation = getBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] directionArray = _cameraPositioning.GetDirectionArray();
        Vector3 leftMove = directionArray[1];
        
        Location newLocalLocation = new Location(localLocation.x + leftMove.x, localLocation.y + leftMove.y, localLocation.z + leftMove.z);
        
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.localLocation = newLocalLocation;
        }
    }

    public void onMoveRight(){
        Tuple<Tetromino, Location> basicInformation = getBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] directionArray = _cameraPositioning.GetDirectionArray();
        Vector3 rightMove = directionArray[0];
        
        Location newLocalLocation = new Location(localLocation.x + rightMove.x, localLocation.y + rightMove.y, localLocation.z + rightMove.z);
        
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.localLocation = newLocalLocation;
        }
    }

    public void onMoveUp(){
        Tuple<Tetromino, Location> basicInformation = getBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        Location newLocalLocation = new Location(localLocation.x, localLocation.y + 1, localLocation.z);

        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.localLocation = newLocalLocation;
        }
    }

    public void onMoveDown(){ 
        Tuple<Tetromino, Location> basicInformation = getBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        Location newLocalLocation = new Location(localLocation.x, localLocation.y - 1, localLocation.z);

        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.localLocation = newLocalLocation;
        }
    }
}