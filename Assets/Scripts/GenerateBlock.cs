using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBlock : MonoBehaviour
{
    public List<GameObject> blocks;

    private GameObject _chosenBlock = null;
    
    private float _screenWidth;
    private float _screenHeight;

    public TMP_Text blockName;
    private GameObject _block;

    public GameObject ChooseBlock(){
        int index = Random.Range(0,blocks.Count);
        return blocks[index];
    }


    public GameObject GetBlock(){
        Debug.Log("show");
        if (_chosenBlock == null){
            _chosenBlock = ChooseBlock();
        }

        Debug.Log(_chosenBlock.name);
        blockName.text = _chosenBlock.name;

        return _chosenBlock;
    }
    
    void Start()
    {
        GetBlock();
        Hide();
    }

    public void Hide(){
        Debug.Log("hide");
        blockName.text = " ";
    }
}
