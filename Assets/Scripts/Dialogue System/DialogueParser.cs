using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
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
        [SerializeField] private Language language;
        [SerializeField] private DialogueGraph graph;
        public TextMeshProUGUI speakerNameText;
        public TextMeshProUGUI narrationLineText;
        public Image speakerImage;
        private Coroutine _parser;
        public void Start()
        {
            ResetGraph();
            DisableUI();
            _parser = StartCoroutine(ParseNode());
            
        }

        private IEnumerator ParseNode()
        {
            BaseNode currentNode = graph.current;
            string[] data = currentNode.GetDataString(language).Split('/');
            switch (data[0]) {
                case "StartNode":
                    EnableUI();
                    ToNextNode("exit");
                    break;
                case "EndNode":
                    DisableUI();
                    yield break;
                case "NarrationLineNode":
                    speakerNameText.text = data[1];
                    narrationLineText.text = data[2];
                    speakerImage.sprite = currentNode.GetSprite();
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
                    ToNextNode("exit");
                    break;
                case "ChoiceNode":
                    speakerNameText.text = data[1];
                    speakerImage.sprite = currentNode.GetSprite();
                    var replies = data.Skip(2).ToArray();
                    narrationLineText.text = replies.Aggregate("", (current, reply) => current + reply + '\n');
                    int nextPortNumber = -1;
                    while (nextPortNumber == -1) {
                        yield return new WaitUntil(() =>
                            Input.anyKeyDown);
                        string inputString = Input.inputString;
                        if (inputString.Length <= 0) continue;
                        int pressedKey = Input.inputString[0] - '0';
                        if (pressedKey >= 1 & pressedKey <= replies.Length) {
                            nextPortNumber = pressedKey - 1;
                        }
                    }
                    ToNextNode($"replies {nextPortNumber}");
                    break;
            }
        }

        private void ToNextNode(string fieldName)
        {
            if (_parser != null) {
                StopCoroutine(_parser);
                _parser = null;
            }
            
            foreach (NodePort port in graph.current.Ports) {
                if (port.fieldName != fieldName) continue;
                if (!port.IsConnected) {
                    throw new Exception($"No node found at port \"{fieldName}\"");
                }
                graph.current = port.Connection.node as BaseNode;
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
