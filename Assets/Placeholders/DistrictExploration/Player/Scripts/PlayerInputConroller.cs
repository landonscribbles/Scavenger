using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputConroller : MonoBehaviour, InputInterface {

    private SceneInputController sceneInputController;
    private PlayerData playerData;
    private CharacterController playerCharController;

    [SerializeField]
    private GameObject playerModel;
    [SerializeField]
    private float playerModelZDegreeOffset;
    [SerializeField]
    private float playerModelXDegrees;

    void Start () {
        playerCharController = GetComponent<CharacterController>();
        playerData = gameObject.GetComponent<PlayerData>();
        sceneInputController = GameObject.Find("SceneInputController").GetComponent<SceneInputController>();
        sceneInputController.ExplorationController = this;
	}

    public void HandlePlayerInput() {

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= playerData.PlayerMovementSpeed;
        playerCharController.Move(moveDirection * Time.deltaTime);

        FacePlayerModelAtMouse();
    }

    private void FacePlayerModelAtMouse() {
        
        Vector3 correctedMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z - playerModel.transform.position.z);
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(correctedMousePos);
        Vector3 mousePosition = new Vector3(screenToWorld.x, screenToWorld.y, playerModel.transform.position.z);

        // Get angle in radians
        float angleRadians = Mathf.Atan2(mousePosition.y - playerModel.transform.position.y, mousePosition.x - playerModel.transform.position.x);
        // Get angle in degrees
        float angleDegress = (180 / Mathf.PI) * angleRadians;

        playerModel.transform.rotation = Quaternion.Euler(0, 0, angleDegress + 90);
    }

}
