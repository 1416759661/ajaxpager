<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #tblist{ width:780px;border-collapse:collapse; margin:0 auto; }
        #tblist td{text-align:center; line-height:24px; width:130px; border:solid 1px #eee; margin:0; padding:0; font-size:12px;}
        #tblist th{text-align:center; line-height:24px; width:130px; border:solid 1px #eee; margin:0; padding:0; font-size:12px;}
        .pager{ font-size:12px;width:780px;margin:0 auto;  }
        .firstpage,.prepage,.nextpage,.lastpage{ cursor:pointer;border:solid 1px #eee; }
        .pagercontent{ line-height:30px; }
        .myHiddenDiv{background-color:White;  font-size:12px; margin-left:5px;}
    </style>
    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" position:relative; width:780px;margin:0 auto;  ">
  <table id="tblist" cellpadding="0" cellspacing="0">
  <thead><tr><th>编号ID</th><th>学号</th><th>姓名</th><th>年龄</th><th>电话</th><th>入学时间</th></tr></thead>
  <tbody id="itemlist"></tbody>
  </table>
   </div>
   <div class="pager">
        <div class="pagercontent"><span class="firstpage">首页</span> <span class="prepage">上一页</span> <span class="nextpage">下一页</span> <span class="lastpage">末页</span> 总共 <span class="allpage">0</span> 页 当前第&nbsp;<span class="currentpage">1</span>&nbsp;页</div>
   </div>    
    <script type="text/javascript">
        $(function() {
            GetData(1, 10);
            $(".firstpage").click(function() {
                GetDataFirst(1, 10);
            })
            $(".prepage").click(function() {
                GetData(parseInt($(".currentpage").text()) - 1, 10);
            })
            $(".nextpage").click(function() {
                GetData(parseInt($(".currentpage").text()) + 1, 10);
            })
            $(".lastpage").click(function() {
                GetData(parseInt($(".allpage").text()), 10);
            })
        })
        //获取数据
        function GetDataFirst(pageindex, PageSize) {
            $.post("ajax.aspx?" + Math.random(), { w: "GetPageContent", _pageindex: pageindex, _PageSize: PageSize }, function(data) {
                var arraydata = data.split("|");
                $("#loding").fadeOut("slow", function() {
                    $("#itemlist").html(arraydata[0]);
                    $(".allpage").html(arraydata[2])
                    $(".currentpage").html(arraydata[1])
                });  
            })
        }
        //获取数据
        function GetData(pageindex, PageSize) {
            if (pageindex > parseInt($(".allpage").text())) {
                pageindex = parseInt($(".allpage").text())
            }
            $.post("ajax.aspx?" + Math.random(), { w: "GetPageContent", _pageindex: pageindex, _PageSize: PageSize }, function(data) {
                var arraydata = data.split("|");
                $("#loding").fadeOut("slow", function() {
                    $("#itemlist").html(arraydata[0]);
                    $(".allpage").html(arraydata[2])
                    $(".currentpage").html(arraydata[1])
                });               
            })
        }
        $("#itemlist").ajaxStart(function() {
            var _html = "<tr id=\"loding\"><td colspan=\"6\" style=\" height:80px;\"><div class=\"myHiddenDiv\"><div >正在加载数据，请稍后...</div><img src=\"/images/load2.gif\" alt=\"\"/></div></td></tr>";
            $(this).html(_html);
        });        
    </script>
    </form>
</body>
</html>
