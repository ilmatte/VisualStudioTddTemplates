<a title="rhinotop" name="rhinotop"></a>
<a href="#introductionrhino">1. Introduction</a>
<a href="#requirementsrhino">2. System Requirements</a>
<a href="#downloadrhino">3. Download and Install</a>
<a href="#contentsrhino">4. Package Contents</a>
<span style="position: relative; left: 10px;"><a href="#contents1rhino">4.1. Test Class Library Project Template</a></span>
<span style="position: relative; left: 10px;"><a href="#contents2rhino">4.2. Test Class ItemTemplate</a></span>
<span style="position: relative; left: 10px;"><a href="#contents3rhino">4.3. Code Snippets</a></span>
<a href="#uninstallrhino">5. Uninstall</a>
<a href="#booksrhino">6. Suggested Books</a>
<h3><a title="introductionrhino" name="introductionrhino"></a><span style="font-size: small; color: #993300;">1. Introduction</span></h3>
If you've been using 'NUnit' you found yourself involved in doing repetitive actions like creating test projects and adding test classes and test methods.
If you ever had a try with the 'Rhino Mocks' mocking framework you had an extra bit of repetitive code to write, in all of your TestFixture classes.

I was tired of always creating a new test project for each project in my solution and manually add references.
I was tired of always prepare test classes with Setup and Teardown methods as well as other standard elements.

Then I created a standard <strong>Project Template</strong> creating a test class library, an <strong>Item Template</strong> creating a Rhino Mocks enabled TestFixture class, and a couple of useful <strong>code snippets</strong>.

All this stuff is targeted to Visual C# projects.
<h3><a title="requirementsrhino" name="requirementsrhino"></a><span style="font-size: small; color: #993300;">2. System Requirements</span></h3>
You'll need the following software:

Visual Studio 2015
<a href="http://nunit.org/" target="_blank" rel="noopener">Nunit</a> <a href="https://github.com/nunit/nunit/releases/tag/3.9" target="_blank" rel="noopener">(Nunit.3.9.0)</a> -- you can find it via Nuget
<a href="https://www.hibernatingrhinos.com/oss/rhino-mocks" target="_blank" rel="noopener">Rhino Mocks</a> <a href="https://www.hibernatingrhinos.com/downloads/rhino-mocks/latest" target="_blank" rel="noopener">(RhinoMocks.3.6.1)</a> -- you can find it via Nuget

<strong>Important</strong>: Project references are based on the above specified Nunit and Rhino.Mocks versions installed via Nuget.

<strong>Note:</strong> If your installations are different from the ones specified, you will need to modify project references once created your project with the 'project template' provided. Otherwise you can download my GitHub project, modify the template project and generate a new .vsix file.
<h3><a title="downloadrhino" name="downloadrhino"></a><span style="font-size: small; color: #993300;">3. Download and Install</span></h3>
First of all download the file: <em>TDDTemplates.vsix</em> that you can find <a title="TDDTemplatesDownload" href="https://github.com/ilmatte/VisualStudioTddTemplates/releases/download/1.0/TDDTemplates.vsix" target="_blank" rel="noopener">here</a>.
All you have to do is download, double click, and proceed with the installation.
If you want more detailed description of what you are downloading, read further.

The file <em>TDDTemplates.vsix</em> is a Visual Studio installer file, containing:
<ol>
 	<li>A Project Template named <span style="color: #003300;"><strong>Test Class Library</strong></span>;</li>
 	<li>An ItemTemplate named <strong>Test Class;</strong></li>
 	<li>Two <strong>Code snippets</strong> useful for test driven develpment (<strong>TDD Snippet</strong> component).</li>
</ol>
Double-clicking on the installer file you will get a dialog window prompting you to choose Visual Studio versions for wich to proceed.
Click on the <em>install</em> button and you're done.

<a href="#rhinotop"><span style="font-size: 80%; color: red;">→ go to top</span></a>
<h3><a title="contentsrhino" name="contentsrhino"></a><span style="font-size: small; color: #993300;">4. Package Contents</span></h3>
<a href="#contents1rhino">4.1. Test Class Library Project Template</a>
<a href="#contents2rhino">4.2. Test Class ItemTemplate</a>
<a href="#contents3rhino">4.3. Code Snippets</a>

In this paragraph you will find a detailed description of the contents installed by the Visual Studio installer we're talking about: <em>TDDTemplates.vsix</em>.
<h4><a title="contents1rhino" name="contents1rhino"></a><span style="font-size: x-small; color: #993300;">4.1. Test Class Library Project Template</span></h4>
When creating a new project, the initial dialog will list a new project template under the <em>Visual C#</em> category: <strong>Test Class Library</strong>, as shown in the following image.

&nbsp;

<a href="http://theworkingdad.it/wp-content/uploads/2018/01/Vsix2-1.bmp"><img class="alignnone size-full wp-image-280" src="http://theworkingdad.it/wp-content/uploads/2018/01/Vsix2-1.bmp" alt="" width="559" height="270" /></a>

Choose a name for your project, say: <em>ProjectName</em> and click <em>ok.</em> A new class library project will be created with the structure shown in the following screenshot of the Solution Explorer window:

<a href="http://theworkingdad.it/wp-content/uploads/2018/01/Vsix3.bmp"><img class="alignnone size-full wp-image-282" src="http://theworkingdad.it/wp-content/uploads/2018/01/Vsix3.bmp" alt="" width="274" height="310" /></a>

The file named <strong>TestClass.cs</strong>, created inside the project, contains the code defining the skeleton of a TestFixture class definition:
<pre class="EnlighterJSRAW" data-enlighter-language="csharp">using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Rhino.Mocks;

namespace ProjectName
{
    [TestFixture]
    public class TestClass
    {
        MockRepository mocks;

        /// &lt;summary&gt;
        /// Prepares mock repository
        /// &lt;/summary&gt;
        [SetUp]
        public void Initialize()
        {
            mocks = new MockRepository();
        }

        /// &lt;summary&gt;
        /// template behavior and state testing method
        /// &lt;/summary&gt;
        [Test]
        public void TestMethod1()
        {
            IDependency dependency = mocks.CreateMock&lt;IDependency&gt;();

            // Record expectations
            using (mocks.Record())
            {
                Expect.Call(dependency.Method1("parameter")).Return("result");
                dependency.Method2();
            }

            // Replay and validate interaction
            Subject subjectUnderTest;
            using (mocks.Playback())
            {
                subjectUnderTest = new Subject(dependency);
                subjectUnderTest.DoWork();
            }

            // Post-interaction assertion
            Assert.That(subjectUnderTest.WorkDone, Is.True);
        }
    }
}
</pre>
As you can see the class definition already contains a default implementation of a Test method.
Such method includes the definition of RhinoMocks expectations for a hypothetical object under test.
I chose to use Rhino Mocks Record-playback Syntax which takes care of calling mocks.ReplayAll() and mocks.VerifyAll() under the hood.
I feel this syntax is very neat and clean providing a clear separation between the moment in which we record expectations and the moment in which we use the object under test.
<div><a href="#contentsrhino"><span style="font-size: 80%; color: red;">→ top of paragraph</span></a>
<a href="#rhinotop"><span style="font-size: 80%; color: red;">→ top of post</span></a></div>
<h4><a title="contents2rhino" name="contents2rhino"></a><span style="font-size: x-small; color: #993300;">4.2. Test Class Item Template</span></h4>
When adding a new item to the project, the <em>Add New Item</em> dialog will list a new project template: <strong>Test Class</strong>.
It is possible that you have to scroll down a little to see the <strong>My Templates</strong> category, as shown in the following image:

<a href="http://theworkingdad.it/wp-content/uploads/2018/01/Vsix4.bmp"><img class="alignnone size-full wp-image-284" src="http://theworkingdad.it/wp-content/uploads/2018/01/Vsix4.bmp" alt="" width="525" height="178" /></a>

Selecting the <strong>Test Class</strong> template, a new class definition file will be created, containing the same class definition you obtained when creating the <strong>Test Class Library</strong> Project (see <a href="#contents1rhino">previous paragraph</a>).

<a href="#contentsrhino"><span style="font-size: 80%; color: red;">→ top of paragraph</span></a>
<a href="#rhinotop"><span style="font-size: 80%; color: red;">→ top of post</span></a>
<h4><a title="contents3rhino" name="contents3rhino"></a><span style="font-size: x-small; color: #993300;">4.3. Code Snippets</span></h4>
From now on, when writing code, you will have two more intellisense entries in your IDE: <em>TDDtestmethod</em> and <em>TDDusing</em>.

<strong>TDDtestmethod</strong> will insert a test method definition as shown in the following sequence of images:

<a href="http://theworkingdad.it/wp-content/uploads/2018/02/Vsix5.bmp"><img class="alignnone size-full wp-image-290" src="http://theworkingdad.it/wp-content/uploads/2018/02/Vsix5.bmp" alt="" width="578" height="60" /></a>

Selecting the keyword as in the preceding image will produce the following code:
<pre class="EnlighterJSRAW" data-enlighter-language="csharp">[Test]
public void TestMethod()
{
}</pre>
The snippet allows the user to choose the method's Attribute (defaults to: Nunit.Framework.TestAttribute) and the methods name (defaults to: TestMethod).

<strong>TDDusing</strong> will insert the two <em>using</em> blocks needed for the Rhino Mocks Record-playback Syntax, as shown in the following sequence of images:

<a href="http://theworkingdad.it/wp-content/uploads/2018/02/Vsix6.bmp"><img class="alignnone size-full wp-image-291" src="http://theworkingdad.it/wp-content/uploads/2018/02/Vsix6.bmp" alt="" width="673" height="79" /></a>

Selecting the keyword as in the preceding image will produce the following code:
<pre class="EnlighterJSRAW" data-enlighter-language="csharp">using (mocks.Record())
{

}

using (mocks.Playback())
{
}</pre>
The snippet allows the user to choose the instance of Rhino.Mocks.MockRepository
to use for mocking objects.

<a href="#contentsrhino"><span style="font-size: 80%; color: red;">→ top of paragraph</span></a>
<a href="#rhinotop"><span style="font-size: 80%; color: red;">→ top of post</span></a>
<h3><a title="uninstallrhino" name="uninstallrhino"></a><span style="font-size: small; color: #993300;">5. Uninstall</span></h3>
If you want to uninstall the installed content you can simply go to the <em>Extensions and Updates</em> dialog box, identify the following entry and click the <strong>Uninstall </strong>button.

<img class="alignnone size-medium wp-image-296" src="http://theworkingdad.it/wp-content/uploads/2018/02/Cattura-300x56.jpg" alt="" width="300" height="56" />

That's all.

Feel free to post a comment if you want to report a bug or to propose enhancement of the templates and snippets.

<a href="#contentsrhino"><span style="font-size: 80%; color: red;">→ top of paragraph</span></a>
<a href="#rhinotop"><span style="font-size: 80%; color: red;">→ top of post</span></a>
