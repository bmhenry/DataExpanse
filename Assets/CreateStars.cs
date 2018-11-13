using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class JsonConfig
{
    // names of the sources for information in the dataset objects
    public string name { get; set; }
    public string color { get; set; }
    public string radius { get; set; }
    public string mass { get; set; }

    public override string ToString()
    {
        return base.GetType().ToString() +
               " { " +
               " name: " + name +
               " color: " + color +
               " radius: " + radius +
               " mass: " + mass +
               " }";
    }
}

public class StarData
{
    public string name { get; set; }
    public Color color { get; set; }
    public float radius { get; set; }
    public float mass { get; set; }

    public Vector3 position { get; set; }
}

public class CreateStars : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // load random json file
        string jsonstr = File.ReadAllText("example.json");
        JObject obj = JObject.Parse(jsonstr);
        JsonConfig conf;
        conf = obj["map"].ToObject<JsonConfig>();
        Debug.Log("map: \n" + conf.ToString());

        JArray dataset = (JArray)obj["data"];
        Debug.Log("dataset: \n" + dataset.ToString());

        Vector3 basePos = new Vector3(0, 0, 0);

        List<StarData> starData = new List<StarData>();
        foreach (JObject s in dataset.Children())
        {
            StarData newStar = new StarData();
            newStar.name = (string)s[conf.name];
            Debug.Log(newStar.name);

            Color starColor;
            if (ColorUtility.TryParseHtmlString((string)s[conf.color], out starColor))
            {
                newStar.color = starColor;
            }
            else
            {
                newStar.color = Color.white;
            }

            newStar.radius = (float)s[conf.radius];
            Debug.Log("radius: " + newStar.radius);
            newStar.mass = (float)s[conf.mass];
            Debug.Log("mass: " + newStar.mass);
            newStar.position = basePos;
            Debug.Log("pos: " + newStar.position);

            starData.Add(newStar);

            basePos.y += 3;
        }

        // create base material that star materials are created from
        Material starMat = new Material(Shader.Find("Standard"));

        // create game object which new stars are based off of
        GameObject starPrim = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        SphereCollider starCol = starPrim.GetComponent<SphereCollider>();
        Renderer starRend = starPrim.GetComponent<Renderer>();

        foreach (StarData star in starData)
        {
            Debug.Log("Trying to set radius to: " + star.radius);
            starPrim.transform.localScale = new Vector3(star.radius, star.radius, star.radius);
            starRend.material = new Material(starMat);
            starRend.material.color = star.color;

            // create stars
            Instantiate(starPrim, star.position, Quaternion.identity);
        }
    }
}
