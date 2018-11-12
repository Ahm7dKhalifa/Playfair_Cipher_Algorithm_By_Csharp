using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesar_Cipher_Algorithm_By_Csharp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            //recive the input from the user and convert in to capital
            string key = textBox1.Text.ToUpper();
            string word = textBox2.Text.ToUpper();
            char[] wordEncrypto = new char[word.Length + 2];
            /*##########################################################################
             * 1. working with the key
             *##########################################################################*/
            //get unique chars only from the key
            string keyUnique = getUniqueChars(key);
            //get the The remaining characters from a to z that the key do does not contain it
            string RemainingLetters = getRemainingLetters(keyUnique);
            //set the final word 
            string FinalWord = keyUnique + RemainingLetters;
            //display the first stage result
            label3.Text = keyUnique;
            label5.Text = RemainingLetters;
            label6.Text = FinalWord;

            //create 2d char array 5*5
            char[,] Matrix = new char[5, 5];
            //fill the matrix with FinalWord
            int count = 0;
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(count < 25)
                    {
                        Matrix[i, j] = FinalWord[count];
                        count++;
                    }   
                }
            }
            
            /*##############################################################################
             *   2.working with the word
             *#############################################################################*/
             
             if(word != null)
             {
                //2.1 seperate the similar two char by x
                for (int i = 0; i < word.Length - 1 ; i++ )
                {
                    if( word[i] == word[i+1])
                    {
                        //insert x and shift
                        word = word.Insert(i + 1, "X");
                    }
                    //compare the another two pair
                    i += 2; 
                }
                //2.2 check the length of the word is even or odd
                if(word.Length % 2 != 0)
                {
                    //if the lenght is odd add x in the end of the word
                    word += "X";
                }


                //2.3 encrpto algorithm
                word += "xx"; //to solve the exception that the index is out of range .. if we do not this instruction we will get correct result but without the last two char
                for (int i = 0; i < word.Length - 2   ; i += 2)
                {
                   
                    //for every two pair get the index of x , y
                   
                    List<int> firstXY = returnXY(Matrix, word[i]);
                    
                    List<int> secondXY = returnXY(Matrix, word[i + 1]);
                    //check if they in the same row
                    if(firstXY[0] == secondXY[0])
                    {

                        wordEncrypto[i] = Matrix[firstXY[0], firstXY[1] + 1];
                        wordEncrypto[i + 1] = Matrix[secondXY[0], secondXY[1] + 1];

                      
                        

                       

                    }
                    //check if they in the same col
                    else if (firstXY[1] == secondXY[1])
                    {

                        wordEncrypto[i] = Matrix[firstXY[0] + 1, firstXY[1]];
                        wordEncrypto[i + 1] = Matrix[secondXY[0] + 1, secondXY[1]];


                    }

                     else 
                    {
                        
                        //row is fixed for both and exchange just columns    
                        wordEncrypto[i] = Matrix[firstXY[0], secondXY[1]];
                        wordEncrypto[i + 1] = Matrix[secondXY[0], firstXY[1] ];
                           
                        

                    }

                }
             }
            char[] c = wordEncrypto;
            label9.Text = string.Join(" ", wordEncrypto);

           
        }


        private string getUniqueChars(string key)
        {
            string keyUnique = new String(key.Distinct().ToArray());
            return keyUnique;
        }

        private string getRemainingLetters(string keyUnique)
        {

            string defaultKeySquare = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            string RemainingLetters = "";
            bool j = keyUnique.Contains('J');
            if (j)
            {
                int i = 0;
                while ((i = keyUnique.IndexOf('J', i)) != -1)
                {
                    keyUnique.Replace("J", "I");
                }
            }
            for (int i = 0; i < defaultKeySquare.Length; i++ )
            {
                if(keyUnique.Contains(defaultKeySquare[i]))
                {
                    continue;
                }
                else
                {
                    RemainingLetters += defaultKeySquare[i];
                }
            }

            return RemainingLetters;
        }


        private List<int> returnXY(char[,] Matrix, char ch)
        {
            List<int> XY = new List<int>();
            

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Matrix[i, j] == ch)
                    {
                        XY.Add(i);
                        XY.Add(j);
                       
                        return XY;
                    }
                }

            }
            XY.Add(-1);
            XY.Add(-1);
            return XY;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        
    }
}
