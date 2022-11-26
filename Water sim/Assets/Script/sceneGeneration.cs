using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sceneGeneration : MonoBehaviour
{
    public Toggle useDefaultToggle;
    public Toggle randomizeToggle;
    public TMP_InputField randomSeedInputField;
    public GameObject MainGUI;
    public GameObject propTest;
    public GameObject prop1;
    public GameObject prop1b;
    public GameObject MainUR;
    public GameObject prop2;
    public GameObject prop3a;
    public GameObject prop3b;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void sceneSetUpSelect()
    {
        if (useDefaultToggle.isOn)
        {
            Debug.Log("Using Default set up!");
            randomSceneSetUp(1);
            return;
        }
        else if (randomizeToggle.isOn)
        {
            int seed;
            if (int.TryParse(randomSeedInputField.text, out seed))
            {
                Debug.Log("Using Random set up with seed " + seed);
                randomSceneSetUp(seed);
            }
            else
            {
                Debug.Log("Using Random set up!");
                randomSceneSetUp(Guid.NewGuid().GetHashCode());
            }
            return;
        }
        else
        {
            // TODO: add custom scene building 
        }
    }

    public void randomSceneSetUp(int randomSeed)
    {
        System.Random rng = new System.Random(randomSeed);

        propTest.GetComponent<Transform>().position = new Vector3(
            randomNumber(-6f, 42f, rng),
            1.264f,
            randomNumber(-5f, 18.61f, rng)
        );

        Vector3 prop1Center = new Vector3(17.5f,0.775f,randomNumber(-3f,18f, rng));
        prop1.GetComponent<Transform>().position = prop1Center;
        prop1b.GetComponent<Transform>().position = prop1Center + new Vector3(-1f,-0.37f,0f);
        prop2.GetComponent<Transform>().position = new Vector3(randomNumber(0f,38f, rng),0.05f,18f);
        Vector3 prop3Center = new Vector3(randomNumber(20f,40f, rng),0.405f,randomNumber(-2f,10f, rng));
        prop3a.GetComponent<Transform>().position = prop3Center;
        prop3b.GetComponent<Transform>().position = (new Vector3(randomNumber(-1f,1f, rng),0,randomNumber(-1f,1f, rng)) + prop3Center);
    }

    // generate a float number between given range
    public float randomNumber(float min, float max, System.Random rand)
    {
        return (min + (max - min) * (float)rand.NextDouble());
    }

    private void costumSceneSetUp()
    {
    }
}
