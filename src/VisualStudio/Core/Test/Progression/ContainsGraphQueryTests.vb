' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.VisualStudio.GraphModel
Imports Microsoft.VisualStudio.LanguageServices.Implementation.Progression
Imports Roslyn.Test.Utilities

Namespace Microsoft.VisualStudio.LanguageServices.UnitTests.Progression
    Public Class ContainsGraphQueryTests
        <Fact, Trait(Traits.Feature, Traits.Features.Progression)>
        Sub TypesContainedInCSharpDocument()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.cs">
                                class C { }
                                enum E { }
                                interface I { }
                                struct S { }
                         </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithDocumentNode(filePath:="Z:\Project.cs")
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 @2)" Category="CodeSchema_ProjectItem" Label="Project.cs"/>
                            <Node Id="(@3 Type=C)" Category="CodeSchema_Class" CodeSchemaProperty_IsInternal="True" CommonLabel="C" Icon="Microsoft.VisualStudio.Class.Internal" Label="C"/>
                            <Node Id="(@3 Type=E)" Category="CodeSchema_Enum" CodeSchemaProperty_IsFinal="True" CodeSchemaProperty_IsInternal="True" CommonLabel="E" Icon="Microsoft.VisualStudio.Enum.Internal" Label="E"/>
                            <Node Id="(@3 Type=I)" Category="CodeSchema_Interface" CodeSchemaProperty_IsAbstract="True" CodeSchemaProperty_IsInternal="True" CommonLabel="I" Icon="Microsoft.VisualStudio.Interface.Internal" Label="I"/>
                            <Node Id="(@3 Type=S)" Category="CodeSchema_Struct" CodeSchemaProperty_IsFinal="True" CodeSchemaProperty_IsInternal="True" CommonLabel="S" Icon="Microsoft.VisualStudio.Struct.Internal" Label="S"/>
                        </Nodes>
                        <Links>
                            <Link Source="(@1 @2)" Target="(@3 Type=C)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=E)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=I)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=S)" Category="Contains"/>
                        </Links>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/Project.csproj"/>
                            <Alias n="2" Uri="File=file:///Z:/Project.cs"/>
                            <Alias n="3" Uri="Assembly=file:///Z:/CSharpAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression)>
        Sub TypesContainedInCSharpDocumentInsideNamespace()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.cs">
                            namespace N.M
                            {
                                class C { }
                                enum E { }
                                interface I { }
                                struct S { }
                            }
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithDocumentNode(filePath:="Z:\Project.cs")
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 @2)" Category="CodeSchema_ProjectItem" Label="Project.cs"/>
                            <Node Id="(@3 Namespace=N.M Type=C)" Category="CodeSchema_Class" CodeSchemaProperty_IsInternal="True" CommonLabel="C" Icon="Microsoft.VisualStudio.Class.Internal" Label="C"/>
                            <Node Id="(@3 Namespace=N.M Type=E)" Category="CodeSchema_Enum" CodeSchemaProperty_IsFinal="True" CodeSchemaProperty_IsInternal="True" CommonLabel="E" Icon="Microsoft.VisualStudio.Enum.Internal" Label="E"/>
                            <Node Id="(@3 Namespace=N.M Type=I)" Category="CodeSchema_Interface" CodeSchemaProperty_IsAbstract="True" CodeSchemaProperty_IsInternal="True" CommonLabel="I" Icon="Microsoft.VisualStudio.Interface.Internal" Label="I"/>
                            <Node Id="(@3 Namespace=N.M Type=S)" Category="CodeSchema_Struct" CodeSchemaProperty_IsFinal="True" CodeSchemaProperty_IsInternal="True" CommonLabel="S" Icon="Microsoft.VisualStudio.Struct.Internal" Label="S"/>
                        </Nodes>
                        <Links>
                            <Link Source="(@1 @2)" Target="(@3 Namespace=N.M Type=C)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Namespace=N.M Type=E)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Namespace=N.M Type=I)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Namespace=N.M Type=S)" Category="Contains"/>
                        </Links>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/Project.csproj"/>
                            <Alias n="2" Uri="File=file:///Z:/Project.cs"/>
                            <Alias n="3" Uri="Assembly=file:///Z:/CSharpAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression)>
        Sub TypesContainedInVisualBasicDocument()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="Visual Basic" CommonReferences="true" FilePath="Z:\Project.vbproj">
                            <Document FilePath="Z:\Project.vb">
                                Class C
                                End Class

                                Enum E
                                End Enum

                                Interface I
                                End Interface

                                Module M
                                End Module
    
                                Structure S
                                End Structure
                         </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithDocumentNode(filePath:="Z:\Project.vb")
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 @2)" Category="CodeSchema_ProjectItem" Label="Project.vb"/>
                            <Node Id="(@3 Type=C)" Category="CodeSchema_Class" CodeSchemaProperty_IsInternal="True" CommonLabel="C" Icon="Microsoft.VisualStudio.Class.Internal" Label="C"/>
                            <Node Id="(@3 Type=E)" Category="CodeSchema_Enum" CodeSchemaProperty_IsFinal="True" CodeSchemaProperty_IsInternal="True" CommonLabel="E" Icon="Microsoft.VisualStudio.Enum.Internal" Label="E"/>
                            <Node Id="(@3 Type=I)" Category="CodeSchema_Interface" CodeSchemaProperty_IsAbstract="True" CodeSchemaProperty_IsInternal="True" CommonLabel="I" Icon="Microsoft.VisualStudio.Interface.Internal" Label="I"/>
                            <Node Id="(@3 Type=M)" Category="CodeSchema_Module" CodeSchemaProperty_IsInternal="True" CommonLabel="M" Icon="Microsoft.VisualStudio.Module.Internal" Label="M"/>
                            <Node Id="(@3 Type=S)" Category="CodeSchema_Struct" CodeSchemaProperty_IsFinal="True" CodeSchemaProperty_IsInternal="True" CommonLabel="S" Icon="Microsoft.VisualStudio.Struct.Internal" Label="S"/>
                        </Nodes>
                        <Links>
                            <Link Source="(@1 @2)" Target="(@3 Type=C)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=E)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=I)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=M)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=S)" Category="Contains"/>
                        </Links>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/Project.vbproj"/>
                            <Alias n="2" Uri="File=file:///Z:/Project.vb"/>
                            <Alias n="3" Uri="Assembly=file:///Z:/VisualBasicAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression)>
        Sub MembersContainedInCSharpScriptDocument()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.csx" Kind="Script">
                                <ParseOptions Kind="Script"/>
                                int F;
                                int P { get; set; }
                                void M()
                                {
                                }
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithDocumentNode(filePath:="Z:\Project.csx")
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 @2)" Category="CodeSchema_ProjectItem" Label="Project.csx"/>
                            <Node Id="(@3 Type=Script Member=F)" Category="CodeSchema_Field" CodeSchemaProperty_IsPrivate="True" CommonLabel="F" Icon="Microsoft.VisualStudio.Field.Private" Label="F"/>
                            <Node Id="(@3 Type=Script Member=M)" Category="CodeSchema_Method" CodeSchemaProperty_IsPrivate="True" CommonLabel="M" Icon="Microsoft.VisualStudio.Method.Private" Label="M"/>
                            <Node Id="(@3 Type=Script Member=P)" Category="CodeSchema_Property" CodeSchemaProperty_IsPrivate="True" CommonLabel="P" Icon="Microsoft.VisualStudio.Property.Private" Label="P"/>
                        </Nodes>
                        <Links>
                            <Link Source="(@1 @2)" Target="(@3 Type=Script Member=F)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=Script Member=M)" Category="Contains"/>
                            <Link Source="(@1 @2)" Target="(@3 Type=Script Member=P)" Category="Contains"/>
                        </Links>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/Project.csproj"/>
                            <Alias n="2" Uri="File=file:///Z:/Project.csx"/>
                            <Alias n="3" Uri="Assembly=file:///Z:/CSharpAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression)>
        Sub MembersContainedInClass()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.cs">
                                class $$C { void M(); event System.EventHandler E; }
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithMarkedSymbolNode()
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 Type=C Member=E)" Category="CodeSchema_Event" CodeSchemaProperty_IsPrivate="True" CommonLabel="E" Icon="Microsoft.VisualStudio.Event.Private" Label="E"/>
                            <Node Id="(@1 Type=C Member=M)" Category="CodeSchema_Method" CodeSchemaProperty_IsPrivate="True" CommonLabel="M" Icon="Microsoft.VisualStudio.Method.Private" Label="M"/>
                            <Node Id="(@1 Type=C)" Category="CodeSchema_Class" CodeSchemaProperty_IsInternal="True" CommonLabel="C" Icon="Microsoft.VisualStudio.Class.Internal" Label="C"/>
                        </Nodes>
                        <Links>
                            <Link Source="(@1 Type=C)" Target="(@1 Type=C Member=E)" Category="Contains"/>
                            <Link Source="(@1 Type=C)" Target="(@1 Type=C Member=M)" Category="Contains"/>
                        </Links>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/CSharpAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression)>
        <WorkItem(543892)>
        Sub NestedTypesContainedInClass()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.cs">
                                class C { class $$D { class E { } } }
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithMarkedSymbolNode()
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 Type=(Name=D ParentType=C))" Category="CodeSchema_Class" CodeSchemaProperty_IsPrivate="True" CommonLabel="D" Icon="Microsoft.VisualStudio.Class.Private" Label="D"/>
                            <Node Id="(@1 Type=(Name=E ParentType=(Name=D ParentType=C)))" Category="CodeSchema_Class" CodeSchemaProperty_IsPrivate="True" CommonLabel="E" Icon="Microsoft.VisualStudio.Class.Private" Label="E"/>
                        </Nodes>
                        <Links>
                            <Link Source="(@1 Type=(Name=D ParentType=C))" Target="(@1 Type=(Name=E ParentType=(Name=D ParentType=C)))" Category="Contains"/>
                        </Links>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/CSharpAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression), WorkItem(545018)>
        Sub EnumMembersInEnum()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.cs">
                                enum $$E { M }
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithMarkedSymbolNode()
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 Type=E Member=M)" Category="CodeSchema_Field" CodeSchemaProperty_IsPublic="True" CodeSchemaProperty_IsStatic="True" CommonLabel="M" Icon="Microsoft.VisualStudio.EnumMember.Public" Label="M"/>
                            <Node Id="(@1 Type=E)" Category="CodeSchema_Enum" CodeSchemaProperty_IsFinal="True" CodeSchemaProperty_IsInternal="True" CommonLabel="E" Icon="Microsoft.VisualStudio.Enum.Internal" Label="E"/>
                        </Nodes>
                        <Links>
                            <Link Source="(@1 Type=E)" Target="(@1 Type=E Member=M)" Category="Contains"/>
                        </Links>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/CSharpAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression), WorkItem(610147)>
        Sub NothingInBrokenCode()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.cs">
                                [(delegate static
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithDocumentNode(filePath:="Z:\Project.cs")
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 @2)" Category="CodeSchema_ProjectItem" Label="Project.cs"/>
                        </Nodes>
                        <Links/>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/Project.csproj"/>
                            <Alias n="2" Uri="File=file:///Z:/Project.cs"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression), WorkItem(610147)>
        Sub NothingInBrokenCode2()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="C#" CommonReferences="true" FilePath="Z:\Project.csproj">
                            <Document FilePath="Z:\Project.cs">
                                int this[] { get { int x; } }
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithDocumentNode(filePath:="Z:\Project.cs")
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 @2)" Category="CodeSchema_ProjectItem" Label="Project.cs"/>
                        </Nodes>
                        <Links/>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/Project.csproj"/>
                            <Alias n="2" Uri="File=file:///Z:/Project.cs"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Progression), WorkItem(608653)>
        Sub NothingInBrokenCode3()
            Using testState = New ProgressionTestState(
                    <Workspace>
                        <Project Language="Visual Basic" CommonReferences="true" FilePath="Z:\Project.vbproj">
                            <Document FilePath="Z:\Project.vb">
                                Module $$Module1
                                    Public Class
                                End Module
                            </Document>
                        </Project>
                    </Workspace>)

                Dim inputGraph = testState.GetGraphWithMarkedSymbolNode()
                Dim outputContext = testState.GetGraphContextAfterQuery(inputGraph, New ContainsGraphQuery(), GraphContextDirection.Contains)

                AssertSimplifiedGraphIs(
                    outputContext.Graph,
                    <DirectedGraph xmlns="http://schemas.microsoft.com/vs/2009/dgml">
                        <Nodes>
                            <Node Id="(@1 Type=Module1)" Category="CodeSchema_Module" CodeSchemaProperty_IsInternal="True" CommonLabel="Module1" Icon="Microsoft.VisualStudio.Module.Internal" Label="Module1"/>
                        </Nodes>
                        <Links/>
                        <IdentifierAliases>
                            <Alias n="1" Uri="Assembly=file:///Z:/VisualBasicAssembly1.dll"/>
                        </IdentifierAliases>
                    </DirectedGraph>)
            End Using
        End Sub
    End Class
End Namespace
