using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    [SerializeField] private int NumberChestRemoved = 2;
    private void createNewArrays()  
    {
        
        //LISTA QUE CONTENDRA LOS INDEX DE LOS CHEST POR BORRAR
        List<int> EnemiesDespawn = new List<int>();
        
        var rand = new System.Random();
        
        //AGREGAMOS EL NUMERO QUE QUEREMOS DESPAWNEAR A LA LISTA
        for (int i = 0; i < NumberChestRemoved; i++)
        {
            int number;
            //ESTO ES PARA QUE NO SE REPITAN LOS NUMEROS
            do {
                number = rand.Next(1, 3);
            } while (EnemiesDespawn.Contains(number));
            EnemiesDespawn.Add(number);
        }
        
        //ELIMINAMOS LOS HIJOS DE NUESTRO PADRE
        foreach (Transform child in transform)
        {
            //CHECA SI ESTA EN NUESTRA LISTA RANDOM
            var check = int.Parse(child.gameObject.name);
            if (EnemiesDespawn.Contains(check))
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        createNewArrays();
    }
    
}
