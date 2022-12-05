using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouch : MonoBehaviour
{
    BlockController[] blocks;

    void Start()
    {
        blocks = GetComponentsInChildren<BlockController>();
    }

    void Update()
    {
        blocks = GetComponentsInChildren<BlockController>();
        try
        {
            if (Input.touchCount == 2)
            {
                foreach (BlockController block in blocks)
                {
                    block.speed = 4f;
                }
            }
            else
            {
                foreach (BlockController block in blocks)
                {
                    block.speed = 1.5f;
                }
            }
        }
        catch
        {
            
        }
    }
}
