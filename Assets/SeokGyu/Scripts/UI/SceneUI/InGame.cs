using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject score;
    [SerializeField] private Vector2 scorePosition;
    [SerializeField] private GameObject feverText;
    [SerializeField] private Vector2 feverTextPosition;
    [SerializeField] private GameObject decideNode;
    [SerializeField] private Vector2[] nodesPosition;
    [SerializeField] private float nodeWidth;
    [SerializeField] private float nodeHeight;
    private int playerNum = 2;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1000f * i;

            GameObject feverTextObject = Instantiate(feverText);
            feverTextObject.transform.SetParent(transform);
            RectTransform feverTextRect = feverTextObject.GetComponent<RectTransform>();
            feverTextRect.transform.localPosition = new Vector3(feverTextPosition.x + distance, feverTextPosition.y, 0);
            feverTextObject.GetComponent<Canvas>().enabled = false;

            //for (int j = 0; j < nodesPosition.Length; j++)
            //{
            //    GameObject nodeObject = Instantiate(decideNode);
            //    nodeObject.transform.SetParent(transform);
            //    RectTransform nodeRect = nodeObject.GetComponent<RectTransform>();
            //    nodeRect.sizeDelta = new Vector2(nodeWidth, nodeHeight);
            //    nodeRect.transform.localPosition = new Vector3(nodesPosition[j].x + distance, nodesPosition[j].y, 0);
            //}

            distance = 1600f * i;
            GameObject scoreObject = Instantiate(score);
            scoreObject.transform.SetParent(transform);
            RectTransform scoreRect = scoreObject.GetComponent<RectTransform>();
            scoreRect.transform.localPosition = scorePosition;
            scoreRect.transform.localPosition = new Vector3(scorePosition.x + distance, scorePosition.y, 0);
        }
    }
}
