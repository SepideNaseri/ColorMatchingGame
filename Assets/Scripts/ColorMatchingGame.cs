using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMatchingGame : MonoBehaviour
{

     // UI Elements References
    public Button[] colorButtons; 
     public Text targetColorName;
      public Text scoreText;
     public Text gameOverText;
     public Text timerText;
     

    // Game Variables
    public int score = 0; 
    private string[] colorNames = { "Red", "Green", "Blue", "yellow", "Purple", "Pink" };
    private Color[] colors = { Color.red, Color.green , Color.blue, Color.yellow,new Color(0.5f, 0f, 0.5f, 1f),  new Color(1f, 0.75f, 0.8f, 1f)};
    private int targetColorIndex;
     public float timeLimit = 30f; 
    private float timeRemaining;



    // Start is called before the first frame update
    void Start()
    {

        if (timeLimit > 0)
        {
            timeRemaining = timeLimit;
        }


        InitializeButtons();
        // Initialize score and timer
        SelectTargetColor();
        score = 0;
        scoreText.text = "Score: " + score;
        gameOverText.text = "";

        if (timeLimit > 0){
            timerText.text = "Time: " + Mathf.FloorToInt(timeRemaining);
        }


        
    }


    void Update()
    {
        if (timeLimit > 0)
        {
            // Update timer every frame
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.FloorToInt(timeRemaining);

            // End game if time runs out
            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }


    void SelectTargetColor()
    {
        targetColorIndex = Random.Range(0, colorNames.Length);
        targetColorName.text = colorNames[targetColorIndex];

        //  Debug.Log("Selected Target Color: " + colorNames[targetColorIndex] + " (Index: " + targetColorIndex + ")");
    }

    void OnButtonClick(int buttonIndex)
    {
        // Debug.Log("Button Clicked: " + buttonIndex + " - Target Index: " + targetColorIndex);
        CheckColor(buttonIndex);
    }

 // Check if the clicked color matches the target color
    public void CheckColor(int buttonIndex)
    {
        Debug.Log("check color  " +colors[buttonIndex] + " == " + colors[targetColorIndex]);
        Debug.Log("result:  " + (colors[buttonIndex] == colors[targetColorIndex]));
        if (colors[buttonIndex] == colors[targetColorIndex])
        {
            score++;
            UpdateScore();
            ReinitalizeButtons();
            Invoke(nameof(SelectTargetColor),1f);

        }
        else
        {
            GameOver();
        }

        
    }

     void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    

    // Game Over Logic
    void GameOver()
    {
        targetColorName.text = "";
        gameOverText.text = "Game Over!";
        foreach (Button button in colorButtons)
        {
            button.interactable = false; // Disable all buttons
        }

        Debug.Log("Game Over!");
    }
    void InitializeButtons(){
                // Assign colors to buttons
         for (int i = 0; i < colorButtons.Length; i++)
        {
            colorButtons[i].GetComponent<Image>().color = colors[i];
            int index = i; // Capture index for the delegate
            colorButtons[i].onClick.AddListener(() => CheckColor(index)); // Add click listener
        }
    }

    void ReinitalizeButtons(){
        for (int i = 0; i < colorButtons.Length; i++)
        {
            colorButtons[i].GetComponent<Image>().color = colors[i];
            int index = i; // Capture index for the delegate
        }
    }


}
