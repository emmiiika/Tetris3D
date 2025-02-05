using System;
using UnityEngine;
using System.Collections;

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
    private Tuple<Tetromino, Location> GetBasicInformation(){
        _grid = _cE.Grid;
        Tetromino tetromino = _tGenerating.GetTetromino();
        Location localLocation = _tGenerating.LocalLocation;
        
        return new Tuple<Tetromino, Location>(tetromino, localLocation);
    }
    
    /// <summary>
    /// Method <c>onMoveLeft</c> moves the tetromino to the left based on the faced face of the cube-grid on button action.
    /// </summary>
    public void OnMoveLeft(){
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] directionArray = _cameraPositioning.GetDirectionArray(_parentWrapper);
        Vector3 leftMove = directionArray[1];
        
        Location newLocalLocation = new Location(localLocation.X + leftMove.x, localLocation.Y + leftMove.y, localLocation.Z + leftMove.z);
        
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.LocalLocation = newLocalLocation;
        } else {
            Debug.Log("Cannot move tetromino left.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("MoveLeftButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.red;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    }

    /// <summary>
    /// Method <c>onMoveRight</c> moves the tetromino to the right based on the faced face of the cube-grid on button action.
    /// </summary>
    public void OnMoveRight(){
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] directionArray = _cameraPositioning.GetDirectionArray(_parentWrapper);
        Vector3 rightMove = directionArray[0];
        
        Location newLocalLocation = new Location(localLocation.X + rightMove.x, localLocation.Y + rightMove.y, localLocation.Z + rightMove.z);
        
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.LocalLocation = newLocalLocation;
        } else {
            Debug.Log("Cannot move tetromino right.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("MoveRightButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.red;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    }

    /// <summary>
    /// Method <c>onMoveUp</c> moves the tetromino upward based on the faced face of the cube-grid on button action.
    /// </summary>
    public void OnMoveUp(){
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        Location newLocalLocation = new Location(localLocation.X, localLocation.Y + 1, localLocation.Z);

        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.LocalLocation = newLocalLocation;
        } else {
            Debug.Log("Cannot move tetromino up.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("MoveUpButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.red;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    }

    /// <summary>
    /// Method <c>onMoveDown</c> moves the tetromino downward based on the faced face of the cube-grid on button action.
    /// </summary>
    public void OnMoveDown(){ 
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        Location newLocalLocation = new Location(localLocation.X, localLocation.Y - 1, localLocation.Z);

        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, newLocalLocation)){
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, newLocalLocation);
            _tGenerating.LocalLocation = newLocalLocation;
        } else {
            Debug.Log("Cannot move tetromino down.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("MoveDownButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.red;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    }

    // Coroutine na oneskorenie zmeny farby tlačidla
    private IEnumerator ResetButtonColor(UnityEngine.UI.Image buttonImage, Color originalColor, float delay) {
        yield return new WaitForSeconds(delay);
        buttonImage.color = originalColor;
    }

}