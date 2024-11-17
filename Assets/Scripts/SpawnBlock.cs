using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public List<GameObject> blocks;
    public new Camera camera;

    // private bool _chosen;
    private GameObject _chosenBlock;
    
    private float _screenWidth;
    private float _screenHeight;

    GameObject chooseBlock(){
        int index = Random.Range(0,blocks.Count);
        return blocks[index];
    }

    void showBlock(){
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        
        Debug.Log(_screenWidth);
        Debug.Log(_screenHeight);
        
        Vector3 screenPosition = new Vector3(_screenWidth / 2f, _screenHeight * 3f / 7f, 0);
        Vector3 worldPosition = camera.ScreenToWorldPoint(screenPosition);
        Debug.Log(worldPosition);
        
        _chosenBlock.transform.position = worldPosition;
        // _chosenBlock.transform.localScale = new Vector3(_chosenBlock.transform.localScale.x * 3, _chosenBlock.transform.localScale.y * 3, _chosenBlock.transform.localScale.z * 3);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // _chosen = false;
        
        _chosenBlock = chooseBlock();
        Debug.Log(_chosenBlock.name);
        // _chosen = true;
        
        GameObject block = Instantiate(_chosenBlock, transform);
        block.transform.rotation = Quaternion.Euler(0,0,0);
        
        showBlock();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
