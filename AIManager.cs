using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform[] points;  //diğer scriptlerden erişilebilecek değişken
	public static Transform[] iyiKi;   //diğer scriptlerden erişilebilecek değişken
	public static Transform[] dogdun;  //diğer scriptlerden erişilebilecek değişken
	public static Transform[] seval;  //diğer scriptlerden erişilebilecek değişken
	public static Transform[] idle;  //diğer scriptlerden erişilebilecek değişken
	public static Transform[] kalp;  //diğer scriptlerden erişilebilecek değişken
	public static float repeatTime;    //""
	public static float closeDistance; //AI'ların varacakları yere ne kadar yakınlaştıklarını kontrol ettiren değişken, bu mesafe kadar yaklaştığında yeni yer oluşturulur
	public static float minWait;	   //vardığı yerde bekleyeceği min süre
	public static float maxWait;	   //vardığı yerde bekleyeceği max süre
	public static float minSpeed;	   //gidebileceği min hız
	public static float maxSpeed;      //gidebileceği max hız
	public static float idleCounter;

	[SerializeField] float _repeatTime;		//bu scriptte inspector üzerinden girilebilecek değişken
	[SerializeField] float _closeDistance;  //""
	[SerializeField] float _minWait;		//""
	[SerializeField] float _maxWait;		//""
	[SerializeField] float _minSpeed;		//""
	[SerializeField] float _maxSpeed;		//""
	[SerializeField] Transform[] _points;   //""
	[SerializeField] Transform[] _iyiKi;    //""
	[SerializeField] Transform[] _dogdun;   //""
	[SerializeField] Transform[] _seval;    //""
	[SerializeField] Transform[] _idle;     //""
	[SerializeField] Transform[] _kalp;     //""

	private void Awake()
	{
		repeatTime = _repeatTime;				 //inspector değerini static değişkene ata
		closeDistance = _closeDistance;			 //""
		minWait = _minWait;						 //""
		maxWait = _maxWait;						 //""
		minSpeed = _minSpeed;					 //""
		maxSpeed = _maxSpeed;					 //""
		points = new Transform[_points.Length];  //static değişken için inspector değişkeni kadar array oluştur
		for (int i = 0; i < _points.Length; i++) //inspector değerini static değişkene ata
		{
			points[i] = _points[i];
		}
		iyiKi = new Transform[_iyiKi.Length];
		for (int i = 0; i < _iyiKi.Length; i++)  //inspector değerini static değişkene ata
		{
			iyiKi[i] = _iyiKi[i];
		}
		dogdun = new Transform[_dogdun.Length];
		for (int i = 0; i < _dogdun.Length; i++)  //inspector değerini static değişkene ata
		{
			dogdun[i] = _dogdun[i];
		}
		seval = new Transform[_seval.Length];
		for (int i = 0; i < _seval.Length; i++)  //inspector değerini static değişkene ata
		{
			seval[i] = _seval[i];
		}
		idle = new Transform[_idle.Length];
		for (int i = 0; i < _idle.Length; i++)  //inspector değerini static değişkene ata
		{
			idle[i] = _idle[i];
		}
		kalp = new Transform[_kalp.Length];
		for (int i = 0; i < _kalp.Length; i++)  //inspector değerini static değişkene ata
		{
			kalp[i] = _kalp[i];
		}
	}
}
