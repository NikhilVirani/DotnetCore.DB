<h2>Stored Procedure's Function Creator</h2>
<p>This Project Contain 2 Class Library and 1 Console applicatin</p>
  <ul>
    <li>
       <b>DotnetCore.DB</b>
    </li>
    <li>
        <b>DotnetCore.DB.Extension</b>
    </li>
    <li>
        <b>DotnetCore.DB.MethodGenerator</b>
    </li>
</ul>

<b>DotnetCore.DB</b>
- This class library contains a generic method for stored procedure and query to call and convert result set into models.

<b>DotnetCore.DB.Extension</b>
- This class library contains a method for getting information related to stored procedure and converts that data into the method

<b>DotnetCore.DB.MethodGenerator</b>
- This console application is for generating functinos for each and every store procedure.

<h2>How to Use</h2>
<p>In Console application open <b>Program.cs</b> file and changes these parameter relevant to your project.</p>
 <ul>
    <li>
        <b>ConnectionString </b>- your ms sql db connection string
    </li>
    <li>
        <b>ContextBasePath </b>- Configure Directory path where class file created where all function are creted.
    </li>
    <li>
        <b>ContextClassName </b>- Add related class name.
    </li>
    <li>
        <b>NamespaceOfContextFile </b>- Configure related to your project
    </li>
</ul>
<pre><code>string ConnectionString = @"--Your DB Connection String--";
string ContextBasePath = @"--Directory of where context file creation--";
string ContextClassName = "TestDBContext";
string NamespaceOfContextFile = "DotnetCore.DB.MethodGenerator.DB";
</code></pre>
  and Run console application
  
<h2>How to call stored Procedure</h2>
<p>you need to initialize class of you named <b>ContextClassName</b> with connection string, Like :</p>
<pre><code>TestDBContext testDBContext = new TestDBContext(ConnectionString);
</code></pre>
<p>and call stored procedure function, Like :</p>
<pre><code>testDBContext.mvc_GetParams(0, "");
</code></pre>
  
  
