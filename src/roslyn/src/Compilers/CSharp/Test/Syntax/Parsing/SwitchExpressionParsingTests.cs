﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;
using Xunit.Abstractions;

namespace Microsoft.CodeAnalysis.CSharp.UnitTests;

public class SwitchExpressionParsingTests : ParsingTests
{
    public SwitchExpressionParsingTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void TestErrantCaseInSwitchExpression1()
    {
        UsingExpression("""
            x switch
            {
                case 0 => 1,
                case 1 => 2,
            }
            """,
            // (3,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 0 => 1,
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(3, 5),
            // (4,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 1 => 2,
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(4, 5));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "1");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantCaseInSwitchExpression1_Semicolons()
    {
        UsingExpression("""
            x switch
            {
                case 0 => 1;
                case 1 => 2;
            }
            """,
            // (3,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 0 => 1;
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(3, 5),
            // (3,16): error CS1003: Syntax error, ',' expected
            //     case 0 => 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 16),
            // (4,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 1 => 2;
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(4, 5),
            // (4,16): error CS1003: Syntax error, ',' expected
            //     case 1 => 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 16));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "1");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantCaseInSwitchExpression2()
    {
        UsingExpression("""
            x switch
            {
                case 0: 1,
                case 1: 2,
            }
            """,
            // (3,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 0: 1,
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(3, 5),
            // (3,11): error CS1003: Syntax error, '=>' expected
            //     case 0: 1,
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(3, 11),
            // (4,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 1: 2,
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(4, 5),
            // (4,11): error CS1003: Syntax error, '=>' expected
            //     case 1: 2,
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 11));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "1");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantCaseInSwitchExpression2_Semicolons()
    {
        UsingExpression("""
            x switch
            {
                case 0: 1;
                case 1: 2;
            }
            """,
            // (3,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 0: 1;
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(3, 5),
            // (3,11): error CS1003: Syntax error, '=>' expected
            //     case 0: 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(3, 11),
            // (3,14): error CS1003: Syntax error, ',' expected
            //     case 0: 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 14),
            // (4,5): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //     case 1: 2;
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(4, 5),
            // (4,11): error CS1003: Syntax error, '=>' expected
            //     case 1: 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 11),
            // (4,14): error CS1003: Syntax error, ',' expected
            //     case 1: 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 14));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "1");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantCaseInSwitchExpression3()
    {
        UsingStatement("""
            {
                var y = x switch
                {
                    case 0:
                        Goo();
                        return Bar;
                    case 1:
                    {
                        Baz();
                        throw new Quux();
                    }
                };
            }
            """,
            // (4,9): error CS9134: A switch expression arm does not begin with a 'case' keyword.
            //         case 0:
            Diagnostic(ErrorCode.ERR_BadCaseInSwitchArm, "case").WithLocation(4, 9),
            // (4,15): error CS1003: Syntax error, '=>' expected
            //         case 0:
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 15),
            // (5,18): error CS1003: Syntax error, ',' expected
            //             Goo();
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(5, 18),
            // (5,19): error CS1513: } expected
            //             Goo();
            Diagnostic(ErrorCode.ERR_RbraceExpected, "").WithLocation(5, 19),
            // (5,19): error CS1002: ; expected
            //             Goo();
            Diagnostic(ErrorCode.ERR_SemicolonExpected, "").WithLocation(5, 19),
            // (6,24): error CS1003: Syntax error, 'switch' expected
            //             return Bar;
            Diagnostic(ErrorCode.ERR_SyntaxError, "").WithArguments("switch").WithLocation(6, 24));
        N(SyntaxKind.Block);
        {
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.LocalDeclarationStatement);
            {
                N(SyntaxKind.VariableDeclaration);
                {
                    N(SyntaxKind.IdentifierName);
                    {
                        N(SyntaxKind.IdentifierToken, "var");
                    }
                    N(SyntaxKind.VariableDeclarator);
                    {
                        N(SyntaxKind.IdentifierToken, "y");
                        N(SyntaxKind.EqualsValueClause);
                        {
                            N(SyntaxKind.EqualsToken);
                            N(SyntaxKind.SwitchExpression);
                            {
                                N(SyntaxKind.IdentifierName);
                                {
                                    N(SyntaxKind.IdentifierToken, "x");
                                }
                                N(SyntaxKind.SwitchKeyword);
                                N(SyntaxKind.OpenBraceToken);
                                N(SyntaxKind.SwitchExpressionArm);
                                {
                                    N(SyntaxKind.ConstantPattern);
                                    {
                                        N(SyntaxKind.NumericLiteralExpression);
                                        {
                                            N(SyntaxKind.NumericLiteralToken, "0");
                                        }
                                    }
                                    M(SyntaxKind.EqualsGreaterThanToken);
                                    N(SyntaxKind.InvocationExpression);
                                    {
                                        N(SyntaxKind.IdentifierName);
                                        {
                                            N(SyntaxKind.IdentifierToken, "Goo");
                                        }
                                        N(SyntaxKind.ArgumentList);
                                        {
                                            N(SyntaxKind.OpenParenToken);
                                            N(SyntaxKind.CloseParenToken);
                                        }
                                    }
                                }
                                M(SyntaxKind.CommaToken);
                                M(SyntaxKind.CloseBraceToken);
                            }
                        }
                    }
                }
                M(SyntaxKind.SemicolonToken);
            }
            N(SyntaxKind.ReturnStatement);
            {
                N(SyntaxKind.ReturnKeyword);
                N(SyntaxKind.IdentifierName);
                {
                    N(SyntaxKind.IdentifierToken, "Bar");
                }
                N(SyntaxKind.SemicolonToken);
            }
            N(SyntaxKind.SwitchStatement);
            {
                M(SyntaxKind.SwitchKeyword);
                M(SyntaxKind.OpenParenToken);
                M(SyntaxKind.IdentifierName);
                {
                    M(SyntaxKind.IdentifierToken);
                }
                M(SyntaxKind.CloseParenToken);
                M(SyntaxKind.OpenBraceToken);
                N(SyntaxKind.SwitchSection);
                {
                    N(SyntaxKind.CaseSwitchLabel);
                    {
                        N(SyntaxKind.CaseKeyword);
                        N(SyntaxKind.NumericLiteralExpression);
                        {
                            N(SyntaxKind.NumericLiteralToken, "1");
                        }
                        N(SyntaxKind.ColonToken);
                    }
                    N(SyntaxKind.Block);
                    {
                        N(SyntaxKind.OpenBraceToken);
                        N(SyntaxKind.ExpressionStatement);
                        {
                            N(SyntaxKind.InvocationExpression);
                            {
                                N(SyntaxKind.IdentifierName);
                                {
                                    N(SyntaxKind.IdentifierToken, "Baz");
                                }
                                N(SyntaxKind.ArgumentList);
                                {
                                    N(SyntaxKind.OpenParenToken);
                                    N(SyntaxKind.CloseParenToken);
                                }
                            }
                            N(SyntaxKind.SemicolonToken);
                        }
                        N(SyntaxKind.ThrowStatement);
                        {
                            N(SyntaxKind.ThrowKeyword);
                            N(SyntaxKind.ObjectCreationExpression);
                            {
                                N(SyntaxKind.NewKeyword);
                                N(SyntaxKind.IdentifierName);
                                {
                                    N(SyntaxKind.IdentifierToken, "Quux");
                                }
                                N(SyntaxKind.ArgumentList);
                                {
                                    N(SyntaxKind.OpenParenToken);
                                    N(SyntaxKind.CloseParenToken);
                                }
                            }
                            N(SyntaxKind.SemicolonToken);
                        }
                        N(SyntaxKind.CloseBraceToken);
                    }
                }
                N(SyntaxKind.CloseBraceToken);
            }
            N(SyntaxKind.EmptyStatement);
            {
                N(SyntaxKind.SemicolonToken);
            }
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantColonsInSwitchExpression1()
    {
        UsingExpression("""
            x switch
            {
                0: 1,
                1: 2,
            }
            """,
            // (3,6): error CS1003: Syntax error, '=>' expected
            //     0: 1,
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(3, 6),
            // (4,6): error CS1003: Syntax error, '=>' expected
            //     1: 2,
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 6));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "1");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantColonsInSwitchExpression1_Semicolons()
    {
        UsingExpression("""
            x switch
            {
                0: 1;
                1: 2;
            }
            """,
            // (3,6): error CS1003: Syntax error, '=>' expected
            //     0: 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(3, 6),
            // (3,9): error CS1003: Syntax error, ',' expected
            //     0: 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 9),
            // (4,6): error CS1003: Syntax error, '=>' expected
            //     1: 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 6),
            // (4,9): error CS1003: Syntax error, ',' expected
            //     1: 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 9));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "1");
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantDefaultInSwitchExpression1()
    {
        UsingExpression("""
            x switch
            {
                0 => 1,
                default: 2,
            }
            """,
            // (4,12): error CS1003: Syntax error, '=>' expected
            //     default: 2,
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 12));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.DefaultLiteralExpression);
                    {
                        N(SyntaxKind.DefaultKeyword);
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantDefaultInSwitchExpression1_Semicolons()
    {
        UsingExpression("""
            x switch
            {
                0 => 1;
                default: 2;
            }
            """,
            // (3,11): error CS1003: Syntax error, ',' expected
            //     0 => 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 11),
            // (4,12): error CS1003: Syntax error, '=>' expected
            //     default: 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 12),
            // (4,15): error CS1003: Syntax error, ',' expected
            //     default: 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 15));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.DefaultLiteralExpression);
                    {
                        N(SyntaxKind.DefaultKeyword);
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantDefaultInSwitchExpression2()
    {
        UsingExpression("""
            x switch
            {
                0 => 1,
                default(int): 2,
            }
            """,
            // (4,17): error CS1003: Syntax error, '=>' expected
            //     default(int): 2,
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 17));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.DefaultExpression);
                    {
                        N(SyntaxKind.DefaultKeyword);
                        N(SyntaxKind.OpenParenToken);
                        N(SyntaxKind.PredefinedType);
                        {
                            N(SyntaxKind.IntKeyword);
                        }
                        N(SyntaxKind.CloseParenToken);
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestErrantDefaultInSwitchExpression2_Semicolons()
    {
        UsingExpression("""
            x switch
            {
                0 => 1;
                default(int): 2;
            }
            """,
            // (3,11): error CS1003: Syntax error, ',' expected
            //     0 => 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 11),
            // (4,17): error CS1003: Syntax error, '=>' expected
            //     default(int): 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ":").WithArguments("=>").WithLocation(4, 17),
            // (4,20): error CS1003: Syntax error, ',' expected
            //     default(int): 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 20));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.DefaultExpression);
                    {
                        N(SyntaxKind.DefaultKeyword);
                        N(SyntaxKind.OpenParenToken);
                        N(SyntaxKind.PredefinedType);
                        {
                            N(SyntaxKind.IntKeyword);
                        }
                        N(SyntaxKind.CloseParenToken);
                    }
                }
                M(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestNormalDefaultInSwitchExpression1()
    {
        // 'default' legal syntactically.  Only a binding error.
        var code = """
            var v = 0 switch
            {
                0 => 1,
                default => 2,
            };
            """;
        CreateCompilation(code).VerifyDiagnostics(
            // (4,5): error CS8505: A default literal 'default' is not valid as a pattern. Use another literal (e.g. '0' or 'null') as appropriate. To match everything, use a discard pattern '_'.
            //     default => 2,
            Diagnostic(ErrorCode.ERR_DefaultPattern, "default").WithLocation(4, 5));

        UsingStatement(code);
        N(SyntaxKind.LocalDeclarationStatement);
        {
            N(SyntaxKind.VariableDeclaration);
            {
                N(SyntaxKind.IdentifierName);
                {
                    N(SyntaxKind.IdentifierToken, "var");
                }
                N(SyntaxKind.VariableDeclarator);
                {
                    N(SyntaxKind.IdentifierToken, "v");
                    N(SyntaxKind.EqualsValueClause);
                    {
                        N(SyntaxKind.EqualsToken);
                        N(SyntaxKind.SwitchExpression);
                        {
                            N(SyntaxKind.NumericLiteralExpression);
                            {
                                N(SyntaxKind.NumericLiteralToken, "0");
                            }
                            N(SyntaxKind.SwitchKeyword);
                            N(SyntaxKind.OpenBraceToken);
                            N(SyntaxKind.SwitchExpressionArm);
                            {
                                N(SyntaxKind.ConstantPattern);
                                {
                                    N(SyntaxKind.NumericLiteralExpression);
                                    {
                                        N(SyntaxKind.NumericLiteralToken, "0");
                                    }
                                }
                                N(SyntaxKind.EqualsGreaterThanToken);
                                N(SyntaxKind.NumericLiteralExpression);
                                {
                                    N(SyntaxKind.NumericLiteralToken, "1");
                                }
                            }
                            N(SyntaxKind.CommaToken);
                            N(SyntaxKind.SwitchExpressionArm);
                            {
                                N(SyntaxKind.ConstantPattern);
                                {
                                    N(SyntaxKind.DefaultLiteralExpression);
                                    {
                                        N(SyntaxKind.DefaultKeyword);
                                    }
                                }
                                N(SyntaxKind.EqualsGreaterThanToken);
                                N(SyntaxKind.NumericLiteralExpression);
                                {
                                    N(SyntaxKind.NumericLiteralToken, "2");
                                }
                            }
                            N(SyntaxKind.CommaToken);
                            N(SyntaxKind.CloseBraceToken);
                        }
                    }
                }
            }
            N(SyntaxKind.SemicolonToken);
        }
        EOF();
    }

    [Fact]
    public void TestNormalDefaultInSwitchExpression1_Semicolons()
    {
        // ;default' legal syntactically.  Only a binding error.
        var code = """
            var v = 0 switch
            {
                0 => 1;
                default => 2;
            };
            """;
        CreateCompilation(code).VerifyDiagnostics(
            // (3,11): error CS1003: Syntax error, ',' expected
            //     0 => 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 11),
            // (4,5): error CS8505: A default literal 'default' is not valid as a pattern. Use another literal (e.g. '0' or 'null') as appropriate. To match everything, use a discard pattern '_'.
            //     default => 2;
            Diagnostic(ErrorCode.ERR_DefaultPattern, "default").WithLocation(4, 5),
            // (4,17): error CS1003: Syntax error, ',' expected
            //     default => 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 17));

        UsingStatement(code,
            // (3,11): error CS1003: Syntax error, ',' expected
            //     0 => 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 11),
            // (4,17): error CS1003: Syntax error, ',' expected
            //     default => 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 17));
        N(SyntaxKind.LocalDeclarationStatement);
        {
            N(SyntaxKind.VariableDeclaration);
            {
                N(SyntaxKind.IdentifierName);
                {
                    N(SyntaxKind.IdentifierToken, "var");
                }
                N(SyntaxKind.VariableDeclarator);
                {
                    N(SyntaxKind.IdentifierToken, "v");
                    N(SyntaxKind.EqualsValueClause);
                    {
                        N(SyntaxKind.EqualsToken);
                        N(SyntaxKind.SwitchExpression);
                        {
                            N(SyntaxKind.NumericLiteralExpression);
                            {
                                N(SyntaxKind.NumericLiteralToken, "0");
                            }
                            N(SyntaxKind.SwitchKeyword);
                            N(SyntaxKind.OpenBraceToken);
                            N(SyntaxKind.SwitchExpressionArm);
                            {
                                N(SyntaxKind.ConstantPattern);
                                {
                                    N(SyntaxKind.NumericLiteralExpression);
                                    {
                                        N(SyntaxKind.NumericLiteralToken, "0");
                                    }
                                }
                                N(SyntaxKind.EqualsGreaterThanToken);
                                N(SyntaxKind.NumericLiteralExpression);
                                {
                                    N(SyntaxKind.NumericLiteralToken, "1");
                                }
                            }
                            M(SyntaxKind.CommaToken);
                            N(SyntaxKind.SwitchExpressionArm);
                            {
                                N(SyntaxKind.ConstantPattern);
                                {
                                    N(SyntaxKind.DefaultLiteralExpression);
                                    {
                                        N(SyntaxKind.DefaultKeyword);
                                    }
                                }
                                N(SyntaxKind.EqualsGreaterThanToken);
                                N(SyntaxKind.NumericLiteralExpression);
                                {
                                    N(SyntaxKind.NumericLiteralToken, "2");
                                }
                            }
                            M(SyntaxKind.CommaToken);
                            N(SyntaxKind.CloseBraceToken);
                        }
                    }
                }
            }
            N(SyntaxKind.SemicolonToken);
        }
        EOF();
    }

    [Fact]
    public void TestNormalDefaultInSwitchExpression2()
    {
        UsingExpression("""
            x switch
            {
                0 => 1,
                default(int) => 2,
            }
            """);
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.DefaultExpression);
                    {
                        N(SyntaxKind.DefaultKeyword);
                        N(SyntaxKind.OpenParenToken);
                        N(SyntaxKind.PredefinedType);
                        {
                            N(SyntaxKind.IntKeyword);
                        }
                        N(SyntaxKind.CloseParenToken);
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            N(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }

    [Fact]
    public void TestNormalDefaultInSwitchExpression2_Semicolons()
    {
        var v = typeof((int, int));
        var v1 = typeof(int[]);

        UsingExpression("""
            x switch
            {
                0 => 1;
                default(int) => 2;
            }
            """,
            // (3,11): error CS1003: Syntax error, ',' expected
            //     0 => 1;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(3, 11),
            // (4,22): error CS1003: Syntax error, ',' expected
            //     default(int) => 2;
            Diagnostic(ErrorCode.ERR_SyntaxError, ";").WithArguments(",").WithLocation(4, 22));
        N(SyntaxKind.SwitchExpression);
        {
            N(SyntaxKind.IdentifierName);
            {
                N(SyntaxKind.IdentifierToken, "x");
            }
            N(SyntaxKind.SwitchKeyword);
            N(SyntaxKind.OpenBraceToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.NumericLiteralExpression);
                    {
                        N(SyntaxKind.NumericLiteralToken, "0");
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "1");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.SwitchExpressionArm);
            {
                N(SyntaxKind.ConstantPattern);
                {
                    N(SyntaxKind.DefaultExpression);
                    {
                        N(SyntaxKind.DefaultKeyword);
                        N(SyntaxKind.OpenParenToken);
                        N(SyntaxKind.PredefinedType);
                        {
                            N(SyntaxKind.IntKeyword);
                        }
                        N(SyntaxKind.CloseParenToken);
                    }
                }
                N(SyntaxKind.EqualsGreaterThanToken);
                N(SyntaxKind.NumericLiteralExpression);
                {
                    N(SyntaxKind.NumericLiteralToken, "2");
                }
            }
            M(SyntaxKind.CommaToken);
            N(SyntaxKind.CloseBraceToken);
        }
        EOF();
    }
}
