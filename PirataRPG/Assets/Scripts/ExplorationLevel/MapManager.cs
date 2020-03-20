using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject Grass1, Grass2, RoadCross, RoadEndHor2, RoadEndHor2Left, RoadEndVer2, RoadEndVer2Down, RoadMiddleHor, RoadMiddleVer1, Tree;
    public GameObject PlayerPrefab, MorahPrefab, LionelPrefab, Enemy1Prefab;
    XmlDocument xmlDoc;
    GameObject currentPrefab = null;
    XmlNode currentNode;
    XmlNodeList nodeList;
    // Start is called before the first frame update
    void Start()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Resources.Load<TextAsset>("Level1").text);
        LoadMap();
    }

    void LoadMap()
    {
        currentPrefab = null;
        nodeList = xmlDoc.SelectNodes("//level/map/row");
        for (int i = 0; i < nodeList.Count; i++)
        {
            currentNode = nodeList[i];
            for(int j = 0; j < currentNode.InnerText.Length; j++)
            {
                switch(currentNode.InnerText[j])
                {
                    case 'A':
                        currentPrefab = Grass1;
                        break;
                    case 'B':
                        currentPrefab = Grass2;
                        break;
                    case 'C':
                        currentPrefab = RoadCross;
                        break;
                    case 'D':
                        currentPrefab = RoadEndHor2;
                        break;
                    case 'E':
                        currentPrefab = RoadEndHor2Left;
                        break;
                    case 'F':
                        currentPrefab = RoadEndVer2;
                        break;
                    case 'G':
                        currentPrefab = RoadEndVer2Down;
                        break;
                    case 'H':
                        currentPrefab = RoadMiddleHor;
                        break;
                    case 'I':
                        currentPrefab = RoadMiddleVer1;
                        break;
                    case 'J':
                        currentPrefab = Tree;
                        break;
                }
                Instantiate(currentPrefab, new Vector3(j, -i), Quaternion.identity);
            }
        }

        // Loading characters:
        LoadCharacters();
    }

    void LoadCharacters()
    {
        GameObject newElement;
        currentPrefab = null;
        nodeList = xmlDoc.SelectNodes("//level/characters/character");
        foreach(XmlNode currentElement in nodeList)
        {
            switch(currentElement.Attributes["prefabName"].Value)
            {
                case "Player":
                    currentPrefab = PlayerPrefab; // En vez de Tree, sería un caracter.
                    break;
                case "Morah":
                    currentPrefab = MorahPrefab;
                    break;
                case "Lionel":
                    currentPrefab = LionelPrefab;
                    break;
                case "Enemy1":
                    currentPrefab = Enemy1Prefab;
                    break;
            }
            newElement = Instantiate(currentPrefab,
                new Vector3(Convert.ToSingle(currentElement.Attributes["posX"].Value),
                -Convert.ToSingle(currentElement.Attributes["posY"].Value)),
                Quaternion.identity);
            newElement.name = currentElement.Attributes["uniqueObjectName"].Value;
        }
    }
}
