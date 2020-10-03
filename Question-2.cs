using System.IO;
using System;
using System.Text;

class Program
{
        static void Main(string[] args)
        {
            //To convert string to binary
            Console.WriteLine("Enter the String = ");
            string s = Console.ReadLine();
           
            //BiOutput prints the final binary output
            string BinOutput = "";
            Console.WriteLine("\n");
            foreach (char c in s)
            {
                string binary = "";
                int no = (int)c;
                while (no > 1)
                {
                    int remainder = no % 2;
                    binary = Convert.ToString(remainder) + binary;
                    no /= 2;
                }
            binary = Convert.ToString(no) + binary;

            //append binary value of each charater to BinOutput
            BinOutput += binary.PadLeft(8, '0');
        }

        Console.WriteLine("bianry = " + BinOutput);
        Console.WriteLine("\n");
        
        //instructions to conevert binary to string

        //encode is used to find the corresponding UTF8 code of each 8 bit binary no: 
        Encoding encode = System.Text.Encoding.UTF8;
        string binString = BinOutput;
        
        var intlen = (int)(binString.Length / 8);
        var bytes = new byte[binString.Length / 8];

        for (var i = 0; i < intlen; i++)
        {
            bytes[i] = Convert.ToByte(binString.Substring(i * 8, 8), 2);
        }

        //command to decode the corresponding UTF8 code of binary value
        string ConvertedStr = encode.GetString(bytes);

        Console.WriteLine("Binary To String = " + ConvertedStr);
        Console.WriteLine("\n");
    }
}
