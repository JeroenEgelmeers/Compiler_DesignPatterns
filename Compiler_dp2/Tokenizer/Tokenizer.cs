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
        private int             currentLevel,       currentLine,    positionInRule;
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
                positionInRule = 0;
                currentLine = cLine;
                strLine = script[cLine - 1];
                text = "";
                generateString = false;
                //Console.WriteLine("Regel " + currentLine + ": " + strLine + " has chars: "+ strLine.Length);

                //string[] ssize = strLine.Split(new char[0]); // Can be used to split string on space.

                // Loop through each char and put them in the token
                for (int cPos = 0; cPos < strLine.Length; cPos++)
                {
                    positionInRule++;
                    // Used switch: it's orders or magnitude quicker than the others here (compared with Dictonary and Hashmap).
                    currentChar = strLine[cPos];

                    // If line starts with ", you've to make a string:
                    if (generateString) {
                        if (currentChar != '"')
                        {
                            text += currentChar;
                        } else
                        {
                            text = getTextType(text, currentLevel, currentLine, positionInRule);
                        }
                    }else
                    {
                        switch (currentChar)
                        {
                            case '\n': // enter
                            case ' ':
                                text = getTextType(text, currentLevel, currentLine, positionInRule);
                                addToken(currentLevel, currentLine, positionInRule, TokenType.String, currentChar.ToString());
                                break;
                            case '(':
                                text = getTextType(text, currentLevel, currentLine, positionInRule);
                                addToken(currentLevel, currentLine, positionInRule, TokenType.EllipsisOpen, currentChar.ToString());
                                pushTokenStack(tokens.Last());
                                currentLevel++;
                                break;
                            case ')':
                                currentLevel--;
                                text = getTextType(text, currentLevel, currentLine, positionInRule);
                                addToken(currentLevel, currentLine, positionInRule, TokenType.EllipsisClose, currentChar.ToString());
                                popTokenStack(tokens.Last());
                                break;
                            case '{':
                                text = ""; // can't have something as this won't be able.
                                addToken(currentLevel, currentLine, positionInRule, TokenType.BracketsOpen, currentChar.ToString());
                                pushTokenStack(tokens.Last());
                                currentLevel++;
                                break;
                            case '}':
                                currentLevel--;
                                text = ""; // can't have something as this won't be able.
                                addToken(currentLevel, currentLine, positionInRule, TokenType.BracketsClose, currentChar.ToString());
                                popTokenStack(tokens.Last());
                                break;
                            case ';':
                                text = getTextType(text, currentLevel, currentLine, positionInRule);
                                addToken(currentLevel, currentLine, positionInRule, TokenType.Semicolon, currentChar.ToString());
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

            if (tokenStack.Count != 0)
            {
                // Stack not empty exception
                throw new Exception_StackNotEmpty("#TZ0001 :: The stack should be empty on execute. You did not manage " + tokenStack.Count + " items.");
            }

            // Show tokenizer output
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

        public string getTextType(string text, int currentLevel, int currentLine, int positionInRule)
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
                else if (text == "==") { type = TokenType.EqualsEquals; }
                else if (text == "write") { type = TokenType.Identifier; }
                else if (int.TryParse(text, out n)) { type = TokenType.Number; }
                else if (generateString) { type = TokenType.String; generateString = false; }
                else { type = TokenType.Function ; }
                addToken(currentLevel, currentLine, positionInRule, type, text);
            }
              text = ""; // text is saved, so can be cleaned
            return text;
        }

        private void addToken(int level, int currentLine, int positionInRule, TokenType tt, String currentChar)
        {
            Token newToken = new Token();
            newToken.ruleNumber = currentLine;
            newToken.positionInRule = positionInRule;
            newToken.value = currentChar;
            newToken.tokenType = tt; // Not yet used
            newToken.level = level;
            // newToken.partner = ""; // Not yet used
            if (newToken.value != " ") { tokens.Add(newToken);  }
            if (_pushTokenStack) { pushTokenStack(newToken); _pushTokenStack = false; }
            else if(_popTokenStack) { popTokenStack(newToken); _popTokenStack = false; }

            if (lastToken != null && lastToken.nextToken != null) { lastToken.nextToken = newToken; }
        }
    }
}
