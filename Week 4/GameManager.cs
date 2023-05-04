using UnityEngine;
using UnityEngine.UI;   // This contains Image, Slider and the legacy Text
using TMPro;            // This contains TextMeshProUGUI, which you should use for text

// Define all of your states (or 'rooms') here
// None is just a default state that was added so that the previous room can be set to nothing in the beginning
public enum State { None, Bed, LivingRoom, Outside }

public class GameManager : MonoBehaviour
{
    // This is a parent object that contains your entire menu panel
    [SerializeField] GameObject menu;

    // These need to be created in your Scene. storyText will be the main text element
    [SerializeField] TextMeshProUGUI storyText;

    // These need to have either a Button or Event Trigger on them
    [SerializeField] TextMeshProUGUI choiceAText;
    [SerializeField] TextMeshProUGUI choiceBText;
    [SerializeField] TextMeshProUGUI choiceCText;

    // These variables are for holding the current state, as well as the previous state
    // You might not need previousState, but it will let you check where you came from
    State currentState;
    State previousState;

    // These are the 'conditions' mentioned in the exercise
    bool hasKey = false;
    int sleepCounter = 0;

    void Start()
    {
        currentState = State.Bed;   // Our entry state will be 'Bed'
        previousState = State.None; // There's no previous state yet
        DisplayState();
    }

    // This toggles the menu on or off
    public void ShowMenu() => menu.SetActive(!menu.activeSelf);

    // This function is just for changing the texts, images and music. The logic for our state machine is in SelectChoice()
    // You can show different texts in the same state using the condition and previousState variables
    void DisplayState()
    {
        switch (currentState)
        {
            case State.Bed:
                storyText.text = previousState == State.Bed ? $"You sleep more, total sleep: {sleepCounter}" : "In bedroom";
                choiceAText.text = "Go to house";
                choiceBText.text = "Sleep more";
                choiceCText.text = "";
                break;
            case State.LivingRoom:
                storyText.text = hasKey ? "In House, you have the key" : "In House, need key";
                choiceAText.text = "Back to bed";
                choiceBText.text = "Go outside";
                choiceCText.text = hasKey ? "" : "Collect key";
                break;
            case State.Outside:
                storyText.text = "Outside";
                choiceAText.text = "Back to title";
                choiceBText.text = "";
                choiceCText.text = "";
                break;
            default:
                break;
        }

        // These statements deactivate all choice texts that aren't containing any texts
        // If you just set the text to be empty, you might still be able to click on the UI element and trigger the event
        choiceBText.gameObject.SetActive(choiceBText.text != "");
        choiceCText.gameObject.SetActive(choiceCText.text != "");
    }

    // This function contains the actual logic for our state machine
    // The choice parameter is given by the OnClick() or OnPointerDown() event found on the Button / Event Trigger component
    public void SelectChoice(int choice)
    {
        previousState = currentState;

        switch (currentState)
        {
            case State.Bed:
                if (choice == 1) currentState = State.LivingRoom;
                else if (choice == 2)
                {
                    sleepCounter++;
                    currentState = State.Bed;   // You don't really need to set the same state again, but it helps to keep an overview
                }
                break;
            case State.LivingRoom:
                if (choice == 1) currentState = State.Bed;
                else if (choice == 2)
                {
                    if (hasKey) currentState = State.Outside;
                    else currentState = State.LivingRoom;
                }
                else if (choice == 3)
                {
                    hasKey = true;
                    currentState = State.LivingRoom;
                }
                break;
            case State.Outside:
                if (choice == 1)
                {
                    hasKey = false;
                    sleepCounter = 0;
                    currentState = State.Bed;
                }
                break;
            default:
                break;
        }

        DisplayState();
    }
}