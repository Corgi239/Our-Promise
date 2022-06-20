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
        [SerializeField] private DialogueGraph graph;
        [SerializeField] private Language language;
        [SerializeField] private NarrativeState stateTemplate;
        [SerializeField] private InterfaceElements InterfaceElements;
        // public TextMeshProUGUI speakerNameText;
        // public TextMeshProUGUI narrationLineText;
        // public Image speakerImage;
        // public Image backsplash;
        private Coroutine _parser;
        private NarrativeState _state;
        public void Start()
        {
            ResetGraph();
            DisableUI();
            _state = Instantiate(stateTemplate);
        }

        public void StartDialogue(float delay=0f)
        {
            ResetGraph();
            _parser = StartCoroutine(ParseNode());
        }

        public void StopDialogue()
        {
            if (!_parser.IsUnityNull()) {
                StopCoroutine(_parser);
            }
            DisableUI();
        }

        private IEnumerator ParseNode()
        {
            BaseNode currentNode = graph.current;
            string[] data = currentNode.GetDataString(language, _state).Split('/');
            switch (data[0]) {
                case "StartNode":
                    EnableUI();
                    ToNextNode("exit");
                    break;
                case "EndNode":
                    DisableUI();
                    yield break;
                case "NarrationLineNode":
                    InterfaceElements.speakerNameText.text = data[1];
                    InterfaceElements.narrationLineText.text = data[2];
                    InterfaceElements.speakerImage.sprite = currentNode.GetSprite();
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
                    ToNextNode("exit");
                    break;
                case "ChoiceNode":
                    InterfaceElements.speakerNameText.text = data[1];
                    InterfaceElements.speakerImage.sprite = currentNode.GetSprite();
                    var replies = data.Skip(2).ToArray();
                    InterfaceElements.narrationLineText.text = replies.Select((value, index) => new {value, index})
                        .Aggregate("", (current, reply) => $"{current} [{reply.index + 1}] {reply.value}" + '\n');
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
                case "ConditionNode":
                    switch(data[1])
                    {
                        case "Pass":
                            ToNextNode("pass");
                            break;
                        default: 
                            ToNextNode("fail");
                            break;
                    }
                    break;
                case "FactNode":
                    foreach (string s in data.Skip(1)) {
                        string[] pair = s.Split(":");
                        _state.SetFact(pair[0], Int32.Parse(pair[1]));
                    }
                    ToNextNode("exit");
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
            InterfaceElements.speakerNameText.enabled = false;
            InterfaceElements.narrationLineText.enabled = false;
            InterfaceElements.speakerImage.enabled = false;
            InterfaceElements.backsplash.enabled = false;
        }

        private void EnableUI()
        {
            InterfaceElements.speakerNameText.enabled = true;
            InterfaceElements.narrationLineText.enabled = true;
            InterfaceElements.speakerImage.enabled = true;
            InterfaceElements.backsplash.enabled = true;
        }

    }

    [Serializable]
    sealed class InterfaceElements
    {
        public TextMeshProUGUI speakerNameText;
        public TextMeshProUGUI narrationLineText;
        public Image speakerImage;
        public Image backsplash;
    }
}
