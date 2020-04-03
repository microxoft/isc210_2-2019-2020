using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public string CurrentLevel;
    public GameObject Grass1, Grass2, RoadCross, RoadEndHor2, RoadEndHor2Left, RoadEndVer2, RoadEndVer2Down, RoadMiddleHor, RoadMiddleVer1, Tree;
    public GameObject PlayerPrefab, MorahPrefab, LionelPrefab, Enemy1Prefab;
    XmlDocument xmlDoc;
    GameObject currentPrefab = null;
    Transform cellsContainer, charactersContainer;
    XmlNode currentNode;
    XmlNodeList nodeList;

    // These is used in Platformer:
    public GameObject PlatGrass1, PlatGrass2, PlatGrass3, PlatGrass4, PlatGrass5, PlatGrass6, PlatGrass7, PlatGrass8, PlatGrass9, PlatGrass10, PlatGrass11, PlatGrass12, PlatGrass13, PlatGrass14, PlatGrass15, PlatGrass16, PlatGrass17;

    private void Awake()
    {
        cellsContainer = GameObject.Find("Cells").transform;
        charactersContainer = GameObject.Find("Characters").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Resources.Load<TextAsset>(CurrentLevel).text);
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
                    case 'a':
                        currentPrefab = PlatGrass1;
                        break;
                    case 'b':
                        currentPrefab = PlatGrass2;
                        break;
                    case 'c':
                        currentPrefab = PlatGrass3;
                        break;
                    case 'd':
                        currentPrefab = PlatGrass4;
                        break;
                    case 'e':
                        currentPrefab = PlatGrass5;
                        break;
                    case 'f':
                        currentPrefab = PlatGrass6;
                        break;
                    case 'g':
                        currentPrefab = PlatGrass7;
                        break;
                    case 'h':
                        currentPrefab = PlatGrass8;
                        break;
                    case 'i':
                        currentPrefab = PlatGrass9;
                        break;
                    case 'j':
                        currentPrefab = PlatGrass10;
                        break;
                    case 'k':
                        currentPrefab = PlatGrass11;
                        break;
                    case 'l':
                        currentPrefab = PlatGrass12;
                        break;
                    case 'm':
                        currentPrefab = PlatGrass13;
                        break;
                    case 'n':
                        currentPrefab = PlatGrass14;
                        break;
                    case 'o':
                        currentPrefab = PlatGrass15;
                        break;
                    case 'p':
                        currentPrefab = PlatGrass16;
                        break;
                    case 'q':
                        currentPrefab = PlatGrass17;
                        break;
                    case ' ':
                        continue;
                }
                currentPrefab = Instantiate(currentPrefab, new Vector3(j, -i), Quaternion.identity);
                currentPrefab.transform.SetParent(cellsContainer);
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
            newElement.transform.SetParent(charactersContainer);

            if(newElement.tag == "Player")
            {
                Camera.main.transform.SetParent(newElement.transform);
                Camera.main.transform.localPosition = new Vector3(0, 0, Camera.main.transform.localPosition.z);
            }
        }
    }
}
