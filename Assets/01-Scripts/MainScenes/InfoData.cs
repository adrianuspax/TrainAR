using ASP;
using ASP.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.MainScenes
{
    public class InfoData : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private Button button;
        [SerializeField, ReadOnly] private CanvasGroup canvasGroup;
        [SerializeField, ReadOnly] private Image background;
        [SerializeField, ReadOnly] private TextMeshProUGUI titleHeaderTMP;
        [SerializeField, NonReorderable, ReadOnly] private Transform[] cells;
        [SerializeField, NonReorderable, ReadOnly] private TextMeshProUGUI[] infoTMPs;
        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            this.GetComponentIfNull(ref button);
            this.GetComponentIfNull(ref canvasGroup);
            this.GetComponentInChildrenIfNull(ref background);
            background.GetComponentInChildrenIfNull(ref titleHeaderTMP, 0);

            if (cells.IsNullOrEmpty())
            {
                int count = background.transform.childCount;
                cells = new Transform[count];

                for (int i = 0; i < count; i++)
                {
                    cells[i] = background.transform.GetChild(i);
                }
            }

            if (infoTMPs.IsNullOrEmpty())
            {
                infoTMPs = new TextMeshProUGUI[cells.Length - 1];

                for (int i = 1; i < cells.Length; i++)
                {
                    if (cells[i].GetChild(1).TryGetComponent(out TextMeshProUGUI tmp))
                        infoTMPs[i - 1] = tmp;
                }
            }

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public Button Button => button;
        public CanvasGroup CanvasGroup => canvasGroup;
        public Image Background => background;
        public TextMeshProUGUI TitleHeader => titleHeaderTMP;
        public Transform[] Cells => cells;
        public TextMeshProUGUI[] InfoTMPs => infoTMPs;
    }
}

