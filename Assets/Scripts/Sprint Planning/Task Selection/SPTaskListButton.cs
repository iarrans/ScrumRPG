using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPTaskListButton : MonoBehaviour
{

    [SerializeField]
    private Text taskname;

    [SerializeField]
    private Text exp;

    [SerializeField]
    private new Text name;

    [SerializeField]
    private Image emoji;
    [SerializeField]
    Sprite happyEmoji;
    [SerializeField]
    Sprite gladEmoji;
    [SerializeField]
    Sprite worry1Emoji;
    [SerializeField]
    Sprite worry2Emoji;
    [SerializeField]
    Sprite scaredEmoji;
    [SerializeField]
    Sprite cryEmoji;

    [SerializeField]
    private Transform roleTags;

    [SerializeField]
    private GameObject developerTag;
    [SerializeField]
    private GameObject qualityAssuranceTag;
    [SerializeField]
    private GameObject testerTag;
    [SerializeField]
    private GameObject uiDesignerTag;
    [SerializeField]
    private GameObject deploymentTag;

    [SerializeField]
    private SPButtonListControl buttonControl;

    private void Update()
    {
        SetEmoji();
    }

    public void SetName(string nombre)
    {
        taskname.text = nombre;
    }

    public void SetExp(int experiencia)
    {
        exp.text = (experiencia / 100f).ToString();

    }

    public void SetTaskName(string nombre)
    {
        name.text = nombre;
    }

    void SetEmoji()
    {
        float exp = float.Parse(this.exp.text);
        if (exp < 5)
        {
            emoji.sprite = happyEmoji;
        }
        else if (exp < 10)
        {
            emoji.sprite = gladEmoji;
        }
        else if (exp < 15)
        {
            emoji.sprite = worry1Emoji;
        }
        else if (exp < 30)
        {
            emoji.sprite = worry2Emoji;
        }
        else
        {
            emoji.sprite = scaredEmoji;
        }
    }

    public void SetRoleTags(int id)
    {
        //Instantiate an empty tag list
        List<RoleTags.Tag> roleTags = new List<RoleTags.Tag>();

        //Get the attack attribute effectiveness of our task
        List<float> atkAttributes = new List<float>(ORK.Combatants.Get(id).atkAttrStart[0].startValue);

        //Iterate the different values. 
        //We use i as an index to find what role it refers to: 0 = developer; 1 = quality assurance; 2 = tester; 3 = UI designer; 4 = deployment
        int i = 0;
        foreach (float value in atkAttributes)
        {
            //Value is a float that determines the effectiveness percentage. 100 means that damage received will be x1, 0 means x0, 50 means x0.5
            //If value > 0 it means that the role we're evaluating will damage it
            if (value > 0)
            {
                //Add a tag depending on what role damages it
                switch (i)
                {
                    case 0:
                        roleTags.Add(RoleTags.Tag.Developer);
                        break;
                    case 1:
                        roleTags.Add(RoleTags.Tag.QualityAssurance);
                        break;
                    case 2:
                        roleTags.Add(RoleTags.Tag.Tester);
                        break;
                    case 3:
                        roleTags.Add(RoleTags.Tag.UIDesigner);
                        break;
                    case 4:
                        roleTags.Add(RoleTags.Tag.Deployment);
                        break;
                }
            }
            //Next role
            i += 1;
        }

        //Reset the same int since we will use it for something else now
        i = 0;
        //Iterate the tags that we've recently added
        foreach (RoleTags.Tag tag in roleTags)
        {
            //Generate the tag that we're at
            GenerateTag(tag, i);
            i += 1;
        }
    }

    void GenerateTag(RoleTags.Tag tag, int index)
    {
        //Instantiate a variable that refers to the gameobject that we'll instantiate
        GameObject tagToInstantiate = null;
        //Depending on the tag we'll instantiate one out of our 5 tags
        switch (tag)
        {
            case RoleTags.Tag.Developer:
                tagToInstantiate = developerTag;
                break;
            case RoleTags.Tag.QualityAssurance:
                tagToInstantiate = qualityAssuranceTag;
                break;
            case RoleTags.Tag.Tester:
                tagToInstantiate = testerTag;
                break;
            case RoleTags.Tag.UIDesigner:
                tagToInstantiate = uiDesignerTag;
                break;
            case RoleTags.Tag.Deployment:
                tagToInstantiate = deploymentTag;
                break;
        }
        //Instantiate the tag
        //index determines what place it'll be placed in, as there is 5 places where we can place the tag:
        //0 = bottom right corner; 1 = at the bottom, in the middle in the X axis; 2 = bottom left corner; 
        //3 = just above 0 and 1 in the Y axis, placed between them in the X axis; 4 = just above 1 and 2 in the Y axis, placed between them in the X axis
        Instantiate(tagToInstantiate, roleTags.GetChild(index));
    }

    //añadir aquí llamada a métodos de quitar o añadir tareas a SB

    public void AddTask(GameObject button)
    {
        buttonControl.AddTaskToSprint(taskname.text, exp.text, button);
    }

    public void RemoveTask(GameObject button)
    {
        buttonControl.RemoveTaskFromSprint(taskname.text, exp.text, button);
    }

    //Funciones de dividir y unir tareas

    public void DivideTaskBy2(string listname)
    {
        string exp = this.exp.text;
        float taskExpF = float.Parse(exp) * 100;
        int taskExp = Mathf.RoundToInt(taskExpF);
        buttonControl.DivideTaskBy2(int.Parse(taskname.text), taskExp, listname);
    }

    public void JoinTasks(string listname)
    {
        buttonControl.JoinTasks(int.Parse(taskname.text), listname);
    }

}
