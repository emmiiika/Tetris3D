using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>GenerateBlock</c> generates a random tetromino.
/// </summary>
public class GenerateBlock : MonoBehaviour
{
    public List<GameObject> blocks; 

    private GameObject _chosenBlock; // already generated random tetromino
    
    private float _screenWidth;
    private float _screenHeight;

    public TMP_Text blockName;
    private GameObject _block;

    /// <summary>
    /// Method <c>ChooseBlock</c> returns a random tetromino.
    /// </summary>
    public GameObject ChooseBlock(){
        int index = Random.Range(0,blocks.Count);
        return blocks[index];
    }


    /// <summary>
    /// Method <c>GetBlock</c> generates a random tetromino if there is not one already generated.
    /// If one is already saved, the method returns the saved one.
    /// </summary>
    public GameObject GetBlock(){
        if (_chosenBlock == null){
            _chosenBlock = ChooseBlock();
        }

        blockName.text = _chosenBlock.name;

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
