using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxSize = 32768;
            byte[] data = new byte[maxSize]; //adatszalag
            string program = ">++++[<++++++++>-]>++++++++[>++++<-";
            //string program = ">++++[<++++++++>-]>++++++++[>++++<-]>>++>>>+>>>+<<<<<<<<<<[-[->+<]>[-<+>>>.<<]>>>[[->++++++++[>++++<-]>.<<[->+<]+>[->++++++++++<<+>]>.[-]>]]+<<<[-[->+<]+>[-<+>>>-[->+<]++>[-<->]<<<]<<<<]++++++++++.+++.[-]<]+++++"; //programkód
            int dataPointer = 0;
            int programPointer = 0;


            //data array initialization
            for(int i = 0; i < maxSize; ++i)
            {
                data[i] = 0;
            }

            //basic syntax check (bracket error)
            int depth = 0;
            int j = 0;
            while((j < program.Length) && (depth >= 0))
            {
                if(program[j] == '[')
                {
                    depth++;
                }
                else if(program[j] == ']')
                {
                    depth--;
                }
                j++;
            }
            if(depth != 0) //Wrong bracket order or unmatching brackets
            {
                Console.WriteLine("Syntax error!");
                return;
            }

            while(programPointer < program.Length)
            {
                switch(program[programPointer])
                {
                    case '>':
                        {
                            if (dataPointer == (maxSize - 1))
                            {
                                return; //data pointer out of bounds (upper)
                            }
                            dataPointer++;
                            programPointer++;
                            break;
                        }
                    case '<':
                        {
                            if (dataPointer == 0)
                            {
                                return; //data pointer out of bounds (lower)
                            }       
                            dataPointer--;
                            programPointer++;
                            break;
                        }
                    case '+':
                        {
                            data[dataPointer]++;
                            programPointer++;
                            break;
                        }
                    case '-':
                        {
                            data[dataPointer]--;
                            programPointer++;
                            break;
                        }
                    case '.':
                        {
                            Console.Write(Convert.ToChar(data[dataPointer]));
                            programPointer++;
                            break;
                        }
                    case ',':
                        {
                            string tmp = Console.ReadLine();
                            data[dataPointer] = byte.Parse(tmp);
                            programPointer++;
                            break;
                        }
                    case '[':
                        {
                            if (data[dataPointer] == 0) //step over the iteration
                            {
                                programPointer++;
                                depth = 1;
                                while (depth != 0) //set "programPointer" to the matching ']' character
                                {
                                    if (program[programPointer] == '[') //another iteration found, depth increases
                                    {
                                        depth++;
                                    }
                                    else if (program[programPointer] == ']') //iteration left, depht decreases
                                    {
                                        depth--;
                                    }
                                    programPointer++;
                                }
                            }
                            else
                            {
                                programPointer++;
                            }
                            break;
                        }
                    case ']':
                        {
                            depth = 1;
                            programPointer--;
                            while(depth != 0) //set "programPointer" to the matching '[' character
                            {
                                if (program[programPointer] == ']') //another iteration found, depth increases
                                {
                                    depth++;
                                }
                                else if (program[programPointer] == '[') //iteration left, depht decreases
                                {
                                    depth--;
                                }
                                programPointer--;
                            }
                            programPointer++;
                            break;
                        }
                    default:
                        {
                            programPointer++;
                            break;
                        }
                }
                ;
            }
            ;
        }
    }
}
