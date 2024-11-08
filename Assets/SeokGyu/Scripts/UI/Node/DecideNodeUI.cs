using UnityEngine;

public class DecideNode : MonoBehaviour
{
    [SerializeField]
    [Range(3,100)]
    private int segments = 24;
    [SerializeField]
    private float circleRadius = 200.0f;
    RectTransform child = null;
    LineRenderer line;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    void Init()
    {
        GameObject ob = GameObject.Find("CircleNode");
        if(ob != null)
        {
            child = ob.GetComponent<RectTransform>();
            line = ob.GetComponent<LineRenderer>();
            line.positionCount = segments+ 1;
            line.useWorldSpace = false;
            line.endWidth = 10f;
            CreatePoints();
        }
        
    }

    void Update()
    {
        UpdateNode();
    }

    void UpdateNode()
    {
        if (circleRadius < 0.0f) return;

        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * circleRadius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * circleRadius;

            line.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / segments);
        }
    }
}
