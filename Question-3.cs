using System;
using System.Text;

namespace Demo1
{
    class Program
    {
        public class BinaryConverter
        {
            int[] positionvalues = { 128, 64, 32, 16, 8, 4, 2, 1 };
            public string ConvertTo(string word)
            {
                string output = "";

                //Iterate over all elements in the array of characters
                for (int i = 0; i < word.Length; i++)
                {
                    string binaryoctet = "";

                    //Get one letter
                    string letter = word.Substring(i, 1);

                    //Convert the letter to a char data type - Converts the character to a Unicode character - takes ASCII value
                    char charletter = System.Convert.ToChar(letter);

                    //Format an output conprised of the combination of all binary bits
                    binaryoctet = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Bit128(charletter),
                        Bit64(charletter),
                        Bit32(charletter),
                        Bit16(charletter),
                        Bit8(charletter),
                        Bit4(charletter),
                        Bit2(charletter),
                        Bit1(charletter)
                        );

                    //Append the binary output to the final output
                    output += binaryoctet;
                }

                return (output);
            }

            public string ConvertBinaryToString(string binaryString)
            {
                string output = "";
                for (int i = 0; i < binaryString.Length; i += 8)
                {
                    string binaryOctet = binaryString.Substring(i, 8);
                    output += ConvertToASCII(binaryOctet);

                }

                return (output);
            }

            public char ConvertToASCII(string binaryvalue)
            {
                string binaryOctet = binaryvalue;
                uint bytevalue = 0;

                for (int c = 0; c < positionvalues.Length; c++)
                {
                    string bit = binaryvalue.Substring(c, 1);
                    bytevalue += bit == "1" ? (uint)positionvalues[c] : 0;
                }

                return ((char)bytevalue);
            }

            public int Bit128(char letter)
            {
                int bit = 128;

                int aa = (int)letter;
                int binaryoutput = ((int)letter) / bit;
                if (binaryoutput < 1)
                {
                    return (0);
                }

                return (1);
            }

            public int Bit64(char letter)
            {
                int bit = 64;

                return (BitValue(PostionalValue(letter, bit)));
            }

            public int Bit32(char letter)
            {
                int bit = 32;

                return (BitValue(PostionalValue(letter, bit)));
            }
            public int Bit16(char letter)
            {
                int bit = 16;

                return (BitValue(PostionalValue(letter, bit)));
            }

            public int Bit8(char letter)
            {
                int bit = 8;

                return (BitValue(PostionalValue(letter, bit)));
            }

            public int Bit4(char letter)
            {
                int bit = 4;

                return (BitValue(PostionalValue(letter, bit)));
            }

            public int Bit2(char letter)
            {
                int bit = 2;

                return (BitValue(PostionalValue(letter, bit)));
            }

            public int Bit1(char letter)
            {
                int bit = 1;

                return (BitValue(PostionalValue(letter, bit)));
            }

            public decimal PostionalValue(char letter, int bit)
            {
                //Get the remainder of ASCII value divided by previous bit value
                decimal modout = ((int)letter) % (bit * 2);

                //Divide the remainder by the current bit value
                decimal binaryoutput = modout / bit;

                return (binaryoutput);
            }

            public int BitValue(decimal value)
            {
                if (value < 1)
                {
                    return (0);
                }

                return (1);
            }
        }
        public class HexadecimalConverter
        {
            int[] positionvalues = { 8, 4, 2, 1 };

            private BinaryConverter _binaryConverter = new BinaryConverter();
            public string ConvertTo(string word)
            {
                string output = "";

                //Iterate over all elements in the array of characters
                for (int i = 0; i < word.Length; i++)
                {
                    //Get one letter
                    string letter = word.Substring(i, 1);

                    //Convert the letter to a char data type
                    char charletter = System.Convert.ToChar(letter);

                    string binaryvalue = _binaryConverter.ConvertTo(letter);
                    string hexvalue = ConvertBinaryToHexadecimal(binaryvalue);

                    //Append the binary output to the final output
                    output += hexvalue;
                }

                return (output);
            }

            public string ConveryFromHexToBinary(string hexvalue)
            {
                string output = "";

                char[] hexparts = hexvalue.ToUpper().ToCharArray();
                for (int i = 0; i < hexvalue.Length; i += 2)
                {
                    int hexpart1ascii = HexPartToASCII(hexparts[i]);
                    int hexpart2ascii = HexPartToASCII(hexparts[i + 1]);

                    output += String.Format("{0}{1}",
                                HexPartToBinary(hexpart1ascii),
                                HexPartToBinary(hexpart2ascii));
                }

                return (output.Trim());
            }


            public string ConveryFromHexToASCII(string hexvalue)
            {
                string output = "";
                string binary = ConveryFromHexToBinary(hexvalue);

                output = _binaryConverter.ConvertBinaryToString(binary);

                return (output.Trim());
            }

            public int HexPartToASCII(char hexpart)
            {
                if (hexpart > 47 && hexpart < 58) //0 - 9 Decimal Value
                {
                    return (System.Convert.ToInt32(hexpart));
                }

                if (hexpart > 64 && hexpart < 71) //A - F Value 10 - 16
                {
                    int ret = hexpart - 55;
                    return (ret);
                }

                return (0);
            }

            public string HexPartToBinary(int asciivalue)
            {
                string output = String.Format("{0}{1}{2}{3}",
                    _binaryConverter.BitValue(PostionalValue(asciivalue, 8)),
                    _binaryConverter.BitValue(PostionalValue(asciivalue, 4)),
                    _binaryConverter.BitValue(PostionalValue(asciivalue, 2)),
                    _binaryConverter.BitValue(PostionalValue(asciivalue, 1)));

                return (output);
            }

            public string ConvertBinaryToHexadecimal(string binaryvalue)
            {
                string output = "";

                if (binaryvalue.Length < 8)
                    throw new Exception("Invalid binary octet, format 00000000");

                string part1 = binaryvalue.Substring(0, 4); //Get first 16 bits of binary set
                string part2 = binaryvalue.Substring(4, 4); //Get second 16 bits of binary set

                string part1product = NumbersToHex(Bit8(part1) + Bit4(part1) + Bit2(part1) + Bit1(part1));
                string part2product = NumbersToHex(Bit8(part2) + Bit4(part2) + Bit2(part2) + Bit1(part2));

                //Format an output conprised of the combination of all binary bits
                output = String.Format("{0}{1}",
                    part1product,
                    part2product
                    );

                return (output);
            }

            private string NumbersToHex(int decimalvalue)
            {
                string output = System.Convert.ToString(decimalvalue);

                if (decimalvalue > 9)
                {
                    char charvalue = (char)(64 + decimalvalue - 9);
                    output = new String(charvalue, 1);

                    return output;
                }

                return (output);
            }

            public int Bit8(string binaryvalue)
            {
                int position = 0;
                int bit = positionvalues[position];

                string part = binaryvalue.Substring(position, 1);

                int value = System.Convert.ToInt32(part);

                if (value == 1)
                    return (bit);

                return (0);
            }

            public int Bit4(string binaryvalue)
            {
                int position = 1;
                int bit = positionvalues[position];

                string part = binaryvalue.Substring(position, 1);

                int value = System.Convert.ToInt32(part);

                if (value == 1)
                    return (bit);

                return (0);
            }

            public int Bit2(string binaryvalue)
            {
                int position = 2;
                int bit = positionvalues[position];

                string part = binaryvalue.Substring(position, 1);

                int value = System.Convert.ToInt32(part);

                if (value == 1)
                    return (bit);

                return (0);
            }

            public int Bit1(string binaryvalue)
            {
                int position = 3;
                int bit = positionvalues[position];

                string part = binaryvalue.Substring(position, 1);

                int value = System.Convert.ToInt32(part);

                if (value == 1)
                    return (bit);

                return (0);
            }

            public decimal PostionalValue(int value, int bit)
            {
                //Get the remainder of ASCII value divided by previous bit value
                decimal modout = value % (bit * 2);

                //Divide the remainder by the current bit value
                decimal binaryoutput = modout / bit;

                return (binaryoutput);
            }

        }
        static void Main(string[] args)
        {
            HexadecimalConverter Hexadecimal = new HexadecimalConverter();
            Console.WriteLine("Enter String:");
            string name = Console.ReadLine();
            string hexval = Hexadecimal.ConvertTo(name);
            Console.WriteLine("Hex value of " + name+ " = " +hexval);
            Encoding encode = System.Text.Encoding.UTF8;
            string hexString = hexval;

            var intlen = (int)(hexString.Length / 2);
            var bytes = new byte[hexString.Length / 2];

            for (var i = 0; i < intlen; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            //command to decode the corresponding UTF8 code of binary value
            string ConvertedStr = encode.GetString(bytes);

            Console.WriteLine("Hex To String = " + ConvertedStr);
            Console.WriteLine("\n");


        }
    }
}



