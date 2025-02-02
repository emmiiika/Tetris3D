using UnityEngine;
using TMPro;


/// <summary>
/// Class <c>ScoreCounter</c> counts the score.
/// </summary>
public class ScoreCounter: MonoBehaviour{
    private int _score;
    
    public TMP_Text scoreText;
    
    public void InitScoreCounter(){
        _score = 0;
        scoreText.text = "Score: 0";
    }

    /// <summary>
    /// Method <c>increaseScore</c> increases the score by <c>amount</c> (default is 1).
    /// </summary>
    public void IncreaseScore(int amount = 1){
        this._score += amount;
        scoreText.text = "Score: " + _score.ToString();
    }
}