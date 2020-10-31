using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidCsharp
{
    public delegate int FuncDele(List<string> args);
    public class SquidCsharpLib
    {
        public struct CommandInfo
        {
            public FuncDele commandFunction;
            public int argcMin, argcMax;
            public List<string> argsPatterns;  //Set to empty will disable patterns check
        }
        public List<string> convert(string _buf)
        {
            _buf.Trim();
            _buf += " \n";
            List<string> tmp = new List<string>();
            string strtmp = "";
            int state = 0;  /*  state==0 scanning blank chars (spqce)
                            state==1 scanning strings between two spaces
                            state==2 scanning strings between two quotes
                            */
            int _counter = 0;
            foreach (char elem in _buf)
            {
                if (elem == '\n')
                {
                    tmp.Add(strtmp);
                    break;
                }

                switch (state)
                {
                    case 0:
                        switch (elem)
                        {
                            case ' ':
                                break;
                            case '"':
                                state = 2;
                                break;
                            default:
                                state = 1;
                                strtmp += elem;
                                break;
                        }
                        break;
                    case 1:
                        if (elem == ' ')
                        {
                            tmp.Add(strtmp);
                            strtmp = "";
                            state = 0;
                        }
                        else
                        {
                            strtmp += elem;
                        }
                        break;
                    case 2:
                        if (elem == '"')
                        {
                            if (_buf[_counter + 1] != ' ' && _buf[_counter + 1] != '\n')
                            {
                                strtmp = "\"" + strtmp;
                                strtmp += elem;
                                state = 1;
                            }
                            else
                            {
                                tmp.Add(strtmp);
                                strtmp = "";
                                state = 0;
                            }
                        }
                        else
                        {
                            strtmp += elem;
                        }
                        break;
                    default:
                        break;
                }

                _counter++;
            }
            return tmp;
        }
    }
    public class SquidCoreStates
    {
        static Dictionary<string, SquidCsharpLib.CommandInfo> commandRegistry = new Dictionary<string, SquidCsharpLib.CommandInfo>();
        public class CommandContainer
        {
            public List<string> argList = new List<string>();
            public int Run()
            {
                if (commandRegistry.ContainsKey(argList[0]))
                {
                    if (true) 
                    {
                        return commandRegistry[argList[0]].commandFunction(argList);
                    }

                }
                else
                {
                    return 0;
                    //throw Unknown Command;
                }
            }
        }
    }
    
}
