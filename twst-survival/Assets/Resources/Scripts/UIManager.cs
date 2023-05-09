using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                healthPool[i].rectTransform.anchoredPosition = new Vector2((i*30) - ((_player.maxHealthPoints / 4)*30), -200);
                //distributes created hearts 
            }

            waveText.text = "Wave " + _gm.wave;
        }

        public void RefreshUI()
        {
            if (healthPool.Length < _player.maxHealthPoints / 2)
            {
                InitializeUI();
            }

            int hpCount = 0;
            for (int j = 0; j < _player.maxHealthPoints / 2; j++) //draws current hp
            {
                hpCount += 2;
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
            
        }

        public void RefreshWave() //more singular so it doesnt need to update hp if you dont get hit, etc.
        {
            waveText.text = "Wave " + _gm.wave;
        }
        
    }
