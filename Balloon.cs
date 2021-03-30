using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Balloon : MonoBehaviour
{
	public float buyumeHizi = 1f,
		sallamaHizi = 1f,
		rastgeleSayi;
	public GameObject patlamaEfekti, CTPanel;
	public Text countdownText;
	public GameObject[] sesler;
	public SoruBankasi soruBankasi;
	public int a, b;
	public bool havaKaciriyorMu = false, oyunBasladi;

	private void Start()
	{
		InvokeRepeating("geriSay", 0f, 1f);
	}
	private void Update()
	{
		if (oyunBasladi)
		{
			balonBuyut();
			balonSalla();
			if (Input.GetKeyDown(KeyCode.Space))
			{
				havaKaciriyorMu = true;
				soruBankasi.soruYerlestir();
			}

			if (havaKaciriyorMu)
			{
				balonHavaKacir();
			}
		}
	}

	void balonBuyut()
	{
		if (!havaKaciriyorMu)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, 
				new Vector3(20, 20, 1), buyumeHizi * Time.deltaTime);

			#region SallamHızı
			if (transform.localScale.x <= 4)
			{
				sallamaHizi = 0.5f;
			}
			else if (transform.localScale.x <= 6.5f)
			{
				sallamaHizi = 1.5f;
			}
			else if (transform.localScale.x <= 8.5f)
			{
				sallamaHizi = 0.7f;
			}
			else if (transform.localScale.x <= 10)
			{
				sallamaHizi = 0.2f;
			}
			#endregion

			if (transform.localScale.x >= 13)
			{
				balonOldur();
			}
		}
	}
	public void balonHavaKacir()
	{
		if (transform.localScale.x >= 1)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, 
				new Vector3(transform.localScale.x - 1f, transform.localScale.y - 1f, 1f), 
				(buyumeHizi * 50) * Time.deltaTime);
			sesAyarlari(false, true, false);
		}
		StartCoroutine(bekle(0.5f));
	}
	void balonSalla()
	{
		rastgeleSayi = Random.Range(a, b);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.x,
			transform.rotation.y,
			rastgeleSayi)), sallamaHizi * Time.deltaTime);
	}
	void balonOldur()
	{
		sesAyarlari(false, false, true);
		Instantiate(patlamaEfekti, new Vector2(0, 1), Quaternion.identity);
		SceneManager.LoadScene(0);
		Destroy(gameObject);
	}
	

	void sesAyarlari(bool pompaSesi, bool inmeSesi, bool patlamaSesi)
	{
		sesler[0].GetComponent<AudioSource>().enabled = pompaSesi;
		sesler[1].GetComponent<AudioSource>().enabled = inmeSesi;
		sesler[2].GetComponent<AudioSource>().enabled = patlamaSesi;
	}

	public IEnumerator bekle(float zaman)
	{
		yield return new WaitForSeconds(zaman);
		havaKaciriyorMu = false;
		sesler[0].GetComponent<AudioSource>().enabled = true;
		sesler[1].GetComponent<AudioSource>().enabled = false;
	}

	public IEnumerator bekle2(float zaman)
	{
		yield return new WaitForSeconds(zaman);
		buyumeHizi = 0.02f;
	}




	#region AÇMA!
	int zaman = 3;
	void geriSay()
	{
		if (countdownText.text != "Başla!")
		{
			countdownText.text = zaman.ToString();
		}

		if (zaman == 0)
		{
			countdownText.text = "Başla!";
		}
		else if (zaman == -1)
		{
			oyunBasladi = true;
			CTPanel.SetActive(false);
			soruBankasi.soruCek();
		}
		zaman--;
	}
	#endregion
}

