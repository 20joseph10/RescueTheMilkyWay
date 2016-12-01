using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;                
        [SerializeField] private Material m_OverMaterial;                  
        [SerializeField] private Material m_ClickedMaterial;               
        [SerializeField] private Material m_DoubleClickedMaterial;         
        [SerializeField] public VRInteractiveItem m_InteractiveItem;
        [SerializeField] public Renderer m_Renderer;
        public GameObject explode;
        public Star StarInfo;

        bool waitActive = false;
        bool canSwitch = false;
        private Text infoPan;
        private void Awake ()
        {
            m_Renderer.material = m_NormalMaterial;
            m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
            infoPan = GameObject.FindWithTag("InfoPan").GetComponent<Text>() as Text;

        }


    private void OnEnable()
        {

            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;


        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            m_Renderer.material = m_OverMaterial;
            string starNotes = "";
            char[] tempchar = StarInfo.Notes.ToCharArray();
            int tempcount = 0;
            foreach(char i in tempchar)
            {
                if (tempcount == 20) {
                    tempcount = 0;
                    starNotes += "-\n";
                }
                tempcount++;
                starNotes += i.ToString();
            }

            infoPan.text = "Name: <b><color=red>" + StarInfo.Name + "</color></b>\nRA: " + StarInfo.RA + "\nDec: " + StarInfo.Dec +"\nMagnitude: " + StarInfo.abs_mag + "\nNotes: " + starNotes;
            
            
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
            infoPan.text = "";
        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;
            
        }

        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}