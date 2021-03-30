using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoruBankasi : MonoBehaviour
{
	public Text buton1, buton2, buton3, buton4, soru;
	List<string> sorularList = new List<string>();
	public Balloon balloon;
	string path = "Assets/Sorular/Burak.txt";

	int toplamSoru;
	public void soruCek()
	{
		string[] lines = File.ReadAllLines(path);

		foreach (string line in lines)
		{
			sorularList.Add(line);
		}
		toplamSoru = sorularList.Count;
		toplamSoru = toplamSoru / 6;
		soruYerlestir();
	}

	int soruIndex;
	int basCevap, sonCevap;
	int rastgele, rastgeleSoru;
	List<int> cikanSorular = new List<int>();
	int index;
	int dongu;
	public void soruYerlestir()
	{
		if (cikanSorular.Count == 0)
		{
			rastgeleSoru = Random.Range(1, toplamSoru + 1);
			cikanSorular.Add(rastgeleSoru);
		} else if (cikanSorular.Count == toplamSoru)
		{
			SceneManager.LoadScene(0); //OYUN BİTTİ HEPSİNİ ÇÖZDÜ
		}
		else
		{
			while (cikanSorular.Contains(rastgeleSoru))
			{
				rastgeleSoru = Random.Range(1, toplamSoru + 1);
			}
			cikanSorular.Add(rastgeleSoru);
		}
		for (int i = 0; i < rastgeleSoru; i++)
		{
			if (dongu == 0)
			{
				soruIndex += 5;
			} else
			{
				soruIndex += 6;
			}
			dongu++;
		}
		while (index < 4) 
		{
			basCevap = soruIndex - 4;
			sonCevap = soruIndex - 1;
			do
			{
				rastgele = Random.Range(basCevap, sonCevap + 1);
			}
			while (sorularList[rastgele] == "BOŞ");
			switch (index)
			{
				case 0:
					buton1.text = sorularList[rastgele];
					if (sorularList[rastgele] == sorularList[basCevap])
					{
						dogruCevap = 1;
					}
					break;
				case 1:
					buton2.text = sorularList[rastgele];
					if (sorularList[rastgele] == sorularList[basCevap])
					{
						dogruCevap = 2;
					}
					break;
				case 2:
					buton3.text = sorularList[rastgele];
					if (sorularList[rastgele] == sorularList[basCevap])
					{
						dogruCevap = 3;
					}
					break;
				case 3:
					buton4.text = sorularList[rastgele];
					if (sorularList[rastgele] == sorularList[basCevap])
					{
						dogruCevap = 4;
					}
					break;
			}
			sorularList[rastgele] = "BOŞ";
			soru.text = sorularList[soruIndex - 5];
			index++;
		}
		soruIndex = 0;
		dongu = 0;
		index = 0;
	}


	int dogruCevap;
	public void buton1Tikla()
	{
		if (dogruCevap == 1)
		{
			balloon.havaKaciriyorMu = true;
			StartCoroutine(bekle(0.5f, true));
		} 
		else
		{
			balloon.buyumeHizi += 0.04f;
			StartCoroutine(bekle(0.5f, false));
		}
	}

	public void buton2Tikla()
	{
		if (dogruCevap == 2)
		{
			balloon.havaKaciriyorMu = true;
			StartCoroutine(bekle(0.5f, true));
		}
		else
		{
			balloon.buyumeHizi += 0.04f;
			StartCoroutine(bekle(0.5f, false));
		}
	}

	public void buton3Tikla()
	{
		if (dogruCevap == 3)
		{
			balloon.havaKaciriyorMu = true;
			StartCoroutine(bekle(0.5f, true));
		}
		else
		{
			balloon.buyumeHizi += 0.04f;
			StartCoroutine(bekle(0.5f, false));
		}
	}

	public void buton4Tikla()
	{
		if (dogruCevap == 4)
		{
			balloon.havaKaciriyorMu = true;
			StartCoroutine(bekle(0.5f, true));
		}
		else
		{
			balloon.buyumeHizi += 0.04f;
			StartCoroutine(bekle(0.5f, false));
		}
	}

	IEnumerator bekle(float zaman, bool dogruMu)
	{
		yield return new WaitForSeconds(zaman);
		if (dogruMu)
		{
			StartCoroutine(balloon.bekle(0.5f));
			soruYerlestir();
		}
		else
		{
			StartCoroutine(balloon.bekle2(0.5f));
			soruYerlestir();
		}
	}
}

