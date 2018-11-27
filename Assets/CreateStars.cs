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

        JArray dataset = (JArray)obj["data"];

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
            newStar.mass = (float)s[conf.mass];
            newStar.position = basePos;

            starData.Add(newStar);

            basePos.y += 3;
        }

        // create base material that star materials are created from
        Material starBodyMat = new Material(Resources.Load("StarMat", typeof(Material)) as Material);
        Material starHaloMat = new Material(Resources.Load("lightup", typeof(Material)) as Material);

        // create game object which new stars are based off of
        GameObject starPrim = new GameObject();
        GameObject starBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject starHalo = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        //SphereCollider starBodyCol = starPrim.GetComponent<SphereCollider>();
        Renderer starBodyRend = starBody.GetComponent<Renderer>();
        Renderer starHaloRend = starHalo.GetComponent<Renderer>();

        foreach (StarData star in starData)
        {
            // set star's position
            starPrim.transform.localScale = new Vector3(star.radius, star.radius, star.radius);
            
            starBodyRend.material = new Material(starBodyMat);
            //starBodyRend.material.color = star.color;
            starBodyRend.material.SetColor("_EmissionColor", star.color);
            starBodyRend.transform.parent = starPrim.transform;

            starHaloRend.material = new Material(starHaloMat);
            starHaloRend.material. color = star.color;
            starHaloRend.transform.parent = starPrim.transform;

            // create stars
            Instantiate(starPrim, star.position, Quaternion.identity);
        }
    }
}
