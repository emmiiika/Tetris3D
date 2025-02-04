using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class <c>TetrominoGenerating</c> generates a random tetromino.
/// </summary>
public class TetrominoGenerating : MonoBehaviour
{
    public List<GameObject> blocks; // all tetrominos
    public TMP_Text blockName;
    
    public Location LocalLocation;

    private Tetromino _chosenBlock; // generated tetromino

    public List<GameObject> blocksBucket; // difference between random generating blocks cannot be grater than 1 
    /// <summary>
    /// Method <c>ChooseBlock</c> returns a random tetromino.
    /// </summary>
    private Tetromino RandomTetromino(){
        //int index = Random.Range(0,blocks.Count);
        //return new Tetromino(blocks[index]);
        //vzdy sa vystriedaju vsetky 4 bloky
        if (blocksBucket.Count == 0) blocksBucket = new List<GameObject>(blocks);
        int index = Random.Range(0, blocksBucket.Count);

        Tetromino tetromino = new Tetromino(blocksBucket[index]);
        blocksBucket.RemoveAt(index);
        return tetromino;

    }

    /// <summary>
    /// Method <c>GetBlock</c> generates a random tetromino if there is not one already generated.
    /// If one is already saved, the method returns the saved one.
    /// </summary>
    public Tetromino GetTetromino(){
        if (_chosenBlock == null){
            _chosenBlock = RandomTetromino();
            LocalLocation = new Location(0, 0, 0);
        }
        blockName.text = _chosenBlock.Name.Substring(0, 5) + ": " + _chosenBlock.Name.Substring(5); // change the shown block name

        return _chosenBlock;
    }

    /// <summary>
    /// Method <c>DeleteTetromino</c> deletes current tetromino.
    /// </summary>
    public void DeleteTetromino(){
        _chosenBlock = null;
        LocalLocation = new Location(0, 0, 0);
    }

}
