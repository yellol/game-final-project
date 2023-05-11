using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
    {
        //I create the UI entirely through script so I can dynamically change it when the player gains more and more hp, items, etc.
        //I feel like it is less of a hassle to get the references this way rather than pre-creating them in the canvas
        
        //REMINDER: add shop ui and item uis soon
        
        public Canvas mainCanvas;
        public Image[] healthPool;
        public Image[] itemPool;
        public TMP_Text waveText;

        public Image heart;
        public Image item;
        
        public GameObject player;
        private Sprite[] _heartAll;
        private Sprite _heartFilled;
        private Sprite _heartHalf;
        private Sprite _heartEmpty;
        private GameManager _gm; //to grab wisp info etc.
        private Player _player;

        void Awake()
        {
            _gm = transform.parent.gameObject.GetComponent<GameManager>();
            _player = player.GetComponent<Player>();
            _heartAll = Resources.LoadAll<Sprite>("Art/UI/Heart");
            _heartEmpty = _heartAll[0];
            _heartFilled = _heartAll[1];
            _heartHalf = _heartAll[2];
            InitializeUI();
        }

        public void InitializeUI()
        {
            //initialize health pool
            healthPool = new Image[_player.maxHealthPoints / 2];
            for (int i = 0; i < _player.maxHealthPoints / 2; i++)
            {
                var h = Instantiate(heart, mainCanvas.transform, true);
                healthPool[i] = h; 
                healthPool[i].rectTransform.anchoredPosition = new Vector2((i*35) - ((_player.maxHealthPoints / 4)*35) + (_player.maxHealthPoints%4==0 ? 17.5f : 0), -230);
                //new Vector2((i*30) - ((_player.maxHealthPoints / 4f)*30), -200);
                //distributes created hearts 
            }

            if (_gm.ItemList.Count > 0)
            {
                itemPool = new Image[_gm.ItemList.Count];
                for (int o = 0; o < _gm.ItemList.Count; o++)
                {
                    var itm = Instantiate(item, mainCanvas.transform, true);
                    itemPool[o] = itm;
                    itemPool[o].sprite = _gm.ItemList[o].GetComponent<Item>().itemSprite;
                    itemPool[o].rectTransform.anchoredPosition =
                        new Vector2((o * 32) - ((_gm.ItemList.Count / 2) * 32) + (_gm.ItemList.Count % 2 == 0 ? 16 : 0), -270);
                    //distributes created items
                }
            }

            waveText.text = "Wave " + _gm.wave;
        }

        public void RefreshUI()
        {
            if (healthPool.Length < _player.maxHealthPoints / 2 || itemPool.Length < _gm.ItemList.Count)
            {
                ReinitializeUI();
            }

            int hpCount = 0;
            for (int j = 0; j < _player.maxHealthPoints / 2; j++) //draws current hp
            {
                hpCount += 2;
                healthPool[j].gameObject.SetActive(true); 
                if (_player.healthPoints >= hpCount)
                {
                    healthPool[j].sprite = _heartFilled;
                } 
                else if (_player.healthPoints + 1 == hpCount)
                {
                    healthPool[j].sprite = _heartHalf;
                }
                else
                {
                    healthPool[j].sprite = _heartEmpty;
                }
            }

            for (int k = 0; k < _gm.ItemList.Count; k++)
            {
                itemPool[k].gameObject.SetActive(true); 
            }

        }

        private void ReinitializeUI() //initializing the UI without deleting the old ones left a mess of inactive ui objects 
        {
            for (int w = 0; w < (_player.maxHealthPoints / 2) - 1; w++) 
            {
                if (healthPool[w].gameObject)
                {
                    Destroy(healthPool[w].gameObject);
                }
            }
            for (int v = 0; v < _gm.ItemList.Count-1; v++)
            {
                if (itemPool[v].gameObject)
                {
                    Destroy(itemPool[v].gameObject);
                }
            }
            InitializeUI();
        }

        private void HideUI()
        {
            waveText.text = "";
            for (int l = 0; l < _player.maxHealthPoints / 2; l++)
            {
                healthPool[l].gameObject.SetActive(false); 
                //hides hearts when shop is open
            }
            
            for (int s = 0; s < _gm.ItemList.Count; s++)
            {
                itemPool[s].gameObject.SetActive(false); 
                //hides items as well
            }
        }

        public void RefreshWave() //more singular so it doesnt need to update hp if you dont get hit, etc.
        {
            waveText.text = "Wave " + _gm.wave;
        }

        public void OpenPanel(GameObject panel)
        {
            _player.SwitchInputState();
            if (!panel.activeSelf)
            {
                HideUI();
                panel.SetActive(true);
            }
            else
            {
                RefreshUI();
                RefreshWave();
                panel.SetActive(false);
            }
        }
    }
