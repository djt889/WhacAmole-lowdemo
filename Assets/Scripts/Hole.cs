using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Hole : MonoBehaviour
{
    // 定义一个浮点数变量qwe
    private float qwe;
    
    // 定义一个Vector2类型的List变量vector2s
    public List<Vector2> vector2s;
    // 定义一个Vector2类型的List变量inPot
    public List<Vector2> inPot;
    // 定义一个Transform类型的变量GeneratePoint
    private Transform GeneratePoint;
    // 定义一个布尔类型的变量isGenerate，用于控制是否生成
    private bool isGenerate = true;

    // 在游戏开始时调用
    public void Awake()
    {
        // 初始化vector2s和inPot
        vector2s = new List<Vector2>();
        inPot = new List<Vector2>();
        // 遍历当前对象的子对象，将子对象的localPosition添加到vector2s中
        foreach (Transform child in transform)
        {
            vector2s.Add(child.localPosition);
        }
        // 遍历vector2s，输出每个元素
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(vector2s[i]);
            Debug.Log("test");
        }

        // 遍历当前对象的子对象，将子对象的第一个子对象的transform赋值给GeneratePoint，并将该对象设为不可见
        foreach (Transform child in transform)
        {
            GeneratePoint = child.GetChild(0).transform;
            GeneratePoint.gameObject.SetActive(false);
        }
    }

    // 每帧调用
    public void Update()
    {
        // 如果isGenerate为true，则调用Generate方法
        if (isGenerate)
        {
            Generate();
            
            // 随机生成一个0.6到0.8之间的时间，调用IsGenerate方法
            float time = Random.Range(0.6f, 0.8f);
            Invoke(nameof(IsGenerate), time);
        }
    }

    // 生成方法
    public void Generate()
    {
        // 将isGenerate设为false
        isGenerate = false;
        // 如果inPot的元素个数大于等于vector2s的元素个数，则输出游戏结束
        if (inPot.Count >= vector2s.Count)
        {
            Debug.Log("游戏结束");
            return;
        }
        // 随机生成一个0到vector2s的元素个数之间的整数
        int generate = Random.Range(0, vector2s.Count);
        // 如果inPot的元素个数小于vector2s的元素个数
        if (inPot.Count < vector2s.Count)
        {
            // 如果inPot不包含vector2s的generate位置的元素
            if (!inPot.Contains(vector2s[generate]))
            {
                // 将vector2s的generate位置的子对象的第一个子对象设为可见
                transform.GetChild(generate).GetChild(0).gameObject.SetActive(true);
                // 将vector2s的generate位置的元素添加到inPot中
                inPot.Add(vector2s[generate]);
                // 如果鼠标左键没有被按下，则调用RemoveAfterDelay方法
                if (!Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(RemoveAfterDelay(vector2s[generate], 1f,generate));
                }
                
            }
            // 否则，调用Generate方法
            else
            {
                Generate();
            }
        }
    }

    // 控制是否生成的方法
    public void IsGenerate()
    {
        // 将isGenerate设为true
        isGenerate = true;
    }


    // 延迟移除元素的方法
    public IEnumerator RemoveAfterDelay(Vector2 element, float delay,int ger)
    {
        // 延迟delay秒
        yield return new WaitForSeconds(delay);
        // 将inPot中的element元素移除
        inPot.Remove(element);
        // 将vector2s的ger位置的子对象的第一个子对象设为不可见
        transform.GetChild(ger).GetChild(0).gameObject.SetActive(false);
        // 输出元素已移除
        Debug.Log("元素已移除: " + element);
    }

}
