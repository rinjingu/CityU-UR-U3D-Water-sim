using System.Collections;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SharpConfig;
using KWS;

///<summary>
///The class control the process of generating a scene
///</summary>
public class sceneGeneration : MonoBehaviour
{
    public Toggle useDefaultToggle;
    public Toggle randomizeToggle;
    public Toggle customizeToggle;
    public GameObject randomSeedLabel;
    public GameObject customIuputPanel;
    public TMP_InputField randomSeedInputField;
    public TMP_InputField profileInputField;
    public GameObject[] publicPropsList;
    private List<Prop> currentPropsList;
    public GameObject propTest;
    public GameObject prop1;
    public GameObject prop1b;
    public GameObject MainUR;
    public GameObject prop2;
    public GameObject prop3a;
    public GameObject prop3b;
    public GameObject environment;
    public GameObject water;
    public List<GameObject> spwanedProps;

    private simEnvironment currentEnvironment;
    private profileIO pio;
    private String[] pList =    {"Transparent","WaterColor","TurbidityColor","Turbidity","WindSpeed","WindRotation",
                                "WindTurbulence","TimeScale","RefractionMode","RefractionAproximatedDepth","RefractionSimpleStrength","UseRefractionDispersion",
                                "RefractionDispersionStrength","UseVolumetricLight","VolumetricLightResolutionQuality","VolumetricLightIteration","VolumetricLightBlurRadius",
                                "VolumetricLightFilter","UseDynamicWaves","DynamicWavesAreaSize","DynamicWavesSimulationFPS","DynamicWavesResolutionPerMeter","DynamicWavesPropagationSpeed",
                                "UseCausticEffect","UseCausticBicubicInterpolation","UseCausticDispersion","CausticTextureSize","CausticMeshResolution",
                                "CausticActiveLods","CausticStrength","UseDepthCausticScale","CausticDepthScaleInEditMode","CausticDepthScale","CausticOrthoDepthPosition",
                                "CausticOrthoDepthAreaSize","CausticOrthoDepthTextureResolution","UseUnderwaterEffect","UseUnderwaterBlur","UnderwaterBlurRadius","UseFiltering","UseAnisotropicFiltering"
                                };

    // Start is called before the first frame update
    void Start()
    {
        Configuration.RegisterTypeStringConverter(new ColorStringConverter());
        Configuration.RegisterTypeStringConverter(new Vector3StringConverter());
        if (currentPropsList == null)
        {
            currentPropsList = new List<Prop>();
            spwanedProps = new List<GameObject>();
        }
        importProfile();
        if (currentPropsList.Count == 0)
        {
            currentPropsList.Add(new Prop());
        }
    }

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
        else if (customizeToggle.isOn)
        {
            // TODO: add custom scene building 
            Debug.Log("Using custom scene set up");
            costumSceneSetUp();
        }
        else
        {
            Debug.Log("Error: no set up method is selected!");
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

        Vector3 prop1Center = new Vector3(17.5f, 0.775f, randomNumber(-3f, 18f, rng));
        prop1.GetComponent<Transform>().position = prop1Center;
        prop1b.GetComponent<Transform>().position = prop1Center + new Vector3(-1f, -0.37f, 0f);
        prop2.GetComponent<Transform>().position = new Vector3(randomNumber(0f, 38f, rng), 0.05f, 18f);
        Vector3 prop3Center = new Vector3(randomNumber(20f, 40f, rng), 0.405f, randomNumber(-2f, 10f, rng));
        prop3a.GetComponent<Transform>().position = prop3Center;
        prop3b.GetComponent<Transform>().position = (new Vector3(randomNumber(-1f, 1f, rng), 0, randomNumber(-1f, 1f, rng)) + prop3Center);
    }

    // generate a float number between given range
    public float randomNumber(float min, float max, System.Random rand)
    {
        return (min + (max - min) * (float)rand.NextDouble());
    }

    public void importProfile()
    {

        string profileString = profileInputField.text;
        //generate new profileIO instance if not exist
        if (pio == null)
        {
            pio = new profileIO();
        }
        pio.findOrBuild(profileString);


        //reset current props list
        currentPropsList = new List<Prop>();

        //load section by name
        var section = pio.cfg["Environment"];
        Debug.Log("Loading environment...");
        if (section["isExist"].GetValue<Boolean>() == true)
        {
            currentEnvironment = section.ToObject<simEnvironment>();
        }
        else
        {
            currentEnvironment = new simEnvironment();
        }


        section = pio.cfg["Water"];
        Debug.Log("Loading water...");
        WaterSystem waterSystem = water.GetComponent<WaterSystem>();
        List<String> propertyToRead = new List<string>(pList);

        foreach (PropertyInfo property in waterSystem.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (propertyToRead.Contains(property.Name))
            {
                try
                {
                    Type T = property.GetValue(waterSystem).GetType();
                    var thisValue = section[property.Name].GetValue(T);
                    property.SetValue(waterSystem, thisValue);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error at loading " + property.Name);
                    throw ex;
                }
            }

        }

        foreach (FieldInfo field in waterSystem.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
            if (propertyToRead.Contains(field.Name))
            {
                try
                {
                    Type T = field.GetValue(waterSystem).GetType();
                    var thisValue = section[field.Name].GetValue(T);

                    field.SetValue(waterSystem, thisValue);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error at loading" + field.Name);
                    throw ex;
                }
            }
        }

        //loading props
        for (int i = 0; !pio.cfg["Prop" + i]["PrefabID"].IsEmpty; i++)
        {
            section = pio.cfg["Prop" + i];
            Debug.Log("Loading Prop" + i);
            Prop tempProp = section.ToObject<Prop>();
            currentPropsList.Add(tempProp);
        }

    }

    private void costumSceneSetUp()
    {
        foreach (var prop in spwanedProps)
        {
            Destroy(prop);
        }
        foreach (Prop prop in currentPropsList)
        {
            spwanedProps.Add(spawnProp(prop));
        }
        return;
    }

    public void saveCostumSetUp()
    {
        //generate new profileIO instance if not exist
        if (pio == null)
        {
            pio = new profileIO();
        }
        pio.cfg = new Configuration();

        //add current Environment setting as the first section
        try
        {
            pio.cfg.Add(Section.FromObject("Environment", currentEnvironment));
        }
        catch (System.Exception)
        {
            Debug.LogError("Current Environment is not created! Creating new environment profile.");
            pio.cfg.Add(Section.FromObject("Environment", new simEnvironment()));
        }

        try
        {
            WaterSystem waterSystem = water.GetComponent<WaterSystem>();
            Section section = pio.cfg["Water"];

            List<String> propertyToRead = new List<string>(pList);
            foreach (PropertyInfo property in waterSystem.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (propertyToRead.Contains(property.Name))
                {
                    var setting = new Setting(property.Name, property.GetValue(waterSystem, null));
                    section.Add(setting);
                }

            }

            foreach (FieldInfo field in waterSystem.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (propertyToRead.Contains(field.Name))
                {
                    var setting = new Setting(field.Name, field.GetValue(waterSystem));
                    section.Add(setting);
                }
            }


        }
        catch (System.Exception ex)
        {
            Debug.LogError("Water error." + ex);
        }

        //add props in props list, index by ascending number
        int i = 0;
        try
        {
            foreach (Prop prop in currentPropsList)
            {
                pio.cfg.Add(Section.FromObject(string.Format("Prop{0}", i), prop));
                i++;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Prop List Error!");
        }

        //save the profile to a file
        pio.saveCFG(profileInputField.text);
    }

    public void startUpMenuRefresh()
    {
        randomSeedLabel.SetActive(randomizeToggle.isOn);
        customIuputPanel.SetActive(customizeToggle.isOn);
    }

    private GameObject spawnProp(Prop targetProp)
    {
        if (targetProp.PrefabID == -1)
        {
            return null;
        }

        Debug.Log(string.Format("Spawned a prop with Prefab ID: {0}", targetProp.PrefabID));
        return Instantiate(publicPropsList[targetProp.PrefabID], targetProp.getPositionVector(), Quaternion.Euler(targetProp.getRotationVector()));
    }
}

