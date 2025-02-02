using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class <c>TetrominoMovement</c> moves the active tetromino on button actions.
/// </summary>
public class TetrominoMovement : MonoBehaviour{
    
    private CreateEnvironment _cE;
    private TetrominoGenerating _tGenerating;
    private TetrominoPlacement _tPlacement;
    private CameraPositioning _cameraPositioning;
    private Transform _parentWrapper;
    
    private Cube[,,,] _grid;
    
    public void Start(){
        GameObject grid = gameObject.transform.parent.Find("Grid").gameObject;
        _cE = grid.GetComponent<CreateEnvironment>();
        
        _tGenerating = gameObject.GetComponent<TetrominoGenerating>();
        _tPlacement = gameObject.GetComponent<TetrominoPlacement>();

        _cameraPositioning = new CameraPositioning();
        
        _parentWrapper = gameObject.transform.parent;
    }

    /// <summary>
    /// Method <c>getBasicInformation</c> gets active tetromino and its location in cube-grid.
    /// </summary>\
    private Tuple<Tetromino, Location> getBasicInformation(){
        _grid = _cE.Grid;
        Tetromino tetromino = _tGenerating.GetTetromino();
        Location localLocation = _tGenerating.localLocation;
        
        return new Tuple<Tetromino, Location>(tetromino, localLocation);
    }
    
    /// <summary>
    /// Method <c>onMoveLeft</c> moves the tetromino to the left based on the faced face of the cube-grid on button action.
    /// </summary>
    public void onMoveLeft(){
        Tuple<Tetromino, Location> basicInformation = getBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] directionArray = _cameraPositioning.GetDirectionArray(_parentWrapper);
        Vector3 leftMove = directionArray[1];
        
        Location newLocalLocation = new Location(localLocation.x + leftMove.x, localLocation.y + leftMove.y, localLocation.z + leftMove.z);
        
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.localLocation = newLocalLocation;
        }
    }

    /// <summary>
    /// Method <c>onMoveRight</c> moves the tetromino to the right based on the faced face of the cube-grid on button action.
    /// </summary>
    public void onMoveRight(){
        Tuple<Tetromino, Location> basicInformation = getBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] directionArray = _cameraPositioning.GetDirectionArray(_parentWrapper);
        Vector3 rightMove = directionArray[0];
        
        Location newLocalLocation = new Location(localLocation.x + rightMove.x, localLocation.y + rightMove.y, localLocation.z + rightMove.z);
        
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.localLocation = newLocalLocation;
        }
    }

    /// <summary>
    /// Method <c>onMoveUp</c> moves the tetromino upward based on the faced face of the cube-grid on button action.
    /// </summary>
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

    /// <summary>
    /// Method <c>onMoveDown</c> moves the tetromino downward based on the faced face of the cube-grid on button action.
    /// </summary>
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