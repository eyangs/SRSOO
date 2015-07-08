<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录</title>
    <link href="Sciript/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" />
    <link href="Sciript/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet" />
    <style type="text/css">
        #uldiv tr { height: 40px; line-height: 40px; }
        .itb { font-size: 14px; height: 28px; line-height: 28px; padding-left: 3px; padding-right: 3px; vertical-align: middle; border: solid 1px #BBBBBB; background-color: white; width: 200px; }
        .ttb { font-size: 14px; padding: 3px; height: 54px; width: 200px; overflow: hidden; border: solid 1px #BBB; }
        .fid tr { height: 40px; line-height: 40px; }
        .fid { padding-left: 15px; padding-top: 10px; }
        .required { color: red; font-weight: bold; font-family: Arial; }
    </style>
    <script src="Sciript/jquery/jquery-1.7.1.min.js"></script>

    <script src="Sciript/ligerUI/js/core/base.js"></script>
    <script src="Sciript/ligerUI/js/plugins/ligerDialog.js"></script>
    <script src="Sciript/ligerUI/js/plugins/ligerDrag.js"></script>
    <script src="Sciript/ligerUI/js/plugins/ligerForm.js"></script>
    <script src="Sciript/jquery/jquery.form.js"></script>
</head>
<body>
 <div id="uldiv" style="display: none;">
        <form id="ulform" class="fid" method="post" action =( )>
            <table>
                <tr>
                    <td style="width: 80px;">用户名&nbsp;(<span class="required">*</span>):</td>
                    <td style="width: 220px;"><input type="text" id="username" name="username" class="itb" /></td>
                </tr>
                <tr>
                    <td style="width: 80px;">密　码&nbsp;(<span class="required">*</span>):</td>
                    <td style="width: 220px;"><input type="password" id="password" name="password" class="itb" /></td>
                </tr>
            </table>
        </form>
    </div>
</body>
<script type="text/javascript">

    $(document).ready(function () {
        login();
    });

    function login() {
        var d = $.ligerDialog.open(
            {
                target: $("#uldiv"),
                title: "登录",
                width: 350,
                height: 180,
                buttons: [
                    {
                        text: "登录",
                        onclick: function () {
                            if (!$("#username").val() || !$("#password").val()) {
                                alert("请输入“用户名”和“密码”登录系统！");
                                return;
                            }
                            $.post(
                                "Login.aspx?Action=Login",
                                { username: $("#username").val(), password: $("#password").val() },
                                function (reslut) {
                                    var data = $.parseJSON(reslut);
                                    if (data) {
                                        if (data.success) {
                                            document.location.href = "index.aspx";
                                        }
                                        else {
                                            alert(data.message);
                                        }
                                    }
                                }
                            );
                        }
                    }
                ]
            }
        );
    }
    </script>
</html>
