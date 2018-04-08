using UnityEngine;
using System.Collections;
using Code;

public class NeuralNetHeap {

	private NeuralNet[] array;
	private int heapSize;
	private NeuralNetComparator comp;

	/**
     * Uuden taulukon luonti on tila- ja aikavaativuudeltaan O(n).
     *
     * @param size tarvittavan keon koko
     * @param comp Comparator joka päättää, kekoavainten suuruusjärjestyksen.
     */
	public NeuralNetHeap(int size, NeuralNetComparator comp) {
		this.heapSize = 0;
		this.array = new NeuralNet[size];
		this.comp = comp;
	}

	public NeuralNet[] getArray() {
		return array;
	}

	public NeuralNetComparator getComp() {
		return comp;
	}

	public int getHeapSize() {
		return heapSize;
	}

	public NeuralNet peek() {
		return array[0];
	}

	public int left(int i) {
		return i * 2;
	}

	public int right(int i) {
		return i * 2 + 1;
	}

	public int parent(int i) {
		return i / 2;
	}

	public void heapify(int i) {
		int l = left(i);
		int r = right(i);
		if (r <= this.heapSize) {
			int largest;
			if (comp.Compare(array[l - 1], array[r - 1]) < 0) {
				largest = l;
			} else {
				largest = r;
			}
			if (comp.Compare(array[i - 1], array[largest - 1]) > 0) {
				swap(i, largest);
				heapify(largest);
			}
		} else if (l == this.heapSize && comp.Compare(array[i - 1], array[l - 1]) > 0) {
			swap(i, l);
		}
	}

	public void swap(int a, int b) {
		NeuralNet temp = array[a - 1];
		array[a - 1] = array[b - 1];
		array[b - 1] = temp;
	}

	public void heapInsert(NeuralNet net) {
		if (this.heapSize >= this.array.Length) {
			growArray();
		}
		this.heapSize = this.heapSize + 1;
		int i = this.heapSize;
		while (i > 1 && comp.Compare(net, this.array[parent(i) - 1]) < 0) {
			this.array[i - 1] = this.array[parent(i) - 1];
			i = parent(i);
		}
		this.array[i - 1] = net;
	}

	public void growArray() {
		NeuralNet[] newArray = new NeuralNet[array.Length * 2];
		for (int i = 0; i < array.Length; i++) {
			newArray[i] = array[i];
		}
		this.array = newArray;
	}

	public NeuralNet heapDelmax() {
		if (this.heapSize == 0) {
			return null;
		}
		NeuralNet max = this.array[0];
		this.array[0] = this.array[this.heapSize - 1];
		this.heapSize = this.heapSize - 1;
		heapify(1);
		return max;
	}

	public bool isEmpty() {
		return this.heapSize == 0;
	}
	// Use this for initialization

}
