using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameCreator.Runtime.Variables;

public class OpenAIController : MonoBehaviour
{
    public static OpenAIController instance;
    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button okButton;
    string roleString;
    string startStringTemp;
    public Image img;
    private OpenAIAPI api;
    private List<ChatMessage> messages;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // This line gets your API key (and could be slightly different on Mac/Linux)
        api = new OpenAIAPI("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");

    }

    public void StartChat(string roleStringtmp, string startStringtmp,Sprite image, string nickName)
    {
        roleString = "Keep your sentences very short (Maximum 100 characters) and your answers won't be too long. Always answer in Turkish. Don't ever say that it's artificial intelligence." + roleStringtmp;
        startStringTemp = startStringtmp;
        img.sprite = image;
        StartConversation();
        okButton.onClick.AddListener(() => GetResponse(nickName));
    }

    private void StartConversation()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System,roleString )
        };

        inputField.text = "";
        string startString = startStringTemp;
        textField.text = startString;
        Debug.Log(startString);
    }

    private async void GetResponse(string nickname)
    {
        if (inputField.text.Length < 1)
        {
            return;
        }

        // Disable the OK button
        okButton.enabled = false;

        // Fill the user message from the input field
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = inputField.text;
        if (userMessage.Content.Length > 100)
        {
            // Limit messages to 100 characters
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0}: {1}", userMessage.rawRole, userMessage.Content));

        // Add the message to the list
        messages.Add(userMessage);

        // Update the text field with the user message
        textField.text = string.Format("Ben: {0}", userMessage.Content);

        // Clear the input field
        inputField.text = "";

        // Send the entire chat to OpenAI to get the next message
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.9,
            MaxTokens = 500,
            Messages = messages
        });

        // Get the response message
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        // Add the response to the list of messages
        messages.Add(responseMessage);

        // Update the text field with the response
        textField.text = string.Format("Ben: {0}\n\n"+nickname+": {1}", userMessage.Content, responseMessage.Content);

        // Re-enable the OK button
        okButton.enabled = true;
    }
}