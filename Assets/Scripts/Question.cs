// By Adam Nixdorf
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Question : MonoBehaviour
{
    public string questionText;
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;
    public int correctAnswerIndex;
    // Constructor to initialize the question
    public Question(string questionText, string answerA, string answerB, string answerC, string answerD, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.answerA = answerA;
        this.answerB = answerB;
        this.answerC = answerC;
        this.answerD = answerD;
        this.correctAnswerIndex = correctAnswerIndex;
    }

   
}

[Serializable]
public class QuestionList
{
    public List<Question> questions;
}