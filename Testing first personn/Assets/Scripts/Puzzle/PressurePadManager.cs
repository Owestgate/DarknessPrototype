using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PressurePadManager : MonoBehaviour
{
    public static PressurePadManager instance;

    public List<GameObject> pads;
    public List<GameObject> boxes;
    public List<Color> possibleColours;

    public int totalCorrectPlacementsNeeded; //3
    public int currentCorrectPlacements; //current no of correct placements
    public int placementsGeneral; // total number of placements

    //public Text canvasText;
    public UnityEvent completeEvent;

    private void Awake()
    {
        if(instance == null){
            instance = this;

        } else if (instance != this){
            Destroy (gameObject);
        }
    }

    void Start(){
        totalCorrectPlacementsNeeded = pads.Count;
        currentCorrectPlacements = 0;

        RandomiseColorList();
        AssignColoursToObjects(boxes);
        RandomiseColorList();
        AssignColoursToObjects(pads);
        ShuffleBoxOrder();
    }

    public void IncreasePlacement(){
        placementsGeneral++;

        if(placementsGeneral == totalCorrectPlacementsNeeded){
            //update canvas text
            //canvasText.text = currentCorrectPlacements.ToString();
        }
    }

    public void DecreasePlacement(){
        placementsGeneral--;
    }

    public void IncreaseCorrectPlacements(){
        currentCorrectPlacements++;

        if(currentCorrectPlacements == totalCorrectPlacementsNeeded){
            Debug.Log("All BOXES PLACED COREECTLY");
        }
    }

    public void DecreaseCorrectPlacements(){
        currentCorrectPlacements--;
    }

    void Update()
    {
        //canvasText.text = currentCorrectPlacements.ToString();
    }

    void AssignColoursToObjects (List<GameObject> objects){
        for(int i = 0; i < objects.Count; i++){
            objects[i].GetComponent<Renderer>().material.color = possibleColours[i];
        }

    }

    void RandomiseColorList (){
        
        List<Color> tempList = new List<Color>();

        for(int i = 0; i < possibleColours.Count; i++){
            tempList.Add(possibleColours[i]);
        }

        for(int i = 0; i <possibleColours.Count; i ++){
            Color tempColor = possibleColours[i];
            int randomIndex = UnityEngine.Random.Range(i, possibleColours.Count);
            possibleColours[i] = possibleColours[randomIndex];
            possibleColours[randomIndex] = tempColor;
        }
    }

    void ShuffleBoxOrder(){
        int number = 0; // box id
        for(int i = 0; i < boxes.Count; i ++){

            GameObject temp = boxes[i];
            int randomIndex = UnityEngine.Random.Range(i, boxes.Count);
            boxes[i] = boxes[randomIndex];
            boxes[randomIndex] = temp;

            boxes[i].GetComponent<PressurePadPickupable>().boxId = number;
            pads[i].GetComponent<PressurePad>().padId = number;
            number++;
        }
    }
}
