using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class Intro_1_Interactive : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;
        [SerializeField] private Material m_OverMaterial;
        [SerializeField] private Material m_ClickedMaterial;
        [SerializeField] private Material m_DoubleClickedMaterial;
        [SerializeField] public VRInteractiveItem m_InteractiveItem;
        [SerializeField] public Renderer m_Renderer;
        bool waitActive = false;
        bool canSwitch = false;
        public string button_name;
        public GameObject cam;


        private void Awake()
        {
            m_Renderer.material = m_NormalMaterial;
            m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();

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
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;

            if (button_name.Equals("back"))
               cam.GetComponent<Intro_1_TextManager>().back();
            else
               cam.GetComponent<Intro_1_TextManager>().next();
        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}