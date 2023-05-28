using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChestSpawns : MonoBehaviour
{

    [SerializeField] private int NumberChestRemoved = 20;
    private void createNewArrays()
    {
        
        //LISTA QUE CONTENDRA LOS INDEX DE LOS CHEST POR BORRAR
        List<int> ChestDespawns = new List<int>();
        
        var rand = new System.Random();
        
        //AGREGAMOS EL NUMERO QUE QUEREMOS DESPAWNEAR A LA LISTA
        for (int i = 0; i < NumberChestRemoved; i++)
        {
            int number;
            //ESTO ES PARA QUE NO SE REPITAN LOS NUMEROS
            do {
                number = rand.Next(1, 36);
            } while (ChestDespawns.Contains(number));
            ChestDespawns.Add(number);
        }
        
        //ELIMINAMOS LOS HIJOS DE NUESTRO PADRE
        foreach (Transform child in transform)
        {
            //CHECA SI ESTA EN NUESTRA LISTA RANDOM
            var check = int.Parse(child.gameObject.name);
            if (ChestDespawns.Contains(check))
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
