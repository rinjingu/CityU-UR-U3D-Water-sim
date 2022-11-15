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
    public GameObject MainUR;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void sceneSetUpSelect()
    {
        if (useDefaultToggle.isOn)
        {
            Debug.Log("Using Default set up!");
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
