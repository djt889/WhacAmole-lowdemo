using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Onclick : MonoBehaviour
{
    private Hole hole;
    private BoxCollider2D boxCollider;
    private static int indexs = 0;
    private Text score;
    private GameObject image;
    private GameObject node;
    private Vector2 ints;
    
    public Sprite mySprite;
    // Start is called before the first frame update
    void Start()
    {
        hole = GameObject.Find("Hole").GetComponent<Hole>();
        boxCollider = GetComponent<BoxCollider2D>();
        score = GameObject.Find("Canvas/Score").GetComponent<Text>();
        score.text = "Score:" + indexs;
        image = GameObject.Find("受伤");
    }
    
    void Update()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // 在2D游戏中通常将z坐标设置为0
            // 检查鼠标位置是否与碰撞体相交
            if (boxCollider.OverlapPoint(new Vector2(mousePosition.x, mousePosition.y)))
            {
                Vector2 inPotRemove = boxCollider.gameObject.transform.position;
                for (int i = 0; i < hole.inPot.Count; i++)
                {
                    if (hole.inPot[i] == inPotRemove)
                    {
                        ints = inPotRemove;
                        node = Instantiate(image,inPotRemove,Quaternion.identity);
                        transform.GetChild(0).gameObject.SetActive(false);
                        indexs++;
                        score.text = "Score:" + indexs;
                        Invoke(nameof(Die), 1f);
                        
                        
                    }
                }
            }
        }
        
    }


    public void Die()
    {
        Destroy(node.gameObject);
        hole.inPot.Remove(ints);
    }



}
