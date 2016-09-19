using UnityEngine;
using System.Collections;

public interface IContainer<T>{

	// Use this for initialization
	void Open ();

	void Close ();

	void Take ();

	void Deposit (T item);

}
