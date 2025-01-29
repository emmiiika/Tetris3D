using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>GenerateBlock</c> generates a random tetromino.
/// </summary>
public class GenerateTetromino : MonoBehaviour
{
    public List<GameObject> blocks; // all tetrominos
    public TMP_Text blockName;

    private Tetromino _chosenBlock; // generated random tetromino

    // private float _screenWidth;
    // private float _screenHeight;

    /// <summary>
    /// Method <c>ChooseBlock</c> returns a random tetromino.
    /// </summary>
    public Tetromino ChooseBlock(){
        int index = Random.Range(0,blocks.Count);
        return new Tetromino(blocks[index]);
    }


    /// <summary>
    /// Method <c>GetBlock</c> generates a random tetromino if there is not one already generated.
    /// If one is already saved, the method returns the saved one.
    /// </summary>
    public Tetromino GetBlock(){
        if (_chosenBlock == null){
            _chosenBlock = ChooseBlock();
        }

        blockName.text = _chosenBlock.Name;

        return _chosenBlock;
    }
    
    /// <summary>
    /// On <c>Start</c> a new tetromino is generated.
    /// </summary>
    void Start()
    {
        
        // blocks = new List<GameObject>();
        GetBlock();
        // Hide();
    }

    public void Hide(){
        blockName.text = " ";
    }
}
