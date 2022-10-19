using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GraphMaker : MonoBehaviour
{
    public int SAMPLE_RATE = 10;
    // 점을 찍을 횟수

    Dictionary<int, int[]> AllTeamTotalGold = new Dictionary<int, int[]> { };

    [Header("GoldGraph")]

    public Transform DotGroup;
    public Transform LineGroup;
    // 생성한 프리팹들을 자식으로 둘 부모 오브젝트이다.
    // 점들을 모아놓는 것과 선과 점의 그려지는 순서를 바꾸기에 편하다.

    public Transform InnerFilledGroup;
    // 생성한 객체를 자식으로 둘 부모

    public GameObject Dot; // 점 프리팹
    public GameObject Line; // 선 프리팹
    public GameObject MaskPanel;
    public Color DotGreen;
    public Color DotRed;
    public Material BlueMat;
    public Material PurpleMat;

    public RectTransform GraphArea;
    // 그래프 길이가 해상도에 따라 달라짐
    // 그 값을 받아올 변수 추가

    private float width;
    private float height;

    void Start()
    {
        var rand = new System.Random();
        int BlueTeamGold = 0, PurpleTeamGold = 0;
        for (int time = 0; time < 100; time++)
        {
            BlueTeamGold = BlueTeamGold + rand.Next(0, 1000);
            PurpleTeamGold = PurpleTeamGold + rand.Next(0, 1000);
            AllTeamTotalGold.Add(time, new[] { BlueTeamGold, PurpleTeamGold });
        }

        width = GraphArea.rect.width;
        height = GraphArea.rect.height;
        // GraphArea의 가로 세로값을 가져온다.

        DrawGoldGraph();
    }

    private void DrawGoldGraph()
    {
        float startPositionX = -width / 2;
        // 그래프 영역의 길이 / 2에 -를 붙이면 시작위치가 된다.

        float maxYPosition = height / 2;
        // 그래프 영역의 높이 / 2 => 점을 찍을 최대 높이

        var comparisonValue = new Dictionary<int, float>();
        //받은 데이터를 기준으로 골드의 격차를 저장할 컬렉션

        var innerFilled = new List<Vector3>();
        // 점들의 위치를 저장할 리스트
        // UI위의 점들이지만 Vector3로 저장해야한다.

        foreach (var pair in AllTeamTotalGold)
            comparisonValue.Add(pair.Key, pair.Value[0] - pair.Value[1]);
        // 양팀의 골드 격차 저장 +면 Blue팀이 우세, -면 Purple팀이 우세

        float MaxValue = comparisonValue.Max(x => Mathf.Abs(x.Value));
        // +와 -가 공존하므로 절대값으로 최대값을 찾는다.
        // 이 최대값은 이따 점을 찍을 때 y값의 최대값이 되고
        // 나머지는 최대값을 기준으로 y값을 정해서 찍을 예정이다.

        Vector2 prevDotPos = Vector2.zero;
        // 이전 점과 현재 점을 이어야하므로 이전 점의 위치를 저장한다.

        for (int i = 0; i < SAMPLE_RATE; i++)
        {
            // Dot ----------------------------------------------------------------------------------------------------------------------------
            // 점 오브젝트 생성 및 부모 설정, 각 컴포넌트 가져오기
            GameObject dot = Instantiate(Dot, DotGroup, true);
            dot.transform.localScale = Vector3.one;

            RectTransform dotRT = dot.GetComponent<RectTransform>();
            Image dotImage = dot.GetComponent<Image>();

            int tick = SAMPLE_RATE - 1 == i ? AllTeamTotalGold.Count - 1 : AllTeamTotalGold.Count / (SAMPLE_RATE - 1) * i;
            // 총 경기시간 / 샘플링할 수로 간격을 정해 값을 가져오지만
            // 마지막 경기 끝났을 때의 격차를 알고싶어 경기 끝난 시간을 따로 가져옴

            float yPos = comparisonValue[tick] / MaxValue;
            // tick시간대의 골드격차 / 골드격차최대값 = -1.0f ~ 1.0f


            dotImage.color = yPos >= 0f ? DotGreen : DotRed;
            // 위 결과에 따라 격차가 0이여도 점은 BlueTeam색상을 하게 된다.
            // 이것도 엄격하게 0일때는 중립색으로 하겠다면 바꾸면 된다.

            dotRT.anchoredPosition = new Vector2(startPositionX + (width / (SAMPLE_RATE - 1) * i), maxYPosition * yPos);
            // 가로는 startPosition부터 각 격차마다 늘어나며 일정간격으로 찍히게 하였고
            // 세로는 격차/격차최대값에 따라 점이 찍힐 최대 높이에서 비율에 맞게 찍히게 하였다.

            innerFilled.Add(dotRT.anchoredPosition);

            // Line ---------------------------------------------------------------------------------------------------------------------------
            // 최초 점을 찍을 땐 연결할 선이 없으므로 넘긴다.
            if (i == 0)
            {
                prevDotPos = dotRT.anchoredPosition;
                continue;
            }

            GameObject line = Instantiate(Line, LineGroup, true);
            line.transform.localScale = Vector3.one;

            RectTransform lineRT = line.GetComponent<RectTransform>();
            Image lineImage = line.GetComponent<Image>();

            float lineWidth = Vector2.Distance(prevDotPos, dotRT.anchoredPosition);
            // 현재 점과 이전 점 사이의 거리 계산
            float xPos = (prevDotPos.x + dotRT.anchoredPosition.x) / 2; // x위치
            yPos = (prevDotPos.y + dotRT.anchoredPosition.y) / 2; // y위치

            Vector2 dir = (dotRT.anchoredPosition - prevDotPos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            lineRT.anchoredPosition = new Vector2(xPos, yPos);
            lineRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lineWidth);
            lineRT.localRotation = Quaternion.Euler(0f, 0f, angle);
            // 두 점 사이의 각도를 tan를 이용하여 구한다.
            // atan를 이용해 라디안 값을 구하고 Rad2Deg를 이용해 라디안을 각도로 변환해준다.

            GameObject maskPanel = Instantiate(MaskPanel, Vector3.zero, Quaternion.identity);
            maskPanel.transform.SetParent(LineGroup);
            maskPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            maskPanel.transform.SetParent(line.transform);
            // 마스크 패널 생성

            prevDotPos = dotRT.anchoredPosition;
            // 이전 점 좌표 업데이트
        }

        /*CreateFilledGraphShape(innerFilled.ToArray());*/
    }

    private void CreateFilledGraphShape(Vector3[] linePoints)
    {
        List<Vector3> filledGraphPointList = new List<Vector3>();
        // 삼각형의 정점의 좌표를 저장할 리스트

        Vector3 tmp;
        float x;
        // 색상이 바뀔 때 점을 추가로 리스트에 저장할 지역변수


        for (int i = 0; i < linePoints.Length; ++i)
        {
            filledGraphPointList.Add(new Vector3(linePoints[i].x, 0, 0));
            filledGraphPointList.Add(new Vector3(linePoints[i].x, linePoints[i].y, 0));

            if (i + 1 < linePoints.Length) // 그래프의 색상이 바뀔 때 대응
            {
                if (linePoints[i].y < 0 && linePoints[i + 1].y >= 0) // 아래에서 위로
                {
                    x = Mathf.Lerp(linePoints[i].x, linePoints[i + 1].x, Mathf.Abs(linePoints[i].y) / (Mathf.Abs(linePoints[i].y) + Mathf.Abs(linePoints[i + 1].y)));
                    tmp = new Vector3(x, 0f, 0f);

                    filledGraphPointList.Add(tmp);

                    MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, PurpleMat);

                    filledGraphPointList.Clear();
                    filledGraphPointList.Add(tmp);
                }
                else if (linePoints[i].y > 0 && linePoints[i + 1].y <= 0) // 위에서 아래로
                {
                    x = Mathf.Lerp(linePoints[i].x, linePoints[i + 1].x, Mathf.Abs(linePoints[i].y) / (Mathf.Abs(linePoints[i].y) + Mathf.Abs(linePoints[i + 1].y)));
                    tmp = new Vector3(x, 0f, 0f);

                    filledGraphPointList.Add(tmp);

                    MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, BlueMat);

                    filledGraphPointList.Clear();
                    filledGraphPointList.Add(tmp);
                }
                // 그래프의 색상이 바뀌었다면 일단 거기까지 MakeRenderer함수를 통해 Mesh를 생성
                // Mesh생성 후 filledGraphPointList는 초기화
            }
        }

        if (filledGraphPointList[filledGraphPointList.Count - 1].y >= 0)
        {
            MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, BlueMat);
        }
        else
        {
            MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, PurpleMat);
        }
    }

    // filledGraphPointList를 통해 Mesh생성
    private void MakeRenderer(Vector3[] graphPoints, int count, Material innerFill)
    {
        int trinangleCount = count - 2;  // 삼각형 개수
        int[] triangles = new int[trinangleCount * 3]; // 정점 수

        int idx = 0;
        int ex = trinangleCount / 2;
        for (int i = 0; i < ex; i++)
        {
            triangles[idx++] = 2 * i;
            triangles[idx++] = 2 * i + 1;
            triangles[idx++] = 2 * i + 2;

            triangles[idx++] = 2 * i + 1;
            triangles[idx++] = 2 * i + 2;
            triangles[idx++] = 2 * i + 3;
        }

        if (count % 2 == 1)
        {
            triangles[idx++] = 2 * ex;
            triangles[idx++] = 2 * ex + 1;
            triangles[idx++] = 2 * ex + 2;
        }

        Mesh filledGraphMesh = new Mesh();
        filledGraphMesh.vertices = graphPoints;
        filledGraphMesh.triangles = triangles;

        GameObject filledGraph = new GameObject("Filled graph");
        CanvasRenderer renderer = filledGraph.AddComponent<CanvasRenderer>();
        renderer.SetMesh(filledGraphMesh);
        renderer.SetMaterial(innerFill, null);
        filledGraph.transform.SetParent(InnerFilledGroup);
        filledGraph.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
        filledGraph.transform.localScale = Vector3.one;
    }
}