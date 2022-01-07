using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PositionCreation : MonoBehaviour
{

    public float[] valuesForDistance; //to find the distances according to the values from the guidelines
    public int currentPosIndex; //value to say how many objects to instantiate
    //[HideInInspector] public Button currentButton;
    public Transform startPos, endPos, insantiatePos; //the starting position, the end position we need for the references in order to instantiate the instantiatePos object
    float distanceMake; //private floate value to calculate the distance for instantiated objects
    public Transform parentContainer;//to hold the instantiated objects
    public Transform[] currentPos;//the array with all the instantiated objects
    

    [HideInInspector] public bool hasCreated = false;
    public CharacterManager characterManager;

    private void Start()
    {
        Init();
        
        //characterManager = FindObjectOfType<CharacterManager>();
    }

    //initialize the indexes for the positions
    public void Init()
    {
        currentPosIndex += 0;
        currentPos = new Transform[currentPosIndex];
        
        CreatePoints();//if no points will be created when pressing a specific instrument on HighSchool Manager, we will use the method in Start.
        
    }

    //create the points on current instrument
    public void CreatePoints()
    {
        hasCreated = true;
        
        for (int i = 0; i < currentPosIndex; i++)
        {
            distanceMake = valuesForDistance[i] / (float)(currentPosIndex); //the distance from each specific value a tone has (according to guidelines)
            Vector2 createPos = startPos.position + (endPos.position - startPos.position) * (distanceMake * i);
            Transform newPos = Instantiate(insantiatePos, createPos, Quaternion.identity);
            newPos.parent = parentContainer;//put the instantiated objects under a specific parent
            
            newPos.name = "Tone "+i;// change the typical name of the gameObject clone to the one that will match our clips
            newPos.GetChild(0).name = "Tone " + i;
            //insert objects to array
            currentPos[i] = newPos;
            characterManager.manager.ShowText(i);
        }
    }

    //when loading a different instrument, remove the previous go's and then create new ones
    public void DestoryInstantiatedPositions()
    {
        for (int i = 0; i < currentPosIndex; i++)
        {
            Destroy(currentPos[i].gameObject);
        }
        hasCreated = false;
       
    }
}
