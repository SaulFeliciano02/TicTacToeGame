using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleBoardControl : MonoBehaviour
{
	public GameObject[] SlateGameObjects;
	public int myTurn; //0 = x, 1 = O
	public int turnCount; //Conteo de turnos en el juego
	public GameObject myPawn; //figura X
	public GameObject cpuPawn; //figura O
	public TMPro.TextMeshProUGUI turnText; //Texto que indica de quien es el turno
	public int[] markedSlates; //guarda el indice de los cuadros marcados
	public int scorePlayer;
	public int scoreCPU;
	public TMPro.TextMeshProUGUI txtScorePlayer;
	public TMPro.TextMeshProUGUI txtScoreCPU;
	public bool gameIsFinished;
	private int randomSlateIndex;
	private System.Random random;

	// Start is called before the first frame update
	void Start()
    {
		Vector3[] positions = new Vector3[] {
			new Vector3(-1.0f, 1.0f),
			new Vector3(0f, 1.0f),
			new Vector3(1.0f, 1.0f),

			new Vector3(-1.0f, 0.0f),
			new Vector3(0f, 0.0f),
			new Vector3(1.0f, 0.0f),

			new Vector3(-1.0f, -1.0f),
			new Vector3(0f, -1.0f),
			new Vector3(1.0f, -1.0f),
		};

		int i = 0;

		foreach (GameObject slate in SlateGameObjects)
        {	
			slate.transform.parent = gameObject.transform;
			slate.transform.position = positions[i];
			i++;
		}

		GameObject pawnLeft = Instantiate(myPawn);
		pawnLeft.transform.parent = SlateGameObjects[0].transform;
		pawnLeft.transform.position = new Vector3(-3.0f, 1.0f);
		pawnLeft.transform.localScale = SlateGameObjects[0].transform.localScale;

		GameObject pawnRight = Instantiate(cpuPawn);
		pawnRight.transform.parent = SlateGameObjects[2].transform;
		pawnRight.transform.position = new Vector3(3.0f, 1.0f);
		pawnRight.transform.localScale = SlateGameObjects[2].transform.localScale;

		scorePlayer = 0;
		scoreCPU = 0;

		random = new System.Random();

		GameSetup();
	}

	void GameSetup()
    {
		myTurn = 0;
		turnCount = 0;
		turnText.text = "My Turn";
		gameIsFinished = false;

		foreach(GameObject slate in SlateGameObjects)
        {
			if(slate.transform.childCount > 0)
            {
				foreach(Transform child in slate.transform)
                {
					GameObject.Destroy(child.gameObject);
				}
            }

			slate.tag = "NONE";
        }

		for(int i = 0; i < markedSlates.Length; i++)
        {
			markedSlates[i] = -100;
        }
    }
	

	// Update is called once per frame
	void Update()
    {
		
	}


	public void SlateClicked(int index)
    {
		if (Input.GetMouseButtonDown(0) && !gameIsFinished)
		{
			GameObject clickedSlate = SlateGameObjects[index];

			if (clickedSlate.tag == "NONE")
            {
				GameObject pawnGO;

				if (myTurn == 0)
				{
					pawnGO = Instantiate(myPawn);
					clickedSlate.tag = "MINE";
				}
				else
				{
					pawnGO = Instantiate(cpuPawn);
					clickedSlate.tag = "CPUs";
				}

				markedSlates[index] = myTurn + 1;

				pawnGO.transform.parent = clickedSlate.transform;
				pawnGO.transform.localPosition = new Vector3();

				turnCount++;

				if(turnCount > 4)
                {
					WinnerCheck();
                }

				if (myTurn == 0)
				{
					myTurn = 1;
					if(!gameIsFinished)
                    {
						turnText.text = "CPU Turn";
						CPUGame();
					}
					
				}
				else
				{
					myTurn = 0;
					if(!gameIsFinished)
                    {
						turnText.text = "My Turn";
					}
					
				}
            }
			
		}
	}

	void WinnerCheck()
    {
		int solution1 = markedSlates[0] + markedSlates[1] + markedSlates[2]; 
		int solution2 = markedSlates[3] + markedSlates[4] + markedSlates[5]; 
		int solution3 = markedSlates[6] + markedSlates[7] + markedSlates[8]; 
		int solution4 = markedSlates[0] + markedSlates[3] + markedSlates[6]; 
		int solution5 = markedSlates[1] + markedSlates[4] + markedSlates[7]; 
		int solution6 = markedSlates[2] + markedSlates[5] + markedSlates[8]; 
		int solution7 = markedSlates[0] + markedSlates[4] + markedSlates[8]; 
		int solution8 = markedSlates[2] + markedSlates[4] + markedSlates[6];
		var solutions = new int[] { solution1, solution2, solution3, solution4, solution5, solution6, solution7, solution8 };
        
		for (int i = 0; i < solutions.Length; i++)
		{
			if (solutions[i] == 3 * (myTurn + 1))
			{
				if (myTurn == 0)
				{
					turnText.text = "Te ganaste un carro!!!";
					scorePlayer++;
					txtScorePlayer.text = "" + scorePlayer;
				}
				else
				{
					turnText.text = "Diablo manin, ute eh un mielda";
					scoreCPU++;
					txtScoreCPU.text = "" + scoreCPU;
				}

				gameIsFinished = true;
			}
			if(gameIsFinished)
            {
				break;
            }
		}

		if (JuegoEmpatado() && !gameIsFinished)
		{
			gameIsFinished = true;
			turnText.text = "Hay bobop";
		}
	}

	public void Rematch()
    {
		GameSetup();
    }

	public void ReturnMenu()
    {
		SceneManager.LoadScene("MainMenu");
	}

	void CPUGame()
    {
		if(myTurn == 1)
        {
			randomSlateIndex = random.Next(0, 9);

			if(markedSlates[randomSlateIndex] == -100)
            {
				SlateClicked(randomSlateIndex);
            }
            else
            {
				CPUGame();
            }
        }
    }

	bool JuegoEmpatado()
    {
		bool empate = true;
		for(int i = 0; i < markedSlates.Length; i++)
        {
			if(markedSlates[i] == -100)
            {
				empate = false;
            }
        }

		return empate;
    }
}
