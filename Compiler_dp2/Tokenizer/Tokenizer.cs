using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Tokenizer
{
    public class Tokenizer
    {
        public  List<Token>     tokens;
        private Stack<Token>    tokenStack;
        private Token           lastToken;
        private string[]        script;
        private int             currentLevel,       currentLine,    positionInRow;
        private char            currentChar;
        private bool            _pushTokenStack,    _popTokenStack, _errorFound,    generateString;

        public Tokenizer(string file)
        {
           script = System.IO.File.ReadAllLines(@file);
           //Console.WriteLine("Amount of lines to parse:" + script.Length); // Can be removed later.

            tokens          = new List<Token>();
            tokenStack      = new Stack<Token>();
            currentLevel    = 1;

            parse();
        }

        public Token getFirstToken()
        {
            return tokens.First();
        }

        private void parse()
        {

            // Variable
            string text, strLine;

            // Loop through lines
            for (int cLine = 1; cLine <= script.Length; cLine++)
            {
                positionInRow = 0;
                currentLine = cLine;
                strLine = script[cLine - 1];
                text = "";
                generateString = false;
                //Console.WriteLine("Regel " + currentLine + ": " + strLine + " has chars: "+ strLine.Length);

                //string[] ssize = strLine.Split(new char[0]); // Can be used to split string on space.

                // Loop through each char and put them in the token
                for (int cPos = 0; cPos < strLine.Length; cPos++)
                {
                    positionInRow++;
                    // Used switch: it's orders or magnitude quicker than the others here (compared with Dictonary and Hashmap).
                    currentChar = strLine[cPos];

                    // If line starts with ", you've to make a string:
                    if (generateString) {
                        if (currentChar != '"')
                        {
                            text += currentChar;
                        } else
                        {
                            text = getTextType(text, currentLevel, currentLine, positionInRow);
                        }
                    }else
                    {
                        switch (currentChar)
                        {
                            case '\n': // enter
                            case ' ':
                                text = getTextType(text, currentLevel, currentLine, positionInRow);
                               // uitgecomment dit horen geen tokens te zijn. addToken(currentLevel, currentLine, positionInRow, TokenType.String, currentChar.ToString());
                                break;
                            case '(':
                                text = getTextType(text, currentLevel, currentLine, positionInRow);
                                addToken(currentLevel, currentLine, positionInRow, TokenType.EllipsisOpen, currentChar.ToString());
                                pushTokenStack(tokens.Last());
                                currentLevel++;
                                break;
                            case ')':
                                currentLevel--;
                                text = getTextType(text, currentLevel, currentLine, positionInRow);
                                addToken(currentLevel, currentLine, positionInRow, TokenType.EllipsisClose, currentChar.ToString());
                                popTokenStack(tokens.Last());
                                break;
                            case '{':
                                text = ""; // can't have something as this won't be able.
                                addToken(currentLevel, currentLine, positionInRow, TokenType.BracketsOpen, currentChar.ToString());
                                pushTokenStack(tokens.Last());
                                currentLevel++;
                                break;
                            case '}':
                                currentLevel--;
                                text = ""; // can't have something as this won't be able.
                                addToken(currentLevel, currentLine, positionInRow, TokenType.BracketsClose, currentChar.ToString());
                                popTokenStack(tokens.Last());
                                break;
                            case ';':
                                text = getTextType(text, currentLevel, currentLine, positionInRow);
                                addToken(currentLevel, currentLine, positionInRow, TokenType.Semicolon, currentChar.ToString());
                                break;
                            case '"':
                                generateString = true;
                                break;                          
                            default:
                                text += currentChar;
                                break;
                        }
                    }
                }
            }

            // Show tokenizer output
            print();            
        }

        private void print()
        {
            foreach (Token t in tokens)
            {
                t.writeLineToken();
            }
        }

        private void pushTokenStack(Token t)
        {
            tokenStack.Push(t);
        }

        private void popTokenStack(Token t)
        {
            if (tokenStack.Count > 0)
            {
                Token partnerToken = tokenStack.Pop();
                if (t.level == partnerToken.level)
                {
                    t.partner = partnerToken;
                    partnerToken.partner = t;
                }
                else if (partnerToken.tokenType == TokenType.IfStatement)
                {
                    if (t.tokenType == TokenType.ElseStatement)
                    {
                        t.partner = partnerToken;
                        partnerToken.partner = t;
                    }
                    else
                    {
                        popTokenStack(t);
                    }
                }
                else
                {
                    throw new Exception_UnableToPopStack("#TZ0002 :: You've got an exception on line " + currentLine + "!");
                }
            }else
            {
                throw new Exception_NoItemsInStack("#TZ0003 :: You've got an exception on line " + currentLine + "!");
            }
        }

        public string getTextType(string text, int currentLevel, int currentLine, int positionInRow)
        {
            text.Trim(); // remove spaces
            _pushTokenStack = false;
            if (text.Length > 0)
            {
                TokenType type;
                int n;

                if (text == "if") { type = TokenType.IfStatement; _pushTokenStack = true; }
                else if (text == "else") { type = TokenType.ElseStatement; _popTokenStack = true; }
                else if (text == "while") { type = TokenType.While; }
                else if (text == "=") { type = TokenType.Equals; }
                else if (text == "+") { type = TokenType.Plus; }
                else if (text == "==") { type = TokenType.EqualsEquals; }
                else if (text == "!=") { type = TokenType.NotEquals; }
                else if (text == ">=") { type = TokenType.GreaterEquals; }
                else if (text == "<=") { type = TokenType.LessEquals; }
                else if (text == "write") { type = TokenType.Function; }
                else if (int.TryParse(text, out n)) { type = TokenType.Number; }
                else if (generateString) { type = TokenType.String; generateString = false; }
                else { type = TokenType.Identifier ; }

                addToken(currentLevel, currentLine, positionInRow, type, text);
            }
              text = ""; // text is saved, so can be cleaned
            return text;
        }

        private void addToken(int level, int currentLine, int positionInRow, TokenType tt, String currentChar)
        {
            Token newToken = new Token();
            newToken.ruleNumber = currentLine;
            newToken.positionInRow = positionInRow;
            newToken.value = currentChar;
            newToken.tokenType = tt; // Not yet used
            newToken.level = level;
            // newToken.partner = ""; // Not yet used
            if (newToken.value != " ") { tokens.Add(newToken);  }
            if (_pushTokenStack) { pushTokenStack(newToken); _pushTokenStack = false; }
            else if(_popTokenStack) { popTokenStack(newToken); _popTokenStack = false; }

            if (lastToken != null && lastToken.nextToken == null) 
            { 
                lastToken.nextToken = newToken; 
            }

            lastToken = newToken;
        }
    }
}
