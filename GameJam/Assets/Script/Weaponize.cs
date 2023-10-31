using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponize : MonoBehaviour
{
    public HingeJoint2D hj;
    JointMotor2D motor;
    public GameObject MainWeapon;   
    string weaponName;
    [SerializeField] GameObject Dagger;
    [SerializeField] GameObject Katana;
    [SerializeField] GameObject Kusarigama;
    [SerializeField] GameObject GreatSword;
    [SerializeField] GameObject Knuckle;
    [SerializeField] GameObject Sword;
    [SerializeField] GameObject Scythe;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Player1")
        {
            weaponName = SharedData.playerSelect1;
        }
        else if(gameObject.tag == "Player2")
        {
            weaponName=SharedData.playerSelect2;
        }

        WeaponSelect(weaponName);
        gameObject.GetComponent<Movement>().weapon = MainWeapon;
        gameObject.GetComponent<Movement>().weaponJoint = hj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DaggerEquip()
    {
        Dagger.SetActive(true);
        hj = gameObject.AddComponent<HingeJoint2D>();
        GameObject dagger1 = Dagger.transform.GetChild(0).gameObject;
        GameObject dagger2 = Dagger.transform.GetChild(1).gameObject;
        MainWeapon = dagger1;

        dagger1.GetComponent<Dagger>().Player = gameObject;
        dagger2.GetComponent<Dagger>().Player= gameObject;

        hj.connectedBody = dagger1.GetComponent<Rigidbody2D>();
        hj.autoConfigureConnectedAnchor = true;
        hj.autoConfigureConnectedAnchor = false;

    }
    void KatanaEquip()
    {

        Katana.SetActive(true);
        hj = gameObject.AddComponent<HingeJoint2D>();
        Katana.GetComponent<Katana>().Player = gameObject;
        MainWeapon = Katana;

        
        motor.maxMotorTorque = 1000000;
   
        hj.motor = motor;
        hj.useMotor = false;

        hj.connectedBody = Katana.GetComponent<Rigidbody2D>();
        hj.autoConfigureConnectedAnchor = true;
        hj.autoConfigureConnectedAnchor = false;
    }
    void KusarigamaEquip()
    {

        Kusarigama.SetActive(true);
        hj = gameObject.AddComponent<HingeJoint2D>();
 
        GameObject handle = Kusarigama.transform.GetChild(0).gameObject;
        handle.GetComponent<Kusarigama>().Player = gameObject;
        hj.connectedBody = handle.GetComponent<Rigidbody2D>();
        hj.autoConfigureConnectedAnchor = true;
        hj.autoConfigureConnectedAnchor = false;

        GameObject scythe = Kusarigama.transform.GetChild(Kusarigama.transform.childCount - 1).gameObject; 
        Debug.Log(scythe);
        scythe.GetComponent<Scythe>().Player = gameObject;

        MainWeapon = handle;
    }
    void GreatSwordEquip()
    {


        GreatSword.SetActive(true);
        hj = gameObject.AddComponent<HingeJoint2D>();
        GreatSword.GetComponent<GreatSword>().Player = gameObject;
        MainWeapon = GreatSword;

        motor.maxMotorTorque = 1000000;

    
        hj.motor = motor;
        hj.useMotor = false;

        hj.connectedBody = GreatSword.GetComponent<Rigidbody2D>();
        hj.autoConfigureConnectedAnchor = true;
        hj.autoConfigureConnectedAnchor = false;
    }
    void KnuckleEquip()
    {
        Knuckle.SetActive(true);
        hj = gameObject.AddComponent<HingeJoint2D>();
        HingeJoint2D hj2 = gameObject.AddComponent<HingeJoint2D>();
        GameObject KnuckleL = Knuckle.transform.GetChild(0).gameObject;
        GameObject KnuckleR = Knuckle.transform.GetChild(1).gameObject;
        MainWeapon = KnuckleR;

        KnuckleR.GetComponent<KnuckleR>().Player = gameObject;
        KnuckleL.GetComponent<KnuckleL>().Player = gameObject;

        hj.connectedBody = KnuckleR.GetComponent<Rigidbody2D>();
        hj.autoConfigureConnectedAnchor = true;
        hj.autoConfigureConnectedAnchor = false;

        hj2.connectedBody = KnuckleL.GetComponent<Rigidbody2D>();
        hj2.autoConfigureConnectedAnchor = true;
        hj2.autoConfigureConnectedAnchor = false;

    }
    void SwordEquip()
    {
        Sword.SetActive(true);
        hj = gameObject.AddComponent<HingeJoint2D>();
        GameObject saber = Sword.transform.GetChild(0).gameObject;
        saber.GetComponent<Sword>().Player = gameObject;
        MainWeapon = saber;

        hj.connectedBody = saber.GetComponent<Rigidbody2D>();
        hj.autoConfigureConnectedAnchor = true;
        hj.autoConfigureConnectedAnchor = false;
    }
    void ScytheEquip()
    {

        Scythe.SetActive(true);
        hj = gameObject.AddComponent<HingeJoint2D>();
        GameObject ScytheHandle = Scythe.transform.GetChild(0).gameObject;
        GameObject ScytheBlade = Scythe.transform.GetChild(1).gameObject;
        GameObject Handle2 = Scythe.transform.GetChild(2).gameObject;
        MainWeapon = ScytheHandle;

        ScytheHandle.GetComponent<Scythe_handle>().Player = gameObject;
        ScytheBlade.GetComponent<Scythe_blade>().Player = gameObject;
        Handle2.GetComponent<ScytheHandle2>().Player = gameObject;

        hj.connectedBody = ScytheHandle.GetComponent<Rigidbody2D>();
        hj.autoConfigureConnectedAnchor = true;
        hj.autoConfigureConnectedAnchor = false;
    }
    void WeaponSelect(string weaponName)
    {
        if(weaponName == "Dagger")
        {
            DaggerEquip();
        }
        else if(weaponName == "Katana")
        {
            KatanaEquip();
        }
        else if (weaponName == "Kusarigama")
        {
            KusarigamaEquip();
        }
        else if (weaponName == "Greatsword")
        {
            GreatSwordEquip();
        }
        else if(weaponName== "Knuckle")
        {
            KnuckleEquip();
        }
        else if(weaponName == "Sword&Shield")
        {
            SwordEquip();
        }
        else if(weaponName == "Scythe")
        {
            ScytheEquip();
        }
        else
        {
            RandomEquip();
        }
    }

    void RandomEquip()
    {
        string[] names = new string[] { "Dagger", "Katana", "Kusarigama", "Greatsword", "Knuckle", "Sword&Shield", "Scythe" };
    
        int randomIndex = Random.Range(0, names.Length);
        WeaponSelect(names[randomIndex]);
        
    }
}
