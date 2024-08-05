using ASP;
using ASP.Extensions;
using System.Collections;
using TMPro;
using TreinAR.UserData.Footers;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Debug = ASP.Debug;

namespace TreinAR.UserData.Pages
{
    public sealed class Page6 : PageDefault
    {
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] private FootersControl footersControl;
        [SerializeField, NonReorderable, ReadOnly] private Button[] buttons;
        [SerializeField, NonReorderable, ReadOnly] private TextMeshProUGUI[] blockDigtTMPs;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private string[] units;

        private readonly string[] _entrys = { "Years", "kg", "kg", "cm" };
        private AsyncOperationHandle<string> _operation;
        private LocalizedStringDatabase _database;
        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;
                buttons[index].onClick.AddListener(() => footersControl.SetCallFooter(index));
            }
        }
        /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        private void Start()
        {
            UnitsAssignment();
            StartCoroutine(_delay());

            IEnumerator _delay()
            {
                yield return new WaitUntil(() => _operation.IsDone);

                for (int i = 0; i < buttons.Length; i++)
                {
                    int index = i;
                    SetSaveInfoValue(index, "--");
                }
            }
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private new void ComponentsAssignment()
        {
            this.GetComponentsInAllChildrenIfNull(ref buttons);
            if (footersControl.IsNull())
                footersControl = FindAnyObjectByType<FootersControl>();
            if (blockDigtTMPs.IsNullOrEmpty())
            {
                int length = buttons.Length;
                blockDigtTMPs = new TextMeshProUGUI[length];

                for (int i = 0; i < length; i++)
                {
                    blockDigtTMPs[i] = buttons[i].transform.parent.GetComponentInChildren<TextMeshProUGUI>();
                }
            }

            base.ComponentsAssignment();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void SetSaveInfoValue(int index, string value)
        {
            int length = buttons.Length;

            if (index < 0 || index >= length)
            {
                Debug.LogError($"The index value must be between 0 and {length}, but {index} has been entered!");
                return;
            }

            blockDigtTMPs[index].text = value + units[index];
        }

        private void UnitsAssignment()
        {
            units = new string[_entrys.Length];
            _database = LocalizationSettings.StringDatabase;

            for (int i = 0; i < _entrys.Length; i++)
            {
                int index = i;
                _operation = _database.GetLocalizedStringAsync("StringTableMain", _entrys[index]);

                _updateString(_operation);

                void _updateString(AsyncOperationHandle<string> operation)
                {
                    if (!operation.IsDone)
                    {
                        operation.Completed += _updateString;
                        return;
                    }

                    units[index] = operation.Result;
                }
            }
        }
    }
}

