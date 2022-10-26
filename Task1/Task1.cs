using System;

public class MyMatrix
{
    public MyMatrix( int row, int col, int MaxValue, int MinValue )
    {
        Random random = new Random();

        Matrix = new int[row, col];

        this.MinValue = MinValue;
        this.MaxValue = MaxValue;

        Fill();
    }

    public void Fill()
    {
        Random random = new Random();

        for (int i = 0; i < Matrix.GetLength( 0 ); i++)
        {
            for (int j = 0; j < Matrix.GetLength( 1 ); j++)
            {
                Matrix[i, j] = random.Next( MinValue, MaxValue );
            }
        }
    }

    public void ChangeSize( int row, int col )
    {
        Random random = new Random();
        int [,] NewMatrix = new int[row, col];

        for (int i = 0; i < row; ++i)
        {
            for (int j = 0; j < col; ++j)
            {
                if (i < Matrix.GetLength( 0 ) && j < Matrix.GetLength( 1 ))
                {
                    NewMatrix[i, j] = Matrix[i, j];
                }
                else
                {
                    NewMatrix[i, j] = random.Next( MinValue, MaxValue );
                }
            }
        }
        Matrix = NewMatrix;
    }

    public void ShowPartialy( int from_row, int to_row, int from_col, int to_col )
    {
        for (int i = from_row; i <= to_row; ++i)
        {
            for (int j = from_col; j <= to_col; ++j)
            {
                Console.Write( $"{Matrix[i, j]}\t" );
            }
            Console.Write( '\n' );
        }
    }
    public void Show()
    {
        ShowPartialy( 0, Matrix.GetLength( 0 ) - 1, 0, Matrix.GetLength( 1 ) - 1 );
    }

    public int this[int i, int j]
    {
        get
        {
            return Matrix[i, j];
        }
        set
        {
            Matrix[i, j] = value;
        }
    }


    int[,] Matrix;
    int MaxValue, MinValue;

    static void Main( string[] args )
    {
        MyMatrix matrix = new MyMatrix(row : 4, col : 3, MaxValue : 10, MinValue : -10);

        Console.WriteLine( "Исходная матрица 4x3: " ); 
        matrix.Show();

        Console.WriteLine( "\nИзменим числа матрицы" ); 
        matrix.Fill(); 
        matrix.Show();

        Console.WriteLine( "\nНовая матрица 2x5" ); 
        matrix.ChangeSize( row: 2, col: 5 ); 
        matrix.Show();

        Console.WriteLine( "\nНовая матрица 5x4: " ); 
        matrix.ChangeSize( row: 5, col: 4 ); 
        matrix.Show();

        Console.WriteLine( "\nРассмотрим строки 1-3 и столбы 1-2" ); 
        matrix.ShowPartialy( from_row: 0, to_row: 2, from_col: 0, to_col: 1 );

        Console.WriteLine( $"\nMatrix[1, 2]: {matrix[1, 2]} заменим на 5" ); 
        matrix[1, 2] = 5; 
        matrix.Show();
    }
}
