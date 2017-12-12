using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script for all message behaviors.
public class UniversalMessageController : MonoBehaviour {
	// STOP AUTOWALK when there's a message
	public Player player;
	// Set of messages that will be displayed.
	public string[] messages;
	// Whether or not the message should be displayed at startup.
	public bool showOnStart;
	// Whether or not the message should center itself in the player's view when shown.
	public bool centerOnPlayerOnShow = true;
	// Name of the scene that is loaded (if any) after the final message.
	public string loadSceneName;
	// Color that the panel will be highlighted with when gazed at.
	public Color highlightColor;
	// Distance from the player at which the message will be placed when centered on the player's view.
	public float distanceFromPlayer = 1.5f;
	// Collider around the message panel (used for raycast detection).
	public Collider messageCollider;
	// The background image of the message panel, used to set the background color.
	public Image uiBackgroundImage;
	// The text component of the message panel.
	public Text uiTextComponent;
	// Can the user click on this message?
	public bool clickEnabled = true;
	// Array index of the current message.
	public int messageIndex = 0;

	// The initial color of the panel (obtained at startup so we can de-highlight and restore this  
	// color when the user stops gazing at the message).
	private Color initialColor;
	// Position & rotation of player's head, should always be the main camera.
	private Transform playerHead;


	// Use this for initialization
	void Start () {
		CheckStartupVariables();
		initialColor = uiBackgroundImage.color;
		playerHead = Camera.main.transform;
		if (showOnStart) {
			ShowMessage ();
		} else {
			SetMessageVisibility (false);
		}
	}

	// Display the message.
	// This should be used by other scripts to show the message.
	public void ShowMessage(){
		// Prevent Autowalk
		//player.enabled = false;
		// Reset message index.
		messageIndex = 0;
		uiTextComponent.text = messages[messageIndex];
		// Center the message if needed.
		if(centerOnPlayerOnShow){
			CenterMessageOnPlayer();
		}
		// Show the message.
		SetMessageVisibility(true);
	}

	// Make the message visible or invisible.
	private void SetMessageVisibility(bool isVisible){
		// Enable/disable the UI components to set their visibility.
		uiBackgroundImage.enabled = isVisible;
		uiTextComponent.enabled = isVisible;
		// Enable/disable the collider so that the gaze cursor does not react to it.
		messageCollider.enabled = isVisible;
	}

	// Center the message in the player's view.
	private void CenterMessageOnPlayer(){
		Vector3 flattenedPlayerForward = playerHead.forward;
		flattenedPlayerForward.y = 0;
		flattenedPlayerForward.Normalize ();
		// Calculate the offset along the player's forward direction.
		Vector3 offsetFromPlayer = distanceFromPlayer * flattenedPlayerForward;
		// Set message position;
		transform.position = playerHead.position + offsetFromPlayer;
		// Make the message face the player.
		transform.forward = flattenedPlayerForward;
	}

	// Check some of the startup variables to make sure we didn't forget to set something.
	private void CheckStartupVariables(){
		// Check each variable, and if it hasn't been set print a reminder to the console.
		if(messageCollider == null){
			LogMissingComponent("collider", true);
		}
		if(uiBackgroundImage == null){
			LogMissingComponent("image", true);
		}
		if(uiTextComponent == null){
			LogMissingComponent("text", true);
		}
		if(highlightColor == Color.black){
			Debug.Log("Make sure you set the highlight color of message controller for" + name);
		}
	}

	public void NextMessage(){
		messageIndex++;
		if(messageIndex < messages.Length){
			uiTextComponent.text = messages[messageIndex];
		}
		else{
			if(loadSceneName == ""){
				SetMessageVisibility(false);
				//player.enabled = true;
			}
			else{
				SceneManager.LoadScene(loadSceneName,LoadSceneMode.Single);
			}
		}
	}

	// Conveniance method for printing a missing component message to the console.
	private void LogMissingComponent(string componentName, bool isFatal = false){
		// If the component being missing will cause major problems (isFatal), throw an error.
		string message = "No " + componentName + " component assigned to message controller for " + name;
		if(isFatal){
			throw new UnityException(message);
		}
		// If it's not fatal, just print to the console.
		Debug.Log(message);
	}

	// Called by GazeController when this object's collider starts being gazed at by player.
	public void OnGazeEnter(){
		uiBackgroundImage.color = highlightColor;
	}

	// Called by GazeController when this object's collider stops being gazed at by player.
	public void OnGazeExit(){
		uiBackgroundImage.color = initialColor;
	}

	public void OnClick(){
		if (clickEnabled) {
			NextMessage();
		}
	}
}
