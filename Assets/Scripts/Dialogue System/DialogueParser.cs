using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using XNode;

namespace Dialogue_System
{
    public class DialogueParser : MonoBehaviour
    {
        [SerializeField] private DialogueGraph graph;
        public TextMeshProUGUI speakerNameText;
        public TextMeshProUGUI narrationLineText;
        public Image speakerImage;
        private Coroutine _parser;
        void Start()
        {
            ResetGraph();
            DisableUI();
            _parser = StartCoroutine(ParseNode());
            
        }

        private IEnumerator ParseNode()
        {
            BaseNode currentNode = graph.current;
            string[] data = currentNode.GetDataString().Split('/');
            Debug.Log(data);
            switch (data[0]) {
                case "StartNode":
                    EnableUI();
                    break;
                case "EndNode":
                    DisableUI();
                    yield break;
                case "NarrationLineNode":
                    speakerNameText.text = data[1];
                    narrationLineText.text = data[2];
                    speakerImage.sprite = currentNode.GetSprite();
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                    break;
            }
            ToNextNode("exit");
        }

        private void ToNextNode(string fieldName)
        {
            if (_parser != null) {
                StopCoroutine(_parser);
                _parser = null;
            }
            
            foreach (NodePort port in graph.current.Ports) {
                if (port.fieldName == fieldName) {
                    graph.current = port.Connection.node as BaseNode;
                }
            }

            _parser = StartCoroutine(ParseNode());
        }

        private void ResetGraph()
        {
            graph.current = graph.GetRootNode();
        }

        private void DisableUI()
        {
            speakerNameText.enabled = false;
            narrationLineText.enabled = false;
            speakerImage.enabled = false;
        }

        private void EnableUI()
        {
            speakerNameText.enabled = true;
            narrationLineText.enabled = true;
            speakerImage.enabled = true;
        }

    }
}
