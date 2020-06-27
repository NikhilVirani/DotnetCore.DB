<h2>Import Stored Procedure Function in .Net Core</h2>
<p>People who work with MSSQL and Entity Framework (EF) those people familiar with the “Stored Procedure” keyword.</p>
<p>In .Net Framework – you can import stored procedure and functions using Entity Framework’s Data entity model and that generate file extension with “.edmx”.</p>
<p>But In .Net Core – There is Entity Framework (EF) Core, which is used for interacting with the database, but they do not support importing stored procedures just like Entity Framework (EF) done in edmx.</p>
<p>So, we are creating a function for each stored procedure just like Entity Framework (EF).</p>
<p>So How to achieve this one,</p>
<ul>
  <li>Find How many stored Procedure in your database</li>
  <li>For each store procedure
    <ul>
      <li>Find what and how many parameters of stored procedure</li>
      <li>Find what object return by the stored procedure (complex type)</li>
    </ul>
  </li>
</ul>
<p>Based on this data we are going to convert to functions.</p>
<h2>Stored Procedure's Function Creator</h2>
<p>This Project Contain 2 Class Library and 1 Console application</p>
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
- This console application is for generating function for every store procedure.

<h2>How to Use</h2>
<p>In the Console application open <b>Program.cs</b> file and change these parameters relevant to your project.</p>
 <ul>
    <li>
        <b>ConnectionString </b>- your ms SQL DB connection string
    </li>
    <li>
        <b>ContextBasePath </b>- Configure Directory path where a class file created where all functions are created.
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
  
  
