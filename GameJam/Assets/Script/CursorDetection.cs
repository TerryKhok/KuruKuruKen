using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CursorDetection : MonoBehaviour
{
    Component script;
    public string currentCharacter;
    public string playerSelection1;
    public string playerSelection2;
    bool charSelected = false;

    //prticle
    public ParticleSystem select;

    //material
    public Material lit;
    public Material sprites;


    void Start()
    {
        //プレイヤー選択初期化
        SharedData.playerSelect1 = null;
        SharedData.playerSelect2 = null;
        currentCharacter = null;
        script = gameObject.GetComponent<MonoBehaviour>();
    }

    //両プレイヤー選択済み、Space押したらGameplay入ります
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space) && SharedData.playerSelect1 != null && SharedData.playerSelect2 != null)
        {
            SceneManager.LoadScene("Gameplay");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("ply1: " + SharedData.playerSelect1 + " ||| ply2: " + SharedData.playerSelect2);
        }
    }

    //プレイヤー今いるマスを読む
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "WeaponSelect")
        {
            currentCharacter = other.gameObject.name;
        }
    }

    //マスを離れたらnullに設定する
    private void OnTriggerExit2D(Collider2D other)
    {
        currentCharacter = null;
    }

    void SelectCharacter()
    {
        if (currentCharacter != null)
        {
            //未選択なら、武器を選択する
            if (charSelected == false)
            {
                charSelected = true;
                FindObjectOfType<AudioManager>().Play("Select");
                if (gameObject.tag == "Player1")
                {
                    SharedData.playerSelect1 = currentCharacter;
                }

                else if (gameObject.tag == "Player2")
                {
                    SharedData.playerSelect2 = currentCharacter;
                }

                GameObject child = transform.GetChild(0).gameObject;
                child.GetComponent<Renderer>().material = sprites;

                ParticleSystem newParticle = Instantiate(select);
                newParticle.transform.position = child.transform.position;
                newParticle.Play();
                Destroy(newParticle.gameObject, 1f);

                script.SendMessage("setSpd0", gameObject);
            }
            //選択済みなら、選択を外す
            else
            {
                charSelected = false;
                FindObjectOfType<AudioManager>().Play("Deselect");
                if (gameObject.tag == "Player1")
                {
                    SharedData.playerSelect1 = null;
                }

                else if (gameObject.tag == "Player2")
                {
                    SharedData.playerSelect2 = null;
                }

                GameObject child = transform.GetChild(0).gameObject;
                child.GetComponent<Renderer>().material = lit;

                script.SendMessage("setSpd10", gameObject);
            }
        }

    }
}
