<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="fields">
            <input data-type="text" data-label="StudentName" data-name="StudentName" validate="{required:true,minlength:5}" />
            <input data-type="text" data-label="ID Number" data-name="ID" validate="{required:true}" />
            <input data-type="text" data-label="Total Course" data-name="TotalCourse" validate="{required:false}" />
        </div>
    </div>
    </form>
</body>
</html>
