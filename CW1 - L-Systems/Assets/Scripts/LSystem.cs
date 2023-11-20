using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;


public class LSystem : MonoBehaviour
{   
    public float length = 5;
    public float width = 1f;
    [SerializeField] private GameObject branch;
    public float angle = 25.7f;
    [SerializeField] private float iterations;
    [SerializeField] private Transform treeSpawnpoint;
    //public Slider iterationSlider;
    public GameObject newTreeSpawner;
    private GameObject newTree;
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    [SerializeField] private Transform cameraBaseTransform;
    private string axiom = "F";

    private Stack<TransformInfo> m_Stack;
    private Dictionary<char, string> m_Dictionary;
    private string currentTree = "";

    //Input Values
    public Slider iterationVal;
    public Slider angleVal;
    public Slider branchLengthVal;
    public Slider branchWidthVal;
    public TMP_InputField axiomInput;
    public TMP_InputField rules;

    public TextMeshProUGUI branchCounter;
    private int branchNumCounter = 0;
    private int currIterLevel = 1;

    private GameObject currIteratationLevel;

    GameObject newBranch;
    Vector3 initialPosition;

    //https://www.youtube.com/watch?v=tUbTGWl-qus

    void Start()
    {
        m_Stack = new Stack<TransformInfo>();

        m_Dictionary = new Dictionary<char, string>();

        iterationVal.value = iterations;
        angleVal.value = angle;
        branchLengthVal.value = length;
        branchWidthVal.value = width;
        axiom = axiomInput.text;
        spawnPos = this.transform.position;
        spawnRot = this.transform.rotation;
    }

    public void regenerateTreeButton()
    {
        deleteTree();
     //   resizeFrameforIteration((int)iterations);
        parseRules(rules.text);
        branchNumCounter = 0;
        currIterLevel = 0;
        this.transform.position = spawnPos;
        this.transform.rotation = spawnRot;
        Camera.main.transform.position = cameraBaseTransform.position;
        Camera.main.transform.rotation = cameraBaseTransform.rotation;
        createTree();
        branchCounter.text = branchNumCounter.ToString();
    }

    public void resizeFrameforIteration(int iterations)
    {
        switch (iterations)
        {
            case 1:
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 50;
            break;
            case 2:
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 50;
                break;
            case 3:
                //Camera.main.transform.LookAt(transform.position + Vector3.up * 50);
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 100;
                break;
            case 4:
                //Camera.main.transform.LookAt(transform.position + Vector3.up * 200);
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 240;
                break;
            case 5:
                //Camera.main.transform.LookAt(transform.position + Vector3.up * 600);
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 650;
                break;
            case 6:
                //Camera.main.transform.LookAt(transform.position + Vector3.up * 1150);
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 1200;
                break;
            case 7:
                //Camera.main.transform.LookAt(transform.position + Vector3.up * 1200);
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 1200;
                break;
            case 8:
                Camera.main.transform.LookAt(transform.position);
                Camera.main.orthographicSize = 100;
                break;
            default:
                Camera.main.transform.LookAt(transform.position);
                break;

                //Debug.Log(iterations);
        }
    }

    private void Update()
    {
        iterations = iterationVal.value;
        angle = angleVal.value;
        length = branchLengthVal.value;
        width = branchWidthVal.value;
        axiom = axiomInput.text;
    }

    public void createTree()
    {
        currentTree = axiom;

        generateTreeString();

        //currentTree = sb.ToString();
        Debug.Log(currentTree);
        
        
        for (int i = 0; i < currentTree.Length; i++)
        {
            switch (currentTree[i])
            {
                case 'F':
                    initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);
                    branch.GetComponent<LineRenderer>().widthMultiplier = width;
                    GameObject newBranch = Instantiate(branch, currIteratationLevel.transform);
                    //Debug.Log("New Branch");
                    newBranch.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    newBranch.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    branchNumCounter++;
                    break;
                case 'G':
                    initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);
                    branch.GetComponent<LineRenderer>().widthMultiplier = width;
                    newBranch = Instantiate(branch, currIteratationLevel.transform);
                    //Debug.Log("New Branch");
                    newBranch.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    newBranch.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    branchNumCounter++;
                    break;
                case 'X':
                    break;
                case 'Y':
                    break;
                case '+':
                    transform.Rotate(Vector3.forward * angle);
                    break;
                case '-':
                    transform.Rotate(Vector3.back * angle);
                    break;
                case '[':
                    m_Stack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;
                case '*':
                    if (currentTree[i+1] != '*')
                    {
                        //Generate new iteration child
                        GameObject iterationLevel = Instantiate(new GameObject("IterationLevel" + ++currIterLevel));
                        iterationLevel.transform.SetParent(transform);
                        currIteratationLevel = iterationLevel;
                        //Debug.Log(currIterLevel);
                        break;
                    } else
                    {
                        currIterLevel++;
                        continue;
                    }
                case ']':
                    TransformInfo ti = m_Stack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;
                default:
                    Debug.LogError("Invalid L-System Operation. Please only use inputs: F, G, X, Y, +, -, [, ]");
                    break;
            }

        }
    }

    private void generateTreeString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            sb.Append("*"); //Marks new iteration
            foreach (char c in currentTree)
            {
                //Debug.Log(m_Dictionary.ContainsKey(c) ? m_Dictionary[c] : c.ToString());
                sb.Append(m_Dictionary.ContainsKey(c) ? m_Dictionary[c] : c.ToString());
            }
            currentTree = sb.ToString();
            Debug.Log(currentTree);
            sb = new StringBuilder();
        }
    }
    public void deleteTree()
    {
        if (this.transform.childCount != 0)
        {
            //newTree = Instantiate(newTreeSpawner);
            m_Dictionary.Clear();
            //this.transform.gameObject.SetActive(false);
            for (int i = 0; i < iterations;i++)
            {
                //Destroy(this.transform.Find("IterationLevel" + i+1 + "(clone)"));
                Destroy(currIteratationLevel);
            }
            //Instantiate(newTreeSpawner, this.transform);
        }
    }


    public void parseRules(string ruleInput)
    {
        char key = ' ';
        string rule = "";
        List<int> dictStarts = new List<int>();
        List<int> dictEnds = new List<int>();


        //Counts and sets the number of rules sets
        for (int i = 0; i < ruleInput.Length; i++)
        {
            if (ruleInput[i] == '{')
            {
                dictStarts.Add(i);
            }
            if (ruleInput[i] == '}')
            {
                dictEnds.Add(i);
            }

        }

        string currDict;
        string[] keyValue;
        for (int i = 0;i < dictStarts.Count; i++)
        {
            currDict = ruleInput.Substring(dictStarts[i], dictEnds[i] - dictStarts[i]); //Creates string of {'', ""}
            keyValue = currDict.Split(',');
            //Debug.Log("Key: " + keyValue[0]);
            //Debug.Log("Value: " + keyValue[1]);

            //Retrieve key value
            char[] temp = keyValue[0].Substring(3,1).ToCharArray();
            key = temp[0];
            //Debug.Log("TrimmedKey: " + key);

            rule = keyValue[1].Substring(2, keyValue[1].Length-4);
            //Debug.Log("TrimmedValue: " + rule);

            //{ 'X', "F[+X]F[-X]+X"}, //20, N=7
            //{ 'F', "FF" },
            //Debug.Log(key + " " + rule);
            m_Dictionary.Add(key, rule);
        }

        //ruleInput.IndexOf('{');
    }
}

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}