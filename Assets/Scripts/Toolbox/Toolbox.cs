using System;
using System.Collections.Generic;

public class Toolbox : Singleton<Toolbox>
{
    private Dictionary<Type, object> data = new Dictionary<Type, object>();
    
    public static void Add(object obj)
    {
        var add = obj;
        var manager = obj as ManagerBase;
        
        // Добавляет в ToolBox копию менеджера, что позволяет не созранять изменения в процессе игры.
        // Закоммитил т.к. не дает работать ивентам.
        // if (manager != null)
        //     add = Instantiate(manager);
        // else return;
        
        Instance.data.Add(obj.GetType(), add);
        
        if (add is IAwake)
        {
            (add as IAwake).OnAwake();
        }
        
        if (add is IStart)
        {
            (add as IStart).OnStart();
        }
    }

    public static T Get<T>() // вынимаем объект из библиотеки по ТИПУ 
    {
        object resolve;
        Instance.data.TryGetValue(typeof(T), out resolve);
        return (T) resolve;
    }

    public void ClearScene()
    {
		// можно будет использовать для очистке singleton со сцены, например при переходе между сценами
    }
}