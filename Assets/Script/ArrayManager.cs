using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class ArrayManager : MonoBehaviour
    {
        [SerializeField] Unit unit;
        [SerializeField] Unit[] units;
        [SerializeField] Text arraySize;

        [SerializeField] bool sortingInProgress;
        [SerializeField] Text info;
        [SerializeField] Slider slider;


        // Start is called before the first frame update
        void Start()
        {
            sortingInProgress = false;
            var size = int.Parse(arraySize.text);
            GenerateUnits(size);
            
            
            

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void UpSizing()
        {
            if (!sortingInProgress)
            {
                var size = int.Parse(arraySize.text);
                if (size < 20)
                {
                    size++;
                    arraySize.text = size + "";
                    DestroyUnits();
                    GenerateUnits(size);
                }
            }
            
        }


        public void SortSpeed()
        {
            Time.timeScale = slider.value;
        }
        
        public void DownSizing()
        {
            if (!sortingInProgress)
            {
                var size = int.Parse(arraySize.text);
                if (size > 5)
                {
                    size--;
                    arraySize.text = size + "";
                    DestroyUnits();
                    GenerateUnits(size);
                }
            }
        }


        public void GenerateUnits(int size)
        {
            units = new Unit[size];
            int startPoint = -size; 
            for (int i = 0; i < size; i++)
            {
                var clone = Instantiate(unit, new Vector3(startPoint,0,0), Quaternion.identity);
                startPoint+=2;
                units[i] = clone;
            }
        }

        public void DestroyUnits()
        {
            foreach (Unit u in units)
            {
                Destroy(u.gameObject);
            }
        }



        public void BubbleSort()
        {
            if (!sortingInProgress)
            {
                sortingInProgress = true;
                StartCoroutine(BubbleSortCoroutine());
            }

        }

        public IEnumerator BubbleSortCoroutine()
        {
            info.text = "Bubble Sorting in progress, Worst complexity: n^2, Average complexity: n^2, Best complexity: n, Space complexity: 1";
            for (int j = 0; j <= units.Length - 2; j++)
            {
                for (int i = 0; i <= units.Length - 2; i++)
                {
                    //Compare >> go up 2 points
                    units[i].SetDestinationUpDown(Vector3.up * 2);
                    units[i+1].SetDestinationUpDown(Vector3.up * 2);
                    yield return new WaitUntil(() => units[i].Stopped && units[i + 1].Stopped);
                    //Compare value
                    if(units[i].Value> units[i + 1].Value)
                    {
                        //Do Swap
                        var unit1 = units[i].transform.position; 
                        var unit2 = units[i+1].transform.position; 
                        units[i].SetDestination(unit2);
                        units[i + 1].SetDestination(unit1);
                        yield return new WaitUntil(() => units[i].Stopped && units[i + 1].Stopped);
                        
                        units[i].SetDestinationUpDown(Vector3.down * 2);
                        units[i + 1].SetDestinationUpDown(Vector3.down * 2);
                        yield return new WaitUntil(() => units[i].Stopped && units[i + 1].Stopped);
                        var temp = units[i + 1];
                        units[i + 1] = units[i];
                        units[i] = temp;
                    }
                    else
                    {
                        units[i].SetDestinationUpDown(Vector3.down * 2);
                        units[i + 1].SetDestinationUpDown(Vector3.down * 2);
                        yield return new WaitUntil(() => units[i].Stopped && units[i + 1].Stopped);
                    }                    
                }
            }
            sortingInProgress = false;
            info.text = "Bubble Sort finished";

        }

        public void SelectionSort()
        {
            if (!sortingInProgress)
            {
                sortingInProgress = true;
                StartCoroutine(SelectionSortCoroutine());
            }

        }

        public IEnumerator SelectionSortCoroutine()
        {
            info.text = "Selection Sorting in progress, Worst complexity: n^2, Average complexity: n^2, Best complexity: n^2, Space complexity: 1";
            int smallest;
            for (int i = 0; i < units.Length - 1; i++)
            {
                smallest = i;
                for (int j = i + 1; j < units.Length; j++)
                {
                    //Compare 
                    units[j].SetDestinationUpDown(Vector3.up * 2);
                    units[smallest].SetDestinationUpDown(Vector3.up * 2);
                    yield return new WaitUntil(() => units[j].Stopped && units[smallest].Stopped);
                    units[j].SetDestinationUpDown(Vector3.down * 2);
                    units[smallest].SetDestinationUpDown(Vector3.down * 2);
                    yield return new WaitUntil(() => units[j].Stopped && units[smallest].Stopped);
                    if (units[j].Value < units[smallest].Value)
                    {
                        smallest = j;
                    }
                }
                if(i!= smallest)
                {
                    units[i].SetDestinationUpDown(Vector3.up * 2);
                    units[smallest].SetDestinationUpDown(Vector3.up * 2);
                    yield return new WaitUntil(() => units[i].Stopped && units[smallest].Stopped);

                    //Do Swap
                    var unit1 = units[i].transform.position;
                    var unit2 = units[smallest].transform.position;
                    units[i].SetDestination(unit2);
                    units[smallest].SetDestination(unit1);
                    yield return new WaitUntil(() => units[i].Stopped && units[smallest].Stopped);

                    units[i].SetDestinationUpDown(Vector3.down * 2);
                    units[smallest].SetDestinationUpDown(Vector3.down * 2);
                    yield return new WaitUntil(() => units[i].Stopped && units[smallest].Stopped);

                    var temp = units[smallest];
                    units[smallest] = units[i];
                    units[i] = temp;


                }               

            }
            info.text = "Selection Sort finished";

            sortingInProgress = false;
        }

        public void InsertionSort()
        {
            if (!sortingInProgress)
            {
                sortingInProgress = true;
                StartCoroutine(InsertionSortCoroutine());
            }

        }


        public IEnumerator InsertionSortCoroutine()
        {
            info.text = "Insertion Sorting in progress, Worst complexity: n^2, Average complexity: n^2, Best complexity: n, Space complexity: 1";
            for (int i = 0; i < units.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    //Compare >> go up 2 points
                    units[j-1].SetDestinationUpDown(Vector3.up * 2);
                    units[j].SetDestinationUpDown(Vector3.up * 2);
                    yield return new WaitUntil(() => units[j-1].Stopped && units[j].Stopped);

                    //Compare value
                    if (units[j-1].Value > units[j].Value)
                    {
                        //Do Swap
                        var unit1 = units[j-1].transform.position;
                        var unit2 = units[j].transform.position;
                        units[j-1].SetDestination(unit2);
                        units[j].SetDestination(unit1);
                        yield return new WaitUntil(() => units[j-1].Stopped && units[j].Stopped);

                        units[j-1].SetDestinationUpDown(Vector3.down * 2);
                        units[j].SetDestinationUpDown(Vector3.down * 2);
                        yield return new WaitUntil(() => units[j].Stopped && units[j-1].Stopped);
                        var temp = units[j-1];
                        units[j - 1] = units[j];
                        units[j] = temp;
                    }
                    else
                    {
                        units[j].SetDestinationUpDown(Vector3.down * 2);
                        units[j-1].SetDestinationUpDown(Vector3.down * 2);
                        yield return new WaitUntil(() => units[j].Stopped && units[j-1].Stopped);
                    }
                    
                }
            }
            info.text = "Insertion Sort finished";
            sortingInProgress = false;
        }



    }
} 