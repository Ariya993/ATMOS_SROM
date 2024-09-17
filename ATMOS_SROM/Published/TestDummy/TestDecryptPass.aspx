<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestDecryptPass.aspx.cs" Inherits="ATMOS_SROM.TestDummy.TestDecryptPass" %>

<!DOCTYPE html>
<!-- Display the countdown timer in an element -->
<p id="demo"></p>

<%--<script>
    // Set the date we're counting down to
    //var countDownDate = new Date("April 3, 2018 17:53:00").getTime();
    var momentOfTime = new Date(); // just for example, can be any other time
    var myTimeSpan = 5 * 60 * 1000; // 5 minutes in milliseconds
   

    var countDownDate = momentOfTime.setTime(momentOfTime.getTime() + myTimeSpan);
    // Update the count down every 1 second
    var x = setInterval(function () {

        // Get todays date and time
        var now = new Date().getTime();

        // Find the distance between now an the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        // Display the result in the element with id="demo"
        document.getElementById("demo").innerHTML = days + "d " + hours + "h "
        + minutes + "m " + seconds + "s ";

        // If the count down is finished, write some text
        if (distance < 0) {
            clearInterval(x);
            document.getElementById("demo").innerHTML = "EXPIRED";
        }
    }, 1000);
</script>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmngr1" runat="server"></asp:ScriptManager>
        <div>
            <table>
                <tr>
                    <td>UserName : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
                        &nbsp
                        <asp:Button ID="btndcrypt" runat="server" OnClick="btndcrypt_Click" />
                    </td>
                </tr>
                <tr>
                    <td>Pass : 
                    </td>
                    <td>
                        <asp:Label ID="lblpass" runat="server"></asp:Label>

                    </td>
                </tr>
            </table>

        </div>
       <%-- <div>
            Test Timer
        
              
                        <asp:Timer ID="tmr1" runat="server" OnTick="tmr1_Tick"></asp:Timer>
            &nbsp
                        TIME :
                        &nbsp
                      
                  <asp:UpdatePanel ID="updPnl" runat="server" UpdateMode="Conditional">
                      <ContentTemplate>
                          <asp:Label ID="lbltimer" runat="server"></asp:Label>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="tmr1" EventName="tick" />
                      </Triggers>
                  </asp:UpdatePanel>
        </div>--%>
        <div>
            Hitung Poin Member
            <table>
                <tr>
                    <td>Price : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtprice" runat="server"></asp:TextBox>
                        &nbsp
                        <asp:Button ID="btnhitpoint" runat="server" OnClick="btnhitpoint_Click" />
                    </td>
                </tr>
                <tr>
                    <td>point : 
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server"></asp:Label>

                    </td>
                </tr>
            </table>

        </div>

         <div>
            Send Email Member
            <table>
                <tr>
                    <td>Price : 
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        &nbsp
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send Mail RP / RD" />
                    </td>
                </tr>
             
            </table>

        </div>
    </form>
</body>
</html>
