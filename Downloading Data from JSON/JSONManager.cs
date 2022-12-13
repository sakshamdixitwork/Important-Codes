using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JSONManager : MonoBehaviour
{
    public static JSONManager instance;
    public string prefixPath = "https://uq-board-3e2fe.firebaseapp.com";
    private string jsonURL;
    private string imageURL;
    public Sprite downloadedImage;
    //public Sprite defalutSprite;
    public int categoryCount = 6;

    public GameObject[] allCategoryObjects;
    public List<int> selectedCategoryIndex = new List<int>();

    public GameObject correctAnswerOption;
    public string correctAnswerOptionName;
    public string freeForAllCorrectAnswer;

    public List<string> displayedQuestions = new List<string>();
    public List<GameObject> categorySelected = new List<GameObject>();

    [SerializeField]
    private int currentQuestion;
    private int CorrectAnswerIndex;

    public bool optionAnswerisCorrect;
    private Color defOptionButtonColor;
    private Color rightAnswerOptionButtonColor;
    private Color wrongAnswerOptionButtonColor;
    public bool AlreadyAnswered;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //  prefixPath = "file://" + Application.dataPath + "/Resources";
    }

    public void CurrentAffairAndPoliticsGenerator()
    {
        Debug.Log("CurrentAffairAndPoliticsGenerator");
        StartCoroutine(DownloadCurrentAffairAndPoliticsJSON());
    }

    public void EvaluateGenerator()
    {
        Debug.Log("EvaluateGenerator");
        StartCoroutine(DownloadEvaluateJSON());
    }

    public void FilmAndEntertainmentGenerator()
    {
        Debug.Log("FilmAndEntertainmentGenerator");
        StartCoroutine(DownloadFilmAndEntertainmentJSON());
    }

    public void FoodGenerator()
    {
        Debug.Log("FoodGenerator");
        StartCoroutine(DownloadFoodJSON());
    }

    public void GeographyGenerator()
    {
        Debug.Log("GeographyGenerator");
        StartCoroutine(DownloadGeographyJSON());
    }

    public void LifestyleGenerator()
    {
        Debug.Log("LifestyleGenerator");
        StartCoroutine(DownloadLifestyleJSON());
    }

    public void HistoryGenerator()
    {
        Debug.Log("HistoryGenerator");
        StartCoroutine(DownloadHistoryJSON());
    }

    public void InOutGenerator()
    {
        Debug.Log("InOutGenerator");
        StartCoroutine(DownloadInOutJSON());
    }

    public void JumblesGenerator()
    {
        Debug.Log("JumblesGenerator");
        StartCoroutine(DownloadJumbleJSON());
    }

    public void LinksMashGenerator()
    {
        Debug.Log("LinksMashGenerator");
        StartCoroutine(DownloadLinksMashJSON());
    }
    public void LitratureGenerator()
    {
        Debug.Log("LitratureGenerator");
        StartCoroutine(DownloadLitratureMashJSON());
    }

    public void LogosGenerator()
    {
        Debug.Log("LogosGenerator");
        StartCoroutine(DownloadLogosJSON());
    }

    public void MusicGenerator()
    {
        Debug.Log("MusicGenerator");
        StartCoroutine(DownloadMusicJSON());
    }

    public void MysteryGenerator()
    {
        Debug.Log("MysteryGenerator");
        StartCoroutine(DownloadMysteryJSON());
    }

    public void GeneralKnowledgeGenerator()
    {
        Debug.Log("GeneralKnowledgeGenerator");
        StartCoroutine(DownloadGeneralKnowledgeJSON());
    }

    public void NaturalWorldGenerator()
    {
        Debug.Log("NaturalWorldGenerator");
        StartCoroutine(DownloadNaturalWorldJSON());
    }

    public void NumbersGenerator()
    {
        Debug.Log("NumbersGenerator");
        StartCoroutine(DownloadNumbersJSON());
    }

    public void RebustersGenerator()
    {
        Debug.Log("RebustersGenerator");
        StartCoroutine(DownloadRebustersJSON());
    }

    public void ScienceAndTechnologyGenerator()
    {
        Debug.Log("ScienceAndTechnologyGenerator");
        StartCoroutine(DownloadScienceAndTechnologyJSON());
    }

    public void SportGenerator()
    {
        Debug.Log("SportGenerator");
        StartCoroutine(DownloadSportJSON());
    }

    public void TheArtsGenerator()
    {
        Debug.Log("TheArtsGenerator");
        StartCoroutine(DownloadTheArtsJSON());
    }

    public void WhichWitchGenerator()
    {
        Debug.Log("WhichWitchGenerator");
        StartCoroutine(DownloadWhichWitchJSON());
    }

    public void WWWGenerator()
    {
        Debug.Log("WWWGenerator");
        StartCoroutine(DownloadWWWJSON());
    }

    public void XplainGenerator()
    {
        Debug.Log("XplainGenerator");
        StartCoroutine(DownloadXplainJSON());
    }

    public void FreeForAllGenerator()
    {
        Debug.Log("FreeForAllGenerator");
        StartCoroutine(DownloadFreeForAllJSON());
    }


    //General JSON class
    [System.Serializable]
    public class JSONData
    {
        public string id;
        public string trivia_type;
        public string category;
        public string subcategory;
        public string age_tag;
        public int difficulty;
        public string image;
        public string image_source;
        public AudioClip audio;
        public string who_image_copyright;
        public string what_image_copyright;
        public string where_image_copyright;
        public int timer;
        public string question;
        public string answer;
        public string answer_option1;
        public string answer_option2;
        public string answer_option3;
        public string answer_option4;
        public string clue;
        public string clue_answer;
        public string who_answer;
        public string what_answer;
        public string where_answer;
        public int correct_answer_no;
    }

    [System.Serializable]
    public class CurrentAffairsAndPoliticsList
    {
        public JSONData[] currentAffairsAndPolitics;
    }

    public CurrentAffairsAndPoliticsList myCurrentAffairsAndPoliticsList = new CurrentAffairsAndPoliticsList();

    IEnumerator DownloadCurrentAffairAndPoliticsJSON()
    {
        jsonURL = prefixPath + "/CurrentAffairsAndPolitics.json";
        Debug.Log("CurrentAffairsAndPolitics Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_CurrentAffairAndPolitics(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download CurrentAffairsAndPolitics JSON Error");
        }

    }

    private void Question_CurrentAffairAndPolitics(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myCurrentAffairsAndPoliticsList = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);

        currentQuestion = Random.Range(0, myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics.Length);

        if (!displayedQuestions.Contains(myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].id))
        {
            imageURL = myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].image_source;
            StartCoroutine(CurrentAffairImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_CurrentAffairAndPolitics(jsonData);
        }
        jsonData = null;
    }

    IEnumerator CurrentAffairImageDownload(string imgURL, string jsonData)
    {
        myCurrentAffairsAndPoliticsList = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;
           
            SetOptions_CurrentAffairAndPolitics(jsonData);
        }
    }

    private void SetOptions_CurrentAffairAndPolitics(string jsonData)
    {
        myCurrentAffairsAndPoliticsList = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if(UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myCurrentAffairsAndPoliticsList.currentAffairsAndPolitics[currentQuestion].id);

        
        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myCurrentAffairsAndPoliticsList = null;
    }


    //Evaluate
    [System.Serializable]
    public class EvaluateList
    {
        public JSONData[] evaluates; 
    }

    public EvaluateList myEvaluateList = new EvaluateList();

    IEnumerator DownloadEvaluateJSON()
    {
        jsonURL = prefixPath + "/Evaluate.json";
        Debug.Log("Evaluate Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Evaluate(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Evaluate JSON Error");
        }
    }

    private void Question_Evaluate(string jsonData)
    {
        //EvaluateList json = JsonUtility.FromJson<EvaluateList>(jsonData);
        myEvaluateList = JsonUtility.FromJson<EvaluateList>(jsonData);

        currentQuestion = Random.Range(0, myEvaluateList.evaluates.Length);

        if (!displayedQuestions.Contains(myEvaluateList.evaluates[currentQuestion].id))
        {
            imageURL = myEvaluateList.evaluates[currentQuestion].image_source;
            StartCoroutine(EvaluateImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Evaluate(jsonData);
        }
        jsonData = null;
    }

    IEnumerator EvaluateImageDownload(string imgURL, string jsonData)
    {
        myEvaluateList = JsonUtility.FromJson<EvaluateList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Evaluate(jsonData);
        }
    }

    private void SetOptions_Evaluate(string jsonData)
    {
        myEvaluateList = JsonUtility.FromJson<EvaluateList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myEvaluateList.evaluates[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myEvaluateList.evaluates[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myEvaluateList.evaluates[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myEvaluateList.evaluates[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myEvaluateList.evaluates[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myEvaluateList.evaluates[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myEvaluateList.evaluates[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text; 

        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myEvaluateList.evaluates[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myEvaluateList = null;
    }

    //FilmAndEntertainment
    [System.Serializable]
    public class FilmAndEntertainmentList
    {
        public JSONData[] filmAndEntertainments;
    }

    public FilmAndEntertainmentList myFilmAndEntertainmentList = new FilmAndEntertainmentList();

    IEnumerator DownloadFilmAndEntertainmentJSON()
    {
        jsonURL = prefixPath + "/FilmAndEntertainment.json";
        Debug.Log("FilmAndEntertainment Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_FilmAndEntertainment(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download FilmAndEntertainment JSON Error");
        }

    }

    private void Question_FilmAndEntertainment(string jsonData)
    {
        Debug.Log("Download FilmAndEntertainment JSON Success");
        myFilmAndEntertainmentList = JsonUtility.FromJson<FilmAndEntertainmentList>(jsonData);

        currentQuestion = Random.Range(0, myFilmAndEntertainmentList.filmAndEntertainments.Length);

        if (!displayedQuestions.Contains(myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].id))
        {
            imageURL = myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].image_source;
            StartCoroutine(FilmAndEntertainmentImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_FilmAndEntertainment(jsonData);
        }
        jsonData = null;
    }

    IEnumerator FilmAndEntertainmentImageDownload(string imgURL, string jsonData)
    {
        myFilmAndEntertainmentList = JsonUtility.FromJson<FilmAndEntertainmentList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_FilmAndEntertainment(jsonData);
        }
    }

    private void SetOptions_FilmAndEntertainment(string jsonData)
    {
        myFilmAndEntertainmentList = JsonUtility.FromJson<FilmAndEntertainmentList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myFilmAndEntertainmentList.filmAndEntertainments[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myFilmAndEntertainmentList = null;
    }

    //FoodAndGastronomy
    [System.Serializable]
    public class FoodList
    {
        public JSONData[] foods;
    }

    public FoodList myFoodList = new FoodList();

    IEnumerator DownloadFoodJSON()
    {
        jsonURL = prefixPath + "/Food.json";
        Debug.Log("Food Json : " + jsonURL);
        print("json url " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Food(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Food JSON Error");
        }
        yield return new WaitForSeconds(1);
    }

    private void Question_Food(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myFoodList = JsonUtility.FromJson<FoodList>(jsonData);

        currentQuestion = Random.Range(0, myFoodList.foods.Length);

        if (!displayedQuestions.Contains(myFoodList.foods[currentQuestion].id))
        {
            imageURL = myFoodList.foods[currentQuestion].image_source;
            StartCoroutine(FoodImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Food(jsonData);
        }
        jsonData = null;
    }

    IEnumerator FoodImageDownload(string imgURL, string jsonData)
    {
        myFoodList = JsonUtility.FromJson<FoodList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Food(jsonData);
        }
    }

    private void SetOptions_Food(string jsonData)
    {
        myFoodList = JsonUtility.FromJson<FoodList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myFoodList.foods[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myFoodList.foods[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myFoodList.foods[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myFoodList.foods[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myFoodList.foods[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myFoodList.foods[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myFoodList.foods[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myFoodList.foods[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myFoodList.foods[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myFoodList = null;
    }


    //Geography
    [System.Serializable]
    public class GeographyList
    {
        public JSONData[] geographys;
    }

    public GeographyList myGeographyList = new GeographyList();

    IEnumerator DownloadGeographyJSON()
    {
        jsonURL = prefixPath + "/Geography.json";

        Debug.Log("Geography Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Geography(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Geography JSON Error");
        }

    }

    private void Question_Geography(string jsonData)
    {
        //GeographyList json = JsonUtility.FromJson<GeographyList>(jsonData);
        myGeographyList = JsonUtility.FromJson<GeographyList>(jsonData);

        currentQuestion = Random.Range(0, myGeographyList.geographys.Length);

        if (!displayedQuestions.Contains(myGeographyList.geographys[currentQuestion].id))
        {
            imageURL = myGeographyList.geographys[currentQuestion].image_source;
            StartCoroutine(GeographyImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Geography(jsonData);
        }
        jsonData = null;
    }

    IEnumerator GeographyImageDownload(string imgURL, string jsonData)
    {
        myGeographyList = JsonUtility.FromJson<GeographyList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Geography(jsonData);
        }
        
    }

    private void SetOptions_Geography(string jsonData)
    {
        myGeographyList = JsonUtility.FromJson<GeographyList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myGeographyList.geographys[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myGeographyList.geographys[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myGeographyList.geographys[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myGeographyList.geographys[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myGeographyList.geographys[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myGeographyList.geographys[currentQuestion].correct_answer_no - 1].gameObject;
        Debug.Log("Correct Answer Number :::: " + myGeographyList.geographys[currentQuestion].correct_answer_no);
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myGeographyList.geographys[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text; 

        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myGeographyList.geographys[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myGeographyList = null;
    }


    //Lifestyle
    [System.Serializable]
    public class LifestyleList
    {
        public JSONData[] Lifestyle;
    }

    public LifestyleList myLifestyleList = new LifestyleList();

    IEnumerator DownloadLifestyleJSON()
    {
        jsonURL = prefixPath + "/Lifestyle.json";
        Debug.Log("Lifestyle Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Lifestyle(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Lifestyle JSON Error");
        }
        www = null;
    }

    private void Question_Lifestyle(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myLifestyleList = JsonUtility.FromJson<LifestyleList>(jsonData);

        currentQuestion = Random.Range(0, myLifestyleList.Lifestyle.Length);

        if (!displayedQuestions.Contains(myLifestyleList.Lifestyle[currentQuestion].id))
        {
            imageURL = myLifestyleList.Lifestyle[currentQuestion].image_source;
            StartCoroutine(LifestyleImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Lifestyle(jsonData);
        }

        jsonData = null;
    }

    IEnumerator LifestyleImageDownload(string imgURL, string jsonData)
    {
        myLifestyleList = JsonUtility.FromJson<LifestyleList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Lifestyle(jsonData);
        }

        www = null;
        jsonData = null;
    }

    private void SetOptions_Lifestyle(string jsonData)
    {
        myLifestyleList = JsonUtility.FromJson<LifestyleList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myLifestyleList.Lifestyle[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myLifestyleList.Lifestyle[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myLifestyleList.Lifestyle[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myLifestyleList.Lifestyle[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myLifestyleList.Lifestyle[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myLifestyleList.Lifestyle[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myLifestyleList.Lifestyle[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myLifestyleList.Lifestyle[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myLifestyleList.Lifestyle[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myLifestyleList = null;
        jsonData = null;
    }



    //History
    [System.Serializable]
    public class HistoryList
    {
        public JSONData[] historys;
    }

    public HistoryList myHistoryList = new HistoryList();

    IEnumerator DownloadHistoryJSON()
    {
        jsonURL = prefixPath + "/History.json";
        Debug.Log("History Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_History(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download History JSON Error");
        }

    }

    private void Question_History(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myHistoryList = JsonUtility.FromJson<HistoryList>(jsonData);

        currentQuestion = Random.Range(0, myHistoryList.historys.Length);

        if (!displayedQuestions.Contains(myHistoryList.historys[currentQuestion].id))
        {
            imageURL = myHistoryList.historys[currentQuestion].image_source;
            StartCoroutine(HistoryImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_History(jsonData);
        }
        jsonData = null;
    }

    IEnumerator HistoryImageDownload(string imgURL, string jsonData)
    {
        myHistoryList = JsonUtility.FromJson<HistoryList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_History(jsonData);
        }
    }

    private void SetOptions_History(string jsonData)
    {
        myHistoryList = JsonUtility.FromJson<HistoryList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myHistoryList.historys[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myHistoryList.historys[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text =myHistoryList.historys[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myHistoryList.historys[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myHistoryList.historys[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myHistoryList.historys[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myHistoryList.historys[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myHistoryList.historys[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myHistoryList.historys[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myHistoryList = null;
    }


    //InOut
    [System.Serializable]
    public class InOutList
    {
        public JSONData[] inOuts;
    }

    public InOutList myInOutList = new InOutList();

    IEnumerator DownloadInOutJSON()
    {
        jsonURL = prefixPath + "/InOut.json";
        Debug.Log("InOut json  : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_InOut(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download InOut JSON Error");
        }

    }

    private void Question_InOut(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myInOutList = JsonUtility.FromJson<InOutList>(jsonData);

        currentQuestion = Random.Range(0, myInOutList.inOuts.Length);

        if (!displayedQuestions.Contains(myInOutList.inOuts[currentQuestion].id))
        {
            imageURL = myInOutList.inOuts[currentQuestion].image_source;
            StartCoroutine(InOutImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_InOut(jsonData);
        }
    }

    IEnumerator InOutImageDownload(string imgURL, string jsonData)
    {
        myInOutList = JsonUtility.FromJson<InOutList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;
            
            SetOptions_InOut(jsonData);
        }
    }

    private void SetOptions_InOut(string jsonData)
    {
        myInOutList = JsonUtility.FromJson<InOutList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myInOutList.inOuts[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myInOutList.inOuts[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myInOutList.inOuts[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myInOutList.inOuts[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myInOutList.inOuts[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myInOutList.inOuts[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myInOutList.inOuts[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myInOutList.inOuts[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myInOutList.inOuts[currentQuestion].id);

        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);   
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myInOutList = null;
    }


    //Jumbles
    [System.Serializable]
    public class JumblesList
    {
        public JSONData[] jumbles;
    }

    public JumblesList myJumblesList = new JumblesList();

    IEnumerator DownloadJumbleJSON()
    {
        jsonURL = prefixPath + "/Jumbles.json";
        Debug.Log("Jumbles Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Jumbles(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Jumbles JSON Error");
        }

    }

    private void Question_Jumbles(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myJumblesList = JsonUtility.FromJson<JumblesList>(jsonData);

        currentQuestion = Random.Range(0, myJumblesList.jumbles.Length);

        if (!displayedQuestions.Contains(myJumblesList.jumbles[currentQuestion].id))
        {
            imageURL = myJumblesList.jumbles[currentQuestion].image_source;
            StartCoroutine(JumblesImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Jumbles(jsonData);
        }
        jsonData = null;
    }

    IEnumerator JumblesImageDownload(string imgURL, string jsonData)
    {
        myJumblesList = JsonUtility.FromJson<JumblesList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Jumbles(jsonData);
        }
    }

    private void SetOptions_Jumbles(string jsonData)
    {
        myJumblesList = JsonUtility.FromJson<JumblesList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myJumblesList.jumbles[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myJumblesList.jumbles[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myJumblesList.jumbles[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myJumblesList.jumbles[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myJumblesList.jumbles[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myJumblesList.jumbles[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myJumblesList.jumbles[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myJumblesList.jumbles[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myJumblesList.jumbles[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myJumblesList = null;
    }


    //LinksMash
    [System.Serializable]
    public class LinksMashList
    {
        public JSONData[] linksMish;
    }

    public LinksMashList myLinkMishList = new LinksMashList();

    IEnumerator DownloadLinksMashJSON()
    {
        jsonURL = prefixPath + "/LinksMash.json";
        Debug.Log("LinksMash json  : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_LinksMash(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download LinksMash JSON Error");
        }

    }

    private void Question_LinksMash(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myLinkMishList = JsonUtility.FromJson<LinksMashList>(jsonData);

        currentQuestion = Random.Range(0, myLinkMishList.linksMish.Length);

        if (!displayedQuestions.Contains(myLinkMishList.linksMish[currentQuestion].id))
        {
            imageURL = myLinkMishList.linksMish[currentQuestion].image_source;
            StartCoroutine(LinksMashImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_LinksMash(jsonData);
        }
        jsonData = null;
    }

    IEnumerator LinksMashImageDownload(string imgURL, string jsonData)
    {
        myLinkMishList = JsonUtility.FromJson<LinksMashList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_LinksMash(jsonData);
        }
    }

    private void SetOptions_LinksMash(string jsonData)
    {
        myLinkMishList = JsonUtility.FromJson<LinksMashList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myLinkMishList.linksMish[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myLinkMishList.linksMish[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myLinkMishList.linksMish[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myLinkMishList.linksMish[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myLinkMishList.linksMish[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myLinkMishList.linksMish[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myLinkMishList.linksMish[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myLinkMishList.linksMish[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myLinkMishList.linksMish[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myLinkMishList = null;
    }


    //Litrature
    [System.Serializable]
    public class LitratureList
    {
        public JSONData[] literature;
    }

    public LitratureList myLitratureList = new LitratureList();

    IEnumerator DownloadLitratureMashJSON()
    {
        jsonURL = prefixPath + "/Literature.json";
        Debug.Log("Literature Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Litrature(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Literature JSON Error");
        }
        www = null;
    }

    private void Question_Litrature(string jsonData)
    {
        Debug.Log("Litrature download successful");
        myLitratureList = JsonUtility.FromJson<LitratureList>(jsonData);

        currentQuestion = Random.Range(0, myLitratureList.literature.Length);

        if (!displayedQuestions.Contains(myLitratureList.literature[currentQuestion].id))
        {
            imageURL = myLitratureList.literature[currentQuestion].image_source;
            StartCoroutine(LitratureImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Litrature(jsonData);
        }
        jsonData = null;
    }

    IEnumerator LitratureImageDownload(string imgURL, string jsonData)
    {
        myLitratureList = JsonUtility.FromJson<LitratureList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Litrature(jsonData);
        }
        www = null;
        jsonData = null;
    }

    private void SetOptions_Litrature(string jsonData)
    {
        myLitratureList = JsonUtility.FromJson<LitratureList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myLitratureList.literature[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myLitratureList.literature[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myLitratureList.literature[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myLitratureList.literature[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myLitratureList.literature[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myLitratureList.literature[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myLitratureList.literature[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myLitratureList.literature[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myLitratureList.literature[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        jsonData = null;
        myLitratureList = null;
    }


    //Logos
    [System.Serializable]
    public class LogosList
    {
        public JSONData[] logos;
    }

    public LogosList myLogosList = new LogosList();

    IEnumerator DownloadLogosJSON()
    {
        jsonURL = prefixPath + "/Logos.json";
        Debug.Log("Logos Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Logos(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Logos JSON Error");
        }

    }

    private void Question_Logos(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myLogosList = JsonUtility.FromJson<LogosList>(jsonData);

        currentQuestion = Random.Range(0, myLogosList.logos.Length);

        if (!displayedQuestions.Contains(myLogosList.logos[currentQuestion].id))
        {
            imageURL = myLogosList.logos[currentQuestion].image_source;
            StartCoroutine(LogosImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Logos(jsonData);
        }
        jsonData = null;
    }

    IEnumerator LogosImageDownload(string imgURL, string jsonData)
    {
        myLogosList = JsonUtility.FromJson<LogosList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Logos(jsonData);
        }
    }

    private void SetOptions_Logos(string jsonData)
    {
        myLogosList = JsonUtility.FromJson<LogosList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myLogosList.logos[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myLogosList.logos[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myLogosList.logos[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myLogosList.logos[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myLogosList.logos[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myLogosList.logos[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myLogosList.logos[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myLogosList.logos[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myLogosList.logos[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myLogosList = null;
    }


    //Music
    [System.Serializable]
    public class MusicList
    {
        public JSONData[] musics;
    }

    public MusicList myMusicList = new MusicList();

    IEnumerator DownloadMusicJSON()
    {
        jsonURL = prefixPath + "/Music.json";
        Debug.Log("Music Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Music(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Music JSON Error");
        }

    }

    private void Question_Music(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myMusicList = JsonUtility.FromJson<MusicList>(jsonData);

        currentQuestion = Random.Range(0, myMusicList.musics.Length);

        if (!displayedQuestions.Contains(myMusicList.musics[currentQuestion].id))
        {
            imageURL = myMusicList.musics[currentQuestion].image_source;
            StartCoroutine(MusicImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Music(jsonData);
        }
        jsonData = null;
    }

    IEnumerator MusicImageDownload(string imgURL, string jsonData)
    {
        myMusicList = JsonUtility.FromJson<MusicList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Music(jsonData);
        }
    }

    private void SetOptions_Music(string jsonData)
    {
        myMusicList = JsonUtility.FromJson<MusicList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myMusicList.musics[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myMusicList.musics[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myMusicList.musics[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myMusicList.musics[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myMusicList.musics[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myMusicList.musics[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myMusicList.musics[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myMusicList.musics[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myMusicList.musics[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myMusicList = null;
    }


    //Mystery
    [System.Serializable]
    public class MysteryList
    {
        public JSONData[] mystery;
    }

    public MysteryList myMysteryList = new MysteryList();

    IEnumerator DownloadMysteryJSON()
    {
        jsonURL = prefixPath + "/Mystery.json";
        Debug.Log("Mystery Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Mystery(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Mystery JSON Error");
        }

    }

    private void Question_Mystery(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myMysteryList = JsonUtility.FromJson<MysteryList>(jsonData);

        currentQuestion = Random.Range(0, myMysteryList.mystery.Length);

        if (!displayedQuestions.Contains(myMysteryList.mystery[currentQuestion].id))
        {
            imageURL = myMysteryList.mystery[currentQuestion].image_source;
            StartCoroutine(MysteryImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Mystery(jsonData);
        }
        jsonData = null;
    }

    IEnumerator MysteryImageDownload(string imgURL, string jsonData)
    {
        myMysteryList = JsonUtility.FromJson<MysteryList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Mystery(jsonData);
        }
    }

    private void SetOptions_Mystery(string jsonData)
    {
        myMysteryList = JsonUtility.FromJson<MysteryList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myMysteryList.mystery[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myMysteryList.mystery[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myMysteryList.mystery[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myMysteryList.mystery[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myMysteryList.mystery[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myMysteryList.mystery[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myMysteryList.mystery[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myMysteryList.mystery[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myMysteryList.mystery[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myMysteryList = null;
    }


    //MysterySounds
    [System.Serializable]
    public class GeneralKnowledgeList
    {
        public JSONData[] GeneralKnowledge;
    }

    public GeneralKnowledgeList myGeneralKnowledgeList = new GeneralKnowledgeList();

    IEnumerator DownloadGeneralKnowledgeJSON()
    {
        jsonURL = prefixPath + "/GeneralKnowledge.json";
        Debug.Log("GeneralKnowledge Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_GeneralKnowledge(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download GeneralKnowledge JSON Error");
        }

    }

    private void Question_GeneralKnowledge(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myGeneralKnowledgeList = JsonUtility.FromJson<GeneralKnowledgeList>(jsonData);

        currentQuestion = Random.Range(0, myGeneralKnowledgeList.GeneralKnowledge.Length);

        if (!displayedQuestions.Contains(myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].id))
        {
            imageURL = myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].image_source;
            StartCoroutine(GeneralKnowledgeImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_GeneralKnowledge(jsonData);
        }
        jsonData = null;
    }

    IEnumerator GeneralKnowledgeImageDownload(string imgURL, string jsonData)
    {
        myGeneralKnowledgeList = JsonUtility.FromJson<GeneralKnowledgeList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_GeneralKnowledge(jsonData);
        }
    }

    private void SetOptions_GeneralKnowledge(string jsonData)
    {
        myGeneralKnowledgeList = JsonUtility.FromJson<GeneralKnowledgeList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myGeneralKnowledgeList.GeneralKnowledge[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myGeneralKnowledgeList = null;
    }


    //NaturalWorld
    [System.Serializable]
    public class NaturalWorldList
    {
        public JSONData[] naturalWorlds;
    }

    public NaturalWorldList myNaturalWorldList = new NaturalWorldList();

    IEnumerator DownloadNaturalWorldJSON()
    {
        jsonURL = prefixPath + "/NaturalWorld.json";
        Debug.Log("Natural World Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_NaturalWorld(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download NaturalWorld JSON Error");
        }

    }

    private void Question_NaturalWorld(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myNaturalWorldList = JsonUtility.FromJson<NaturalWorldList>(jsonData);

        currentQuestion = Random.Range(0, myNaturalWorldList.naturalWorlds.Length);

        if (!displayedQuestions.Contains(myNaturalWorldList.naturalWorlds[currentQuestion].id))
        {
            imageURL = myNaturalWorldList.naturalWorlds[currentQuestion].image_source;
            StartCoroutine(NaturalWorldImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_NaturalWorld(jsonData);
        }
        jsonData = null;
    }

    IEnumerator NaturalWorldImageDownload(string imgURL, string jsonData)
    {
        myNaturalWorldList = JsonUtility.FromJson<NaturalWorldList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_NaturalWorld(jsonData);
        }
    }

    private void SetOptions_NaturalWorld(string jsonData)
    {
        myNaturalWorldList = JsonUtility.FromJson<NaturalWorldList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myNaturalWorldList.naturalWorlds[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myNaturalWorldList.naturalWorlds[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myNaturalWorldList.naturalWorlds[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myNaturalWorldList.naturalWorlds[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myNaturalWorldList.naturalWorlds[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myNaturalWorldList.naturalWorlds[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myNaturalWorldList.naturalWorlds[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myNaturalWorldList.naturalWorlds[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myNaturalWorldList.naturalWorlds[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myNaturalWorldList = null;
    }


    //Numbers
    [System.Serializable]
    public class NumbersList
    {
        public JSONData[] numbers;
    }

    public NumbersList myNumbersList = new NumbersList();

    IEnumerator DownloadNumbersJSON()
    {
        jsonURL = prefixPath + "/Numbers.json";
        Debug.Log("Numbers json  : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Numbers(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Numbers JSON Error");
        }

    }

    private void Question_Numbers(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myNumbersList = JsonUtility.FromJson<NumbersList>(jsonData);

        currentQuestion = Random.Range(0, myNumbersList.numbers.Length);

        if (!displayedQuestions.Contains(myNumbersList.numbers[currentQuestion].id))
        {
            imageURL = myNumbersList.numbers[currentQuestion].image_source;
            StartCoroutine(NumbersImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Numbers(jsonData);
        }
    }

    IEnumerator NumbersImageDownload(string imgURL, string jsonData)
    {
        myNumbersList = JsonUtility.FromJson<NumbersList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;
            
            SetOptions_Numbers(jsonData);
        }
    }

    private void SetOptions_Numbers(string jsonData)
    {
        myNumbersList = JsonUtility.FromJson<NumbersList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myNumbersList.numbers[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myNumbersList.numbers[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myNumbersList.numbers[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myNumbersList.numbers[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myNumbersList.numbers[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myNumbersList.numbers[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myNumbersList.numbers[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myNumbersList.numbers[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myNumbersList.numbers[currentQuestion].id);

        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myNumbersList = null;
    }


    //Rebusters
    [System.Serializable]
    public class RebustersList
    {
        public JSONData[] rebusters;
    }

    public RebustersList myRebustersList = new RebustersList();

    IEnumerator DownloadRebustersJSON()
    {
        jsonURL = prefixPath + "/Rebusters.json";
        Debug.Log("Rebusters  json  : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Rebusters(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download History JSON Error");
        }

    }

    private void Question_Rebusters(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myRebustersList = JsonUtility.FromJson<RebustersList>(jsonData);

        currentQuestion = Random.Range(0, myRebustersList.rebusters.Length);

        if (!displayedQuestions.Contains(myRebustersList.rebusters[currentQuestion].id))
        {
            imageURL = myRebustersList.rebusters[currentQuestion].image_source;
            StartCoroutine(RebustersImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Rebusters(jsonData);
        }
        jsonData = null;
    }

    IEnumerator RebustersImageDownload(string imgURL, string jsonData)
    {
        myRebustersList = JsonUtility.FromJson<RebustersList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Rebusters(jsonData);
        }
    }

    private void SetOptions_Rebusters(string jsonData)
    {
        myRebustersList = JsonUtility.FromJson<RebustersList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myRebustersList.rebusters[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myRebustersList.rebusters[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myRebustersList.rebusters[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myRebustersList.rebusters[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myRebustersList.rebusters[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myRebustersList.rebusters[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myRebustersList.rebusters[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myRebustersList.rebusters[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myRebustersList.rebusters[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myRebustersList = null;
    }


    //ScienceAndTechnology
    [System.Serializable]
    public class ScienceAndTechnologyList
    {
        public JSONData[] scienceAndTechnologys;
    }

    public ScienceAndTechnologyList myScienceAndTechnologyList = new ScienceAndTechnologyList();

    IEnumerator DownloadScienceAndTechnologyJSON()
    {
        jsonURL = prefixPath + "/ScienceAndTechnology.json";
        Debug.Log("Science and Technology Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_ScienceAndTechnology(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download ScienceAndTechnology JSON Error");
        }

    }

    private void Question_ScienceAndTechnology(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myScienceAndTechnologyList = JsonUtility.FromJson<ScienceAndTechnologyList>(jsonData);

        currentQuestion = Random.Range(0, myScienceAndTechnologyList.scienceAndTechnologys.Length);

        if (!displayedQuestions.Contains(myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].id))
        {
            imageURL = myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].image_source;
            StartCoroutine(ScienceAndTechnologyImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_ScienceAndTechnology(jsonData);
        }
        jsonData = null;
    }

    IEnumerator ScienceAndTechnologyImageDownload(string imgURL, string jsonData)
    {
        myScienceAndTechnologyList = JsonUtility.FromJson<ScienceAndTechnologyList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_ScienceAndTechnology(jsonData);
        }
    }

    private void SetOptions_ScienceAndTechnology(string jsonData)
    {
        myScienceAndTechnologyList = JsonUtility.FromJson<ScienceAndTechnologyList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myScienceAndTechnologyList.scienceAndTechnologys[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myScienceAndTechnologyList = null;
    }


    //Sport
    [System.Serializable]
    public class SportList
    {
        public JSONData[] sports;
    }

    public SportList mySportList = new SportList();

    IEnumerator DownloadSportJSON()
    {
        jsonURL = prefixPath + "/Sport.json";
        Debug.Log("Sport Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Sport(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Sport JSON Error");
        }

    }

    private void Question_Sport(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        mySportList = JsonUtility.FromJson<SportList>(jsonData);

        currentQuestion = Random.Range(0, mySportList.sports.Length);

        if (!displayedQuestions.Contains(mySportList.sports[currentQuestion].id))
        {
            imageURL = mySportList.sports[currentQuestion].image_source;
            StartCoroutine(SportImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Sport(jsonData);
        }
        jsonData = null;
    }

    IEnumerator SportImageDownload(string imgURL, string jsonData)
    {
        mySportList = JsonUtility.FromJson<SportList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Sport(jsonData);
        }
    }

    private void SetOptions_Sport(string jsonData)
    {
        mySportList = JsonUtility.FromJson<SportList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = mySportList.sports[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = mySportList.sports[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = mySportList.sports[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = mySportList.sports[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = mySportList.sports[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[mySportList.sports[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[mySportList.sports[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = mySportList.sports[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(mySportList.sports[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        mySportList = null;
    }


    //TheArts
    [System.Serializable]
    public class TheArtsList
    {
        public JSONData[] theArts;
    }

    public TheArtsList myTheArtsList = new TheArtsList();

    IEnumerator DownloadTheArtsJSON()
    {
        jsonURL = prefixPath + "/TheArts.json";
        Debug.Log("The Arts Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_TheArts(www.downloadHandler.text);

        }
        else
        {
            Debug.Log("Download TheArts JSON Error");
        }

    }

    private void Question_TheArts(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myTheArtsList = JsonUtility.FromJson<TheArtsList>(jsonData);

        currentQuestion = Random.Range(0, myTheArtsList.theArts.Length);

        if (!displayedQuestions.Contains(myTheArtsList.theArts[currentQuestion].id))
        {
            imageURL = myTheArtsList.theArts[currentQuestion].image_source;
            StartCoroutine(TheArtsImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_TheArts(jsonData);
        }
        jsonData = null;
    }

    IEnumerator TheArtsImageDownload(string imgURL, string jsonData)
    {
        myTheArtsList = JsonUtility.FromJson<TheArtsList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_TheArts(jsonData);
        }
    }

    private void SetOptions_TheArts(string jsonData)
    {
        myTheArtsList = JsonUtility.FromJson<TheArtsList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myTheArtsList.theArts[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myTheArtsList.theArts[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myTheArtsList.theArts[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myTheArtsList.theArts[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myTheArtsList.theArts[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myTheArtsList.theArts[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myTheArtsList.theArts[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myTheArtsList.theArts[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myTheArtsList.theArts[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myTheArtsList = null;
    }


    //WhichWitch
    [System.Serializable]
    public class WhichWitchList
    {
        public JSONData[] whichWitchs;
    }

    public WhichWitchList myWhichWitchList = new WhichWitchList();

    IEnumerator DownloadWhichWitchJSON()
    {
        jsonURL = prefixPath + "/WhichWitch.json";
        Debug.Log("WhichWitch Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_WhichWitch(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download WhichWitch JSON Error");
        }

    }

    private void Question_WhichWitch(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myWhichWitchList = JsonUtility.FromJson<WhichWitchList>(jsonData);

        currentQuestion = Random.Range(0, myWhichWitchList.whichWitchs.Length);

        if (!displayedQuestions.Contains(myWhichWitchList.whichWitchs[currentQuestion].id))
        {
            imageURL = myWhichWitchList.whichWitchs[currentQuestion].image_source;
            StartCoroutine(WhichWitchImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_WhichWitch(jsonData);
        }
        jsonData = null;
    }

    IEnumerator WhichWitchImageDownload(string imgURL, string jsonData)
    {
        myWhichWitchList = JsonUtility.FromJson<WhichWitchList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_WhichWitch(jsonData);
        }
    }

    private void SetOptions_WhichWitch(string jsonData)
    {
        myWhichWitchList = JsonUtility.FromJson<WhichWitchList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myWhichWitchList.whichWitchs[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myWhichWitchList.whichWitchs[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myWhichWitchList.whichWitchs[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myWhichWitchList.whichWitchs[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myWhichWitchList.whichWitchs[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myWhichWitchList.whichWitchs[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myWhichWitchList.whichWitchs[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myWhichWitchList.whichWitchs[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myWhichWitchList.whichWitchs[currentQuestion].id);


        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myWhichWitchList = null;
    }

    //WWW
    [System.Serializable]
    public class WWWList
    {
        public JSONData[] wWWs;
    }

    public WWWList myWWWList = new WWWList();

    IEnumerator DownloadWWWJSON()
    {
        jsonURL = prefixPath + "/WWW.json";
        Debug.Log("WWW json  : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_WWW(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download WWW JSON Error");
        }

    }

    private void Question_WWW(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myWWWList = JsonUtility.FromJson<WWWList>(jsonData);

        currentQuestion = Random.Range(0, myWWWList.wWWs.Length);

        if (!displayedQuestions.Contains(myWWWList.wWWs[currentQuestion].id))
        {
            imageURL = myWWWList.wWWs[currentQuestion].image_source;
            StartCoroutine(WWWImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_WWW(jsonData);
        }
    }

    IEnumerator WWWImageDownload(string imgURL, string jsonData)
    {
        myWWWList = JsonUtility.FromJson<WWWList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;
            
            SetOptions_WWW(jsonData);
        }
    }

    private void SetOptions_WWW(string jsonData)
    {
        myWWWList = JsonUtility.FromJson<WWWList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myWWWList.wWWs[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myWWWList.wWWs[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myWWWList.wWWs[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myWWWList.wWWs[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myWWWList.wWWs[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myWWWList.wWWs[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myWWWList.wWWs[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myWWWList.wWWs[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myWWWList.wWWs[currentQuestion].id);

        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myWWWList = null;
    }

    //Xplain
    [System.Serializable]
    public class XplainList
    {
        public JSONData[] xplains;
    }

    public XplainList myXplainList = new XplainList();

    IEnumerator DownloadXplainJSON()
    {
        jsonURL = prefixPath + "/Xplain.json";
        Debug.Log("Xplain Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_Xplain(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download Xplain JSON Error");
        }

    }

    private void Question_Xplain(string jsonData)
    {
        //CurrentAffairsAndPoliticsList json = JsonUtility.FromJson<CurrentAffairsAndPoliticsList>(jsonData);
        myXplainList = JsonUtility.FromJson<XplainList>(jsonData);

        currentQuestion = Random.Range(0, myXplainList.xplains.Length);

        if (!displayedQuestions.Contains(myXplainList.xplains[currentQuestion].id))
        {
            imageURL = myXplainList.xplains[currentQuestion].image_source;
            StartCoroutine(XplainImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_Xplain(jsonData);
        }
    }

    IEnumerator XplainImageDownload(string imgURL, string jsonData)
    {
        myXplainList = JsonUtility.FromJson<XplainList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.QuestionScreenImage.sprite = sprite;

            SetOptions_Xplain(jsonData);
        }
    }

    private void SetOptions_Xplain(string jsonData)
    {
        myXplainList = JsonUtility.FromJson<XplainList>(jsonData);
        UIManager.instance.QuestionScreenQuestionText.text = myXplainList.xplains[currentQuestion].question;
        UIManager.instance.QuestionScreenOptions[0].transform.GetChild(0).GetComponent<Text>().text = myXplainList.xplains[currentQuestion].answer_option1;
        UIManager.instance.QuestionScreenOptions[1].transform.GetChild(0).GetComponent<Text>().text = myXplainList.xplains[currentQuestion].answer_option2;
        UIManager.instance.QuestionScreenOptions[2].transform.GetChild(0).GetComponent<Text>().text = myXplainList.xplains[currentQuestion].answer_option3;
        UIManager.instance.QuestionScreenOptions[3].transform.GetChild(0).GetComponent<Text>().text = myXplainList.xplains[currentQuestion].answer_option4;

        TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenQuestionText.text);
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            if (UIManager.instance.IsMCQ)
            {
                TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.QuestionScreenOptions[i].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        correctAnswerOption = UIManager.instance.QuestionScreenOptions[myXplainList.xplains[currentQuestion].correct_answer_no - 1].gameObject;
        correctAnswerOptionName = UIManager.instance.QuestionScreenOptions[myXplainList.xplains[currentQuestion].correct_answer_no - 1].transform.GetChild(0).GetComponent<Text>().text;
        CorrectAnswerIndex = myXplainList.xplains[currentQuestion].correct_answer_no;
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = true;
        displayedQuestions.Add(myXplainList.xplains[currentQuestion].id);

        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.QuestionScreen.SetActive(true);
        UIManager.instance.BridgeScreen.SetActive(false);

        myXplainList = null;
    }

    //FreeForAll
    //Xplain
    [System.Serializable]
    public class FreeForAllList
    {
        public JSONData[] freeforall;
    }

    public FreeForAllList myFreeforallList = new FreeForAllList();

    IEnumerator DownloadFreeForAllJSON()
    {
        jsonURL = prefixPath + "/FreeForAll.json";
        Debug.Log("FreeFor All Json : " + jsonURL);
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Question_FreeForAll(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Download FreeForAll JSON Error");
        }
    }

    private void Question_FreeForAll(string jsonData)
    {
        //FreeForAllList json = JsonUtility.FromJson<FreeForAllList>(jsonData);
        myFreeforallList = JsonUtility.FromJson<FreeForAllList>(jsonData);

        currentQuestion = Random.Range(0, myFreeforallList.freeforall.Length);

        if (!displayedQuestions.Contains(myFreeforallList.freeforall[currentQuestion].id))
        {
            imageURL = myFreeforallList.freeforall[currentQuestion].image_source;
            StartCoroutine(FreeForAllImageDownload(imageURL, jsonData));
        }
        else
        {
            Question_FreeForAll(jsonData);
        }
        jsonData = null;
    }

    IEnumerator FreeForAllImageDownload(string imgURL, string jsonData)
    {
        myFreeforallList = JsonUtility.FromJson<FreeForAllList>(jsonData);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgURL);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //After Code
            UIManager.instance.freeForAllScreenPictureImage.sprite = sprite;
            UIManager.instance.freeForAllScreenQuestionText.text = myFreeforallList.freeforall[currentQuestion].question;
            TextTOSpeachManager.instance.ConvertTextToSpeech(UIManager.instance.freeForAllScreenQuestionText.text);
            displayedQuestions.Add(myFreeforallList.freeforall[currentQuestion].id);
            
            SetCorrectAnswer_FreeForAll(jsonData);   
        }
    }

    private void SetCorrectAnswer_FreeForAll(string jsonData)
    {
        myFreeforallList = JsonUtility.FromJson<FreeForAllList>(jsonData);
        freeForAllCorrectAnswer = myFreeforallList.freeforall[currentQuestion].answer;

        UIManager.instance.RollDiceScreen.SetActive(false);
        UIManager.instance.LoaderScreen.SetActive(false);
        UIManager.instance.FreeForAllQuestionScreen.SetActive(true);
        SpecialButtonsLogic.instance.freeForAllStartCount = true;

        myFreeforallList = null;
    }
    
    public void CheckAnswerisCorrectOrWrong(Button button)
    {
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = false;
        TextTOSpeachManager.instance.StopSpeech();
        AlreadyAnswered = true;
        defOptionButtonColor = UIManager.instance.QuestionScreenOptions[0].GetComponent<Image>().color;

        if (correctAnswerOptionName == button.gameObject.transform.GetChild(0).GetComponent<Text>().text)
        {
            ColorUtility.TryParseHtmlString("#2F9873", out rightAnswerOptionButtonColor);
            button.gameObject.GetComponent<Image>().color = rightAnswerOptionButtonColor;
            PlayerTurnData.instance.isCorrect = true;
            StartCoroutine(WaitTime(button));
        }
        else
        {
            ColorUtility.TryParseHtmlString("#bc3850", out wrongAnswerOptionButtonColor);
            button.gameObject.GetComponent<Image>().color = wrongAnswerOptionButtonColor;
            PlayerTurnData.instance.isCorrect = false;
            StartCoroutine(WaitTime(button));
        }
        Debug.Log("CheckAnswerisCorrectOrWrong_ENDDDDD:::::::: " + button.gameObject.name);

        if (PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].bridgeReached)
        {
            if ((PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].BridgeCounter >= 0 && PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].BridgeCounter <= 6 /*4*/) && PlayerTurnData.instance.isCorrect)
            {
                Debug.Log("MCQ - Increment Bridge ++++++++++++++++++");
                PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].BridgeCounter++;
            }
        }
    }

    public void CheckAnswerisCorrectOrWrong_ShowAnswer(Button button)
    {
        QuestionAnswerGenerator.instance.questionScreenTimerstartCount = false;
        TextTOSpeachManager.instance.StopSpeech();
        AlreadyAnswered = true;
        Debug.Log("CheckAnswerisCorrectOrWrong_ShowAnswer__START:::::::  " + button.gameObject.name);

        if (PlayerTurnData.instance.isCorrect)
        {
            UIManager.instance.QuestionScreenOptionScreen.SetActive(false);
            UIManager.instance.QuestionScreenCorrectAnswerResultScreen.SetActive(true);


            if (!PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].PlayerWonColorTilesList.Contains(QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color))
            {
                print("color " + QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color);
                UIManager.instance.QuestionScreenCorrectAnswerYouWonColorTileResultHolder.SetActive(true);
                UIManager.instance.QuestionScreenCorrectAnswerYouWonColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
            }
            else
            {
                if(PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].PlayerWonColorTilesList.Count < 6 /*3*/)
                {
                    Debug.Log("Already have color tile result:::::::::::");
                    UIManager.instance.QuestionScreenCorrectAnswerAlreadyHaveColorTileResultHolder.SetActive(true);
                    UIManager.instance.QuestionScreenCorrectAnswerAlreadyHaveColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
                    //UIManager.instance.ShowAnswer_AlreadyHaveColorTileResultHolder.SetActive(true);
                    //UIManager.instance.ShowAnswer_AlreadyHaveColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
                }
            }

            PlayerData.instance.SetPlayerImage(PlayerTurnData.instance.playerTurn, QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color);
        }
        else
        {
            UIManager.instance.QuestionScreenOptionScreen.SetActive(false);
            UIManager.instance.QuestionScreenWrongAnswerResultScreen.SetActive(true);

            UIManager.instance.QuestionScreenWrongAnswerResultScreenResultOptionText.text = correctAnswerOption.transform.GetChild(0).GetComponent<Text>().text;
            UIManager.instance.QuestionScreenWrongAnswerResultScreenResultOptionIconText.text = correctAnswerOption.transform.GetChild(1).GetChild(0).GetComponent<Text>().text;
        }
        Debug.Log("CheckAnswerisCorrectOrWrong_ShowAnswer__END:::::::  " + button.gameObject.name);
    }

    private IEnumerator WaitTime(Button button)
    {
        JSONManager.instance.DisableOptionButton();

        yield return new WaitForSeconds(1f);
        button.gameObject.GetComponent<Image>().color = defOptionButtonColor;

        if (correctAnswerOptionName == button.gameObject.transform.GetChild(0).GetComponent<Text>().text)
        {
            UIManager.instance.QuestionScreenOptionScreen.SetActive(false);
            UIManager.instance.QuestionScreenCorrectAnswerResultScreen.SetActive(true);

            if (!PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].PlayerWonColorTilesList.Contains(QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color))
            {
                print("color " + QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color);
                UIManager.instance.QuestionScreenCorrectAnswerYouWonColorTileResultHolder.SetActive(true);
                UIManager.instance.QuestionScreenCorrectAnswerYouWonColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
            }
            else
            {
                if(PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].PlayerWonColorTilesList.Count < 6 /*3*/)
                {
                    Debug.Log("AlreadyHaveColorTile::::::::");
                    UIManager.instance.QuestionScreenCorrectAnswerAlreadyHaveColorTileResultHolder.SetActive(true);
                    UIManager.instance.QuestionScreenCorrectAnswerAlreadyHaveColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
                }
            }

            PlayerData.instance.SetPlayerImage(PlayerTurnData.instance.playerTurn, QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color);
        }
        else
        {
            UIManager.instance.QuestionScreenOptionScreen.SetActive(false);
            UIManager.instance.QuestionScreenWrongAnswerResultScreen.SetActive(true);

            UIManager.instance.QuestionScreenWrongAnswerResultScreenResultOptionText.text = correctAnswerOption.transform.GetChild(0).GetComponent<Text>().text;
            UIManager.instance.QuestionScreenWrongAnswerResultScreenResultOptionIconText.text = correctAnswerOption.transform.GetChild(1).GetChild(0).GetComponent<Text>().text;
        }
    }
    private IEnumerator WaitTime_ShowAnswer(/*Button button*/)
    {
        //JSONManager.instance.DisableOptionButton();

        yield return new WaitForSeconds(0f);
        //button.gameObject.GetComponent<Image>().color = defOptionButtonColor;

        //if (PlayerTurnData.instance.isCorrect)
        //{
        //    UIManager.instance.QuestionScreenOptionScreen.SetActive(false);
        //    UIManager.instance.QuestionScreenCorrectAnswerResultScreen.SetActive(true);


        //    if (!PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].PlayerWonColorTilesList.Contains(QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color))
        //    {
        //        print("color " + QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color);
        //        UIManager.instance.QuestionScreenCorrectAnswerYouWonColorTileResultHolder.SetActive(true);
        //        UIManager.instance.QuestionScreenCorrectAnswerYouWonColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
        //    }
        //    else
        //    {
        //        UIManager.instance.QuestionScreenCorrectAnswerAlreadyHaveColorTileResultHolder.SetActive(true);
        //        UIManager.instance.QuestionScreenCorrectAnswerAlreadyHaveColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
        //    }

        //    PlayerData.instance.SetPlayerImage(PlayerTurnData.instance.playerTurn, QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color);
        //}
        //else
        //{
        //    UIManager.instance.QuestionScreenOptionScreen.SetActive(false);
        //    UIManager.instance.QuestionScreenWrongAnswerResultScreen.SetActive(true);

        //    UIManager.instance.QuestionScreenWrongAnswerResultScreenResultOptionText.text = correctAnswerOption.transform.GetChild(0).GetComponent<Text>().text;
        //    UIManager.instance.QuestionScreenWrongAnswerResultScreenResultOptionIconText.text = correctAnswerOption.transform.GetChild(1).GetChild(0).GetComponent<Text>().text;
        //}
    }

    public void DisableOptionButton()
    {
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            UIManager.instance.QuestionScreenOptions[i].GetComponent<Button>().enabled = false;
        }
    }

    public void EnableOptionButton()
    {
        for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
        {
            UIManager.instance.QuestionScreenOptions[i].GetComponent<Button>().enabled = true;
        }
    }

    public void OnClickHint()
    {
        print("on click hint");
        if (PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].hintCounter > 0 && !AlreadyAnswered && QuestionAnswerGenerator.instance.questionScreenTimerstartCount)
        {
            AlreadyAnswered = true;
            UIManager.instance.QuestionScreenOptionScreen.SetActive(true);
            UIManager.instance.showAnswerBtn.SetActive(false);
            UIManager.instance.ViewAnswerScreen.SetActive(false);
            PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].hintCounter--;

            int counter = 0;
            UIManager.instance.HintText.text = PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].hintCounter.ToString();
            for (int i = 0; i < UIManager.instance.QuestionScreenOptions.Length; i++)
            {
                print("on  Question Screen Options " + i);
                if (counter < 2)
                    if (UIManager.instance.QuestionScreenOptions[i] != correctAnswerOption)
                    {
                        counter++;
                        UIManager.instance.QuestionScreenOptions[i].SetActive(false);
                    }
            }

        }
        
    }
    
    public void ViewAnswer(GameObject button)
    {
        TextTOSpeachManager.instance.StopSpeech();
        if (!PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].PlayerWonColorTilesList.Contains(QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color) || PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].bridgeReached)
        {
            AlreadyAnswered = true;
            QuestionAnswerGenerator.instance.questionScreenTimerstartCount = false;
            UIManager.instance.ViewAnswerScreen.SetActive(true);
            UIManager.instance.AnswerText.text = correctAnswerOptionName;
            string OptionLetter = "";
            Debug.Log("Index answer: " + CorrectAnswerIndex);
            switch (CorrectAnswerIndex)
            {
                case 1: OptionLetter = "A"; break;
                case 2: OptionLetter = "B"; break;
                case 3: OptionLetter = "C"; break;
                case 4: OptionLetter = "D"; break;
                default: OptionLetter = "A"; break;
            }

            UIManager.instance.OptionText.text = OptionLetter.ToString();

            button.gameObject.SetActive(false);
            UIManager.instance.showAnswerBtn.SetActive(true);
        }
        else
        {
            if (PlayerData.instance.PlayersDataList[PlayerTurnData.instance.playerTurn].PlayerWonColorTilesList.Count < 6 /*3*/)
            {
                Debug.Log("Already have color tile result:::::::::::");
                PlayerTurnData.instance.isCorrect = true;
                QuestionAnswerGenerator.instance.questionScreenTimerstartCount = false;

                UIManager.instance.ShowAnswer_AlreadyHaveColorTileResultHolder.SetActive(true);
                UIManager.instance.ShowAnswer_AlreadyHaveColorTileImage.color = QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color;
                
                PlayerData.instance.SetPlayerImage(PlayerTurnData.instance.playerTurn, QuestionAnswerGenerator.instance.categoryButton.GetComponent<Image>().color);
            }
        }
    }


}
