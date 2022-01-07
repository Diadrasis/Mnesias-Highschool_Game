using System.Linq;
using UnityEngine;

public class AudioHighSchool : MonoBehaviour
{
    public AudioSource sourceCam;
    public AudioClip[] clips;
    [HideInInspector] public string nameOfFolder;
    // Start is called before the first frame update
    void Start()
    {
        sourceCam = Camera.main.gameObject.GetComponent<AudioSource>();
  
    }

    //load audio from resources and specific filename
    public void LoadAudio(string fileName)
    {
        nameOfFolder = fileName;
        clips = Resources.LoadAll(fileName, typeof(AudioClip)).Cast<AudioClip>().ToArray();
        
        //Debug.Log("Folder Name Audio: " + fileName);
    }
}
