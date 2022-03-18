using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class HighSchoolManager : MonoBehaviour
{
    
    //public PositionCreation[] btnInstrument;
    public PositionCreation btnInstrumentTest;//if we proceed with the "fixed" positions we won't need the previous array and we will use this variable.
    public TextMeshProUGUI txtMain;
    public AudioHighSchool highSchool;

    public GameObject pnlInfoMain;
    public Image imgForInfo;
    public TextMeshProUGUI txtInfo;
    public Sprite spChange;
    public Button btnClosePanel;
    public Sprite[] imageLoads;//load images from resources to show them when player selects a tone to hear and the pnlInfo is prompt

   [HideInInspector] public bool isClosed;

    public CharacterManager character;
    public void Start()
    {
        //btnInstrument = FindObjectsOfType<PositionCreation>();
        character = FindObjectOfType<CharacterManager>();
        highSchool = FindObjectOfType<AudioHighSchool>();
        pnlInfoMain.SetActive(false);
        btnInstrumentTest = FindObjectOfType<PositionCreation>();
        //btnInstrumentTest.CreatePoints();//if no points will be created when pressing a specific instrument, we will use the method in Start.
        highSchool.LoadAudio("soundsHigh/");//load the sounds of the specific game.
        SubscribeToButtonsManager();

        LoadImages("imagesHigh/");//to load the images to show
        isClosed = false;
    }

    //when a toggle is on, we load it's corresponding audio and if we choose a different toggle we remove the previous points
    public void SubscribeToButtonsManager()
    {
        btnClosePanel.onClick.AddListener(ClosePanel);
        

    }
       

    //to close the panel info
    void ClosePanel()
    {
        if (pnlInfoMain.activeSelf)
        {
            pnlInfoMain.SetActive(false);
        }
        if (highSchool.sourceCam.isPlaying)
        {
            highSchool.sourceCam.Stop();
        }
    }

    //we will use more data to show info on each istrument per provided data
    public void PanelInfoMethod()
    {
        pnlInfoMain.SetActive(true);
                
    }
    public void ShowText(int num)
    {
        switch (num)
        {
            case 5:
                txtInfo.text = "Συλλαβή ή συλλαβά ή δια τεσσάρων ή επίτριτος λόγος (4:3)\nΟ λόγος με τον οποίο εκφράζεται το διάστημα 4ης.\nΗ λέξη «συλλαβή» προέρχεται από το ρήμα συλλαμβάνω = παίρνω μαζί, συνδέω,  θέτω μαζί, γι αυτό συλλαβή στη μουσική ήταν μια ένωση ή συνδυασμός φθόγγων. Ο όρος χρησιμοποιήθηκε για το διάστημα 4ης επειδή ήταν η πρώτη συμφωνία, το πρώτο σύμφωνο διάστημα.Όπου σύμφωνο ή αλλιώς συμφωνία ήταν το ταίριασμα δύο φθόγγων με την έννοια της καλής αρμονικής σχέσης μεταξύ τους.";
                break;
            case 7:
                txtInfo.text = "Δια πέντε ή δι’ οξειών ή διοξεία ή ημιόλιος (3:2)\nΟ λόγος με τον οποίο εκφράζεται το διάστημα της 5ης, το διάστημα της οποίας οι Πυθαγόρειοι θεωρούσαν πως ανήκει στα σύμφωνα διαστήματα";
                break;
            case 2:
                txtInfo.text = "Επόγδοος (9:8)\nΟ λόγος με τον οποίο εκφράζεται το διάστημα του μείζωνος  τόνου.\nΌπως αναφέρει το θεώρημα της Κατατομής Κανόνος του Ευκελίδου «Εάν από ημιολίου διαστήματος επίτριτον διάστημα αφαιρεθή, το λοιπόν καταλείπεται επόγδοον», δηλαδή αν αφαιρέσουμε μια 4η από μια 5η παίρνουμε το διάστημα του τόνου.";
                break;
            case 8:
                txtInfo.text = "Δια πασών και δις διαπασών (2:1 το δια πασών,4:1 το δις διαπασών)\nΟ λόγος με τον οποίο εκφράζεται το διάστημα της οκτάβας, της 8ης  ( ή της διπλής οκτάβας για το δις διαπασών), το οποίο ακόμα παλαιότερα ονομαζόταν και «αρμονία», καθώς θεωρείτο το πλέον ωραίο και ενωτικό, κατά τον Πτολεμαίο.";
                break;
            case 4:
                txtInfo.text = "Δίτονον (81:64)\nΟ λόγος με τον οποίο εκφράζεται το διάστημα που περιέχει δύο τόνους, δηλ. το διάστημα 3ης μεγάλο, το οποίο στη σύγχρονη μουσική είναι υπεύθυνο για να χαρακτιριστεί μια συγορδία ως μείζων, ματζόρε.";
                break;
            case 3:
                txtInfo.text = "Ημιδίτονον (32:27)\nΟ λόγος με τον οποίο εκφράζεται το διάστημα τρίτης μικρό, το οποίο στη σύγχρονη μουσική είναι υπεύθυνο για να χαρακτιριστεί μια συγχορδία ως ελάσσων, μινόρε. ";
                break;
            case 1:
                txtInfo.text = "Αποτομή (2187:2048)\nΟ λόγος με τον οποίο εκφράζεται το μείζων ημιτόνιο. Οι Πυθαγόρειοι αποδοκίμαζαν τη διαίρεση του τόνου σε δύο ίσα μέρη.Ως εκ τούτου το μεγαλύτερο από τα δύο αυτά μέρη το ονόμασαν αποτομή.";
                break;
            case 0:
                txtInfo.text = "Λείμμα (256:243)\nΟ λόγος με τον οποίο εκφράζεται το ελάττον ημιτόνιο.\nΟι Πυθαγόρειοι αποδοκίμαζαν τη διαίρεση του τόνου σε δύο ίσα μέρη.Το μεγαλύτερο ονομαζόταν αποτομή και ότι περίσσευε ονομαζόταν λείμμα.Ο Πτολεμαίος το καθορίζει ως το διάστημα κατά το οποίο η καθαρή Τετάρτη είναι μεγαλύτερη από το δίτονο.";
                break;
            case 6:
                txtInfo.text = "Τρίτονον (729:512)\nΟ λόγος με τον οποίο εκφράζεται το διάστημα της αυξημένης 4ης.";
                break;
            default:
                break;
        }
    }

    public void LoadImages(string fileName)
    {
        imageLoads = Resources.LoadAll(fileName, typeof(Sprite)).Cast<Sprite>().ToArray();
        
    }
}
