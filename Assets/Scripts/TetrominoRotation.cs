using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;


/// <summary>
/// Class <c>TetrominoRotation</c> rotates the active tetromino on button actions.
/// </summary>
public class TetrominoRotation : MonoBehaviour{
    
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
    /// Method <c>getBasicInformation</c> gets the active tetromino and its location in the cube-grid.
    /// </summary>
    private Tuple<Tetromino, Location> GetBasicInformation(){
        _grid = _cE.Grid;
        Tetromino tetromino = _tGenerating.GetTetromino();
        Location localLocation = _tGenerating.LocalLocation;
        
        return new Tuple<Tetromino, Location>(tetromino, localLocation);
    }

    /// <summary>
    /// Method <c>RotateAroundXAxis</c> rotates the vector <c>blockPosition</c> around the X axis.
    /// </summary>
    /// <param name="blockPosition">the vector to be rotated.</param>
    /// <param name="clockwise">defines the direction of the rotation facing from the center of the
    /// coordinate system.</param>
    private Vector3 RotateAroundXAxis(Vector3 blockPosition, bool clockwise){
        float x, y, z;
        if (clockwise){
            x = blockPosition.x;
            y = blockPosition.z;
            z = -blockPosition.y;
        }
        else{
            x = blockPosition.x;
            y = -blockPosition.z;
            z = blockPosition.y;
        }

        return new Vector3(x, y, z);
    }
    
    /// <summary>
    /// Method <c>RotateAroundYAxis</c> rotates the vector <c>blockPosition</c> around the Y axis.
    /// </summary>
    /// <param name="blockPosition">the vector to be rotated.</param>
    /// <param name="clockwise">defines the direction of the rotation facing from the center of the
    /// coordinate system.</param>
    private Vector3 RotateAroundYAxis(Vector3 blockPosition, bool clockwise){
        float x, y, z;
        if (clockwise){
            x = -blockPosition.z;
            y = blockPosition.y;
            z = blockPosition.x;
        }
        else{
            x = blockPosition.z;
            y = blockPosition.y;
            z = -blockPosition.x;
        }

        return new Vector3(x, y, z);
    }
    
    /// <summary>
    /// Method <c>RotateAroundZAxis</c> rotates the vector <c>blockPosition</c> around the Z axis.
    /// </summary>
    /// <param name="blockPosition">the vector to be rotated.</param>
    /// <param name="clockwise">defines the direction of the rotation facing from the center of the
    /// coordinate system.</param>
    private Vector3 RotateAroundZAxis(Vector3 blockPosition, bool clockwise){
        float x, y, z;
        if (clockwise){
            x = blockPosition.y;
            y = -blockPosition.x;
            z = blockPosition.z;
        }
        else{
            x = -blockPosition.y;
            y = blockPosition.x;
            z = blockPosition.z;
        }

        return new Vector3(x, y, z);
    }
    
    /// <summary>
    /// Method <c>OnRotateLeft</c> rotates the active tetromino around Y axis in counterclockwise direction.
    /// </summary>
    public void OnRotateLeft(){ // around y axis
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] newBlockPositions = new Vector3[4];
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            newBlockPositions[_] = RotateAroundYAxis(tetromino.BlockPositions[_], false);
        }

        Vector3[] oldBlockPositions = tetromino.BlockPositions;
        tetromino.BlockPositions = newBlockPositions;
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, localLocation)){
            tetromino.BlockPositions = oldBlockPositions;
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            
            tetromino.BlockPositions = newBlockPositions;
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, localLocation);

            Debug.Log("Tetromino rotated left.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateLeftButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.green;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
        else{
            tetromino.BlockPositions = oldBlockPositions;
            Debug.Log("Cannot rotate tetromino left.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateLeftButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.red;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    } 
    
    /// <summary>
    /// Method <c>OnRotateLeft</c> rotates the active tetromino around Y axis in clockwise direction.
    /// </summary>
    public void OnRotateRight(){ // around y axis
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] newBlockPositions = new Vector3[4];
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            newBlockPositions[_] = RotateAroundYAxis(tetromino.BlockPositions[_], true);
        }

        Vector3[] oldBlockPositions = tetromino.BlockPositions;
        tetromino.BlockPositions = newBlockPositions;
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, localLocation)){
            tetromino.BlockPositions = oldBlockPositions;
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            
            tetromino.BlockPositions = newBlockPositions;
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, localLocation);

            Debug.Log("Tetromino rotated right.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateRightButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.green;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
        else {
            tetromino.BlockPositions = oldBlockPositions;
            Debug.Log("Cannot rotate tetromino right.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateRightButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.red;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    }
    
    /// <summary>
    /// Method <c>Rotate</c> rotates the vector <c>blockPosition</c> based on the faced face of the cube-grid upwards or downwards.
    /// </summary>
    /// <param name="blockPosition"> the vector to be rotated.</param>
    /// <param name="isUpward">if the rotation is upwards or not.</param>
    private Vector3 Rotate(Vector3 blockPosition, bool isUpward){
        int maxVisibleSide = _cameraPositioning.GetCameraFacingFace(_parentWrapper);
        if (maxVisibleSide == 0){  // z / +-. up / -+. down
            return isUpward ? RotateAroundZAxis(blockPosition, false) : RotateAroundZAxis(blockPosition, true);
        }
        if (maxVisibleSide == 1){ // x / .-+ up / .+- down
            return isUpward ? RotateAroundXAxis(blockPosition, true) : RotateAroundXAxis(blockPosition, false);
        }
        if (maxVisibleSide == 2){  // z / -+. up / +-. down
            return isUpward ? RotateAroundZAxis(blockPosition, true) : RotateAroundZAxis(blockPosition, false);
        }
        if (maxVisibleSide == 3){  // x / .+- up / .-+ down
            return isUpward ? RotateAroundXAxis(blockPosition, false) : RotateAroundXAxis(blockPosition, true);
        }

        return blockPosition;
    }

    /// <summary>
    /// Method <c>OnRotateUp</c> rotates the active tetromino around X or Z axis upwards.
    /// </summary>
    public void OnRotateUp(){ // around x or z axis
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] newBlockPositions = new Vector3[4];
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            newBlockPositions[_] = Rotate(tetromino.BlockPositions[_], true);
        }

        Vector3[] oldBlockPositions = tetromino.BlockPositions;
        tetromino.BlockPositions = newBlockPositions;
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, localLocation)){
            tetromino.BlockPositions = oldBlockPositions;
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            
            tetromino.BlockPositions = newBlockPositions;
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, localLocation);

            Debug.Log("Tetromino rotated up.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateUpButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.green;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
        else{
            tetromino.BlockPositions = oldBlockPositions;
            Debug.Log("Cannot rotate tetromino up.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateUpButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.red;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
    }

    /// <summary>
    /// Method <c>OnRotateUp</c> rotates the active tetromino around X or Z axis upwards.
    /// </summary>
    public void OnRotateDown(){ //  around x or z axis
        Tuple<Tetromino, Location> basicInformation = GetBasicInformation();
        Tetromino tetromino = basicInformation.Item1;
        Location localLocation = basicInformation.Item2;
        
        Vector3[] newBlockPositions = new Vector3[4];
        for (int _ = 0; _ < tetromino.BlockPositions.Length; _++){
            newBlockPositions[_] = Rotate(tetromino.BlockPositions[_], false);
        }

        Vector3[] oldBlockPositions = tetromino.BlockPositions;
        tetromino.BlockPositions = newBlockPositions;
        if (_tPlacement.IsTetrominoInsideCube(_grid, tetromino, localLocation)){
            tetromino.BlockPositions = oldBlockPositions;
            _tPlacement.HideTetrominoPreview(_grid, tetromino, localLocation);
            
            tetromino.BlockPositions = newBlockPositions;
            _tPlacement.ShowTetrominoPreview(_grid, tetromino, localLocation);

            Debug.Log("Tetromino rotated down.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateDownButton");
            RectTransform rectTransform = moveButton.GetComponent<RectTransform>();
            UnityEngine.UI.Image buttonImage = rectTransform.GetComponent<UnityEngine.UI.Image>();

            Color originalColor = buttonImage.color;
            buttonImage.color = Color.green;

            // Spusti Coroutine na reset farby po 0.5 sekundy
            StartCoroutine(ResetButtonColor(buttonImage, originalColor, 0.1f));
        }
        else{
            tetromino.BlockPositions = oldBlockPositions;
            Debug.Log("Cannot rotate tetromino down.");
            // Highlight the "Place" button in pink for 0.5 seconds
            GameObject moveButton = GameObject.Find("RotateDownButton");
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