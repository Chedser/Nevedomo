using UnityEngine;
using UnityEngine.UI;

public class LMLevelPanels : LanguageManager
{

    [Header("Win Panel")]
    [SerializeField]
    Text winTxt;
    [SerializeField]
    Text nextFightButtonTxt;
    [SerializeField]
    Text fightAgainButtonTxt;
    [SerializeField]
    Text exitButtonTxt;

    [Header("Lose Panel")]
    [SerializeField]
    Text loseTxt;
    [SerializeField]
    Text fightAgainLoseButtonTxt;
    [SerializeField]
    Text exitLoseButtonTxt;

    [Header("Escape Panel")]
    [SerializeField]
    Text backdownTxt;
    [SerializeField]
    Text fightButtonBackdownTxt;
    [SerializeField]
    Text exitButtonBackdownTxt;

    [SerializeField]
    GameManager gameManager;

    private void Awake(){

        UpdateLanguageUI();

    }

    protected override void UpdateLanguageUI(){

        if (currentLanguage == Language.EN){

            winTxt.text = "YOU WIN!";

            if (gameManager.isLastLevel)
            {

                nextFightButtonTxt.text = "AVAILABLE IN NEXT UPDATE";

            }
            else {


                nextFightButtonTxt.text = "NEXT FIGHT";

            }
   
            fightAgainButtonTxt.text = "FIGHT AGAIN";
            exitButtonTxt.text = "EXIT";

            loseTxt.text = "YOU LOSE";
            fightAgainLoseButtonTxt.text = "FIGHT AGAIN";
            exitLoseButtonTxt.text = "EXIT";

            backdownTxt.text = "BACK DOWN";
            fightButtonBackdownTxt.text = "FIGHT!";
            exitButtonBackdownTxt.text = "EXIT";

        }
        else{

            winTxt.text = "������!";
            if (gameManager.isLastLevel){

                nextFightButtonTxt.text = "�������� � ����������";

            }
            else{

                nextFightButtonTxt.text = "��������� ���";

            }
            fightAgainButtonTxt.text = "������ ���";
            exitButtonTxt.text = "�����";

            loseTxt.text = "�� ���������";
            fightAgainLoseButtonTxt.text = "������ ���";
            exitLoseButtonTxt.text = "�����";

            backdownTxt.text = "�������";
            fightButtonBackdownTxt.text = "���!";
            exitButtonBackdownTxt.text = "�����";

        }

    }
}
