using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public List<GameObject> blocks;

    private bool chosen;
    private GameObject chosenBlock;

    GameObject chooseBlock(){
        int index = Random.Range(0,blocks.Count);
        return blocks[index];
    }
    
    // Start is called before the first frame update
    void Start()
    {
        chosen = false;
        
        chosenBlock = chooseBlock();
        Debug.Log(chosenBlock.name);
        chosen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
