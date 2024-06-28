using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class CombatScript : MonoBehaviour
{
    public static CombatScript instance;

    public TextMeshProUGUI turnText;

    [SerializeField] private Transform prefabEnemy,prefabPlayer;
     private Transform enemy,player;

    [SerializeField] private GameObject button;

    [SerializeField] private Vector3 spawnPointEnemy, spawnPointPlayer;

    Animator playerAnimator;
    Animator enemyAnimator;
   
    private bool isAttacking = false;
    private bool isPlayerTurn = true;
    private bool isFightOver = false;

    public float playerHp;
    public float enemyHp;

    public float currentPlayerHp;
    public float currentEnemyHp;

    public int turnCounter;


    TextMeshProUGUI playerCurrentHPText;
    TextMeshProUGUI enemyCurrentHPText;

    public GameObject playerHealthBar, enemyHealthBar;
    public GameObject winLosePanel;
    public GameObject combatObj;
    public Image playerFillBar, enemyFillBar;

    public List<Elixir> waitElixirs = new List<Elixir>();
    public List<Button> waitSkillsButtons = new List<Button>();
    //public List<Skill1> waitSkill1 = new List<Skill1>();
    //public List<Skill2> waitSkill2 = new List<Skill2>();
    //public List<Skill3> waitSkill3 = new List<Skill3>();
    //public List<Skill4> waitSkill4 = new List<Skill4>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentPlayerHp = playerHp;
        currentEnemyHp = enemyHp;

        enemyCurrentHPText = enemyHealthBar.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
        playerCurrentHPText = playerHealthBar.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();

        AssignStartingHpTextValue();
    }

    void Update()
    {
    }


    
    //public void PlayerCombatActions()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        StartCoroutine(Player1stAttack(50));
    //    }
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        StartCoroutine(Player2ndAttack(40));
    //    }
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        StartCoroutine(Player3rdAttack(30));
    //    }
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        StartCoroutine(Player4thAttack(20));
    //    }
        

    //}

    public IEnumerator Player1stAttack(int dmg, Skills Skills)
    {
        if (!isAttacking && isPlayerTurn)
        {
            waitSkillsButtons.Add(Skills.SkillsButtonList[0]);
            // Saldýrý baþladýðýnda bayraðý true yap
            isAttacking = true;

            // player enemye koþar animasyonu çalýþýr
            playerAnimator.SetBool("IsRunning", true);
            player.DOMoveX(spawnPointEnemy.x - 1, 1).OnComplete(() =>
            {
                // player koþma animasyonu durur ve atak animasyonu çalýþýr
                playerAnimator.SetBool("IsRunning", false);
                playerAnimator.SetBool("IsAttacking", true);
                // Attack animasyonu IsClose baðlantýsý ile baðlayan animasyon olacak
            });

            Skills.SkillsButtonList[0].interactable = false;

            yield return new WaitForSeconds(1 + 2);
            // Attack animasyonu Süresi YAzýlacak (Domovex fonksiyonunun saniyesi + Attack animasyonunun saniyesi)
            PlayersDamage(dmg);
            playerAnimator.SetBool("IsTurnBack", true);
            // dönmeden önce idle kýsmýna geçmesi 
            player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
            {
                playerAnimator.SetBool("IsRunning", true);
                playerAnimator.SetBool("IsAttacking", false);
                playerAnimator.SetBool("IsTurnBack", false);
                // Döndükten sonra koþuþ animasyonu
                player.DOMoveX(spawnPointPlayer.x, 1).OnComplete(() =>
                {
                    playerAnimator.SetBool("IsRunning", false);
                    playerAnimator.SetBool("IsComeBack", true);
                    // Tekrar Ýdle pozisyonuna geçmesi
                    player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
                    {
                        playerAnimator.SetBool("IsComeBack", false);
                        // Saldýrý tamamlandýðýnda bayraðý false yap
                        isAttacking = false;
                        isPlayerTurn = false;
                        EndFightCheck();
                        TurnSign();
                        StartCoroutine(EnemyAttack());

                    });
                }
                );
            });
        }
    }
    public IEnumerator Player2ndAttack(int dmg, Button waitSkillsButton)
    {
        if (!isAttacking && isPlayerTurn)
        {
            // Saldýrý baþladýðýnda bayraðý true yap
            isAttacking = true;

            // player enemye koþar animasyonu çalýþýr
            playerAnimator.SetBool("IsRunning", true);
            player.DOMoveX(spawnPointEnemy.x - 1, 1).OnComplete(() =>
            {
                // player koþma animasyonu durur ve atak animasyonu çalýþýr
                playerAnimator.SetBool("IsRunning", false);
                playerAnimator.SetBool("IsAttacking", true);
                // Attack animasyonu IsClose baðlantýsý ile baðlayan animasyon olacak
            });

            waitSkillsButton.interactable = false;

            yield return new WaitForSeconds(1 + 2);
            // Attack animasyonu Süresi YAzýlacak (Domovex fonksiyonunun saniyesi + Attack animasyonunun saniyesi)
            PlayersDamage(dmg);
            playerAnimator.SetBool("IsTurnBack", true);
            // dönmeden önce idle kýsmýna geçmesi 
            player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
            {
                playerAnimator.SetBool("IsRunning", true);
                playerAnimator.SetBool("IsAttacking", false);
                playerAnimator.SetBool("IsTurnBack", false);
                // Döndükten sonra koþuþ animasyonu
                player.DOMoveX(spawnPointPlayer.x, 1).OnComplete(() =>
                {
                    playerAnimator.SetBool("IsRunning", false);
                    playerAnimator.SetBool("IsComeBack", true);
                    // Tekrar Ýdle pozisyonuna geçmesi
                    player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
                    {
                        playerAnimator.SetBool("IsComeBack", false);
                        // Saldýrý tamamlandýðýnda bayraðý false yap
                        isAttacking = false;
                        isPlayerTurn = false;
                        EndFightCheck();
                        TurnSign();
                        StartCoroutine(EnemyAttack());

                    });
                }
                );
            });
        }
    }
    public IEnumerator Player3rdAttack(int dmg, Button waitSkillsButton)
    {
        if (!isAttacking && isPlayerTurn)
        {
            // Saldýrý baþladýðýnda bayraðý true yap
            isAttacking = true;

            // player enemye koþar animasyonu çalýþýr
            playerAnimator.SetBool("IsRunning", true);
            player.DOMoveX(spawnPointEnemy.x - 1, 1).OnComplete(() =>
            {
                // player koþma animasyonu durur ve atak animasyonu çalýþýr
                playerAnimator.SetBool("IsRunning", false);
                playerAnimator.SetBool("IsAttacking", true);
                // Attack animasyonu IsClose baðlantýsý ile baðlayan animasyon olacak
            });

            waitSkillsButton.interactable = false;

            yield return new WaitForSeconds(1 + 2);
            // Attack animasyonu Süresi YAzýlacak (Domovex fonksiyonunun saniyesi + Attack animasyonunun saniyesi)
            PlayersDamage(dmg);
            playerAnimator.SetBool("IsTurnBack", true);
            // dönmeden önce idle kýsmýna geçmesi 
            player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
            {
                playerAnimator.SetBool("IsRunning", true);
                playerAnimator.SetBool("IsAttacking", false);
                playerAnimator.SetBool("IsTurnBack", false);
                // Döndükten sonra koþuþ animasyonu
                player.DOMoveX(spawnPointPlayer.x, 1).OnComplete(() =>
                {
                    playerAnimator.SetBool("IsRunning", false);
                    playerAnimator.SetBool("IsComeBack", true);
                    // Tekrar Ýdle pozisyonuna geçmesi
                    player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
                    {
                        playerAnimator.SetBool("IsComeBack", false);
                        // Saldýrý tamamlandýðýnda bayraðý false yap
                        isAttacking = false;
                        isPlayerTurn = false;
                        EndFightCheck();
                        TurnSign();
                        StartCoroutine(EnemyAttack());

                    });
                }
                );
            });
        }
    }
    public IEnumerator Player4thAttack(int dmg, Button waitSkillsButton)
    {
        if (!isAttacking && isPlayerTurn)
        {
            // Saldýrý baþladýðýnda bayraðý true yap
            isAttacking = true;

            // player enemye koþar animasyonu çalýþýr
            playerAnimator.SetBool("IsRunning", true);
            player.DOMoveX(spawnPointEnemy.x - 1, 1).OnComplete(() =>
            {
                // player koþma animasyonu durur ve atak animasyonu çalýþýr
                playerAnimator.SetBool("IsRunning", false);
                playerAnimator.SetBool("IsAttacking", true);
                // Attack animasyonu IsClose baðlantýsý ile baðlayan animasyon olacak
            });

            waitSkillsButton.interactable = false;

            yield return new WaitForSeconds(1 + 2);
            // Attack animasyonu Süresi YAzýlacak (Domovex fonksiyonunun saniyesi + Attack animasyonunun saniyesi)
            PlayersDamage(dmg);
            playerAnimator.SetBool("IsTurnBack", true);
            // dönmeden önce idle kýsmýna geçmesi 
            player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
            {
                playerAnimator.SetBool("IsRunning", true);
                playerAnimator.SetBool("IsAttacking", false);
                playerAnimator.SetBool("IsTurnBack", false);
                // Döndükten sonra koþuþ animasyonu
                player.DOMoveX(spawnPointPlayer.x, 1).OnComplete(() =>
                {
                    playerAnimator.SetBool("IsRunning", false);
                    playerAnimator.SetBool("IsComeBack", true);
                    // Tekrar Ýdle pozisyonuna geçmesi
                    player.DORotate(player.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
                    {
                        playerAnimator.SetBool("IsComeBack", false);
                        // Saldýrý tamamlandýðýnda bayraðý false yap
                        isAttacking = false;
                        isPlayerTurn = false;
                        EndFightCheck();
                        TurnSign();
                        StartCoroutine(EnemyAttack());

                    });
                }
                );
            });
        }
    }


    public IEnumerator EnemyAttack() 
    {
        if (!isPlayerTurn && !isFightOver)
        {
            // player enemye koþar animasyonu çalýþýr
            enemyAnimator.SetBool("IsRunning", true);
            enemy.DOMoveX(spawnPointPlayer.x + 1, 1).OnComplete(() =>
            {
                // player koþma animasyonu durur ve atak animasyonu çalýþýr
                enemyAnimator.SetBool("IsRunning", false);
                enemyAnimator.SetBool("IsAttacking", true);
                // Attack animasyonu IsClose baðlantýsý ile baðlayan animasyon olacak
            });

            yield return new WaitForSeconds(1 + 2);
            // Attack animasyonu Süresi YAzýlacak (Domovex fonksiyonunun saniyesi + Attack animasyonunun saniyesi)

            EnemysDamage(20);

            enemyAnimator.SetBool("IsTurnBack", true);
            // dönmeden önce idle kýsmýna geçmesi 
            enemy.DORotate(enemy.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
            {
                enemyAnimator.SetBool("IsRunning", true);
                enemyAnimator.SetBool("IsAttacking", false);
                enemyAnimator.SetBool("IsTurnBack", false);
                // Döndükten sonra koþuþ animasyonu
                enemy.DOMoveX(spawnPointEnemy.x, 1).OnComplete(() =>
                {
                    enemyAnimator.SetBool("IsRunning", false);
                    enemyAnimator.SetBool("IsComeBack", true);
                    // Tekrar Ýdle pozisyonuna geçmesi
                    enemy.DORotate(enemy.eulerAngles + (Vector3.up * 180), 1).OnComplete(() =>
                    {
                        enemyAnimator.SetBool("IsComeBack", false);
                        // Saldýrý tamamlandýðýnda bayraðý false yap
                        isPlayerTurn = true;
                        TurnSign();
                        EndFightCheck();
                    });
                }
                );
            });
        }
    }

    public void TurnSign(bool startTurn = false)
    {
        if (!startTurn)
        {
            turnCounter++;
        }
        
        ElixirListCheck();
        if (isPlayerTurn)
        {
            turnText.text = "YOUR TURN";

        }
        else
        {
            turnText.text = "ENEMY TURN";
        }
    }
    public void EndFightCheck()
    {
        if (currentPlayerHp <= 0)
        {
            EndFightPanelAction();
            winLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Lose!";
        }
        else if(currentEnemyHp <= 0)
        {
            EndFightPanelAction();
            winLosePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Victory!";
        }
    }
    public void EndFightPanelAction()
    {
        isFightOver = true;
        winLosePanel.SetActive(true);
        playerHealthBar.SetActive(false);
        enemyHealthBar.SetActive(false);
        turnText.gameObject.SetActive(false);
    }
    public void EnemysDamage(int dmg)
    {
        DecreaseCurrentHpPlayerText(currentPlayerHp, dmg);
        currentPlayerHp = currentPlayerHp - dmg;
        if (currentPlayerHp < 0)
        {
            currentPlayerHp = 0;
        }
        PlayerHpbarAssign();
       
    }
    public void PlayersDamage(int dmg)
    {
        DecreaseCurrentHpEnemyText(currentEnemyHp, dmg);
        currentEnemyHp = currentEnemyHp - dmg;
        if (currentEnemyHp < 0 )
        {
            currentEnemyHp = 0;
        }
        EnemyHpbarAssign();
    }
    public void PlayerHpbarAssign()
    {
        float fillamount = playerFillBar.fillAmount;
        float currentfillamount = currentPlayerHp / playerHp;
        DOTween.To(() => fillamount, x => fillamount = x, currentfillamount, 1).OnUpdate(() =>
        {

            playerFillBar.fillAmount = fillamount;
        });
        
    }
    public void EnemyHpbarAssign()
    {
        float fillamount = enemyFillBar.fillAmount;
        float currentfillamount =  (currentEnemyHp / enemyHp);
        DOTween.To(() => fillamount, x => fillamount = x, currentfillamount, 1).OnUpdate(() =>
        {
            enemyFillBar.fillAmount = fillamount;
        });
    }
    public void ButtonClick()
    {
        enemy = Instantiate(prefabEnemy, spawnPointEnemy, Quaternion.Euler(0, 270, 0));
        player = Instantiate(prefabPlayer, spawnPointPlayer, Quaternion.Euler(0, 90, 0));
        playerAnimator = player.GetComponent<Animator>();
        enemyAnimator = enemy.GetComponent<Animator>();
        TurnSign(true);
        
        button.SetActive(false);
        combatObj.SetActive(true);
        
    }

    public void AssignStartingHpTextValue()
    {
        enemyCurrentHPText.text = currentEnemyHp.ToString("0");
        playerCurrentHPText.text = currentPlayerHp.ToString("0");
    }

    public void DecreaseCurrentHpPlayerText(float currenthp, float dmg)
    {
        float currhp = currenthp;
        float newCurrentHp = currenthp - dmg;
        if (newCurrentHp < 0)
        {
            newCurrentHp = 0;
        }
        DOTween.To(() => currhp, x => currhp = x, newCurrentHp , 1).OnUpdate(() =>
        {
            playerCurrentHPText.text = currhp.ToString("0");
        });
    }
    public void IncreaseCurrentHpPlayerText(float currenthp, float value)
    {
        float currhp = currenthp;
        float newCurrentHp = currenthp + value;
        if (newCurrentHp > playerHp)
        {
            newCurrentHp = playerHp;
        }
        DOTween.To(() => currhp, x => currhp = x, newCurrentHp, 1).OnUpdate(() =>
        {
            playerCurrentHPText.text = currhp.ToString("0");
        });
    }
    public void DecreaseCurrentHpEnemyText(float currenthp, float dmg)
    {
        float currhp = currenthp;
        float newCurrentHp = currenthp - dmg;
        if (newCurrentHp < 0)
        {
            newCurrentHp = 0;
        }
        DOTween.To(() => currhp, x => currhp = x, newCurrentHp, 1).OnUpdate(() =>
        {
            enemyCurrentHPText.text = currhp.ToString("0");
        });
    }


    public IEnumerator ElixirUse(int elixirValue,Elixir elixir)
    {
        if (!isAttacking && isPlayerTurn)
        {
            waitElixirs.Add(elixir);
            isAttacking = true;
            playerAnimator.SetBool("IsUsingElixir", true);
            currentPlayerHp = currentPlayerHp + elixirValue;
            if (currentPlayerHp > playerHp)
            {
                currentPlayerHp = playerHp;
            }
            PlayerHpbarAssign();
            IncreaseCurrentHpPlayerText(currentPlayerHp, elixirValue);
            yield return new WaitForSeconds(2);
            //Animasyon süresi yazýlacak WFS içine
            //---------ElixirÝnteract----------------
            elixir.elixirButton.interactable = false;
            
            //-------------------------------
            playerAnimator.SetBool("IsUsingElixir", false);
            isAttacking = false;
            isPlayerTurn = false;
            TurnSign();
            StartCoroutine(EnemyAttack());
        }
    }

    private void ElixirListCheck()
    {
        for (int i = 0; i < waitElixirs.Count; i++)
        {
            waitElixirs[i].waitTurnCounter++;
            if (waitElixirs[i].waitTurnCounter == waitElixirs[i].elixirRefillTurn * 2)
            {
                waitElixirs[i].waitTurnCounter = 0;
                waitElixirs[i].elixirButton.interactable = true;
            }
        }

        for (int i = 0; i < waitElixirs.Count; i++)
        {
            if (waitElixirs[i].elixirButton.interactable == true)
            {
                waitElixirs.RemoveAt(i);
            }
        }
    }

    private void SkillsListCheck(Button skills)
    {
        //for (int i = 0; i < waitSkillsButtons.Count; i++)
        //{
        //    skills.waitSkillsButtons[i].waitTurnCounter++;
        //    if (waitSkillsButtons[i].waitTurnCounter == waitSkillsButtons[i].elixirRefillTurn * 2)
        //    {
        //        waitSkillsButtons[i].waitTurnCounter = 0;
        //        waitSkillsButtons[i].elixirButton.interactable = true;
        //    }
        //}

        //for (int i = 0; i < waitSkillsButtons.Count; i++)
        //{
        //    if (waitSkillsButtons[i].elixirButton.interactable == true)
        //    {
        //        waitSkillsButtons.RemoveAt(i);
        //    }
        //}
    }
}
