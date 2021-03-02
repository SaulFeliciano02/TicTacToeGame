using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSlateBoardControl : MonoBehaviour
{
    public ushort Index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.parent != null)
            {
                SingleBoardControl board = transform.parent.gameObject.GetComponent<SingleBoardControl>();
                if (board != null)
                {
                    board.SlateClicked(Index);
                }
            }
        }
    }
}
