using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
	NavMeshAgent agent;
	int random;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>(); //agenti eşitle
		InvokeRepeating("CheckDistance", 0f, GameManager.repeatTime); //mesafe kontrol voidini 2 saniyede bir çağır, update içinde vector3.distance çağırmak performansı etkiliyor
		random = Random.Range(0, GameManager.points.Length); //ilk pozisyon için random sayı oluştur
		do
		{
			random = Random.Range(0, GameManager.points.Length);
		} while (!GameManager.points[random].gameObject.activeSelf); //sahnede devre dışı olmayan bir objeye erişebilesiye kadar random sayı oluştur
		agent.SetDestination(GameManager.points[random].position); //AI'ın gideceği yeri bulunan objeye ayarla
		GameManager.points[random].gameObject.SetActive(false); //AI'ın gideceği yeri devre dışı bırak, böylelikle diğer AI'lar aynı yere gidemeyecek
		agent.speed = Random.Range(GameManager.minSpeed, GameManager.maxSpeed); //AI'a random hız ver
	}

	float timer;
	bool closeEnough, started, started1, started2, started3;
	bool should;
	bool should1;
	bool should2;
	bool should3;
	bool pause, pause1, pause2, pause3;
	private void Update()
	{
		if (closeEnough && !agent.pathPending && !started) //gideceği yere yaklaşmışsa ve yol bulma aşamasında değilse
		{
			timer += Time.deltaTime;
			if (timer >= Random.Range(GameManager.minWait, GameManager.maxWait)) //gideceği yere geldiğinde belirlenen saniye arası bekle
			{
				//Debug.Log(gameObject.name + " reached its destination and moving to another.");
				GameManager.points[random].gameObject.SetActive(true); //şu an vardığı yeri tekrar etkinleştirerek erişilebilir yap
				random = Random.Range(0, GameManager.points.Length); //yeni yer için random sayı oluştur
				do
				{
					random = Random.Range(0, GameManager.points.Length);
				} while (!GameManager.points[random].gameObject.activeSelf); //sahnede devre dışı olmayan bir objeye erişebilesiye kadar random sayı oluştur
				agent.SetDestination(GameManager.points[random].position); //AI'ın gideceği yeri bulunan objeye ayarla
				GameManager.points[random].gameObject.SetActive(false); //AI'ın gideceği yeri devre dışı bırak, böylelikle diğer AI'lar aynı yere gidemeyecek
				timer = 0; //timerı sıfırla
			}
			else
			{
				//bekliyor durumunda, idle oynatılabilir
			}
		}

		if (Input.GetKeyDown(KeyCode.Keypad0)) ResetIdle();
		if (Input.GetKeyDown(KeyCode.Keypad1)) started = true;
		if (Input.GetKeyDown(KeyCode.Keypad2)) started1 = true;
		if (Input.GetKeyDown(KeyCode.Keypad3)) started2 = true;
		if (Input.GetKeyDown(KeyCode.Keypad4)) started3 = true;
		if (Input.GetKeyDown(KeyCode.Space)) InvokeRepeating("Heart", 0f, 0.01f);

		if (started && !pause)
		{
			if (!should)
			{
				agent.speed = Random.Range(GameManager.minSpeed + 5, GameManager.maxSpeed + 5);
				random = Random.Range(0, GameManager.iyiKi.Length); //yeni yer için random sayı oluştur
				foreach (Transform item in GameManager.iyiKi)
				{
					if (item.gameObject.activeSelf)
					{
						should = true;
					}
				}
				do
				{
					random = Random.Range(0, GameManager.iyiKi.Length);
				} while (!GameManager.iyiKi[random].gameObject.activeSelf && should);
			}//sahnede devre dışı olmayan bir objeye erişebilesiye kadar random sayı oluştur
			if (should)
			{
				agent.SetDestination(GameManager.iyiKi[random].position);
				GameManager.iyiKi[random].gameObject.SetActive(false);
				should = false;
				pause = true;
			}
			else if (!should)
			{
				for (int i = 0; i < GameManager.idle.Length; i++)
				{
					if (GameManager.idle[i].gameObject.activeSelf)
					{
						agent.SetDestination(GameManager.idle[i].position);
						GameManager.idle[i].gameObject.SetActive(false);
						GameManager.idleCounter++;
						break;
					}
				}
				/*random = Random.Range(0, GameManager.idle.Length); 
				do
				{
					random = Random.Range(0, GameManager.idle.Length);
				} while (!GameManager.idle[random].gameObject.activeSelf);
				agent.SetDestination(GameManager.idle[random].position);
				GameManager.idle[random].gameObject.SetActive(false);*/
				pause = true;
			}
		}
		if (started1 && !pause1)
		{
			if (!should1)
			{
				//agent.speed = Random.Range(GameManager.minSpeed + 5, GameManager.maxSpeed + 5);
				random = Random.Range(0, GameManager.dogdun.Length); //yeni yer için random sayı oluştur
				foreach (Transform item in GameManager.dogdun)
				{
					if (item.gameObject.activeSelf)
					{
						should1 = true;
					}
				}
				do
				{
					random = Random.Range(0, GameManager.dogdun.Length);
				} while (!GameManager.dogdun[random].gameObject.activeSelf && should1);
			}//sahnede devre dışı olmayan bir objeye erişebilesiye kadar random sayı oluştur
			if (should1)
			{
				agent.SetDestination(GameManager.dogdun[random].position);
				GameManager.dogdun[random].gameObject.SetActive(false);
				should1 = false;
				pause1 = true;
			}
			else if (!should1)
			{
				for (int i = 0; i < GameManager.idle.Length; i++)
				{
					if (GameManager.idle[i].gameObject.activeSelf)
					{
						agent.SetDestination(GameManager.idle[i].position);
						GameManager.idle[i].gameObject.SetActive(false);
						GameManager.idleCounter++;
						break;
					}
				}
				/*random = Random.Range(0, GameManager.idle.Length);
				do
				{
					random = Random.Range(0, GameManager.idle.Length);
				} while (!GameManager.idle[random].gameObject.activeSelf);
				agent.SetDestination(GameManager.idle[random].position);
				GameManager.idle[random].gameObject.SetActive(false);*/
				pause1 = true;
			}
		}
		if (started2 && !pause2)
		{
			if (!should2)
			{
				//agent.speed = Random.Range(GameManager.minSpeed + 5, GameManager.maxSpeed + 5);
				random = Random.Range(0, GameManager.seval.Length); //yeni yer için random sayı oluştur
				foreach (Transform item in GameManager.seval)
				{
					if (item.gameObject.activeSelf)
					{
						should2 = true;
					}
				}
				do
				{
					random = Random.Range(0, GameManager.seval.Length);
				} while (!GameManager.seval[random].gameObject.activeSelf && should2);
			}//sahnede devre dışı olmayan bir objeye erişebilesiye kadar random sayı oluştur
			if (should2)
			{
				agent.SetDestination(GameManager.seval[random].position);
				GameManager.seval[random].gameObject.SetActive(false);
				should2 = false;
				pause2 = true;
			}
			else if (!should2)
			{
				for (int i = 0; i < GameManager.idle.Length; i++)
				{
					if (GameManager.idle[i].gameObject.activeSelf)
					{
						agent.SetDestination(GameManager.idle[i].position);
						GameManager.idle[i].gameObject.SetActive(false);
						GameManager.idleCounter++;
						break;
					}
				}
				/*random = Random.Range(0, GameManager.idle.Length);
				do
				{
					random = Random.Range(0, GameManager.idle.Length);
				} while (!GameManager.idle[random].gameObject.activeSelf);
				agent.SetDestination(GameManager.idle[random].position);
				GameManager.idle[random].gameObject.SetActive(false);*/
				pause2 = true;
			}
		}
		if (started3 && !pause3)
		{
			if (!should3)
			{
				random = Random.Range(0, GameManager.kalp.Length);
				foreach (Transform item in GameManager.kalp)
				{
					if (item.gameObject.activeSelf)
					{
						should3 = true;
					}
				}
				do
				{
					random = Random.Range(0, GameManager.kalp.Length);
				} while (!GameManager.kalp[random].gameObject.activeSelf && should3);
			}
			if (should3)
			{
				agent.SetDestination(GameManager.kalp[random].position);
				GameManager.kalp[random].gameObject.SetActive(false);				
				should3 = false;
				pause3 = true;
			}
			else if (!should3)
			{
				for (int i = 0; i < GameManager.idle.Length; i++)
				{
					if (GameManager.idle[i].gameObject.activeSelf)
					{
						agent.SetDestination(GameManager.idle[i].position);
						GameManager.idle[i].gameObject.SetActive(false);
						GameManager.idleCounter++;
						break;
					}
				}
				pause3 = true;
			}
		}
	}

	private void CheckDistance()
	{
		closeEnough = Vector3.Distance(transform.position, GameManager.points[random].position) <= GameManager.closeDistance; //AI ve gideceği yer yeterince yakınsa değişkeni true yap
	}

	private void ResetIdle()
	{
		foreach (var item in GameManager.idle)
		{
			item.gameObject.SetActive(true);
		}
		GameManager.idleCounter = 0;
	}

	private void Heart()
	{
		if (!GameManager.kalp[random].gameObject.activeSelf) GameManager.kalp[random].gameObject.SetActive(false);
		agent.SetDestination(GameManager.kalp[random].position);
	}
}
