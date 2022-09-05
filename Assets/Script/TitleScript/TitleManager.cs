using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoSingleTon<TitleManager>
{
    //components
    Cursor cursor;
    Animator cursorAnimator;
    GameObject nextViewPosition;
    GameObject tutorialUI;
    GameObject optionUI;

    //cursor info
    private int cursorIndex;

    //initial data
    private Vector2 initPosition;

    //constant value
    [SerializeField] private float swapSpeed = 1200f;
    [SerializeField] private float exitTime = 1f;

    //variable value
    private float coroutineTimer = 0f;

    public bool IsTutorial { get; set; }
    public bool IsOption { get; set; }

    enum MenuIndex
    {
        Start,
        Tutorial,
        Option,
        Exit,
        Max
    }

    private void Awake()
    {
        nextViewPosition = GameObject.Find("NextPosition");
        cursorAnimator = GameObject.Find("Cursor").GetComponent<Animator>();
        cursor = FindObjectOfType<Cursor>();
        tutorialUI = GameObject.Find("TutorialUI");
        optionUI = GameObject.Find("OptionUI");

    }
    private void Start()
    {
        tutorialUI.SetActive(false);
        optionUI.SetActive(false);
        nextViewPosition.SetActive(false);
        initPosition = cursor.transform.position;

        cursorIndex = (int)MenuIndex.Start;

        IsTutorial = false;
        IsOption = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        InputControll();
    }

    void InputControll()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
            cursorIndex++;
            if (cursorIndex == 4) cursorIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            cursorIndex--;
            if (cursorIndex == -1) cursorIndex = 3;
        }

        cursor.transform.position = initPosition + Vector2.down * cursorIndex * 100f;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            cursorAnimator.SetTrigger("isChoiced");
            cursor.SendMessage("Action", 3f , SendMessageOptions.DontRequireReceiver);
            if (cursorIndex == (int)MenuIndex.Start)
            {
                SceneManager.LoadScene("LoadingScene");
            }
            else if(cursorIndex == (int)MenuIndex.Tutorial)
            {
                tutorialUI.SetActive(true);
                IsTutorial = true;
                Invoke("MoveNextView", 1.5f); 
            }
            else if (cursorIndex == (int)MenuIndex.Option)
            {
                optionUI.SetActive(true);
                IsOption = true;
                Invoke("MoveNextView", 1.5f);
            }
            else 
            {
                Application.Quit();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsTutorial = false;
            IsOption = false;
            optionUI.SetActive(false);
            tutorialUI.SetActive(false);
            nextViewPosition.SetActive(false);
        }
    }

    void MoveNextView()
    {
        nextViewPosition.SetActive(true);
    }
}
