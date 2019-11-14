using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State
{
    StartScreen,
    StoryScreen,
    GameScreen,
    ResultScreen
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string ip;
    [SerializeField]
    private TextMeshProUGUI textDidYou;
    [SerializeField]
    private GameObject textGame;
    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite startScreen, didYouScreen, gameScreen;

    private InputController inputController;

    private State state = State.StartScreen;

    private Dictionary<long, string> dict = new Dictionary<long, string>();

    private float jumpedHeight;
    private long nuid;

    // Start is called before the first frame update
    void Start()
    {
        dict[4319018228] = "Ramon";
        dict[2502546022] = "Inge Loes";

        inputController = new InputController();
        inputController.Begin(ip, 26);

        image.sprite = startScreen;
        textDidYou.text = "";
        textGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.StartScreen:
                StartScreen();
                break;
            case State.StoryScreen:
                StoryScreen();
                break;
            case State.GameScreen:
                GameScreen();
                break;
            case State.ResultScreen:
                ResultScreen();
                break;
            default:
                throw new System.Exception("State isn't implemented");
        }
    }

    private void StartScreen()
    {
        if (inputController.currentValue != 0)
        {
            image.sprite = didYouScreen;

            var name = dict[inputController.currentValue];
            textDidYou.text = "Hey, my name is Arti\n";
            textDidYou.text += "I received a message from outer space, saying that my friend is stuck somewhere in our solar system.\n\n";
            textDidYou.text += name + ", it's dangerous to go on an adventure alone.\n";
            textDidYou.text += "Could you help me find my friend?";

            state = State.StoryScreen;
        }
    }

    private void StoryScreen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            image.sprite = gameScreen;
            textDidYou.text = "";
            textGame.SetActive(true);

            state = State.GameScreen;
        }
    }

    private void GameScreen()
    {
        float height = 0;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            height = 125;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            height = 150;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            height = 175;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            height = 200;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            height = 225;

        if(height != 0)
        {
            textGame.SetActive(false);

            image.sprite = didYouScreen;
            textDidYou.text = "Wow, you reached " + height + " centimeters\n";
            textDidYou.text += "If you were on Mars, you would have reached " + Mathf.Round(height / 0.38f) + " centimeters!\n\n";
            textDidYou.text += "If the animal in front of you was on mars. It would be able to jump on top of the building to the right";

            jumpedHeight = height;
            state = State.ResultScreen;
        }
    }

    private void ResultScreen()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            image.sprite = startScreen;
            textDidYou.text = "";

            state = State.StartScreen;
            inputController.currentValue = 0;
        }
    }

    private IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(3f);
        textDidYou.text = "Hold tag in close proximity";
    }
}
