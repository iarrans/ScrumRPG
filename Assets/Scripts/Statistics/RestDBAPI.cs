using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using UnityEngine.Networking;
using System.Text;

public class RestDBAPI : MonoBehaviour
{

    public string uvus;
    public int numberOfSprints;
    public float poEvaluationRatio;
    public float averageFinalHealth;
    public float averagePH;
    public float averagePHRatio;
    public float averageTurns;
    public float averageTimeRatio;
    public string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        //Llamada a los tests. Descomentar para probar la conexión con la API de restdb.io
        //TestAPIGet();
        //StartCoroutine(TestAPIPost());

        ///Inicializamos los atributos
        this.uvus = "unknown";
        this.numberOfSprints = 0;
        this.poEvaluationRatio = 0;
        this.averageFinalHealth = 0;
        this.averagePH = 0;
        this.averagePHRatio = 0;
        this.averageTurns = 0;
        this.averageTimeRatio = 123;
        this.difficulty = "facil";

       // StartCoroutine(TestAPIPost());
    }



    //Evento que trae las estadísticas de EndSprint para enviarlas a la API
    public void SendStatistics(int sprintNumber, float poEvaluation, float finalhealth, float averageph, float averagephratio, float averageturns, float timeratio)
    {

        this.numberOfSprints = sprintNumber;
        this.poEvaluationRatio = poEvaluation;
        this.averageFinalHealth = finalhealth;
        this.averagePH = averageph;
        this.averagePHRatio = averagephratio;
        this.averageTurns = averageturns;
        this.averageTimeRatio = timeratio;

        StartCoroutine(StatsAPIPost());
    }

    IEnumerator StatsAPIPost()
    {
        WWWForm form = new WWWForm();

        form.AddField("uvus", uvus);
        form.AddField("poevaluationratio", poEvaluationRatio.ToString().Replace(",", "."));
        form.AddField("averagefinalhealth", averageFinalHealth.ToString().Replace(",", "."));
        form.AddField("averageph", averagePH.ToString().Replace(",", "."));
        form.AddField("averagephratio", averagePHRatio.ToString().Replace(",", "."));
        form.AddField("averageturns", averageTurns.ToString().Replace(",", "."));
        form.AddField("averagetimeratio", averageTimeRatio.ToString().Replace(",", "."));
        form.AddField("numberofsprints", numberOfSprints.ToString());
        form.AddField("difficulty", difficulty.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("https://scrumrpg-e916.restdb.io/rest/estadisticaspartida", form))
        {
            www.SetRequestHeader("x-apikey", "362ba6a30f8bc8bcd078ef16ea67ddb281303");
            yield return www.SendWebRequest();

            Debug.Log("Post done!");
        }
    }


    //--------------------------------------------------------------------------- TESTS

    private void TestAPIGet()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://scrumrpg-e916.restdb.io/rest/estadisticaspartida"));
        request.Headers.Add("x-apikey", "362ba6a30f8bc8bcd078ef16ea67ddb281303");
        request.Method = "GET";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Debug.Log(jsonResponse);
    }

    IEnumerator TestAPIPost()
    {
        WWWForm form = new WWWForm();
        form.AddField("uvus", "UUVUUU");
        form.AddField("poevaluationratio", poEvaluationRatio.ToString());
        form.AddField("averagefinalhealth", averageFinalHealth.ToString());
        form.AddField("averageph", averagePH.ToString());
        form.AddField("averagephratio", averagePHRatio.ToString());
        form.AddField("averageturns", averageTurns.ToString());
        form.AddField("averagetimeratio", "12.32");
        form.AddField("numberofsprints", numberOfSprints.ToString());
        form.AddField("difficulty", difficulty.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("https://scrumrpg-e916.restdb.io/rest/estadisticaspartida", form))
        {
            www.SetRequestHeader("x-apikey", "362ba6a30f8bc8bcd078ef16ea67ddb281303");
            yield return www.SendWebRequest();

            Debug.Log("Test done");
        }
    }



}
