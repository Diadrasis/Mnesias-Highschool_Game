using System.Linq;
using UnityEngine;
using TMPro;
using System.Collections;

public class PositionCreation : MonoBehaviour
{
    public Vector2[] pointsOnpref;// is used only for the values to show on top of each stop
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
        
    }

    //initialize the indexes for the positions
    public void Init()
    {
        currentPosIndex += 0;
        currentPos = new Transform[currentPosIndex];

        
        StartCoroutine(SpawnPointsDelay());//start creating the points with a small dealy between them
    }

    //create the points on current instrument
    /*public void CreatePoints()
    {
        hasCreated = true;
        
        for (int i = 0; i < currentPosIndex; i++)
        {
            distanceMake = (valuesForDistance[i]/10) / (float)(currentPosIndex); //the distance from each specific value a tone has (according to guidelines)
            Vector2 createPos = startPos.position + (endPos.position - startPos.position) * (distanceMake * i);
            
            Transform newPos = Instantiate(insantiatePos, createPos, Quaternion.identity);
            spawnLoad(newPos.transform.gameObject, new Vector3(0.025f, 0.1f, 1), 0.5f);
            newPos.parent = parentContainer;//put the instantiated objects under a specific parent
            newPos.name = "Tone "+i;// change the typical name of the gameObject clone to the one that will match our clips
            newPos.GetChild(0).name = "Tone " + i;
            //insert objects to array
            currentPos[i] = newPos;
            characterManager.manager.ShowText(i);
            string temp = pointsOnpref[i].x+" / "+pointsOnpref[i].y;
            newPos.gameObject.GetComponentInChildren<TextMesh>().text = temp;

            if(i==0||i==2 || i == 4)
            {
                newPos.transform.localScale = new Vector3(0.025f, 0.15f, 1f);
                newPos.position = new Vector3(newPos.position.x,newPos.transform.position.y+0.2f,newPos.transform.position.z);
            }
        }
    }
*/
    //when loading a different instrument, remove the previous go's and then create new ones
    public void DestoryInstantiatedPositions()
    {
        for (int i = 0; i < currentPosIndex; i++)
        {
            Destroy(currentPos[i].gameObject);
        }
        hasCreated = false;
       
    }

    IEnumerator SpawnPointsDelay()
    {
        hasCreated = true;

        for (int i = 0; i < currentPosIndex; i++)
        {
            distanceMake = (valuesForDistance[i]/5f+0.4f) / (float)(currentPosIndex); //the distance from each specific value a tone has (according to guidelines)
            Vector2 createPos = startPos.position + (endPos.position - startPos.position) * (distanceMake * i);

            Transform newPos = Instantiate(insantiatePos, createPos, Quaternion.identity);
           
            spawnLoad(newPos.transform.gameObject, new Vector3(0.025f, 0.15f, 1), 0.5f);
            
            
            newPos.parent = parentContainer;//put the instantiated objects under a specific parent
            newPos.name = "Tone " + i;// change the typical name of the gameObject clone to the one that will match our clips
            newPos.GetChild(0).name = "Tone " + i;
            //insert objects to array
            currentPos[i] = newPos;
            characterManager.manager.ShowText(i);
            string temp = pointsOnpref[i].x + " / " + pointsOnpref[i].y;
            newPos.gameObject.GetComponentInChildren<TextMesh>().text = temp;

            if (i == 1)
            {
                //newPos.transform.localScale = new Vector3(0.025f, 0.4f, 1f);
                newPos.GetChild(1).position = new Vector3(newPos.position.x, newPos.transform.position.y - 1.4f, newPos.transform.position.z);
                newPos.position = new Vector3(newPos.position.x, newPos.transform.position.y + 0.5f, newPos.transform.position.z);
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    void spawnLoad(GameObject gameObject, Vector3 vector, float time)
    {
        iTween.ScaleTo(gameObject, vector, time);
    }

}
