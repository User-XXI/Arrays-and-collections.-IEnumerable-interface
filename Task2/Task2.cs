public class MyList<T>
{
    public MyList()
    {
        array = new T[0];
    }
    public void Add( T item )
    {
        T[] new_array = new T[array.Length + 1];
        Array.Copy( sourceArray: array, destinationArray: new_array, array.Length );
        new_array[new_array.Length - 1] = item;
        array = new_array;
    }
    public void Print()
    {
        for (int i = 0; i < array.Length; ++i)
        {
            Console.WriteLine( $"list[{i}] = {array[i]}" );
        }
    }
    public T this[int index]
    {
        get { return array[index]; }
    }
    public int Lenght
    {
        get { return array.Length; }
    }

    T[] array = null;
}



internal class Task2
{
    static void Main( string[] args )
    {
        MyList<int> list = new MyList<int>();

        list.Add( 1 );
        list.Add( 2 );
        list.Add( 3 );

        list.Print();
        Console.WriteLine( $"list[1] = {list[1]}" );
        Console.WriteLine( $"\nДлина листа {list.Lenght}" );
    }
}
