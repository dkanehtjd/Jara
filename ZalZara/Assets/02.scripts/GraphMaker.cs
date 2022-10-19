using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GraphMaker : MonoBehaviour
{
    public int SAMPLE_RATE = 10;
    // ���� ���� Ƚ��

    Dictionary<int, int[]> AllTeamTotalGold = new Dictionary<int, int[]> { };

    [Header("GoldGraph")]

    public Transform DotGroup;
    public Transform LineGroup;
    // ������ �����յ��� �ڽ����� �� �θ� ������Ʈ�̴�.
    // ������ ��Ƴ��� �Ͱ� ���� ���� �׷����� ������ �ٲٱ⿡ ���ϴ�.

    public Transform InnerFilledGroup;
    // ������ ��ü�� �ڽ����� �� �θ�

    public GameObject Dot; // �� ������
    public GameObject Line; // �� ������
    public GameObject MaskPanel;
    public Color DotGreen;
    public Color DotRed;
    public Material BlueMat;
    public Material PurpleMat;

    public RectTransform GraphArea;
    // �׷��� ���̰� �ػ󵵿� ���� �޶���
    // �� ���� �޾ƿ� ���� �߰�

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
        // GraphArea�� ���� ���ΰ��� �����´�.

        DrawGoldGraph();
    }

    private void DrawGoldGraph()
    {
        float startPositionX = -width / 2;
        // �׷��� ������ ���� / 2�� -�� ���̸� ������ġ�� �ȴ�.

        float maxYPosition = height / 2;
        // �׷��� ������ ���� / 2 => ���� ���� �ִ� ����

        var comparisonValue = new Dictionary<int, float>();
        //���� �����͸� �������� ����� ������ ������ �÷���

        var innerFilled = new List<Vector3>();
        // ������ ��ġ�� ������ ����Ʈ
        // UI���� ���������� Vector3�� �����ؾ��Ѵ�.

        foreach (var pair in AllTeamTotalGold)
            comparisonValue.Add(pair.Key, pair.Value[0] - pair.Value[1]);
        // ������ ��� ���� ���� +�� Blue���� �켼, -�� Purple���� �켼

        float MaxValue = comparisonValue.Max(x => Mathf.Abs(x.Value));
        // +�� -�� �����ϹǷ� ���밪���� �ִ밪�� ã�´�.
        // �� �ִ밪�� �̵� ���� ���� �� y���� �ִ밪�� �ǰ�
        // �������� �ִ밪�� �������� y���� ���ؼ� ���� �����̴�.

        Vector2 prevDotPos = Vector2.zero;
        // ���� ���� ���� ���� �̾���ϹǷ� ���� ���� ��ġ�� �����Ѵ�.

        for (int i = 0; i < SAMPLE_RATE; i++)
        {
            // Dot ----------------------------------------------------------------------------------------------------------------------------
            // �� ������Ʈ ���� �� �θ� ����, �� ������Ʈ ��������
            GameObject dot = Instantiate(Dot, DotGroup, true);
            dot.transform.localScale = Vector3.one;

            RectTransform dotRT = dot.GetComponent<RectTransform>();
            Image dotImage = dot.GetComponent<Image>();

            int tick = SAMPLE_RATE - 1 == i ? AllTeamTotalGold.Count - 1 : AllTeamTotalGold.Count / (SAMPLE_RATE - 1) * i;
            // �� ���ð� / ���ø��� ���� ������ ���� ���� ����������
            // ������ ��� ������ ���� ������ �˰�;� ��� ���� �ð��� ���� ������

            float yPos = comparisonValue[tick] / MaxValue;
            // tick�ð����� ������ / �������ִ밪 = -1.0f ~ 1.0f


            dotImage.color = yPos >= 0f ? DotGreen : DotRed;
            // �� ����� ���� ������ 0�̿��� ���� BlueTeam������ �ϰ� �ȴ�.
            // �̰͵� �����ϰ� 0�϶��� �߸������� �ϰڴٸ� �ٲٸ� �ȴ�.

            dotRT.anchoredPosition = new Vector2(startPositionX + (width / (SAMPLE_RATE - 1) * i), maxYPosition * yPos);
            // ���δ� startPosition���� �� �������� �þ�� ������������ ������ �Ͽ���
            // ���δ� ����/�����ִ밪�� ���� ���� ���� �ִ� ���̿��� ������ �°� ������ �Ͽ���.

            innerFilled.Add(dotRT.anchoredPosition);

            // Line ---------------------------------------------------------------------------------------------------------------------------
            // ���� ���� ���� �� ������ ���� �����Ƿ� �ѱ��.
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
            // ���� ���� ���� �� ������ �Ÿ� ���
            float xPos = (prevDotPos.x + dotRT.anchoredPosition.x) / 2; // x��ġ
            yPos = (prevDotPos.y + dotRT.anchoredPosition.y) / 2; // y��ġ

            Vector2 dir = (dotRT.anchoredPosition - prevDotPos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            lineRT.anchoredPosition = new Vector2(xPos, yPos);
            lineRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lineWidth);
            lineRT.localRotation = Quaternion.Euler(0f, 0f, angle);
            // �� �� ������ ������ tan�� �̿��Ͽ� ���Ѵ�.
            // atan�� �̿��� ���� ���� ���ϰ� Rad2Deg�� �̿��� ������ ������ ��ȯ���ش�.

            GameObject maskPanel = Instantiate(MaskPanel, Vector3.zero, Quaternion.identity);
            maskPanel.transform.SetParent(LineGroup);
            maskPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            maskPanel.transform.SetParent(line.transform);
            // ����ũ �г� ����

            prevDotPos = dotRT.anchoredPosition;
            // ���� �� ��ǥ ������Ʈ
        }

        /*CreateFilledGraphShape(innerFilled.ToArray());*/
    }

    private void CreateFilledGraphShape(Vector3[] linePoints)
    {
        List<Vector3> filledGraphPointList = new List<Vector3>();
        // �ﰢ���� ������ ��ǥ�� ������ ����Ʈ

        Vector3 tmp;
        float x;
        // ������ �ٲ� �� ���� �߰��� ����Ʈ�� ������ ��������


        for (int i = 0; i < linePoints.Length; ++i)
        {
            filledGraphPointList.Add(new Vector3(linePoints[i].x, 0, 0));
            filledGraphPointList.Add(new Vector3(linePoints[i].x, linePoints[i].y, 0));

            if (i + 1 < linePoints.Length) // �׷����� ������ �ٲ� �� ����
            {
                if (linePoints[i].y < 0 && linePoints[i + 1].y >= 0) // �Ʒ����� ����
                {
                    x = Mathf.Lerp(linePoints[i].x, linePoints[i + 1].x, Mathf.Abs(linePoints[i].y) / (Mathf.Abs(linePoints[i].y) + Mathf.Abs(linePoints[i + 1].y)));
                    tmp = new Vector3(x, 0f, 0f);

                    filledGraphPointList.Add(tmp);

                    MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, PurpleMat);

                    filledGraphPointList.Clear();
                    filledGraphPointList.Add(tmp);
                }
                else if (linePoints[i].y > 0 && linePoints[i + 1].y <= 0) // ������ �Ʒ���
                {
                    x = Mathf.Lerp(linePoints[i].x, linePoints[i + 1].x, Mathf.Abs(linePoints[i].y) / (Mathf.Abs(linePoints[i].y) + Mathf.Abs(linePoints[i + 1].y)));
                    tmp = new Vector3(x, 0f, 0f);

                    filledGraphPointList.Add(tmp);

                    MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, BlueMat);

                    filledGraphPointList.Clear();
                    filledGraphPointList.Add(tmp);
                }
                // �׷����� ������ �ٲ���ٸ� �ϴ� �ű���� MakeRenderer�Լ��� ���� Mesh�� ����
                // Mesh���� �� filledGraphPointList�� �ʱ�ȭ
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

    // filledGraphPointList�� ���� Mesh����
    private void MakeRenderer(Vector3[] graphPoints, int count, Material innerFill)
    {
        int trinangleCount = count - 2;  // �ﰢ�� ����
        int[] triangles = new int[trinangleCount * 3]; // ���� ��

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