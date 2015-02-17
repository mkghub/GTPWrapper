﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GTPWrapper.Sgf {
    /// <summary>
    /// Provides methods to parse a SGF string.
    /// </summary>
    public static class SgfParser {
        /// <summary>
        /// Token types of a SGF string.
        /// </summary>
        public enum TokenType {
            Parenthesis,
            Semicolon,
            PropIdent,
            CValueType
        }

        /// <summary>
        /// Create the token list of a SGF string.
        /// </summary>
        /// <param name="input">The SGF string.</param>
        public static IEnumerable<Tuple<TokenType, string>> Tokenize(string input) {
            StringBuilder builder = new StringBuilder();
            Regex propIdentRegex = new Regex(@"[A-Z]+");

            int i = 0;
            bool inCValueType = false;
            bool inBackslash = false;

            while (i < input.Length) {
                if (!inCValueType) {
                    builder.Clear();
                    inBackslash = false;

                    switch (input[i]) {
                        case '(':
                            yield return Tuple.Create(TokenType.Parenthesis, "(");
                            i++;
                            break;
                        case ')':
                            yield return Tuple.Create(TokenType.Parenthesis, ")");
                            i++;
                            break;
                        case ';':
                            yield return Tuple.Create(TokenType.Semicolon, ";");
                            i++;
                            break;
                        case '[':
                            inCValueType = true;
                            i++;
                            break;
                        default:
                            if (char.IsWhiteSpace(input[i])) {
                                i++;
                                break;
                            }

                            Match m = propIdentRegex.Match(input, i);

                            if (m.Success && m.Index == i) {
                                yield return Tuple.Create(TokenType.PropIdent, m.Value);
                                i += m.Length;
                            } else {
                                throw new ParseException("Unexpected token '" + input[i] + "'");
                            }
                            break;
                    }
                } else {
                    switch (input[i]) {
                        case '\\':
                            if (!inBackslash) {
                                inBackslash = true;
                                i++;
                            } else {
                                goto default;
                            }
                            break;
                        case '\n':
                            if (!inBackslash) goto default;
                            else i++;
                            break;
                        case ']':
                            if (!inBackslash) {
                                inCValueType = false;
                                yield return Tuple.Create(TokenType.CValueType, builder.ToString());
                                i++;
                                break;
                            } else {
                                goto default;
                            }
                        default:
                            inBackslash = false;
                            builder.Append(input[i]);
                            i++;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the corresponding game tree of a certain range in the given token list.
        /// </summary>
        /// <param name="tokens">The token list.</param>
        /// <param name="start">The start index.</param>
        /// <param name="end">The end index.</param>
        public static GameTree Parse(List<Tuple<TokenType, string>> tokens, int start = 0, int end = int.MaxValue) {
            int i = start;
            end = Math.Min(end, tokens.Count - 1);

            GameTree tree = new GameTree();
            Node node = new Node();
            SgfProperty property = new SgfProperty("", "");

            while (i <= end) {
                Tuple<TokenType, string> token = tokens[i];

                if (token.Item1 == TokenType.Semicolon) {
                    node = new Node();
                    tree.Elements.Add(node);
                } else if (token.Item1 == TokenType.PropIdent) {
                    property = new SgfProperty(token.Item2, new List<string>());
                    node.Properties.Add(property);
                } else if (token.Item1 == TokenType.CValueType) {
                    property.Values.Add(token.Item2);
                } else if (token.Item1 == TokenType.Parenthesis && token.Item2 == "(") {
                    break;
                } else if (token.Item1 == TokenType.Parenthesis) {
                    throw new ParseException("Unexpected parenthesis.");
                }

                i++;
            }

            int depth = 0;
            int newstart = 0;

            while (i <= end) {
                Tuple<TokenType, string> token = tokens[i];

                if (token.Item1 == TokenType.Parenthesis && token.Item2 == "(") {
                    depth++;
                    if (depth == 1) newstart = i + 1;
                } else if (token.Item1 == TokenType.Parenthesis && token.Item2 == ")") {
                    depth--;
                    if (depth == 0) tree.SubTrees.Add(Parse(tokens, newstart, i - 1));
                }

                i++;
            }

            return tree;
        }
    }
}
