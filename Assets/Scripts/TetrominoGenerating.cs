using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class <c>GenerateBlock</c> generates a random tetromino.
/// </summary>
public class TetrominoGenerating : MonoBehaviour
{
    public List<GameObject> blocks; // all tetrominos
    public TMP_Text blockName;

    private Tetromino _chosenBlock; // generated tetromino

    /// <summary>
    /// Method <c>ChooseBlock</c> returns a random tetromino.
    /// </summary>
    public Tetromino RandomTetromino(){
        int index = Random.Range(0,blocks.Count);
        return new Tetromino(blocks[index]);
    }


    /// <summary>
    /// Method <c>GetBlock</c> generates a random tetromino if there is not one already generated.
    /// If one is already saved, the method returns the saved one.
    /// </summary>
    public Tetromino GetTetromino(){
        if (_chosenBlock == null){
            _chosenBlock = RandomTetromino();
        }
        blockName.text = _chosenBlock.Name; // change the shown block name

        return _chosenBlock;
    }
}
