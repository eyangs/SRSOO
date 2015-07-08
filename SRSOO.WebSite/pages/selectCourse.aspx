<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectCourse.aspx.cs" Inherits="pages_selectCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Sciript/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />
    <script src="../sciript/jquery/jquery-1.7.1.min.js"></script>
    <script src="../Sciript/ligerUI/js/ligerui.min.js"></script>

    <script src="../sciript/jquery-validation/jquery.validate.min.js"></script>
    <script src="../sciript/jquery-validation/jquery.metadata.js"></script>
    <script src="../sciript/jquery-validation/messages_cn.js"></script>
    <style type="text/css">
        .middle input {
            display: block;
            width: 30px;
            margin: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" class="liger-form" data-validate="{}">
        <div class="fields" >
            <input id="Student_Name" data-type="text" type="text" data-label="StudentName" data-name="StudentName" validate="{required:true,minlength:5}" />
            <input data-type="text" data-label="ID Number" data-name="ID" validate="{required:true}" />
            <input data-type="text" data-label="Total Course" data-name="TotalCourse" validate="{required:false}" />
        </div>
        <div>
            <div style="margin: 4px; float: left;">
                <p>Schedule Of Classes</p>
                <div id="listbox1"></div>
            </div>
            <div style="margin: 4px; float: left;" class="middle">
                <p>&nbsp;&nbsp;</p>
                <input type="button" onclick="moveToLeft()" value="<" />
                <input type="button" onclick="moveToRight()" value=">" />
                <input type="button" onclick="moveAllToLeft()" value="<<" />
                <input type="button" onclick="moveAllToRight()" value=">>" />
            </div>
            <div style="margin: 4px; float: left;">
                <p>RegisteredFor</p>
                <div id="listbox2"></div>
            </div>
        </div>
    </form>
    <div class="liger-button" data-click="f_validate" data-width="150">Save</div>
</body>
<script type="text/javascript">
    $(function () {
        $("#listbox1,#listbox2").ligerListBox({
            isShowCheckBox: true,
            isMultiSelect: true,
            width: 450,
            height: 140
        });

        loadSchedule();
        loadStudentInfo();
    });
    //从服务器加载选课列表
    function loadSchedule() {
        $.post(
            "selectCourse.aspx?Action=LoadSchedule",
            function (reslut) {
                var json = $.parseJSON(reslut);
                liger.get("listbox1").setData(json);

            }
        );
    }
    //从服务器加载当前登陆学生已选课程
    function loadResigistion() {
        $.post(
            "selectCourse.aspx?Action=loadResigistion",
            function (reslut) {
                var json = $.parseJSON(reslut);
                liger.get("listbox2").setData(json.Attends);

            }
        );

    }
    function loadStudentInfo() {
        
        $.post(
            "SelectCourse.aspx?Action=loadStudentInfo",
           function (reslut) {
               
               var json = $.parseJSON(reslut);
               $.ligerui.g("ID").setValue(json.Id);
               $.ligerui.g("StudentName").setValue(json.Name);
               
               
           }


            );



    }
    function moveToLeft() {
        var box1 = liger.get("listbox1"), box2 = liger.get("listbox2");
        var selecteds = box2.getSelectedItems();
        if (!selecteds || !selecteds.length) return;
        box2.removeItems(selecteds);
        box1.addItems(selecteds);
    }
    function moveToRight() {
        var box1 = liger.get("listbox1"), box2 = liger.get("listbox2");
        var selecteds = box1.getSelectedItems();
        if (!selecteds || !selecteds.length) return;
        box1.removeItems(selecteds);
        box2.addItems(selecteds);
    }
    function moveAllToLeft() {
        var box1 = liger.get("listbox1"), box2 = liger.get("listbox2");
        var selecteds = box2.data;
        if (!selecteds || !selecteds.length) return;
        box1.addItems(selecteds);
        box2.removeItems(selecteds);
    }
    function moveAllToRight() {
        var box1 = liger.get("listbox1"), box2 = liger.get("listbox2");
        var selecteds = box1.data;
        if (!selecteds || !selecteds.length) return;
        box2.addItems(selecteds);
        box1.removeItems(selecteds);
    }



</script>
</html>
