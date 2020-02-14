using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour /*, IPointerDownHandler*/
{
    public Text dialogueText;
    public GameObject nextText;
    public CanvasGroup dg;
    private bool istyping;
    player_manager pl;
    public Queue<string> sentences;

    private string currentSentence;


    public static DialogueManager instance
    {
        get
        {
            //if(_instance)
            //{
            //    _instance = FindObjectOfType<DialogueManager>();
            //    if(!_instance)
            //    {
            //        GameObject container = new GameObject();
            //        container.name = "DialogueManagerContainer";
            //        _instance = container.AddComponent(typeof(DialogueManager)) as DialogueManager;
            //    }
            //}
            if(_instance==null)
            {
                _instance = FindObjectOfType<DialogueManager>();
            }
            return _instance;
        }


    }
    public static DialogueManager _instance;

private void Awake()
    {
        //if (instance != this) { Destroy(gameObject); Debug.Log(""); }
        //instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("player").GetComponent<player_manager>();
        sentences = new Queue<string>();
    }

    public void OnDialogue(string[] lines)
    {
        
        if (lines == null) Debug.Log("ㅇㄴㄹㄴㅇㄹㄹㅇ");
        if (sentences != null)
        {
            sentences.Clear();
            Debug.Log("샌즈?");
        }
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dg.alpha = 1;
        dg.blocksRaycasts = true;
        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count !=0)
        {
            currentSentence = sentences.Dequeue();

            istyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
            StartCoroutine("wait");
        }
        else
        {
            dg.alpha = 0;
            dg.blocksRaycasts = false;
        }
    }
    IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.08f);
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        NextSentence();
    }

        // Update is called once per frame
        void Update()
    {
        if(dg.alpha==1)
        {
            pl.istalking = true;
        }
        else 
        {
            pl.istalking = false;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (!istyping)
            {
                StopCoroutine("wait");
                NextSentence();
            }
        }

        if (dialogueText.text.Equals(currentSentence))
        {
            nextText.SetActive(true);
            istyping = false;
        }
    }
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (!istyping)
    //        NextSentence();
    //}

}
